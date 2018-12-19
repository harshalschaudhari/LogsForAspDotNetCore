using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebApiDemo.Helper;
using Serilog;

namespace SampleWebApiDemo.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase {

        // GET api/hello/alex
        [HttpGet()]
        public ActionResult<string> Get(string name) {
            Serilogger.Information("Serilog Log: Write log on console from API (Serilog).");
            Serilogger.Information("Serilog: Hello " + name);
            Console.WriteLine("Console Log: Write log on console from API.");
            return "Hello " + name;
        }
    }
}