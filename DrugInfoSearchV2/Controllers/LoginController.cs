using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DrugInfoSearchV2.Models;

namespace DrugInfoSearchV2.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        readonly CustomMembershipProvider membershipProvider = new CustomMembershipProvider();
        private DrugInfoContext db = new DrugInfoContext();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include ="UserName,Password")] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(this.membershipProvider.ValidateUser(model.UserName, model.Password))
                {
                    var user = db.Users.Where(u => u.UserName.Equals(model.UserName)
                    && u.Password.Equals(model.Password)).FirstOrDefault();

                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserId.ToString(), false);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ViewBag.Message = "ログインに失敗しました。";
            return View(model);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}