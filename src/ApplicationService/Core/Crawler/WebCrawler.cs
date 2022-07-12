using HtmlAgilityPack;
using Infrastructure.Abstractions;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("WebCrawlerTests")]
namespace Core.Crawler
{
    public class WebCrawler : IWebCrawler
    {
        private readonly HtmlDocument htmlDocument;

        public string CurrentUrl { get; set; }

        public WebCrawler(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }
        public async Task<WebPictureInfo> GetPictureDataAsync(string data)
        {

            htmlDocument.LoadHtml(data);

            bool hasCameraBrand = htmlDocument.DocumentNode.SelectNodes("//span[@class=\'exif-info-label\']")[0].InnerText == "brand";

            if (!hasCameraBrand)
            {
                return null;
            }

            var cameraBrandSelector = htmlDocument.DocumentNode.SelectNodes("//span[@class=\'exif-info-data\']")[0];
            var ratingSelector = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(@class, 'rating-type ratinga')]/span[contains(@class, 'counter')]/span");

            string cameraBrand = cameraBrandSelector.InnerText;
            int ratingScore = int.Parse(ratingSelector.InnerText);

            WebPictureInfo model = new WebPictureInfo()
            {
                Brand = cameraBrand,
                Url = this.CurrentUrl,
                Rating = ratingScore
            };
            
            return model;
           
        }
    }
}
