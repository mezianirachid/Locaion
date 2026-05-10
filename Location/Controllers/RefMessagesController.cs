using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Location.DAL;

namespace Location.Controllers
{
    [Authorize]
    public class RefMessagesController : Controller
    {
        private ApplicationConnection db = new ApplicationConnection();

        // GET: RefMessages
        public ActionResult Index()
        {
            return View(db.RefMessages.ToList());
        }

        // GET: RefMessages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefMessages refMessages = db.RefMessages.Find(id);
            if (refMessages == null)
            {
                return HttpNotFound();
            }
            return View(refMessages);
        }

        // GET: RefMessages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RefMessages/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,Description,Statut")] RefMessages refMessages)
        {
            if (ModelState.IsValid)
            {
                db.RefMessages.Add(refMessages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(refMessages);
        }

        // GET: RefMessages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefMessages refMessages = db.RefMessages.Find(id);
            if (refMessages == null)
            {
                return HttpNotFound();
            }
            return View(refMessages);
        }

        // POST: RefMessages/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,Description,Statut")] RefMessages refMessages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(refMessages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(refMessages);
        }

        // GET: RefMessages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefMessages refMessages = db.RefMessages.Find(id);
            if (refMessages == null)
            {
                return HttpNotFound();
            }
            return View(refMessages);
        }

        // POST: RefMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RefMessages refMessages = db.RefMessages.Find(id);
            db.RefMessages.Remove(refMessages);
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
