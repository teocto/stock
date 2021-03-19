using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Stock.Models;
using Stock.Repository.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Export([FromHeader]StockModel model)
        {
            using (TextWriter writer = new StreamWriter(@"C:\test.csv", true, System.Text.Encoding.UTF8))
            {
                string json = JsonConvert.SerializeObject(model);
                writer.WriteLine(json);
            }
            return Ok();
        }

        public string ReadData()
        {
            string data = "[";
            using (var reader = new StreamReader(@"C:\test.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    data += line + ",";
                }
            }
            data += "]";
            return data;
        }

    }
}
