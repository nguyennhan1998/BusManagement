using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusManagement.Models;
using PagedList;

namespace BusManagement.Controllers
{
    public class DeparturesController : Controller
    {
        private busmanagementEntities1 db = new busmanagementEntities1();

        // GET: Departures
        public ViewResult Index(string sortOrder, string search, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (search != null)
            {
                page = 1; // nếu search có giá trị trả về page = 1
            }
            else
            {
                search = currentFilter;
            }
            ViewBag.CurrentFilter = search;
            var departure = from s in db.Departures select s;
            if (!String.IsNullOrEmpty(search)) // nếu search string có thì in ra hoặc không thì không in ra
            {
                departure = departure.Where(s => s.DepartureName.Contains(search)); // contains là để check xem lastname hoặc firstName có chứa search string ở trên 

            }
            switch (sortOrder)
            {
                case "name desc":
                    departure = departure.OrderByDescending(s => s.DepartureName);
                    break;

                default:
                    departure = departure.OrderBy(s => s.DepartureName);
                    break;
            }
            int pageSize = 3; //  khai báo số lượng record trên 1 page
            int pageNumber = (page ?? 1); // page number là page đang chọn nếu không chọn sẽ = 1
            return View(departure.ToPagedList(pageNumber, pageSize));
            /*      var students = db.Students.Include(s => s.Class);*/
            /*   return View(students.ToList());*/
        }
        // GET: Departures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departure departure = db.Departures.Find(id);
            if (departure == null)
            {
                return HttpNotFound();
            }
            return View(departure);
        }

        // GET: Departures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DepartureName")] Departure departure)
        {
            if (ModelState.IsValid)
            {
                db.Departures.Add(departure);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(departure);
        }

        // GET: Departures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departure departure = db.Departures.Find(id);
            if (departure == null)
            {
                return HttpNotFound();
            }
            return View(departure);
        }

        // POST: Departures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DepartureName")] Departure departure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departure);
        }

        // GET: Departures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departure departure = db.Departures.Find(id);
            if (departure == null)
            {
                return HttpNotFound();
            }
            return View(departure);
        }

        // POST: Departures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departure departure = db.Departures.Find(id);
            db.Departures.Remove(departure);
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
