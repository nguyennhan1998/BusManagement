﻿using System;
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
    public class DeparturesController : Controller
    {
        private busmanagementEntities db = new busmanagementEntities();

        // GET: Departures
        public ActionResult Index()
        {
            return View(db.Departures.ToList());
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
