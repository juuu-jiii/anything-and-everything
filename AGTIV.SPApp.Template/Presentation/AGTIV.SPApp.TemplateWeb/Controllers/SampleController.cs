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
        public ActionResult Index(SampleViewModel vm)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            SampleProcess sampleProcess = new SampleProcess();
            vm.Title = "New Title";
            vm.Description = "New Description";
            vm.spHostURL = spContext.SPHostUrl.AbsoluteUri;
            vm.accessToken = spContext.UserAccessTokenForSPHost;
            vm = sampleProcess.SamplePost(vm);


            return View(vm);
        }

        [SharePointContextFilter]
        public ActionResult GetMyLogin()
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            SampleTokenModel vm = new SampleTokenModel();            
            vm.spHostURL = spContext.SPHostUrl.AbsoluteUri;
            vm.accessToken = spContext.UserAccessTokenForSPHost;

            SampleProcess sampleProcess = new SampleProcess();
            string name = sampleProcess.GetSampleGetMyLogin(vm);
            ViewBag.Name = name;
            return View(vm);
        }
    }
}