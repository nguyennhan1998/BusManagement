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
    public class StopStationsController : Controller
    {
        private busmanagementEntities1 db = new busmanagementEntities1();

        // GET: StopStations
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
            var stopstaion = from s in db.StopStations select s;
            if (!String.IsNullOrEmpty(search)) // nếu search string có thì in ra hoặc không thì không in ra
            {
                stopstaion = stopstaion.Where(s => s.StopLocation.Contains(search)); // contains là để check xem lastname hoặc firstName có chứa search string ở trên 

            }
            switch (sortOrder)
            {
                case "name desc":
                    stopstaion = stopstaion.OrderByDescending(s => s.StopLocation);
                    break;

                default:
                    stopstaion = stopstaion.OrderBy(s => s.StopLocation);
                    break;
            }
            int pageSize = 3; //  khai báo số lượng record trên 1 page
            int pageNumber = (page ?? 1); // page number là page đang chọn nếu không chọn sẽ = 1
            return View(stopstaion.ToPagedList(pageNumber, pageSize));
            /*      var students = db.Students.Include(s => s.Class);*/
            /*   return View(students.ToList());*/
        }


        // GET: StopStations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StopStation stopStation = db.StopStations.Find(id);
            if (stopStation == null)
            {
                return HttpNotFound();
            }
            return View(stopStation);
        }

        // GET: StopStations/Create
        public ActionResult Create()
        {
            ViewBag.RouteID = new SelectList(db.Routes, "ID", "Price");
            return View();
        }

        // POST: StopStations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StopLocation,RouteID,TimeStop")] StopStation stopStation)
        {
            if (ModelState.IsValid)
            {
                db.StopStations.Add(stopStation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RouteID = new SelectList(db.Routes, "ID", "Price", stopStation.RouteID);
            return View(stopStation);
        }

        // GET: StopStations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StopStation stopStation = db.StopStations.Find(id);
            if (stopStation == null)
            {
                return HttpNotFound();
            }
            ViewBag.RouteID = new SelectList(db.Routes, "ID", "Price", stopStation.RouteID);
            return View(stopStation);
        }

        // POST: StopStations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StopLocation,RouteID,TimeStop")] StopStation stopStation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stopStation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RouteID = new SelectList(db.Routes, "ID", "Price", stopStation.RouteID);
            return View(stopStation);
        }

        // GET: StopStations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StopStation stopStation = db.StopStations.Find(id);
            if (stopStation == null)
            {
                return HttpNotFound();
            }
            return View(stopStation);
        }

        // POST: StopStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StopStation stopStation = db.StopStations.Find(id);
            db.StopStations.Remove(stopStation);
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
