using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DapperDemoAPI.Controllers
{
    public class WorkMateController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            string val = "fairhr";
            int valInt = int.Parse(val);
            //return new EmptyResult();
            return View();
        }

    }
}
