using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebNewsMVC.Models;

namespace WebNewsMVC.Controllers
{
    public class HomeController : Controller
    {
        private DbModel db = new DbModel();
        public ActionResult Index(int? id)
        {
            var news = db.News.ToList();
            // Lấy các tin theo mã danh mục
            if (id != null)
            {
                news = news.FindAll(s => s.Category_ID == id);
            }
            return View(news);
        }
        public ActionResult DetailNews(int? id)
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
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly] //controller tạo partial view menu
        // khai báo action child
        public ActionResult RenderMenu()
        {
            var model = from c in db.Categorys.ToList()
                        where (c.Category_Status == 1)
                        select c;
            return PartialView(model);
        }
    }
}