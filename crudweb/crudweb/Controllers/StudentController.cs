using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using crudweb.Models;

namespace crudweb.Controllers
{
    public class StudentController : Controller
    {
        crudwebEntities db = new crudwebEntities();

        
        // GET: Student
        public ActionResult Index()
        {
            var list = db.Students.ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student model)
        {
            try
            {
                db.Students.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Index");
        }

        
       
        public ActionResult Edit(Int32 id)
        {
            var Studentdata = db.Students.Where(x => x.id == id).FirstOrDefault();
            if (Studentdata != null)
            {
                TempData["StudentID"] = id;
                TempData.Keep();
                return View(Studentdata);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Student model)
        {
            Int32 StudentId = (int)TempData["StudentId"];
            var StudentData = db.Students.Where(x => x.id == StudentId).FirstOrDefault();
            if (StudentData != null)
            {
                StudentData.Fname = model.Fname;
                StudentData.Lname = model.Lname;
                StudentData.Address = model.Address;
                StudentData.Phone = model.Phone;
                StudentData.Major = model.Major;
                db.Entry(StudentData).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Int32 id)
        {
            var data = db.Students.Find(id);
            return View(data);
        }
        
        [HttpPost]
        public ActionResult DeleteConfirm(Student student)
        {
                var studentbyid = db.Students.Where(x => x.id == student.id).FirstOrDefault();
                if (studentbyid != null)
                {
                    db.Students.Remove(studentbyid);
                    db.SaveChanges();
                }
            return RedirectToAction("Index");
        }

    }
}


