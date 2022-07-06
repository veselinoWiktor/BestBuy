using Infrastructure.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Abstractions
{
    public abstract class Manager
    {
        private AppSettings appSettings;
        public Manager(AppSettings appSettings)
        {
            this.appSettings = appSettings; 
        }
        public abstract Task DownloadData(string url, int from, int to);
    }
}
