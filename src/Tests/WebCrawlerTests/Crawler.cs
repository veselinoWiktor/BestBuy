using Core.Crawler;
using HtmlAgilityPack;
using Infrastructure.DataServices;
using Infrastructure.Extensions;
using Infrastructure.WebUtils;
using Moq;

namespace WebCrawlerTests
{
    public class UnitTest1
    {
        [Fact]
        public async void Test_WebReader()
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            WebReader webReader = new WebReader(new WebCrawler(htmlDocument));
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
            var res = await webReader.ReadUrlData();


            Assert.Equal(64, res[0].Rating);
            Assert.Equal("NIKON CORPORATION", res[0].Brand);
            Assert.Equal("https://photo-forum.net/i/2417488", res[0].Url);
        }

        [Fact]
        public async void Test_WebCrawler_Brand_null()
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            WebReader webReader = new WebReader(new WebCrawler(htmlDocument));
            ServerSettings storageSettings = new ServerSettings()
            {
                ServerName = ".",
                Database = "BestCam"
            };
            webReader.AppSettings.Setup(x =>
            {
                x.Url = "https://photo-forum.net/i/";

                x.DataProvider = new StorageService(storageSettings);
               
                x.To = 241742;
            });
            var res = await webReader.ReadUrlData();

            Assert.Equal(0, res.Count);
        }
    }
}