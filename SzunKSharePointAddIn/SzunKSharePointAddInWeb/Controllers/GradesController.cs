using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SzunKSharePointAddInWeb.Models;
using SzunKSharePointAddInWeb.ViewModels;

namespace SzunKSharePointAddInWeb.Controllers
{
    public class GradesController : Controller
    {
        [SharePointContextFilter]
        [HttpGet]
        public ActionResult ChangeGrades()
        {
            // The following code does not work - use the default code that comes with HomeController. There 
            //      seems to be an issue with the ClientContext object.
            //// Start with ClientContext - the constructor requires a URL to the server running SharePoint.
            //// This opens Context to the web, in a similar way to opening a connection to a database.
            //// Copy the URL of the website the Add-In is linked to.
            //ClientContext context = new ClientContext("https://agtivcaonsulting1.sharepoint.com/sites/trainingsite/SzunK/TrainingDay4/");

            User spUser = null;

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            GradesViewModel vm = new GradesViewModel();

            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    spUser = clientContext.Web.CurrentUser;

                    clientContext.Load(spUser, user => user.Title);

                    clientContext.ExecuteQuery();

                    ViewBag.UserName = spUser.Title;
                }

                // The SharePoint web site contains a list called "Student Gradebook" - grab and store it in a List variable.
                // Linking to the list, like finding a table in a database
                List studentGradebook = clientContext.Web.Lists.GetByTitle("Student Gradebook");

                // CAML = Collaborative Application Markup Language
                // Query allows you to query for specific items from a list, using the ViewXml property.
                // Using CamlQuery.CreateAllItemsQuery() creates a query that retrieves all list items.
                CamlQuery query = CamlQuery.CreateAllItemsQuery();

                // This line grabs items from the studentGradebook List object based on the specified query, and
                //      stores them all in a ListItemCollection object.
                ListItemCollection studentGradebookCollection = studentGradebook.GetItems(query);

                // This line ties the ListItemCollection to the ClientContext object, providing a line of communication
                //      between the program and the SharePoint server. Without this line, the items queried for will
                //      not be retrieved from SharePoint.
                clientContext.Load(studentGradebookCollection);

                // Call ExecuteQuery() to perform the query, loading the specified items from SharePoint.
                clientContext.ExecuteQuery();

                foreach (ListItem grade in studentGradebookCollection)
                {
                    string courseTitle = (string)grade["Title"];
                    string passed = (string)grade["Result"];
                    vm.GradeBook.Add(new Grade(courseTitle, passed));
                }

                // Call ExecuteQuery to commit changes made to the list.
                clientContext.ExecuteQuery();
            }

            // Changing display ResultColour of Grade Passed string
            // This is done in the controller because it has direct access to the GradeBook List of Grade objects.
            foreach (Grade grade in vm.GradeBook)
            {
                grade.ResultColor = grade.Passed.ToLower() == "pass" ? "color:Green" : "color:Red";
            }

            // Logic done here
            // Check for click of Save button, and execute whatever is required (aka copy from slides and notebook)
            // You might need to make [HttpGet] and [HttpPost] attributed methods 
            // Upon obtaining the list, remember to update the colour of the text
            // Might also need to populate a local list based on data obtained from controller

            // Need to have both read and write functionality.

            // Pass the View Model object into the View. Why must this be done?
            return View(vm);
        }

        [SharePointContextFilter]
        [HttpPost]
        public ActionResult ChangeGrades(GradesViewModel vm)
        {
            User spUser = null;

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    spUser = clientContext.Web.CurrentUser;

                    clientContext.Load(spUser, user => user.Title);

                    clientContext.ExecuteQuery();

                    ViewBag.UserName = spUser.Title;
                }

                // The SharePoint web site contains a list called "Student Gradebook" - grab and store it in a List variable.
                // Linking to the list, like finding a table in a database
                List studentGradebook = clientContext.Web.Lists.GetByTitle("Student Gradebook");

                if (Request.Form["btnSave"] != null)
                {
                    if (ModelState.IsValid)
                    {
                        // Create the list item, and set the property value.
                        ListItemCreationInformation itemCreationInfo = new ListItemCreationInformation();
                        ListItem newItem = studentGradebook.AddItem(itemCreationInfo);
                        newItem["Title"] = vm.Grade.CourseTitle;
                        newItem["Result"] = vm.Grade.Passed;
                        newItem.Update();

                        clientContext.ExecuteQuery();

                        return RedirectToAction("ChangeGrades", "Grades", new { SPHostUrl = "https://agtivconsulting1.sharepoint.com/sites/trainingsite/SzunK/TrainingDay4/" });
                    }

                    
                }
            }

            return View(vm);
        }
    }
}