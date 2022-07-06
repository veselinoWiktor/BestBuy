using Infrastructure.Abstractions;
using Infrastructure.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ApplicationManager:Manager
    {
        public ApplicationManager(AppSettings settingsBase):base(settingsBase)
        {

        }
        public override async Task DownloadData(string url, int from, int to)
        {
            for (int i = from; i < to; i++)
            {

            }
        }
    }
}
