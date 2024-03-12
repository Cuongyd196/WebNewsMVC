using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebNewsMVC.Models;
using PagedList;
namespace WebNewsMVC.Areas.Admin.Controllers
{
    public class CategorysController : Controller
    {
        private DbModel db = new DbModel();
        // GET: Admin/Categorys
        public ActionResult Index(int? page, string searchString)
        {
            // Cú pháp lấy tất cả danh mục từ bảng Categorys
            var categoryList = from category in db.Categorys select category;
            if (!String.IsNullOrEmpty(searchString))
            {
                categoryList = categoryList.Where(s => s.Category_Name.Contains(searchString));
            }
            // Sắp xếp theo Category_ID Tăng dần
            categoryList = categoryList.OrderBy(x => x.Category_ID);
            // Phân trang
            if (page == null) page = 1;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //trả về kết quả sử dụng phân trang
            return View(categoryList.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Categorys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorys categorys = db.Categorys.Find(id);
            if (categorys == null)
            {
                return HttpNotFound();
            }
            return View(categorys);
        }

        // GET: Admin/Categorys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categorys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Category_ID,Category_Name,Category_Note,Category_Status")] Categorys categorys)
        {
            if (ModelState.IsValid)
            {
                db.Categorys.Add(categorys);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categorys);
        }

        // GET: Admin/Categorys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorys categorys = db.Categorys.Find(id);
            if (categorys == null)
            {
                return HttpNotFound();
            }
            return View(categorys);
        }

        // POST: Admin/Categorys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Category_ID,Category_Name,Category_Note,Category_Status")] Categorys categorys)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorys).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categorys);
        }

        // GET: Admin/Categorys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorys categorys = db.Categorys.Find(id);
            if (categorys == null)
            {
                return HttpNotFound();
            }
            return View(categorys);
        }

        // POST: Admin/Categorys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categorys categorys = db.Categorys.Find(id);
            db.Categorys.Remove(categorys);
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
