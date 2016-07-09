using System.Reflection;
using System.IO;

namespace com.nmtinstaller.csi.Utilities
{
    public static class Constants
    {
        public static readonly string TemporaryFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "Temp" + Path.DirectorySeparatorChar;
    }
}