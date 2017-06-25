using System;
using System.IO;
using System.Threading.Tasks;

using Didstopia.PDFReader.Utils;

namespace Didstopia.PDFReader
{
    public static class PDFReader
    {
        public static PDFBook OpenBook(string filePath)
        {
            return OpenBookAsync(filePath).Result;
        }

        public static async Task<PDFBook> OpenBookAsync(string filePath)
        {
            if (UrlUtils.FileIsUrl(filePath))
            {
                var localFilePath = await UrlUtils.UrlToFile(filePath);
                if (string.IsNullOrEmpty(localFilePath))
                    throw new FileNotFoundException("Specified remote PDF file could not be downloaded to local filesystem.", filePath);

                filePath = localFilePath;
            }

            //filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Specified local PDF file not found.", filePath);

            var book = new PDFBook(filePath);
            return book;
        }
    }
}
