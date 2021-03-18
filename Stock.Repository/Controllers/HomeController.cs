using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public IActionResult Export(StockModel model)
        {
            using (TextWriter writer = new StreamWriter(@"C:\test.csv", false, System.Text.Encoding.UTF8))
            {
                var csv = new CsvWriter(writer, new System.Globalization.CultureInfo("ru"), true);
                csv.WriteRecord(model); // where values implements IEnumerable
            }
            return Ok();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
