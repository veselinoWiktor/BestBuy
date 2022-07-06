using Infrastructure.Abstractions;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IWebCrawler
    {
        public string CurrentUrl { get; set; }
        public Task<WebPictureInfo> GetPictureDataAsync(string data);
    }
}
