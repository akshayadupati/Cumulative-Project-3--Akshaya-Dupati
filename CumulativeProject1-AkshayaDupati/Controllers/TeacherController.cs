using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CumulativeProject1_AkshayaDupati.Models;

namespace CumulativeProject1_AkshayaDupati.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET : Teacher/List/{SearchKey}
        [Route("/Teacher/List/{SearchKey}/{order}")]

        /// <summary>
        /// Controller that fetches list of teachers from TeacherDataController
        /// </summary>
        /// <param name="SearchKey"></param>
        /// <returns>Returns the list of teachers to the view.</returns>
        
        public ActionResult List(string SearchKey)
        {
            TeacherDataController controller = new TeacherDataController();

            IEnumerable<Teacher> Teachers= controller.ListTeachers(SearchKey);

            return View(Teachers);
        }

        // GET : /Teacher/Show/{id}

        /// <summary>
        /// Controller that fetches particular data typed in the URL from the 
        /// TeacherDataController
        /// </summary>
        /// <returns>Returns the particular teacher data to the view.</returns>
        public ActionResult Show(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher); 
        }

        //GET: /Teacher/New
        public ActionResult New()
        {

            return View();
        }


        //POST: /Teacher/Create
        [HttpPost]
        public ActionResult Create(string teacherfname, string teacherlname, decimal teachersalary)
        {

            Teacher NewTeacher = new Teacher();

            NewTeacher.TeacherFName = teacherfname;
            NewTeacher.TeacherLName = teacherlname;
            NewTeacher.TeacherSalary = teachersalary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);


            //redirects to the list view
            return RedirectToAction("List");
        }

        //GET: /Teacher/DeleteTeacher/{id}
        public ActionResult DeleteTeacher(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        //POST: /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);

            return RedirectToAction("List");
        }

        /// <summary>
        /// This method retrieves the data and show the details of selected author in the webpage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET : /Teacher/Edit/{id}
        public ActionResult Edit(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            return View(SelectedTeacher);
        }

        /// <summary>
        /// This method updates the data written in the webpage to the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //POST: /Teacher/Update/{id}
        [HttpPost]

        public ActionResult Update(int id, string teacherfname, string teacherlname, decimal teachersalary)
        {

            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFName = teacherfname;
            TeacherInfo.TeacherLName = teacherlname;
            TeacherInfo.TeacherSalary = teachersalary;
            TeacherInfo.TeacherId = id;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(TeacherInfo);
            //return to the updated teacher page
            return RedirectToAction("Show/" + id);
        }
    }
}