﻿using System;
using AutoMapper;
using Bonsai.Areas.Front.Logic;
using Bonsai.Areas.Front.ViewModels.Media;
using Bonsai.Code.DomainModel.Media;
using Bonsai.Code.Infrastructure;
using Bonsai.Code.Utils.Date;
using Bonsai.Code.Utils.Helpers;

namespace Bonsai.Areas.Admin.ViewModels.Dashboard
{
    /// <summary>
    /// Additional information about a media file.
    /// </summary>
    public class MediaThumbnailExtendedVM: MediaThumbnailVM, IMapped
    {
        /// <summary>
        /// Unique ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Date of the media file's upload.
        /// </summary>
        public DateTimeOffset UploadDate { get; set; }

        /// <summary>
        /// Number of tagged entities.
        /// </summary>
        public int MediaTagsCount { get; set; }

        /// <summary>
        /// Readable title.
        /// </summary>
        public string Title { get; set; }

        public void Configure(IProfileExpression profile)
        {
            profile.CreateMap<Data.Models.Media, MediaThumbnailExtendedVM>()
                   .MapMember(x => x.Title, x => x.Title)
                   .MapMember(x => x.MediaTagsCount, x => x.Tags.Count)
                   .MapMember(x => x.Date, x => FuzzyDate.TryParse(x.Date))
                   .MapMember(y => y.ThumbnailUrl, x => MediaPresenterService.GetSizedMediaPath(x.FilePath, MediaSize.Small));
        }
    }
}
