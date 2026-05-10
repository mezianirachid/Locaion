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
    public class InclusionMeublesController : Controller
    {
        private ApplicationConnection db = new ApplicationConnection();

        // GET: InclusionMeubles
        public ActionResult Index()
        {
            var inclusionMeubles = db.InclusionMeubles.Include(i => i.Meubles).Include(i => i.Baux);
            return View(inclusionMeubles.ToList());
        }

        // GET: InclusionMeubles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InclusionMeubles inclusionMeubles = db.InclusionMeubles.Find(id);
            if (inclusionMeubles == null)
            {
                return HttpNotFound();
            }
            return View(inclusionMeubles);
        }

        // GET: InclusionMeubles/Create
        public ActionResult Create()
        {
            ViewBag.MeubleId = new SelectList(db.Meubles, "Id", "Nom");
            ViewBag.BauxId = new SelectList(db.Baux, "Id", "NumeroBail");
            return View();
        }

        // POST: InclusionMeubles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MeubleId,Observation,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,BauxId")] InclusionMeubles inclusionMeubles)
        {
            if (ModelState.IsValid)
            {
                db.InclusionMeubles.Add(inclusionMeubles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MeubleId = new SelectList(db.Meubles, "Id", "Nom", inclusionMeubles.MeubleId);
            ViewBag.BauxId = new SelectList(db.Baux, "Id", "NumeroBail", inclusionMeubles.BauxId);
            return View(inclusionMeubles);
        }

        // GET: InclusionMeubles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InclusionMeubles inclusionMeubles = db.InclusionMeubles.Find(id);
            if (inclusionMeubles == null)
            {
                return HttpNotFound();
            }
            ViewBag.MeubleId = new SelectList(db.Meubles, "Id", "Nom", inclusionMeubles.MeubleId);
            ViewBag.BauxId = new SelectList(db.Baux, "Id", "NumeroBail", inclusionMeubles.BauxId);
            return View(inclusionMeubles);
        }

        // POST: InclusionMeubles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InclusionMeubles inclusionMeubles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inclusionMeubles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MeubleId = new SelectList(db.Meubles, "Id", "Nom", inclusionMeubles.MeubleId);
            ViewBag.BauxId = new SelectList(db.Baux, "Id", "NumeroBail", inclusionMeubles.BauxId);
            return View(inclusionMeubles);
        }

        // GET: InclusionMeubles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InclusionMeubles inclusionMeubles = db.InclusionMeubles.Find(id);
            if (inclusionMeubles == null)
            {
                return HttpNotFound();
            }
            return View(inclusionMeubles);
        }

        // POST: InclusionMeubles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InclusionMeubles inclusionMeubles = db.InclusionMeubles.Find(id);
            db.InclusionMeubles.Remove(inclusionMeubles);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult PostInclusionsBail(int bauxId)
        {           
           
            return new JsonResult { Data = new { Statut = true, Message = "Les meubles sont ajoutés avec succès" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
         
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
