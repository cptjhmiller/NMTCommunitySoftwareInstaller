using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace com.nmtinstaller.csi.Utilities
{
    public static class Functions
    {
        public delegate void UpdateProgressInfo(int Procent, string description);

        public static string HtmlToRtf(string InstallResult, bool IncludeNewLines)
        {
            InstallResult = InstallResult.Replace("<br>", "\\line ");

            if (IncludeNewLines)
            {
                InstallResult = InstallResult.Replace("\n", "\\line ");
            }

            InstallResult = InstallResult.Replace("<b>", "\\b ");
            InstallResult = InstallResult.Replace("</b>", "\\b0");
            InstallResult = InstallResult.Replace("<u>", "\\ul ");
            InstallResult = InstallResult.Replace("</u>", "\\ul0");
            InstallResult = InstallResult.Replace("<i>", "\\i ");
            InstallResult = InstallResult.Replace("</i>", "\\i0");
            InstallResult = Regex.Replace(InstallResult, "\\s+", " ");
            InstallResult = Regex.Replace(InstallResult, "<a .*href=\"(.*)\".*>.*</a>", "\\1");
            InstallResult = Regex.Replace(InstallResult, @"<h1>", "\\fs24\\b ");
            InstallResult = Regex.Replace(InstallResult, @"</h1>", "\\fs18\\b0\\line ");
            InstallResult = Regex.Replace(InstallResult, @"<h2>", "\\fs22\\b ");
            InstallResult = Regex.Replace(InstallResult, @"</h2>", "\\fs18\\b0\\line ");
            InstallResult = Regex.Replace(InstallResult, @"<h3>", "\\fs20\\b ");
            InstallResult = Regex.Replace(InstallResult, @"</h3>", "\\fs18\\b0\\line ");
            InstallResult = Regex.Replace(InstallResult, @"<h\d{1}>", "\\b ");
            InstallResult = Regex.Replace(InstallResult, @"</h\d{1}>", "\\b0\\line ");

            //strip the rest of the unsupported tags
            InstallResult = Regex.Replace(InstallResult, "<.*?>", "");
            return "{\\rtf1\\ansi \\fs18" + InstallResult + "}";
        }
    }
}
