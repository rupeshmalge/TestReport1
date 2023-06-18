using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestReport.Models;

namespace TestReport.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment= webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
            return Print();
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
        public IActionResult Print()
        {
            List<UserDto> userlist = new List<UserDto>();
            userlist.Add(new UserDto { business="Computer"});

            string mimetype = "";
            int extension = 1;

            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Report1.rdlc";
            Dictionary<string, string> param= new Dictionary<string, string>();
            param.Add("ReportParameter1", "Welcome To Report Demo");
                LocalReport localreport = new LocalReport(path);
          //  localreport.AddDataSource("Main",userlist);
            var result = localreport.Execute(RenderType.Pdf,extension,param,mimetype);
            return File(result.MainStream,"application/pdf");

        }
    }
}
