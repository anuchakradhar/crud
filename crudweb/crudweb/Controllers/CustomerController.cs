using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using crudweb.Models;

namespace crudweb.Controllers
{
    public class CustomerController : Controller
    {
        crudwebEntities db = new crudwebEntities();

        // GET: Customer
        public ActionResult Index()
        {
            var list = db.Customers.ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer model)
        {
            try
            {
                db.Customers.Add(model);
                db.SaveChanges();
            }
            catch(Exception e)
            {

            }

            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Edit(Int32 id)
        {
            var data = db.Customers.Where(x => x.customer_id == id).FirstOrDefault();
            if (data != null)
            {
                TempData["CustomerID"] = id;
                TempData.Keep();
                return View(data);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Customer model)
        {
            Int32 CusId = (int)TempData["CustomerId"];
            var Data = db.Customers.Where(x => x.customer_id == CusId).FirstOrDefault();
            if (Data != null)
            {
                Data.customer_name = model.customer_name;
                Data.phone_no = model.phone_no;
                Data.address = model.address;
                //Data.Address = model.Address;
                //Data.Phone = model.Phone;
                db.Entry(Data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Int32 id)
        {
            var data = db.Customers.Find(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult DeleteConfirm(Customer model)
        {
            var customerbyid = db.Customers.Where(x => x.customer_id == model.customer_id).FirstOrDefault();
            if (customerbyid != null)
            {
                var list = db.Orders.Where(x => x.customer_id == model.customer_id).ToList();
                db.Orders.RemoveRange(list);
                db.Customers.Remove(customerbyid);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Order()
        {
            var list = db.Orders.ToList();
            return View(list);
        }

        public ActionResult AddOrder()
        {
            ViewBag.CustomerID = new SelectList(db.Customers.ToList(), "customer_id", "customer_name");
            return View();
        }


        [HttpPost]
        public ActionResult AddOrder(Order model)
        {
            try
            {
                db.Orders.Add(model);
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }

            return RedirectToAction("Order", "Customer");
        }
    }
}