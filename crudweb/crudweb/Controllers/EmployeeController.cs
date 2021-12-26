
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using crudweb.Models;

namespace crudweb.Controllers
{
    public class EmployeeController : Controller
    {
        crudwebEntities db = new crudwebEntities();
        // GET: Employee
        public ActionResult Index()
        {
            var list = db.Employees.ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee model)
        {
            try
            {
                db.Employees.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Index");
        }



        public ActionResult Edit(Int32 id)
        {
            var Employeedata = db.Employees.Where(x => x.id == id).FirstOrDefault();
            if (Employeedata != null)
            {
                TempData["EmployeeID"] = id;
                TempData.Keep();
                return View(Employeedata);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Employee model)
        {
            Int32 EmployeeId = (int)TempData["EmployeeId"];
            var EmployeeData = db.Employees.Where(x => x.id == EmployeeId).FirstOrDefault();
            if (EmployeeData != null)
            {
                EmployeeData.Name = model.Name;
                EmployeeData.Department = model.Department;
                EmployeeData.Salary = model.Salary;
                EmployeeData.Address = model.Address;
                EmployeeData.Phone = model.Phone;
                EmployeeData.Company.Company_name = model.Company.Company_name;
                EmployeeData.Company.Contact = model.Company.Contact;
                EmployeeData.Company.Address = model.Company.Address;
                db.Entry(EmployeeData).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Int32 id)
        {
            var data = db.Employees.Find(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult DeleteConfirm(Employee employee)
        {
            var employeebyid = db.Employees.Where(x => x.id == employee.id).FirstOrDefault();
            if (employeebyid != null)
            {
                //var data = db.Companies.Find(id)
                db.Companies.Remove(employeebyid.Company);
                db.Employees.Remove(employeebyid);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Employee");
        }
    }
}