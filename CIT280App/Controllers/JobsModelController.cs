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
    [Authorize]
    public class JobsModelController : Controller
    {
        private XyphosContext db = new XyphosContext();

        // GET: JobsModel
        //public ActionResult Index()
        //{
        //    var jobs = db.Jobs.Include(j => j.User);
        //    return View(jobs.ToList());
        //}
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.JobNameSortParm = String.IsNullOrEmpty(sortOrder) ? "jobname_desc" : "";
            ViewBag.EmpNameSortParm = sortOrder == "empname" ? "empname_desc" : "empname";
            ViewBag.CitySortParm = sortOrder == "city" ? "city_desc" : "city";
            ViewBag.PaySortParm = sortOrder == "pay" ? "pay_desc" : "pay";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.currentFilter = searchString;

            var jobs = from j in db.Jobs
                            select j;
            if (!String.IsNullOrEmpty(searchString))
            {
                jobs = jobs.Where(j => j.Name.Contains(searchString)
                                        || j.City.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "city_desc":
                    jobs = jobs.OrderByDescending(j => j.City);
                    break;
                case "city":
                    jobs = jobs.OrderBy(j => j.City);
                    break;
                case "jobname_desc":
                    jobs = jobs.OrderByDescending(j => j.Name);
                    break;
                case "empname":
                    jobs = jobs.OrderBy(j => j.Employer);
                    break;
                case "empname_desc":
                    jobs = jobs.OrderByDescending(j => j.Employer);
                    break;
                case "pay":
                    jobs = jobs.OrderBy(j => j.Pay);
                    break;
                case "pay_desc":
                    jobs = jobs.OrderByDescending(j => j.Pay);
                    break;
                default:
                    jobs = jobs.OrderBy(j => j.Name);
                    break; 
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(jobs.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Map()
        {
            return View();
        }
        // GET: JobsModel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobsModel jobsModel = db.Jobs.Find(id);
            if (jobsModel == null)
            {
                return HttpNotFound();
            }
            return View(jobsModel);
        }

        // GET: JobsModel/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Admins, "ID", "FirstName");
            return View();
        }

        // POST: JobsModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,Name,Description,City,State,RequiredSkills,Photo,Pay,IsComplete")] JobsModel jobsModel)
        {
            if (ModelState.IsValid)
            {
                db.Jobs.Add(jobsModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Admins, "ID", "FirstName", jobsModel.UserID);
            return View(jobsModel);
        }

        // GET: JobsModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobsModel jobsModel = db.Jobs.Find(id);
            if (jobsModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Admins, "ID", "FirstName", jobsModel.UserID);
            return View(jobsModel);
        }

        // POST: JobsModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,Name,Description,City,State,RequiredSkills,Photo,Pay,IsComplete")] JobsModel jobsModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobsModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Admins, "ID", "FirstName", jobsModel.UserID);
            return View(jobsModel);
        }

        // GET: JobsModel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobsModel jobsModel = db.Jobs.Find(id);
            if (jobsModel == null)
            {
                return HttpNotFound();
            }
            return View(jobsModel);
        }

        // POST: JobsModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobsModel jobsModel = db.Jobs.Find(id);
            db.Jobs.Remove(jobsModel);
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
