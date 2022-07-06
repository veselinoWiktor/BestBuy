using Infrastructure.DataServices;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Abstractions
{
    public abstract class WebReaderBase : IWebReader
    {
        protected readonly IWebCrawler webCrawler;
        public string Url
        {
            get
            {
                return AppSettings.Url;
            }
           
        }

        public WebReaderBase(IWebCrawler webCrawler)
        {
            this.webCrawler = webCrawler;
        }
        public AppSettings AppSettings { get; set; }=new AppSettings();
        

        public abstract Task<List<WebPictureInfo>> ReadUrlData();
    }
}
