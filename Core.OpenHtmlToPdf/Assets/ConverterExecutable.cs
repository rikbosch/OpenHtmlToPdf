using System;
using System.IO;
using System.Reflection;

namespace Core.OpenHtmlToPdf.Assets
{
    internal sealed class ConverterExecutable
    {
        private const string ConverterExecutableFilename = "Core.OpenHtmlToPdf.WkHtmlToPdf.exe";

        private ConverterExecutable()
        {
        }

        public static ConverterExecutable Get()
        {
            ConverterExecutable bundledFile = new ConverterExecutable();

            bundledFile.CreateIfConverterExecutableDoesNotExist();

            return bundledFile;
        }

        public string FullConverterExecutableFilename => ResolveFullPathToConverterExecutableFile();

        private void CreateIfConverterExecutableDoesNotExist()
        {
            if (!File.Exists(FullConverterExecutableFilename))
            {
                Create(GetConverterExecutableContent());
            }
        }

        private static byte[] GetConverterExecutableContent()
        {
            using (Stream resourceStream = GetConverterExecutable())
            {
                byte[] resource = new byte[resourceStream.Length];

                resourceStream.Read(resource, 0, resource.Length);

                return resource;
            }
        }

        private static Stream GetConverterExecutable() => typeof(ConverterExecutable).Assembly.GetManifestResourceStream(typeof(ConverterExecutable), "Core.OpenHtmlToPdf.WkHtmlToPdf.exe");

        private void Create(byte[] fileContent)
        {
            try
            {
                if (!Directory.Exists(BundledFilesDirectory()))
                {
                    Directory.CreateDirectory(BundledFilesDirectory());
                }

                using (FileStream file = File.Open(FullConverterExecutableFilename, FileMode.Create))
                {

                    file.Write(fileContent, 0, fileContent.Length);
                }
            }
            catch (IOException)
            {
            }
        }

        private static string ResolveFullPathToConverterExecutableFile() => Path.Combine(BundledFilesDirectory(), ConverterExecutableFilename);

        private static string BundledFilesDirectory() => Path.Combine(Path.GetTempPath(), "Core.OpenHtmlToPdf", Version());

        private static string Version() => string.Format("{0}_{1}",
                Assembly.GetExecutingAssembly().GetName().Version, 64
                );
    }
}