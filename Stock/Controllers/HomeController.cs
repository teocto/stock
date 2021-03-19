using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Stock.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Stock.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ConfigurationModel _configuration;
        public HomeController(ILogger<HomeController> logger, IOptions<ConfigurationModel> configuration)
        {
            _logger = logger;
            _configuration = configuration.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new ResponseModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string name)
        {
            var data = await GetData(name);
            return View(data);
        }

        public async Task<IActionResult> SendResults(string name)
        {
            var datas = await GetData(name);
            if(datas.data != null)
            foreach(var data in datas.data)
            {
                string url = $"https://localhost:4300/Home/Export";
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Add("Open", data.Open) ;
                request.Headers.Add("High", data.High);
                request.Headers.Add("Low", data.Low);
                request.Headers.Add("Last", data.Last);
                request.Headers.Add("Close", data.Close);
                request.Headers.Add("Volume", data.Volume);
                request.Headers.TryAddWithoutValidation("Date", data.Date);
                request.Headers.Add("Symbol", data.Symbol);
                request.Headers.Add("Exchange", data.Exchange);
                   
                using (var client = new HttpClient())
                {
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                            Console.WriteLine("send");
                    }
                }
            }
            return Ok();
        }

        public async Task<IActionResult> GetDataFromDb()
        {
            string url = $"https://localhost:4300/Home/ReadData";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var data = new ResponseModel();
            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStringAsync();
                    data.data = JsonConvert.DeserializeObject<List<StockModel>>(responseStream);
                }
            }
            return View("Index", data);
        }

        private async Task<ResponseModel> GetData(string name)
        {
            string url = $"http://api.marketstack.com/v1/intraday?access_key={_configuration.Key}&symbols={name}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var data = new ResponseModel();
            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ResponseModel>(responseStream);
                    data.Name = name;
                }
            }
            return data;
        }

    }
}
