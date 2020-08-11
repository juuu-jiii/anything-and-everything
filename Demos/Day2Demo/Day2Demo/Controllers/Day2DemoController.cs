using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Day2Demo.Data_Access_Layer;

// Provides access to classes, as well as everything else declared within the models folder
// Without this, the full path name of Student.cs will need to be specified.
using Day2Demo.Models;
using Day2Demo.ViewModel;

namespace Day2Demo.Controllers
{
    public class Day2DemoController : Controller
    {
        // GET: Day2Demo
        public ActionResult ResultsStatement()
        {
            // Instantiating an object of the View Model
            StudentGradesViewModel studentGradesViewModel = new StudentGradesViewModel();
            
            // Creating a student object - this is the Model that will be used within the View Model
            Student crazyHoe = new Student("Crazy Hoe", 3, "MMSC - Magical Mysterious Science College");

            // Populate the View Model's student field with the instance of Student created above.
            studentGradesViewModel.Student = crazyHoe;

            //// Hard-code the elements of the View Model's gradebook field
            //studentGradesViewModel.GradeBook.Add(new Grade("DADA330/Defense Against the Dark Arts", true));
            //studentGradesViewModel.GradeBook.Add(new Grade("CLHC120/Fine Wines of the World", true));
            //studentGradesViewModel.GradeBook.Add(new Grade("WHEE420/Diabetes Synthesis and Dissolution", false));
            //studentGradesViewModel.GradeBook.Add(new Grade("MATH520/Thesis in Stupidity", false));
            //studentGradesViewModel.GradeBook.Add(new Grade("IGME106/mAgIc mAKeR sTuDIo", true));
            //studentGradesViewModel.GradeBook.Add(new Grade("FREE333/Free Real Estate", true));

            // Instantiate the DAL wherever it is needed - in this case, to read from and print to the screen.
            StudentGradeDAL studentGradeDAL = new StudentGradeDAL();

            // .ToList creates and returns a list from the specified gradebook entry. Assign Gradebook within the 
            //      DAL to this list.
            studentGradesViewModel.GradeBook = studentGradeDAL.GradeBook.ToList();

            // Pass the View Model object into the View upon returning it, so all its class fields can be utilised.
            // As always, not specifying string viewName will cause VS to look for a View whose name matches that
            //      of the method name - in this case ResultsStatement.
            return View(studentGradesViewModel);
        }

        // The [HttpGet] attribute is only used when displaying the webpage.
        // Type in browser to get the webapge, from the client side
        // There are two other [Http_] attributes: HttpPut - update HttpDelete - delete
        [HttpGet]
        public ActionResult AddCourse()
        {
            return View();
        }

        // The [HttpPost] attribute is used to handle interactions with the webpage eg button clicks.
        [HttpPost]
        // Recall that the @Html.TextBoxFor returns the data collected, stored in a field of the view model class type
        // Also recall that interactions with the View are handled by the HttpPost attribute - passing in a view model
        //      as the parameter allows the action method to make use of the data stored in the vm that is returned. 
        // However, the compiler calls the default constructor when creating an instance of the vm object to be passed in,
        //      so ensure that whichever fields are required within the vm have a default constructor.
        // Post data to the server side, to be processed by the database.
        public ActionResult AddCourse(StudentGradesViewModel vm)
        {
            StudentGradeDAL studentGradeDAL = new StudentGradeDAL();

            // Poll for clicks from btnCreate
            // Request tracks any requests made from the current View
            // .Form[string name] specifies the NAME of the form control to track requests from.
            // If the button matching the specified name is clicked, its value gets returned. If no click 
            //      event occurs, btnAdd will return null. Thus, when it is idle, the else block runs.
            if (Request.Form["btnCreate"] != null)
            {
                // The following is incorrect. Why create a new Grade with the same values as the current Grade???
                //studentGradeDAL.GradeBook.Add(new Grade(vm.Grade.CourseTitle, vm.Grade.Passed));

                // This checks if any errors exist on the server side (basically any attributes like [RegularExpression] that
                //      are not satisfied)
                if (ModelState.IsValid)
                {
                    // The Grade object is created and populated based on the entries in the form - just add to Gradebook.
                    studentGradeDAL.GradeBook.Add(vm.Grade);

                    // Without this, any changes will not be reflected in the database.
                    studentGradeDAL.SaveChanges();

                    // RedirectToAction redirects away from the current webpage. Parameters are the name of the
                    //      action method to redirect to, followed by the name of the Controller the action method 
                    //      belongs to, both represented as strings.
                    return RedirectToAction("ResultsStatement", "Day2Demo");
                }

                // If the above check fails, the same View is returned. B  nnecause the error messages are specified in Grade.cs, as
                //      well as instructed to show in AddCourse.cshtml, they will display on the screen this time round.
                return View();
            }
            else if (Request.Form["btnCancel"] != null)
            {
                return RedirectToAction("ResultsStatement", "Day2Demo");
            }
            else
            {
                // Return the same view if no buttons are pressed.
                return View();
            }
        }
    }
}