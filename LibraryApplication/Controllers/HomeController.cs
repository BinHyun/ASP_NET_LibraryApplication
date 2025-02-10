using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryApplication.Context;
using LibraryApplication.Models;

namespace LibraryApplication.Controllers
{
    public class HomeController : Controller
    {
        private LibraryDb db = new LibraryDb();

        // GET: Home
        public ActionResult Index()
        {
            //하나의 페이지의 특정 갯수의 게시물 출력
            int maxListCount = 3;

            //페이지 넘버 (기본값)
            int pageNum = 1;

            //QueryString 즉 주소 파라미터에 page의 값이 null 아닌경우
            //pageNum 라는 변수의 Convert 한 값을 담아준다.
            if (Request.QueryString["page"] != null)
                pageNum = Convert.ToInt32(Request.QueryString["page"]);

            //Take 키워드는 db.Books 즉 모델에서 몇개를 가지고 올 것인지에 대한 것
            //OrderBy 키워드는 쿼리문의 그 OrderBy 이다.
            //Skip 키워드는 말 그대로 Skip의 의미
            //(pageNum-1)* listCount 의 의미는 페이지가 넘어갈때 마다 곱하기를 하기 때문에 그만큼 Skip한 결과를 출력 한다. 
            //즉 0, 3, 6 과 같이 한번에 보여줄 리스트는 3개 이기 때문에, 3개씩 Skip하기 위함이다.
            var books = db.Books.OrderBy(x => x.Book_U).Skip((pageNum-1)* maxListCount).Take(maxListCount).ToList();

            //ViewBag에 담아서 view로 값을 넘겨준다.
            ViewBag.Page = pageNum;

            //db.Books의 갯수
            ViewBag.TotalCount = db.Books.Count();

            //listCount 값
            ViewBag.MaxListCount = maxListCount;

            //paging에 대해 정리를 해보자면, 일단 데이터를 가져와 정렬을 한다. 
            //그 뒤 화면에서 몇개의 row로 보여줄지 정한다.
            //마지막으로 view에서는 가지고 온 전체 데이터의 갯수 / 몇개의 row로 보여줄 값으로 나눠준다.
            //즉 (전체 데이터의 갯수 / 화면의 출력할 row의 갯수)
            return View(books);
        }

        // GET: Home/Details/5
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

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하세요. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하세요.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Book_U,Title,Writer,Summary,Publisher,Published_data")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Home/Edit/5
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

        // POST: Home/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하세요. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하세요.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Book_U,Title,Writer,Summary,Publisher,Published_data")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Home/Delete/5
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

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
