using Core.Crawler;
using Infrastructure.DataServices;
using Infrastructure.Extensions;
using Infrastructure.Repository;
using Infrastructure.WebUtils;

using HtmlAgilityPack;


WebReader webReader = new WebReader(new WebCrawler(new HtmlDocument()));
ServerSettings storageSettings = new ServerSettings()
{
    ServerName = ".",
    Database = "BestCam"
};
webReader.AppSettings.Setup(x =>
{
    x.Url = "https://photo-forum.net/i/";

    x.DataProvider = new StorageService(storageSettings);
    
    x.StartFrom = 2416488;
    x.To = 2417488;
});
UowService uowService = new UowService(webReader.AppSettings.DataProvider);

//var result = await webReader.ReadUrlData();

//var addRangeResponse = await uowService.DataRepository.AddRangeAsync(result);
//Console.WriteLine(addRangeResponse);
//var res = await uowService.DataRepository.ListBrandsAsync();
//foreach (var item in res)
//{
//    Console.WriteLine(item.Key);
//    foreach (var p in item.Value)
//    {
//        Console.WriteLine($"{p.Rate} {p.Url}");
//    }
//}

