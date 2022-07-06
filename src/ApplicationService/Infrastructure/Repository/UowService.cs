using Infrastructure.Abstractions;
using Infrastructure.DataServices;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UowService
    {
        private bool disposed = false;
        private IDataRepository repository;
        private readonly StorageData data;

        public UowService(StorageData data)
        {
            this.data = data;
        }
        public IDataRepository DataRepository
        {
            get
            {
                if (repository==null)
                {
                    repository = new DataRepository(data);
                }
                return repository;
            }
        }
       
    }
}
