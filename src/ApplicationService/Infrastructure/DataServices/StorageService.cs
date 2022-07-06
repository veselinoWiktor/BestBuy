using Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataServices
{
    public class StorageService : StorageData
    {
        public StorageService(ServerSettingsBase settings) : base(settings)
        {
        }
    }
}
