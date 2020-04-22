using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CIT280App.DAL;
using CIT280App.Models;
using PagedList;

namespace CIT280App.Controllers
{
   // [Authorize]
    public class EmployerModelController : Controller
    {
        private XyphosContext db = new XyphosContext();

        // GET: EmployerModels
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.EmpNameSortParm = String.IsNullOrEmpty(sortOrder) ? "empname_desc" : "";
            ViewBag.EmpTypeSortParm = sortOrder == "emptype" ? "emptype_desc" : "emptype";
            ViewBag.CitySortParm = sortOrder == "city" ? "city_desc" : "city";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.currentFilter = searchString;

            var employers = from e in db.Employers
                            select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                employers = employers.Where(e => e.BuisnessName.Contains(searchString)
                                            || e.BuisnessType.Contains(searchString)
                                            || e.City.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "city_desc":
                    employers = employers.OrderByDescending(e => e.City);
                    break;
                case "city":
                    employers = employers.OrderBy(e => e.City);
                    break;
                case "empname_desc":
                    employers = employers.OrderByDescending(e => e.BuisnessName);
                    break;
                case "emptype":
                    employers = employers.OrderBy(e => e.BuisnessType);
                    break;
                case "emptype_desc":
                    employers = employers.OrderByDescending(e => e.BuisnessType);
                    break;
                default:
                    employers = employers.OrderBy(e => e.BuisnessName);
                    break; ;
            }
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(employers.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult EmployersDashboard() 
        {
            return View();
        }

        // GET: EmployerModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //CHANGE BACK BEFORE MASTER
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                id = 3;
            }
            EmployerModel employerModel = db.Employers.Find(id);
            if (employerModel == null)
            {
                return HttpNotFound();
            }
            return View(employerModel);
        }

        public ActionResult Profile(int? id)
        {
            if (id == null)
            {
                //CHANGE BACK BEFORE MASTER
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                id = 4;
            }
            EmployerModel employerModel = db.Employers.Find(id);
            if (employerModel == null)
            {
                return HttpNotFound();
            }
            return View(employerModel);
        }

        // GET: EmployerModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployerModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Role,FirstName,LastName,City,State,Description,Email,Phone,ProfilePic,Reviews,BuisnessName,BuisnessType")] EmployerModel employerModel)
        {
            if (ModelState.IsValid)
            {
                employerModel.Role = UserRole.Employer;
                db.Employers.Add(employerModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employerModel);
        }

        // GET: EmployerModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployerModel employerModel = db.Employers.Find(id);
            if (employerModel == null)
            {
                return HttpNotFound();
            }
            return View(employerModel);
        }

        // POST: EmployerModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Role,FirstName,LastName,City,State,Description,Email,Phone,ProfilePic,Reviews,BuisnessName,BuisnessType")] EmployerModel employerModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employerModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employerModel);
        }
  
        // GET: EmployerModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployerModel employerModel = db.Employers.Find(id);
            if (employerModel == null)
            {
                return HttpNotFound();
            }
            return View(employerModel);
        }

        // POST: EmployerModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployerModel employerModel = db.Employers.Find(id);
            db.Employers.Remove(employerModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
