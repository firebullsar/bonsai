﻿using System;

namespace Bonsai.Areas.Admin.ViewModels.Media
{
    /// <summary>
    /// Results of the media file's upload.
    /// </summary>
    public class MediaUploadResultVM
    {
        /// <summary>
        /// Unique ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Unique key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Full path to preview.
        /// </summary>
        public string ThumbnailPath { get; set; }
    }
}