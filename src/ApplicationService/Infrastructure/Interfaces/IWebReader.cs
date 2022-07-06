using Infrastructure.Abstractions;
using Infrastructure.DataServices;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IWebReader
    {
        public AppSettings AppSettings { get; set; }
        public Task<List<WebPictureInfo>> ReadUrlData();
    }
}
