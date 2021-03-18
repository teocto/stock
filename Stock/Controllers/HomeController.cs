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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string name)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "http://api.marketstack.com/v1/intraday?access_key="+_configuration.Key+"&symbols="+name);
            var data = new ResponseModel();
            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    var encoding = ASCIIEncoding.ASCII;
                    using (var reader = new System.IO.StreamReader(responseStream, encoding))
                    {
                        string responseText = reader.ReadToEnd();
                        data = JsonConvert.DeserializeObject<ResponseModel>(responseText);
                    }
                }
            }
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
