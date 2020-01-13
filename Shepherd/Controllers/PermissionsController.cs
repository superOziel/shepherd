using Shepherd.Models;
using Shepherd.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shepherd.Controllers
{
    public class PermissionsController : Controller
    {
        //Model database
        APQP_betaEntities db = new APQP_betaEntities();

        //
        // GET: /Permissions/

        public ActionResult Index()
        {
            ViewBag.Title = "Shepherd Commitment Permissions";
            return View();
        }

        //GET: /All
        public ActionResult GetPermissions() {
            return Json(db.Shepherd_permissions.Where(x => x.active == true).OrderBy(x => x.name).ToList(), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Permissions/Details/5

        public ActionResult GetPermission(string aliasname)
        {
            return Json(db.Shepherd_permissions.Where(x => x.aliasname == aliasname && x.active == true).ToList() , JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Permissions/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Permissions/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection, int userid = 0)
        {
            try
            {
                // TODO: Add insert logic here
                string email = collection[3];
                string location = collection[6];
                var sp = db.Shepherd_permissions.Where(s => s.email == email && s.location == location).FirstOrDefault();
                Boolean result = false;
                bool.TryParse(collection[5], out result);
                if (sp != null)
                {
                    sp.aliasname = collection[0];
                    sp.name = collection[1];
                    sp.lastname = collection[2];
                    sp.email = collection[3];
                    sp.location = collection[6];
                    sp.baancompany = collection[7];
                    sp.active = result;
                }
                else 
                {
                    Shepherd_permissions perm = new Shepherd_permissions();
                    perm.aliasname = collection[0];
                    perm.name = collection[1];
                    perm.lastname = collection[2];
                    perm.email = collection[3];
                    perm.location = collection[6];
                    perm.baancompany = collection[7];
                    perm.active = true;
                    db.Shepherd_permissions.Add(perm);
                }
                

                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Permissions/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Permissions/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        //
        // POST: /Permissions/Delete/5

        [HttpPost]
        public ActionResult Delete(int userid)
        {
            try
            {
                var sp = db.Shepherd_permissions.Where(s => s.id == userid).FirstOrDefault();

                if (sp != null)
                {
                    sp.active = false;
                }

                db.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
        }
    }
}
