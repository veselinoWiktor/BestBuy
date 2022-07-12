using Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class WebPictureInfo : BaseModel
    {
        public string Brand { get; set; }

        public string Url { get; set; }

        public int Rating { get; set; }
    }
}
