﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers
{
    public class BooksController : Controller
    {
        private ProjectDBEntities2 db = new ProjectDBEntities2();

        // GET: Admin
        public ActionResult Index()
        {
            if (Session["username"] != null)
                return View(db.Books.ToList());
            else
                return RedirectToAction("login", "Login");
        }

        public ActionResult Index2()
        {
            if (Session["username"] != null)
                return View(db.Books.ToList());
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
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpGet]
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
        public ActionResult Create([Bind(Include = "BId,Bname,Bauthor,Bprice,no_of_books,Catagry_Id,BUrl")] Book book)
        {
            if (ModelState.IsValid)
            {

                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BId,Bname,Bauthor,Bprice,no_of_books,catagry_Id,BUrl")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }
        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search(string key)
        { if (string.IsNullOrEmpty(key)) { return RedirectToAction("Index2"); }
            else
            {
                var listOfBooks = db.Books.Where(x => x.Bname == key).ToList();
                return View(listOfBooks);
            }
        }

        public ActionResult Search_ByAdmin(string key)
        {
            if (string.IsNullOrEmpty(key)) { return RedirectToAction("Index"); }
            else
            {
                var listOfBooks = db.Books.Where(x => x.Bname == key).ToList();
                return View(listOfBooks);
            }
        }
        public ActionResult Filter(string key)
        {
            ProjectDBEntities2 online = new ProjectDBEntities2();
            var categorylist = online.catagries.ToList();
            SelectList list = new SelectList(categorylist, "catId", "catagry_name");
            ViewBag.CategoryList = list;
            var CategoryID = (from p in db.catagries
                              where p.catagry_name == key
                              select p.catId).FirstOrDefault();
            var listOfBooks = db.Books.Where(x => x.catagry_Id == CategoryID).ToList();
            return View(listOfBooks);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Borrow(int id)
        {
            Book book = db.Books.Find(id);
            int count_of_books = book.no_of_books.Value;
			if(count_of_books > 0)
			{
				book.no_of_books = count_of_books - 1;
				db.Entry(book).State = EntityState.Modified;
			}
            db.SaveChanges();
            return RedirectToAction("Index2");
        }
		public ActionResult MoreInfo(int id)
		{
			Book P = new Book();

			using (db)
			{
				P = db.Books.Include(x => x.catagry).SingleOrDefault(p => p.BId == id);
			}
			return View(P);
		}


	}
}
