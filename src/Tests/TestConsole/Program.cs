using Core.Crawler;
using Infrastructure.DataServices;
using Infrastructure.Extensions;
using Infrastructure.Repository;
using Infrastructure.WebUtils;


WebReader webReader = new WebReader(new WebCrawler());
ServerSettings storageSettings = new ServerSettings()
{
    ServerName = ".",
    Database = "BestCam"
};
webReader.AppSettings.Setup(x =>
{
    x.Url = "https://photo-forum.net/i/";

    x.DataProvider = new StorageService(storageSettings);
    
    x.StartFrom = 2417488;
    x.To = 2417489;
});
UowService uowService = new UowService(webReader.AppSettings.DataProvider);

var res=await uowService.DataRepository.ListBrandsAsync();
foreach (var item in res)
{
    Console.WriteLine(item.Key);
    foreach (var p in item.Value)
    {
        Console.WriteLine($"{p.Rate} {p.Url}");
    }
}
//var res=await webReader.ReadUrlData();
//await webReader.AppSettings.DataProvider.SaveRangeAsync(res);

