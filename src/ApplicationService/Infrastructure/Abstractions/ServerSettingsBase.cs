using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Abstractions
{
    public class ServerSettingsBase
    {
        public string ServerName { get; set; }
        public string Account { get; set; }
        public string Database { get; set; }
    }
}
