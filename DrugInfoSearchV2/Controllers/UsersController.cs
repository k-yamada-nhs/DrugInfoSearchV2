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
    [Authorize(Roles = "Administrators")]
    public class UsersController : Controller
    {
        private DrugInfoContext db = new DrugInfoContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
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

        // GET: Users/Create
        public ActionResult Create()
        {
            this.SetRoleItems();
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,Password,RoleId")] Users users)
        {
            if (ModelState.IsValid)
            {
                foreach(int roleId in users.RoleId)
                {
                    var userRole = new UserRole
                    {
                        UserID = users.UserId,
                        RoleID = roleId
                    };
                    users.UserRole.Add(userRole);
                }

                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            this.SetRoleItems();
            return View(users);
        }

        // GET: Users/Edit/5
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
            var roles = users.UserRole.Select(item => item.Roles).ToList();
            this.SetRoleItems(roles);
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,Password,RoleId")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;

                var roleIds = db.UserRole.Where(item => item.UserID == users.UserId)
                    .Select(item => item.RoleID).ToList();

                foreach(int roleId in users.RoleId)
                {
                    if (db.UserRole.Where(item => item.UserID == users.UserId && item.RoleID == roleId)
                        .Count() > 0)
                    {
                        // 既に登録済み
                        roleIds.Remove(roleId);
                    }
                    else
                    {
                        // 追加
                        var userRole = new UserRole
                        {
                            UserID = users.UserId,
                            RoleID = roleId
                        };
                        db.UserRole.Add(userRole);
                    }
                }

                // 削除されたロールをdbに反映
                foreach(var roleId in roleIds)
                {
                    var item = db.UserRole.Where(ur => ur.UserID == users.UserId && ur.RoleID == roleId).FirstOrDefault();
                    
                    if(item != null)
                    {
                        db.UserRole.Remove(item);
                    }

                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var roles = users.UserRole.Select(item => item.Roles).ToList();
            this.SetRoleItems(roles);
            return View(users);
        }

        // GET: Users/Delete/5
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

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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

        private void SetRoleItems()
        {
            SetRoleItems(new List<Roles>());   
        }

        /// <summary>
        /// ロールのリストをViewBagにセット
        /// </summary>
        /// <param name="roles"></param>
        private void SetRoleItems(List<Roles> roles)
        {
            var roleIds = roles.Select(item => item.RoleId).ToArray();

            var list = db.Roles.Select(item => new SelectListItem {

                Text = item.RoleName,
                Value = item.RoleId.ToString(),
                Selected = roleIds.Contains(item.RoleId)
                
            }).ToList();

            ViewBag.RoleId = list;
        }
    }
}
