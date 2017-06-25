using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Didstopia.PDFReader.Utils
{
    internal static class UrlUtils
    {
        public static bool FileIsUrl(string url)
        {
            Uri uriResult;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uriResult)) return false;
            if (uriResult == null) return false;

            return  uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps || uriResult.Scheme == Uri.UriSchemeFtp;
        }

        public static async Task<string> UrlToFile(string url)
        {
            HttpWebRequest httpRequest = WebRequest.Create(url) as HttpWebRequest;
            httpRequest.Method = WebRequestMethods.Http.Get;
            httpRequest.AllowAutoRedirect = true;

            HttpWebResponse httpResponse = await httpRequest.GetResponseAsync() as HttpWebResponse;

            var tempFilePath = Path.GetTempFileName();
            Stream httpResponseStream = httpResponse.GetResponseStream();
            using (var fs = File.Create(tempFilePath))
            {
                httpResponseStream.CopyTo(fs);
            }

            httpResponse.Close();

            return tempFilePath;
        }
    }
}
