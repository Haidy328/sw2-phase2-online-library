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
    public class LoginController : Controller
    {
        private ModelDBEntities db = new ModelDBEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View(db.UsersTables.ToList());
        }

        // GET: Login/Details/5
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

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
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

        // GET: Login/Edit/5
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

        // POST: Login/Edit/5
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

        // GET: Login/Delete/5
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

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsersTable usersTable = db.UsersTables.Find(id);
            db.UsersTables.Remove(usersTable);
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

        public ActionResult login()
        {
            Session["username"] = null;
            return View();
        }

        [HttpPost]

        public ActionResult login([Bind(Include = "Username,Password,Email")] UsersTable usersTable)
        {
            var rec = db.UsersTables.Where(x => x.Username == usersTable.Username && x.Password == usersTable.Password && x.Email==usersTable.Email).ToList().FirstOrDefault();
            
                if (rec != null && rec.Email=="user")
                {
                    Session["username"] = rec.Username;
                    return RedirectToAction("Index2", "Books");// el mfrood hna el access bta3 el sf7a ally hyro7 liha el user b3d login 

                }

            else if (rec != null && rec.Email == "1")
            {
                Session["username"] = rec.Username;
                return RedirectToAction("Index", "Admin"); 

            }


            else
                {
                    ViewBag.ErrorMessage = "This is invalide user";
                    return View("login");
                }
            
           
        }



    }
}
