using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using Core.OpenHtmlToPdf.WkHtmlToPdf.Assets;
using Core.OpenHtmlToPdf.WkHtmlToPdf.Interop;

namespace Core.OpenHtmlToPdf.WkHtmlToPdf.WkHtmlToX
{
    internal sealed class WkHtmlToPdfLibrary
    {
        private const string LibraryFilename = "wkhtmltox.dll";
        private const string Compressed32BitLibraryFilename = "wkhtmltox_32.dll";
        private const string Compressed64BitLibraryFilename = "wkhtmltox_64.dll";

        public static NativeLibrary Load() => NativeLibrary.Load(LibraryFilename, LoadLibraryContent);

        private static byte[] LoadLibraryContent()
        {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                throw new PlatformNotSupportedException(string.Format("Platform {0} is not supported", Platform()));
            }

            using (ZipArchive wkhtmltoxZipArchive = WkHtmlToXZipArchive())
            {
                return wkhtmltoxZipArchive.ReadFile(CompressedLibraryFilename());
            }
        }

        private static ZipArchive WkHtmlToXZipArchive() => new ZipArchive(GetManifestResourceStream());

        private static Stream GetManifestResourceStream() => Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream("Core.OpenHtmlToPdf.WkHtmlToPdf.Assets.wkhtmltox.zip");

        private static string CompressedLibraryFilename() => Environment.Is64BitProcess
            ? Compressed64BitLibraryFilename
            : Compressed32BitLibraryFilename;

        private static string Platform() => Enum.GetName(typeof(PlatformID), Environment.OSVersion.Platform);
    }
}