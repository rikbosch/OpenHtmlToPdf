using System.IO;
using System.Threading.Tasks;

namespace Core.OpenHtmlToPdf
{
    public interface IPdfDocument
    {
        IPdfDocument WithGlobalSetting(string key, string value);
        IPdfDocument WithObjectSetting(string key, string value);
        Task<byte[]> Content();
        Task WriteToStream(Stream target);
    }
}