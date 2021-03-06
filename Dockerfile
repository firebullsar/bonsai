FROM node:lts-alpine as node
WORKDIR /build/Bonsai

ADD src/Bonsai/package.json .
ADD src/Bonsai/package-lock.json .
RUN npm ci
ADD src/Bonsai .
RUN node_modules/.bin/gulp build

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as net-builder
WORKDIR /build
ADD src/Bonsai.sln .
ADD src/Bonsai/Bonsai.csproj Bonsai/
ADD src/Bonsai.Tests.Search/Bonsai.Tests.Search.csproj Bonsai.Tests.Search/

RUN dotnet restore
COPY --from=node /build .

RUN dotnet publish --output ../out/ --configuration Release --runtime linux-x64 Bonsai/Bonsai.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

RUN apt-get -yqq update && \
    apt-get -yqq install ffmpeg libc6-dev libgdiplus libx11-dev && \
    rm -rf /var/lib/apt/lists/*

RUN curl -sL https://deb.nodesource.com/setup_10.x | bash - && \
    apt-get install -y nodejs && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY --from=net-builder /out .

RUN mkdir /app/External/ffmpeg
RUN ln -s /usr/bin/ffmpeg /app/External/ffmpeg/ffmpeg
RUN ln -s /usr/bin/ffprobe /app/External/ffmpeg/ffprobe

ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Bonsai.dll"]
