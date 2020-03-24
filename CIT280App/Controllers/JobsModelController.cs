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

namespace CIT280App.Controllers
{
    [Authorize]
    public class JobsModelController : Controller
    {
        private XyphosContext db = new XyphosContext();

        // GET: JobsModel
        public ActionResult Index()
        {
            var jobs = db.Jobs.Include(j => j.User);
            return View(jobs.ToList());
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
