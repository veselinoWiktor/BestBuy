using Infrastructure.Abstractions;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IDataRepository
    {
        Task<WebPictureInfo> GetByIdAsync(int id);
        Task<WebPictureInfo> GetByNameAsync(string brand);
        Task<IEnumerable<WebPictureInfo>> ListEntitiesAsync();
        Task<Dictionary<string,List<Picture>>> ListBrandsAsync();
        Task<int> AddAsync(WebPictureInfo entity);
        Task<int> AddRangeAsync(List<WebPictureInfo> entities);
        Task<List<WebPictureInfo>> GetTop10();
       
    }
}
