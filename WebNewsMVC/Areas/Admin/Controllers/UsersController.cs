using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebNewsMVC.Models;
using WebNewsMVC.Common;

namespace WebNewsMVC.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private DbModel db = new DbModel();

        // GET: Admin/Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_ID,User_Name,User_Phone,User_Email,User_UserName,User_Password,User_Role")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.User_Password = (MaHoaMD5.GetMD5(users.User_Password));
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }
        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_ID,User_Name,User_Phone,User_Email,User_UserName,User_Password,User_Role")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.User_Password = (MaHoaMD5.GetMD5(users.User_Password));
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET Admin/Users/Login
        public ActionResult Login()
        {
            return View();
        }
        // POST: Admin/Users/Login
        [HttpPost]
        public ActionResult Login(FormCollection usersForm)
        {
            string usernameForm = usersForm["User_UserName"].ToString();
            string passwordForm = usersForm["User_Password"].ToString();
            var matkhauMD5 = MaHoaMD5.GetMD5(passwordForm);
            var nguoiDung = db.Users.SingleOrDefault(x =>
            x.User_UserName.Equals(usernameForm) && x.User_Password.Equals(matkhauMD5));
            if (nguoiDung != null)
            {
                Session["user"] = nguoiDung;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Fail = "Tài khoản hoặc mật khẩu không chính xác.";
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Admin/Users/Login");
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
