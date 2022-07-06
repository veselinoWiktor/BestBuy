using Infrastructure.Abstractions;
using Infrastructure.DataServices;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerTests
{
    public class Database
    {
        [Fact]
        public async void Test_GetAll_Records_Repository()
        {
            ServerSettings storageSettings = new ServerSettings()
            {
                ServerName = ".",
                Database = "BestCam"
            };

            DataRepository dataRepository = new DataRepository(new StorageService(storageSettings));
            var res=await dataRepository.ListEntitiesAsync();
            Assert.Equal(1, res.Count());
        }
        [Fact]
        public async void Test_Get_Entity_By_Id()
        {
            ServerSettings storageSettings = new ServerSettings()
            {
                ServerName = ".",
                Database = "BestCam"
            };

            DataRepository dataRepository = new DataRepository(new StorageService(storageSettings));
            var res = await dataRepository.GetByIdAsync(1);
            Assert.Equal(1,res.Id);
        }
        [Fact]
        public async void Test_Get_Entity_By_Name()
        {
            ServerSettings storageSettings = new ServerSettings()
            {
                ServerName = ".",
                Database = "BestCam"
            };

            DataRepository dataRepository = new DataRepository(new StorageService(storageSettings));
            var res = await dataRepository.GetByNameAsync("NIkoN");
            Assert.Equal(1, res.Id);
        }
        [Fact]
        public async void Test_UOW()
        {
            ServerSettings storageSettings = new ServerSettings()
            {
                ServerName = ".",
                Database = "BestCam"
            };

            UowService dataRepository = new UowService(new StorageService(storageSettings));
            var res = await dataRepository.DataRepository.GetByNameAsync("NIkoN");
            Assert.Equal(1, res.Id);
        }
        //[Fact]
        //public void Test_If_Db_NotExists()
        //{
        //    StorageData storageData = new StorageService(new AppSettings() { ServerName = ".", Database = "BestCam" });
        //    MethodInfo mi = typeof(StorageData).GetMethod("InitializeDatabase", BindingFlags.NonPublic | BindingFlags.Instance);
        //    object[] parameters = { "CamData"};
        //    object result = mi.Invoke(storageData, parameters);

        //    Assert.False((bool)result);
        //}
        //[Fact]
        //public async Task Test_StorageData()
        //{
        //    StorageData storageData = new StorageService(new AppSettings() { ServerName=".",Database="BestCam"});
        //    var parameter = new SqlParameter()
        //    {
        //        ParameterName = "@brand",
        //        Value = "Nikon"
        //    };
        //    var parameters= new SqlParameter[] { parameter };
        //    var res=await storageData.ExecuteSaveQuery("insert into Brands (BrandName) VALUES(@brand)",parameters);
        //    //TODO assert
        //}
    }
}
