using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Day1Demo.Controllers
{
    public class TestController : Controller
    {
        public string GetString()
        {
            return "Hello world";
        }

        public ActionResult GetView()
        {
            return View("MyView");
        }
    }
}