using Infrastructure.Abstractions;
using Infrastructure.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class StorageSettings
    {
        public static void StorageSetup(this StorageData settings, Action<StorageData> action)
        {
            action(settings);
        }
    }
}
