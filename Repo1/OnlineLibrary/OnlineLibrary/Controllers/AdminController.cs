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
    public class AdminController : Controller
    {
        private ModelDBEntities db = new ModelDBEntities();

        // GET: Admin
        public ActionResult Index()
        {
            if (Session["username"] != null)
                return View(db.UsersTables.ToList());
            else
                return RedirectToAction("login", "Login");
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersTable usersTable = db.UsersTables.Find(id);
            if (usersTable == null)
            {
                return HttpNotFound();
            }
            return View(usersTable);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            if (Session["username"] != null)
                return View();
            else
                return RedirectToAction("login", "Login");
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,Email,PhoneNo,Age")] UsersTable usersTable)
        {
            if (ModelState.IsValid)
            {
                db.UsersTables.Add(usersTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usersTable);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersTable usersTable = db.UsersTables.Find(id);
            if (usersTable == null)
            {
                return HttpNotFound();
            }
            return View(usersTable);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,Email,PhoneNo,Age")] UsersTable usersTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usersTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usersTable);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersTable usersTable = db.UsersTables.Find(id);
            if (usersTable == null)
            {
                return HttpNotFound();
            }
            return View(usersTable);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsersTable usersTable = db.UsersTables.Find(id);
            db.UsersTables.Remove(usersTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // the search method
        public ActionResult SearchUser(string key)
        {
            var listOfUsers = db.UsersTables.Where(x => x.Username == key).ToList();
            
                if (Session["username"] != null)
                {

                    return View(listOfUsers);
                }

                else
                {
                    return RedirectToAction("login", "Login");
                }
            

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



