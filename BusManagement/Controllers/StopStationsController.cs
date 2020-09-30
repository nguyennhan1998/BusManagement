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
    public class StopStationsController : Controller
    {
        private busmanagementEntities db = new busmanagementEntities();

        // GET: StopStations
        public ActionResult Index()
        {
            var stopStations = db.StopStations.Include(s => s.Route);
            return View(stopStations.ToList());
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
