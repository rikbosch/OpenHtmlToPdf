using System;
using System.IO;
using System.Reflection;

namespace Core.OpenHtmlToPdf.WkHtmlToPdf.Assets
{
    internal sealed class BundledFile
    {
        private readonly string _bundledFilename;
        private readonly Func<byte[]> _fileContentProvider;

        private BundledFile(string bundledFilename, Func<byte[]> fileContentProvider)
        {
            _bundledFilename = bundledFilename;
            _fileContentProvider = fileContentProvider;
        }

        public static BundledFile From(string bundledFilename, Func<byte[]> fileContentProvider)
        {
            BundledFile bundledFile = new BundledFile(bundledFilename, fileContentProvider);

            bundledFile.CreateIfNotExist();

            return bundledFile;
        }

        public string FullPathToBundledFile => ResolveFullPathToBundledFile();

        private void CreateIfNotExist()
        {
            if (!File.Exists(FullPathToBundledFile))
            {
                Create(_fileContentProvider());
            }
        }

        private void Create(byte[] fileContent)
        {
            try
            {
                if (!Directory.Exists(BundledFilesDirectory()))
                {
                    Directory.CreateDirectory(BundledFilesDirectory());
                }

                using (FileStream file = File.Open(FullPathToBundledFile, FileMode.Create))
                {

                    file.Write(fileContent, 0, fileContent.Length);
                }
            }
            catch (IOException)
            {
            }
        }

        private string ResolveFullPathToBundledFile() => Path.Combine(BundledFilesDirectory(), _bundledFilename);

        private static string BundledFilesDirectory() => Path.Combine(Path.GetTempPath(), "OpenHtmlToPdf", Version());

        private static string Version() => string.Format("{0}_{1}",
                Assembly.GetExecutingAssembly().GetName().Version,
                Environment.Is64BitProcess ? 64 : 32);
    }
}