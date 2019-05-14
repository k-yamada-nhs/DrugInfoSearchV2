using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DrugInfoSearchV2.Models;

namespace DrugInfoSearchV2.Controllers
{
    [Authorize]
    public class DrugsController : Controller
    {
        private DrugInfoContext db = new DrugInfoContext();

        // GET: Drugs
        public ActionResult Index()
        {
            var drugs = db.Drugs.Include(d => d.Classifications);
            return View(drugs.ToList());
        }

        // GET: Drugs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drugs drugs = db.Drugs.Find(id);
            if (drugs == null)
            {
                return HttpNotFound();
            }
            return View(drugs);
        }

        // GET: Drugs/Create
        public ActionResult Create()
        {
            ViewBag.ClassificationId = db.Classifications.Select(item => new SelectListItem {
                Text = item.ClassificationCode + ":" + item.Name,
                Value = item.ClassificationId.ToString()
            });

            return View();
        }

        // POST: Drugs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DrugId,DrugCode,Name,Company,ClassificationId")] Drugs drugs)
        {
            if (ModelState.IsValid)
            {
                db.Drugs.Add(drugs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassificationId = new SelectList(db.Classifications, "ClassificationId", "ClassificationCode", drugs.ClassificationId);
            return View(drugs);
        }

        // GET: Drugs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drugs drugs = db.Drugs.Find(id);
            if (drugs == null)
            {
                return HttpNotFound();
            }

            // 薬品コードが登録されている薬品は編集不可
            if (!string.IsNullOrEmpty(drugs.DrugCode))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.ClassificationId = db.Classifications.Select(item => new SelectListItem
            {
                Text = item.ClassificationCode + ":" + item.Name,
                Value = item.ClassificationId.ToString(),
                Selected = item.ClassificationId == drugs.ClassificationId
            });
            return View(drugs);
        }

        // POST: Drugs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DrugId,DrugCode,Name,Company,ClassificationId")] Drugs drugs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drugs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassificationId = new SelectList(db.Classifications, "ClassificationId", "ClassificationCode", drugs.ClassificationId);
            return View(drugs);
        }

        // GET: Drugs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drugs drugs = db.Drugs.Find(id);
            if (drugs == null)
            {
                return HttpNotFound();
            }
            // 薬品コードが登録されている薬品は編集不可
            if (!string.IsNullOrEmpty(drugs.DrugCode))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(drugs);
        }

        // POST: Drugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Drugs drugs = db.Drugs.Find(id);
            db.Drugs.Remove(drugs);
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
