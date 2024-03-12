using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebNewsMVC.Models;
using PagedList;

namespace WebNewsMVC.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private DbModel db = new DbModel();

        // GET: Admin/News
        public ActionResult Index(int? page, string searchString)
        {

            // Cú pháp lấy tất cả danh mục từ bảng News
            var news = from n in db.News select n;
            if (!String.IsNullOrEmpty(searchString))
            {
                news = news.Where(s => s.New_Title.Contains(searchString));
            }
            // Sắp xếp theo Category_ID Tăng dần
            news = news.OrderBy(x => x.Category_ID);
            // Phân trang
            if (page == null) page = 1;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //trả về kết quả sử dụng phân trang
            return View(news.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            ViewBag.Category_ID = new SelectList(db.Categorys, "Category_ID", "Category_Name");
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "New_ID,Category_ID,New_Title,New_Summary,New_img,New_Content,New_Date,New_Status,New_User_Name")] News news)
        {
            if (ModelState.IsValid)
            {
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_ID = new SelectList(db.Categorys, "Category_ID", "Category_Name", news.Category_ID);
            return View(news);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_ID = new SelectList(db.Categorys, "Category_ID", "Category_Name", news.Category_ID);
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "New_ID,Category_ID,New_Title,New_Summary,New_img,New_Content,New_Date,New_Status,New_User_Name")] News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_ID = new SelectList(db.Categorys, "Category_ID", "Category_Name", news.Category_ID);
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
