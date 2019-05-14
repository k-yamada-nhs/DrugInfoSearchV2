using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrugInfoSearchV2.Models;

namespace DrugInfoSearchV2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private DrugInfoContext db = new DrugInfoContext();

        // GET: Home
        public ActionResult Index()
        {
            var model = new SearchViewModel();
            model.ClassificationId = 0;

            if(Session["DrugName"] != null || Session["ClassificationId"] != null)
            {
                model.DrugName = (string)Session["DrugName"];
                model.ClassificationId = (int)Session["ClassificationId"];

                // 検索処理
                var list = db.Drugs.Where(item => (string.IsNullOrEmpty(model.DrugName) || item.Name.Contains(model.DrugName))
                && (model.ClassificationId == 0 || item.ClassificationId == model.ClassificationId)).ToList();

                model.Drugs = list;
            }

            this.SetClassificationItems(model.ClassificationId);
            return View(model);
        }

        // POST: Home
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include ="DrugName,ClassificationId")] SearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 検索処理
                var list = db.Drugs.Where(item => 
                (string.IsNullOrEmpty(model.DrugName) || item.Name.Contains(model.DrugName)) 
                && (model.ClassificationId == 0 || item.ClassificationId == model.ClassificationId)).ToList();
                model.Drugs = list;

                // 検索条件を復元するためにSessionに保持
                Session["DrugName"] = model.DrugName;
                Session["ClassificationId"] = model.ClassificationId;
            }
            this.SetClassificationItems();
            return View(model);
        }

        private void SetClassificationItems()
        {
            this.SetClassificationItems(0);
        }

        /// <summary>
        /// ViewBagにコンボボックスの項目をセット
        /// </summary>
        private void SetClassificationItems(int selectedId)
        {
            var list = db.Classifications.Select(item => new SelectListItem
            {
                Text = item.ClassificationCode + ":" + item.Name,
                Value = item.ClassificationId.ToString(),
                Selected = item.ClassificationId == selectedId
            }).ToList();

            //先頭にブランクを挿入
            list.Insert(0, new SelectListItem
            {
                Text = string.Empty,
                Value = "0",
                Selected = selectedId == 0
            });

            ViewBag.ClassificationId = list;
        }
    }
}