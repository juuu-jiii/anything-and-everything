using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AGTIV.SPApp.TemplateProcess;
using AGTIV.SPApp.Template.Entities;

namespace AGTIV.SPApp.TemplateWeb.Controllers
{
    public class GradesController : Controller
    {
        [SharePointContextFilter]
        public ActionResult ChangeGrades(GradesViewModel vm)
        {
            

            if (Request.Form["btnSave"] != null)
            {
                var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
                SampleProcess sampleProcess = new SampleProcess();
                vm.spHostURL = spContext.SPHostUrl.AbsoluteUri;
                vm.accessToken = spContext.UserAccessTokenForSPHost;
                sampleProcess.SamplePost(vm);

                return View(vm);
            }
            else
            {
                var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
                TokenModel tokenModel = new TokenModel();
                vm.spHostURL = spContext.SPHostUrl.AbsoluteUri;
                vm.accessToken = spContext.UserAccessTokenForSPHost;

                SampleProcess sampleProcess = new SampleProcess();
                string name = sampleProcess.GetSampleGetMyLogin(tokenModel);
                ViewBag.Name = name;

                return View(vm);
            }
        }

        //[SharePointContextFilter]
        //[HttpPost]
        //public ActionResult ChangeGrades(GradesViewModel vm)
        //{
        //    User spUser = null;

        //    var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

        //    using (var clientContext = spContext.CreateUserClientContextForSPHost())
        //    {
        //        if (clientContext != null)
        //        {
        //            spUser = clientContext.Web.CurrentUser;

        //            clientContext.Load(spUser, user => user.Title);

        //            clientContext.ExecuteQuery();

        //            ViewBag.UserName = spUser.Title;
        //        }

        //        // The SharePoint web site contains a list called "Student Gradebook" - grab and store it in a List variable.
        //        // Linking to the list, like finding a table in a database
        //        List studentGradebook = clientContext.Web.Lists.GetByTitle("Student Gradebook");

        //        if (Request.Form["btnSave"] != null)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                // Create the list item, and set the property value.
        //                ListItemCreationInformation itemCreationInfo = new ListItemCreationInformation();
        //                ListItem newItem = studentGradebook.AddItem(itemCreationInfo);
        //                newItem["Title"] = vm.Grade.CourseTitle;
        //                newItem["Result"] = vm.Grade.Passed;
        //                newItem.Update();

        //                clientContext.ExecuteQuery();

        //                return RedirectToAction("ChangeGrades", "Grades", new { SPHostUrl = "https://agtivconsulting1.sharepoint.com/sites/trainingsite/SzunK/TrainingDay4/" });
        //            }


        //        }
        //    }

        //    return View(vm);
        //}
    }
}
