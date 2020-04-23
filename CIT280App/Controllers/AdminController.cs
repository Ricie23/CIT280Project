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
    public class AdminController : Controller
    
    {
        private XyphosContext db = new XyphosContext();

        // GET: Admin
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.RoleSortParm = String.IsNullOrEmpty(sortOrder) ? "role_desc" : "";
            ViewBag.FirstNameSortParm = sortOrder == "firstname" ? "firstname_desc" : "firstname";
            ViewBag.LastNameSortParm = sortOrder == "lastname" ? "lastname_desc" : "lastname";
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

            var user = from u in db.Admins
                       select u;
            if (!String.IsNullOrEmpty(searchString))
            {
                user = user.Where(u => u.LastName.Contains(searchString)
                                        || u.City.Contains(searchString)
                                        || u.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "city_desc":
                    user = user.OrderByDescending(u => u.City);
                    break;
                case "city":
                    user = user.OrderBy(u => u.City);
                    break;
                case "lastname_desc":
                    user = user.OrderByDescending(u => u.LastName);
                    break;
                case "lastname":
                    user = user.OrderBy(u => u.LastName);
                    break;
                case "firstname":
                    user = user.OrderByDescending(u => u.FirstName);
                    break;
                case "firstname_desc":
                    user = user.OrderByDescending(u => u.FirstName);
                    break;
                case "role_desc":
                    user = user.OrderByDescending(u => u.Role);
                    break;
                default:
                    user = user.OrderBy(u => u.Role);
                    break;
            }
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(user.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult AdminDashboard()
        {
            return View();
        }

        public ActionResult CreateUserDashboard()
        {
            return View();
        }
        public ActionResult AllUsers()
        {
            var employers = db.Employers.ToList();
            var students = db.Students.ToList();
            var admins = db.Admins.ToList();
            var users = new List<UserModel>(employers).Concat(students).Concat(admins).ToList();
            return View(users);
        }

        public ViewResult AllJobs(string sortOrder, string currentFilter, string searchString, int? page)
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
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(jobs.ToPagedList(pageNumber, pageSize));
        }

        // GET: JobsModel/Details/5
        public ActionResult JobDetails(int? id)
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

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = db.Admins.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Role,FirstName,LastName,City,State,Description,Email,Phone,ProfilePic,Reviews")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                userModel.Role = UserRole.Admin;
                db.Admins.Add(userModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userModel);
        }

        // Job Create

        public ActionResult JobCreate()
        {
            ViewBag.UserID = new SelectList(db.Employers, "ID", "FirstName");
            return View();
        }

        // POST: JobsModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JobCreate([Bind(Include = "ID,UserID,Name,Description,City,State,RequiredSkills,Photo,Pay,IsComplete")] JobsModel jobsModel)
        {
            if (ModelState.IsValid)
            {
                db.Jobs.Add(jobsModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Employers, "ID", "FirstName", jobsModel.UserID);
            return View(jobsModel);
        }

        // GET: JobsModel/Edit/5
        public ActionResult JobEdit(int? id)
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
        public ActionResult JobEdit([Bind(Include = "ID,UserID,Name,Description,City,State,RequiredSkills,Photo,Pay,IsComplete")] JobsModel jobsModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobsModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllJobs");
            }
            ViewBag.UserID = new SelectList(db.Admins, "ID", "FirstName", jobsModel.UserID);
            return View(jobsModel);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = db.Admins.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Role,FirstName,LastName,City,State,Description,Email,Phone,ProfilePic,Reviews")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userModel);
        }


        // GET: JobsModel/Delete/5
        public ActionResult JobDelete(int? id)
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
        [HttpPost, ActionName("JobDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult JobDeleteConfirmed(int id)
        {
            JobsModel jobsModel = db.Jobs.Find(id);
            db.Jobs.Remove(jobsModel);
            db.SaveChanges();
            return RedirectToAction("AllJobs");
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = db.Admins.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserModel userModel = db.Admins.Find(id);
            db.Admins.Remove(userModel);
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
