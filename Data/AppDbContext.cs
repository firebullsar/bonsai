﻿using Bonsai.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bonsai.Data
{
    /// <summary>
    /// Main data context of the application.
    /// </summary>
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<AppConfig> Config => Set<AppConfig>();
        public virtual DbSet<AccessRule> AccessRules => Set<AccessRule>();
        public virtual DbSet<Changeset> Changes => Set<Changeset>();
        public virtual DbSet<Media> Media => Set<Media>();
        public virtual DbSet<MediaTag> MediaTags => Set<MediaTag>();
        public virtual DbSet<Page> Pages => Set<Page>();
        public virtual DbSet<Relation> Relations => Set<Relation>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                   .HasMany(x => x.Changes).WithOne(x => x.Author);

            builder.Entity<Changeset>()
                   .HasOne(x => x.Author).WithMany();

            builder.Entity<AccessRule>()
                   .HasOne(x => x.Page).WithMany();

            builder.Entity<AccessRule>()
                .HasOne(x => x.User).WithMany();

            builder.Entity<Relation>()
                   .HasOne(x => x.Subject).WithMany(x => x.Relations);

            builder.Entity<Relation>()
                   .HasOne(x => x.Object).WithMany();

            builder.Entity<MediaTag>()
                   .HasOne(x => x.Media).WithMany(x => x.Tags);

            builder.Entity<MediaTag>()
                   .HasOne(x => x.Object).WithMany(x => x.MediaTags);
        }
    }
}