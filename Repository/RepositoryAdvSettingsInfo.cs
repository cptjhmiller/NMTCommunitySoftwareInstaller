﻿using TransmissionApplicationUploader;

namespace com.nmtinstaller.csi.RemoteFileInfo
{
    public class RepositoryAdvSettingsInfo : RepositoryFileInfoBase
    {
        public string WebserviceUrl { get; set; }

        public RepositoryAdvSettingsInfo(string Name, string Author, string Maintainer, string Version, string Homepage, string Forum, string Description, string Changelog, string UsageInstructions, string DownloadURL, string[] Screenshots, string WebserviceUrl)
            : base(Name, Author, Maintainer, Version, Homepage, Forum, Description, Changelog, UsageInstructions, DownloadURL, Screenshots)
        {
            this.WebserviceUrl = WebserviceUrl;
        }
    }
}