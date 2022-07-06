using Infrastructure.Abstractions;
using Infrastructure.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class Settings
    {
        public static void Setup(this AppSettings settings, Action<AppSettings> action)
        {
            action(settings);
        }
    }
}
