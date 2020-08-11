using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Day1Demo.Controllers
{
    public class Day1DemoController : Controller
    {
        // GET: Day1Demo
        public ActionResult ParentView()
        {
            // right-click on View > add view > enter view name + uncheck use a layout page
            // return View("your input view name");
            //      The above forces VS to look for a view with the specified name
            // right-click on View > add view > view name matches method name + check use a layout page and leave blank
            // return View();
            //      The above causes VS to look for a view with a name matching that of the method
            //      It also results in VS loading from _Layout in the Shared folder
            //      Commands such as ViewBag become available
            //      If using a custom layout, do not leave the relevant field blank
            return View();
        }
    }
}