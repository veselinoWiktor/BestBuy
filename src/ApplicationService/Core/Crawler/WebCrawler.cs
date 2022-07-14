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


            //TODO implement agility code           

            string name = htmlDocument.DocumentNode
                .SelectSingleNode("//span[@class='exif-info-label']")
                .InnerText;
            if (name != "brand")
            {
                return null;
            }

            string brandName = htmlDocument.DocumentNode
                .SelectSingleNode("//span[@class='exif-info-data']")
                .InnerText;

            int rating = int.Parse(htmlDocument.DocumentNode
                .SelectSingleNode("//li[contains(@class, 'rating-type ratinga')]/span[contains(@class, 'counter')]/span")
                .InnerText);

            WebPictureInfo model = new WebPictureInfo();
            model.Brand = brandName;
            model.Rating = rating;
            model.Url = this.CurrentUrl;
            return model;
        }
    }
}
