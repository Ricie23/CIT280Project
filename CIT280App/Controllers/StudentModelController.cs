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
    public class StudentModelController : Controller
    {
        private XyphosContext db = new XyphosContext();

        // GET: StudentModel
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";
            ViewBag.FirstNameSortParm = sortOrder == "First Name" ? "firstname_desc" : "firstname";
            ViewBag.CitySortParm = sortOrder == "City" ? "city_desc" : "city";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.currentFilter = searchString;

            var students = from s in db.Students
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                            || s.FirstName.Contains(searchString)
                                            || s.City.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "city_desc":
                    students = students.OrderByDescending(s => s.City);
                    break;
                case "city":
                    students = students.OrderBy(s => s.City);
                    break;
                case "lastname_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "firstname":
                    students = students.OrderBy(s => s.FirstName);
                    break;
                case "firstname_desc":
                    students = students.OrderByDescending(s => s.FirstName);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break; ;
            }
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult StudentsDashboard()
        {
            return View();
        }

        // GET: StudentModel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //CHANGE BACK BEFORE PUSH TO MASTER
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                id = 1;
            }
            StudentModel studentModel = db.Students.Find(id);
            if (studentModel == null)
            {
                return HttpNotFound();
            }
            return View(studentModel);
        }

        public ActionResult Profile(int? id)
        {
            if (id == null)
            {
                //CHANGE BACK BEFORE PUSH TO MASTER
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                id = 1;
            }
            StudentModel studentModel = db.Students.Find(id);
            if (studentModel == null)
            {
                return HttpNotFound();
            }
            return View(studentModel);
        }

        // GET: StudentModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Role,FirstName,LastName,City,State,Description,Email,Phone,ProfilePic,Reviews,Major,School,YearInSchool")] StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                studentModel.Role = UserRole.Student;
                db.Students.Add(studentModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentModel);
        }

        // GET: StudentModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentModel studentModel = db.Students.Find(id);
            if (studentModel == null)
            {
                return HttpNotFound();
            }
            return View(studentModel);
        }

        // POST: StudentModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Role,FirstName,LastName,City,State,Description,Email,Phone,ProfilePic,Reviews,Major,School,YearInSchool")] StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentModel);
        }

        // GET: StudentModel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentModel studentModel = db.Students.Find(id);
            if (studentModel == null)
            {
                return HttpNotFound();
            }
            return View(studentModel);
        }

        // POST: StudentModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentModel studentModel = db.Students.Find(id);
            db.Admins.Remove(studentModel);
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
