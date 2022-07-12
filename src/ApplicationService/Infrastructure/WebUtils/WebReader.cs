using Infrastructure.Abstractions;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.WebUtils
{
    public class WebReader : WebReaderBase
    {
        private List<WebPictureInfo> models=new List<WebPictureInfo>();
        public WebReader(IWebCrawler webCrawler):base(webCrawler)
        {

        }
       
        public override async Task<List<WebPictureInfo>> ReadUrlData()
        {
            using (HttpClient http = new HttpClient())
            {
                for (int i = AppSettings.StartFrom; i <= AppSettings.To; i++)
                {
                    var currentUrl = string.Format($"{Url}{i}");
                    webCrawler.CurrentUrl = currentUrl;
                    var response = Task.Run(async delegate
                    {

                        return await http.GetAsync(currentUrl);

                    }).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var htmlResponse = await response.Content.ReadAsStringAsync();
                        var model = await webCrawler.GetPictureDataAsync(htmlResponse);
                        if (model == null)
                        {
                            continue;
                        }
                        else
                        {
                            models.Add(model);
                        }
                    }
                }
            }
            return models;
        }
    }
}
