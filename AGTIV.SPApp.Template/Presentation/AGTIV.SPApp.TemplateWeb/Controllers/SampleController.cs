using AGTIV.SPApp.Template.Entities;
using AGTIV.SPApp.TemplateProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGTIV.SPApp.TemplateWeb.Controllers
{
    public class SampleController : Controller
    {
        // GET: Sample
        [SharePointContextFilter]
        public ActionResult Index(GradesViewModel vm)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            SampleProcess sampleProcess = new SampleProcess();
            vm.Grade.CourseTitle = "GGG6666 Good Games Galore";
            vm.Grade.Passed = "Fail";
            vm.spHostURL = spContext.SPHostUrl.AbsoluteUri;
            vm.accessToken = spContext.UserAccessTokenForSPHost;
            vm = sampleProcess.SamplePost(vm);


            return View(vm);
        }

        [SharePointContextFilter]
        public ActionResult GetMyLogin()
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            TokenModel vm = new TokenModel();            
            vm.spHostURL = spContext.SPHostUrl.AbsoluteUri;
            vm.accessToken = spContext.UserAccessTokenForSPHost;

            SampleProcess sampleProcess = new SampleProcess();
            string name = sampleProcess.GetSampleGetMyLogin(vm);
            ViewBag.Name = name;
            return View(vm);
        }
    }
}