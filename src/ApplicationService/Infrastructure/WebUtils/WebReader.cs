using Infrastructure.Abstractions;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.WebUtils
{
    public class WebReader : WebReaderBase
    {
        private List<WebPictureInfo> models=new List<WebPictureInfo>();
        public WebReader(IWebCrawler webCrawler):base(webCrawler)
        {

        }
       
        public override async Task<List<WebPictureInfo>> ReadUrlData()
        {
            using (HttpClient http = new HttpClient())
            {
               //TODO
            }
            return models;
        }
    }
}
