using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusManagement.Models;

namespace BusManagement.Controllers
{
    public class RoutesController : Controller
    {
        private busmanagementEntities db = new busmanagementEntities();

        // GET: Routes
        public ActionResult Index()
        {
            var routes = db.Routes.Include(r => r.Bus).Include(r => r.Departure).Include(r => r.Destination);
            return View(routes.ToList());
        }

        // GET: Routes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // GET: Routes/Create
        public ActionResult Create()
        {
            ViewBag.BusID = new SelectList(db.Buses, "ID", "BusName");
            ViewBag.DepartmentID = new SelectList(db.Departures, "ID", "DepartureName");
            ViewBag.DestinationtID = new SelectList(db.Destinations, "ID", "Name");
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Price,StarTime,DepartmentID,DestinationtID,EstimateDistance,BusID,EstimateArrivalTime,EstimatePassenger,Status")] Route route)
        {
            if (ModelState.IsValid)
            {
                db.Routes.Add(route);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusID = new SelectList(db.Buses, "ID", "BusName", route.BusID);
            ViewBag.DepartmentID = new SelectList(db.Departures, "ID", "DepartureName", route.DepartmentID);
            ViewBag.DestinationtID = new SelectList(db.Destinations, "ID", "Name", route.DestinationtID);
            return View(route);
        }

        // GET: Routes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusID = new SelectList(db.Buses, "ID", "BusName", route.BusID);
            ViewBag.DepartmentID = new SelectList(db.Departures, "ID", "DepartureName", route.DepartmentID);
            ViewBag.DestinationtID = new SelectList(db.Destinations, "ID", "Name", route.DestinationtID);
            return View(route);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Price,StarTime,DepartmentID,DestinationtID,EstimateDistance,BusID,EstimateArrivalTime,EstimatePassenger,Status")] Route route)
        {
            if (ModelState.IsValid)
            {
                db.Entry(route).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusID = new SelectList(db.Buses, "ID", "BusName", route.BusID);
            ViewBag.DepartmentID = new SelectList(db.Departures, "ID", "DepartureName", route.DepartmentID);
            ViewBag.DestinationtID = new SelectList(db.Destinations, "ID", "Name", route.DestinationtID);
            return View(route);
        }

        // GET: Routes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Route route = db.Routes.Find(id);
            db.Routes.Remove(route);
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
