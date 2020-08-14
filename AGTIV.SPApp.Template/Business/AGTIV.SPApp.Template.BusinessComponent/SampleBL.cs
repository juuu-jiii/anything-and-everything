using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGTIV.SPApp.Template.Entities;
using AGTIV.SPApp.Template.Framework;
using Microsoft.SharePoint.Client;

namespace AGTIV.SPApp.Template.BusinessComponent
{
    public class SampleBL
    {
        public GradesViewModel DemoPost(GradesViewModel vm)
        {
            try
            {
                ProjectTokenHelper.CheckValidAccessToken(vm.spHostURL, vm.accessToken);

                using (ClientContext clientContext = TokenHelper.GetClientContextWithAccessToken(vm.spHostURL, vm.accessToken))
                {
                    //vm.Grade.CourseTitle = "API Response: " + vm.Grade.CourseTitle;
                    //vm.Grade.Passed = "API Response: " + vm.Grade.Passed;

                    // The SharePoint web site contains a list called "Student Gradebook" - grab and store it in a List variable.
                    // Linking to the list, like finding a table in a database
                    List studentGradebook = clientContext.Web.Lists.GetByTitle("Student Gradebook");

                    // Create the list item, and set the property value.
                    ListItemCreationInformation itemCreationInfo = new ListItemCreationInformation();
                    ListItem newItem = studentGradebook.AddItem(itemCreationInfo);
                    newItem["Title"] = vm.Grade.CourseTitle;
                    newItem["Result"] = vm.Grade.Passed;
                    newItem.Update();

                    clientContext.ExecuteQuery();
                }
            }
            catch(Exception)
            {

            }

            return (vm);
        }

        public GradesViewModel GetMyLogin(GradesViewModel vm)
        {
            //TokenModel result = new TokenModel();

            try
            {
                ProjectTokenHelper.CheckValidAccessToken(vm.spHostURL, vm.accessToken);

                using (ClientContext clientContext = TokenHelper.GetClientContextWithAccessToken(vm.spHostURL, vm.accessToken))
                {
                    User spUser = clientContext.Web.CurrentUser;

                    clientContext.Load(spUser, user => user.Title);

                    clientContext.ExecuteQuery();

                    //result.Name = spUser.Title;

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

                    //GradesViewModel gradesViewModel = new GradesViewModel();

                    foreach (ListItem grade in studentGradebookCollection)
                    {
                        string courseTitle = (string)grade["Title"];
                        string passed = (string)grade["Result"];
                        vm.GradeBook.Add(new Grade(courseTitle, passed));
                    }

                    // Call ExecuteQuery to commit changes made to the list.
                    clientContext.ExecuteQuery();

                    // Changing display ResultColour of Grade Passed string
                    // This is done in the controller because it has direct access to the GradeBook List of Grade objects.
                    foreach (Grade grade in vm.GradeBook)
                    {
                        grade.ResultColor = grade.Passed.ToLower() == "pass" ? "color:Green" : "color:Red";
                    }
                }

            }
            catch (Exception)
            {

            }
            return vm;
        }

        //public TokenModel GetMyLogin(TokenModel vm)
        //{           
        //    TokenModel result = new TokenModel();
        //    try
        //    {
        //        ProjectTokenHelper.CheckValidAccessToken(vm.spHostURL, vm.accessToken);

        //        using (ClientContext clientContext = TokenHelper.GetClientContextWithAccessToken(vm.spHostURL, vm.accessToken))
        //        {
        //            User spUser = clientContext.Web.CurrentUser;

        //            clientContext.Load(spUser, user => user.Title);

        //            clientContext.ExecuteQuery();

        //            result.Name = spUser.Title;

        //            // The SharePoint web site contains a list called "Student Gradebook" - grab and store it in a List variable.
        //            // Linking to the list, like finding a table in a database
        //            List studentGradebook = clientContext.Web.Lists.GetByTitle("Student Gradebook");

        //            // CAML = Collaborative Application Markup Language
        //            // Query allows you to query for specific items from a list, using the ViewXml property.
        //            // Using CamlQuery.CreateAllItemsQuery() creates a query that retrieves all list items.
        //            CamlQuery query = CamlQuery.CreateAllItemsQuery();

        //            // This line grabs items from the studentGradebook List object based on the specified query, and
        //            //      stores them all in a ListItemCollection object.
        //            ListItemCollection studentGradebookCollection = studentGradebook.GetItems(query);

        //            // This line ties the ListItemCollection to the ClientContext object, providing a line of communication
        //            //      between the program and the SharePoint server. Without this line, the items queried for will
        //            //      not be retrieved from SharePoint.
        //            clientContext.Load(studentGradebookCollection);

        //            // Call ExecuteQuery() to perform the query, loading the specified items from SharePoint.
        //            clientContext.ExecuteQuery();

        //            GradesViewModel gradesViewModel = new GradesViewModel();

        //            foreach (ListItem grade in studentGradebookCollection)
        //            {
        //                string courseTitle = (string)grade["Title"];
        //                string passed = (string)grade["Result"];
        //                gradesViewModel.GradeBook.Add(new Grade(courseTitle, passed));
        //            }

        //            // Call ExecuteQuery to commit changes made to the list.
        //            clientContext.ExecuteQuery();

        //            // Changing display ResultColour of Grade Passed string
        //            // This is done in the controller because it has direct access to the GradeBook List of Grade objects.
        //            foreach (Grade grade in gradesViewModel.GradeBook)
        //            {
        //                grade.ResultColor = grade.Passed.ToLower() == "pass" ? "color:Green" : "color:Red";
        //            }
        //        }

        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return result;
        //}
    }
}
