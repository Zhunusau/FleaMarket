using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.HtmlParserInterfaces;

namespace GodelMastery.FleaMarket.HtmlParser
{
    public class HtmlLoader : IDisposable
    {
        private readonly HttpClient client;
        private readonly string url;
        private bool disposed;

        public HtmlLoader(IParserSettings parserSettings)
        {
            client = new HttpClient();
            url = $"{parserSettings.BaseUrl}/{parserSettings.Prefix}";
        }

        public async Task<string> GetSourceByPageId(int page)
        {
            var currentUrl = url.Replace("{currentPage}", page.ToString());
            var response = await client.GetAsync(currentUrl);
            string source = null;
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }
            return source;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    client.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
