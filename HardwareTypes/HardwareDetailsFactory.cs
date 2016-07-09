using System;
using System.Collections.Generic;
using System.Text;
using com.nmtinstaller.csi.HardwareTypes;

namespace com.nmtinstaller.csi.Utilities
{
    class HardwareDetailsFactory
    {
        public static HardwareDetails CreateDetails(HardwareTypeEnum HardwareIndex)
        {
            /*
             * Select hardware type
                Popcorn Hour A-100
                Popcorn Hour A-110
                Popcorn Hour A-200
                Popcorn Hour A-200 (USB)
                Popcorn Hour A-210
                Popcorn Hour A-210 (USB)
                Popcorn Hour S-210
				Popcorn Hour S-210 (USB)
				Popcorn Hour B-110
                Popcorn Hour C-200
                Popcorn Hour C-200 (USB)
                Popcorn Hour A-300
                Popcorn Hour A-300 (USB)
                Popcorn Hour C-300
                Popcorn Hour C-300 (USB)
                Popcorn Hour A-400
                Popcorn Hour A-400 (USB)
                Popcorn Hour A-400 (SD Card)
                Popcorn Hour A-410
                Popcorn Hour A-410 (USB)
                Popcorn Hour A-410 (SD Card)
                Popcorn Hour A-500
                Popcorn Hour A-500 (USB)
                Popcorn Hour A-500 (SD Card)
                Popcorn Hour A-500 (Esata)
                Popcorn Hour VTEN
                Popcorn Hour VTEN (USB)
                Popcorn Hour VTEN (SD Card)
                Popcorn Hour VTEN (Esata)
                eGreat
                Digitek HDX
                Kaiboer
                All others
             */

            HardwareDetails result;
            switch (HardwareIndex)
            {
                case HardwareTypeEnum.A100:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "", DefaultWebserverSubFolder = "HARD_DISK", RepositoryType = "AB", DefaultServerName = "PCH-A100", DefaultFtpUsername = "ftpuser", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.A110:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "", DefaultWebserverSubFolder = "HARD_DISK", RepositoryType = "AB", DefaultServerName = "PCH-A110", DefaultFtpUsername = "ftpuser", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.B110:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "", DefaultWebserverSubFolder = "SATA_DISK", RepositoryType="AB", DefaultServerName="PCH-B110", DefaultFtpUsername="ftpuser", DefaultFtpPassword="1234" };
                    break;
                case HardwareTypeEnum.A200:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "SATA_DISK", DefaultWebserverSubFolder = "SATA_DISK", RepositoryType = "C", DefaultServerName = "PCH-A200", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.A210:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "SATA_DISK", DefaultWebserverSubFolder = "SATA_DISK", RepositoryType = "C", DefaultServerName = "PCH-A210", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.S210:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "SATA_DISK", DefaultWebserverSubFolder = "SATA_DISK", RepositoryType = "C", DefaultServerName = "PCH-S210", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.C200:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "SATA_DISK", DefaultWebserverSubFolder = "SATA_DISK", RepositoryType = "C", DefaultServerName = "PCH-C200", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.A300:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "SATA_DISK", DefaultWebserverSubFolder = "SATA_DISK", RepositoryType = "C", DefaultServerName = "PCH-A300", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.C300:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "SATA_DISK", DefaultWebserverSubFolder = "SATA_DISK", RepositoryType = "C", DefaultServerName = "PCH-C300", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.A400:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "SATA_DISK", DefaultWebserverSubFolder = "SATA_DISK", RepositoryType = "A4", DefaultServerName = "PCH-A400", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.A410:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "SATA_DISK", DefaultWebserverSubFolder = "SATA_DISK", RepositoryType = "A4", DefaultServerName = "PCH-A410", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.A500:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "SATA_DISK", DefaultWebserverSubFolder = "SATA_DISK", RepositoryType = "V", DefaultServerName = "PCH-A500", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.VTEN:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "SATA_DISK", DefaultWebserverSubFolder = "SATA_DISK", RepositoryType = "V", DefaultServerName = "PCH-VTEN", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.A200_USB:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "USB_DRIVE", DefaultWebserverSubFolder = "USB_DRIVE", RepositoryType = "C", DefaultServerName = "PCH-A200", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.A210_USB:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "USB_DRIVE", DefaultWebserverSubFolder = "USB_DRIVE", RepositoryType = "C", DefaultServerName = "PCH-A210", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.S210_USB:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "USB_DRIVE", DefaultWebserverSubFolder = "USB_DRIVE", RepositoryType = "C", DefaultServerName = "PCH-S210", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.C200_USB:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "USB_DRIVE", DefaultWebserverSubFolder = "USB_DRIVE", RepositoryType = "C", DefaultServerName = "PCH-C200", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.A300_USB:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "USB_DRIVE", DefaultWebserverSubFolder = "USB_DRIVE", RepositoryType = "C", DefaultServerName = "PCH-A300", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.C300_USB:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "USB_DRIVE", DefaultWebserverSubFolder = "USB_DRIVE", RepositoryType = "C", DefaultServerName = "PCH-C300", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.A400_USB:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "USB_DRIVE", DefaultWebserverSubFolder = "USB_DRIVE", RepositoryType = "A4", DefaultServerName = "PCH-A400", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.A410_USB:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "USB_DRIVE", DefaultWebserverSubFolder = "USB_DRIVE", RepositoryType = "A4", DefaultServerName = "PCH-A410", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.A500_USB:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "USB_DRIVE", DefaultWebserverSubFolder = "USB_DRIVE", RepositoryType = "V", DefaultServerName = "PCH-A500", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.VTEN_USB:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "USB_DRIVE", DefaultWebserverSubFolder = "USB_DRIVE", RepositoryType = "V", DefaultServerName = "PCH-VTEN", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.A400_SDC:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "USB_DRIVE_SD_CARD", DefaultWebserverSubFolder = "USB_DRIVE_SD_CARD", RepositoryType = "A4", DefaultServerName = "PCH-A400", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.A410_SDC:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "USB_DRIVE_SD_CARD", DefaultWebserverSubFolder = "USB_DRIVE_SD_CARD", RepositoryType = "A4", DefaultServerName = "PCH-A410", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.A500_SDC:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "USB_DRIVE_SD_CARD", DefaultWebserverSubFolder = "USB_DRIVE_SD_CARD", RepositoryType = "V", DefaultServerName = "PCH-A500", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.A500_ESAT:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "ESATA_DRIVE", DefaultWebserverSubFolder = "ESATA_DRIVE", RepositoryType = "V", DefaultServerName = "PCH-A500", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.VTEN_SDC:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "USB_DRIVE_SD_CARD", DefaultWebserverSubFolder = "USB_DRIVE_SD_CARD", RepositoryType = "V", DefaultServerName = "PCH-VTEN", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.VTEN_ESAT:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "ESATA_DRIVE", DefaultWebserverSubFolder = "ESATA_DRIVE", RepositoryType = "V", DefaultServerName = "PCH-VTEN", DefaultFtpUsername = "nmt", DefaultFtpPassword = "1234" };
                    break;
                case HardwareTypeEnum.EGREAT:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "", DefaultWebserverSubFolder = "HARD_DISK", RepositoryType = "AB", DefaultServerName = "HDEYESHOT", DefaultFtpUsername = "ftpuser", DefaultFtpPassword = "1234" };
                    break;
                default:
                    result = new HardwareDetails() { DefaultFTPSubFolder = "", DefaultWebserverSubFolder = "HARD_DISK", RepositoryType = "AB", DefaultServerName = "(ip address)", DefaultFtpUsername = "ftpuser", DefaultFtpPassword = "1234" };
                    break;
            }

            return result;
        }
    }
}
