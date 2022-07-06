using Core.Crawler;
using Infrastructure.DataServices;
using Infrastructure.Extensions;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repository;
using Infrastructure.WebUtils;

namespace BestCameraApp.Services
{
    public class ApplicationService
    {
        private readonly IWebCrawler webCrawler;

        public ApplicationService(IWebCrawler webCrawler)
        {
            this.webCrawler = webCrawler;
        }
        public async Task<Dictionary<string, List<Picture>>> GetData()
        {
            WebReader webReader = new WebReader(webCrawler);
            ServerSettings storageSettings = new ServerSettings()
            {
                ServerName = ".",
                Database = "BestCam"
            };
            webReader.AppSettings.Setup(x =>
            {
                x.Url = "https://photo-forum.net/i/";

                x.DataProvider = new StorageService(storageSettings);

                x.StartFrom = 2417488;
                x.To = 2417489;
            });

            UowService uowService = new UowService(webReader.AppSettings.DataProvider);

            var res = await uowService.DataRepository.ListBrandsAsync();
            var ordered = res.ToDictionary((x) => x.Key, (y) => y.Value.OrderByDescending(z => z.Rate).ToList()).OrderByDescending(z => z.Value.Select(a => a.Rate).Sum()).ToDictionary((x) => x.Key, (v) => v.Value);

            return ordered;
        }
    }
}
