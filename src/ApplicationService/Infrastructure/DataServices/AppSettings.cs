using Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataServices
{
    public class AppSettings
    {

        private int _startFrom=1;
        private int _to=1;
        public string Url { get; set; }
        /// <summary>
        /// Get pictures from started index. Minimum index value 1
        /// </summary>
        public int StartFrom
        {
            get
            {
                return _startFrom;
            }
            set
            {
                if (value<1)
                {
                    _startFrom = value;
                }
                
            }
        }
        /// <summary>
        /// If StartFrom is set to default image number will be set from/to from this property
        /// </summary>
        public int To
        {
            get
            {
                return _to;
            }
            set
            {
                
                if (value>1 && _startFrom == 1)
                {
                    _startFrom = value;
                    _to = value;
                }
                else if (value > 1 && _startFrom!=1)
                {
                    _to = value;
                }

            }
        }
       // public ServerSettings ServerSettings { get; set; } = new ServerSettings();
        public StorageData DataProvider { get; set; }
    }
}
