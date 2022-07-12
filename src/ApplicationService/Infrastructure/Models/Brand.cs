using Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Brand : BaseModel
    {
        public string BrandName { get; set; }
        public List<Picture> Pictures { get; set; }
    }
}
