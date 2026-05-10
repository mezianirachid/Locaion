using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Location;

namespace Location.Controllers
{
    public class TypePrelevementsController : Controller
    {
        private BDCovidCEMTLEntities db = new BDCovidCEMTLEntities();

        // GET: TypePrelevements
        public ActionResult Index()
        {
            return View(db.TypePrelevements.ToList());
        }

        // GET: TypePrelevements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypePrelevements typePrelevements = db.TypePrelevements.Find(id);
            if (typePrelevements == null)
            {
                return HttpNotFound();
            }
            return View(typePrelevements);
        }

        // GET: TypePrelevements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypePrelevements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NomTypePrelevement,DescTypePrelevement")] TypePrelevements typePrelevements)
        {
            if (ModelState.IsValid)
            {
                db.TypePrelevements.Add(typePrelevements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typePrelevements);
        }

        // GET: TypePrelevements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypePrelevements typePrelevements = db.TypePrelevements.Find(id);
            if (typePrelevements == null)
            {
                return HttpNotFound();
            }
            return View(typePrelevements);
        }

        // POST: TypePrelevements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NomTypePrelevement,DescTypePrelevement")] TypePrelevements typePrelevements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typePrelevements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typePrelevements);
        }

        // GET: TypePrelevements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypePrelevements typePrelevements = db.TypePrelevements.Find(id);
            if (typePrelevements == null)
            {
                return HttpNotFound();
            }
            return View(typePrelevements);
        }

        // POST: TypePrelevements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypePrelevements typePrelevements = db.TypePrelevements.Find(id);
            db.TypePrelevements.Remove(typePrelevements);
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
