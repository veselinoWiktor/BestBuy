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
            return null;
           
        }
    }
}
