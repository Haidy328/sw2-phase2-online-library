using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers
{
    public class catagriesController : Controller
    {
        private ProjectDBEntities2 db = new ProjectDBEntities2();

        // GET: catagries
        public ActionResult Index()
        {
            return View(db.catagries.ToList());
        }

        // GET: catagries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catagry catagry = db.catagries.Find(id);
            if (catagry == null)
            {
                return HttpNotFound();
            }
            return View(catagry);
        }

        // GET: catagries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: catagries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "catagry_name")] catagry catagry)
        {
            if (ModelState.IsValid)
            {
                db.catagries.Add(catagry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(catagry);
        }

        // GET: catagries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catagry catagry = db.catagries.Find(id);
            if (catagry == null)
            {
                return HttpNotFound();
            }
            return View(catagry);
        }

        // POST: catagries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "catId,catagry_name")] catagry catagry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catagry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(catagry);
        }

        // GET: catagries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catagry catagry = db.catagries.Find(id);
            if (catagry == null)
            {
                return HttpNotFound();
            }
            return View(catagry);
        }

        // POST: catagries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            catagry catagry = db.catagries.Find(id);
            db.catagries.Remove(catagry);
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
