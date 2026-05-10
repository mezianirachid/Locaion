using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Location.DAL; 

namespace Location.Controllers
{
    public class AppartementsController : Controller
    {
        private ApplicationConnection db = new ApplicationConnection();

        // GET: Appartements
        public ActionResult Index()
        {
            var appartements = db.Appartements.Include(a => a.Immeubles).Include(a => a.TypeAppartements);
            return View(appartements.ToList());
        }

        // GET: Appartements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appartements appartements = db.Appartements.Find(id);
            if (appartements == null)
            {
                return HttpNotFound();
            }
            return View(appartements);
        }

        // GET: Appartements/Create
        public ActionResult Create()
        {
            ViewBag.ImmeubleId = new SelectList(db.Immeubles, "Id", "NomImmeuble");
            ViewBag.TypeAppartementId = new SelectList(db.TypeAppartements, "Id", "NomTypeAppartement");
            return View();
        }

        // POST: Appartements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AdresseAppartement,NbSalleBain,NbEtages,ChauffeOuiNon,Annee,nbBalcons,nbStationnementsInt,nbStationnementsExt,nbGarages,Orientation,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,ImmeubleId,NumeroEtage,StatutAppartement,TypeChauffage,TypeAppartementId,Superficie")] Appartements appartements)
        {
            if (ModelState.IsValid)
            {
                db.Appartements.Add(appartements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ImmeubleId = new SelectList(db.Immeubles, "Id", "NomImmeuble", appartements.ImmeubleId);
            ViewBag.TypeAppartementId = new SelectList(db.TypeAppartements, "Id", "NomTypeAppartement", appartements.TypeAppartementId);
            return View(appartements);
        }

        // GET: Appartements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appartements appartements = db.Appartements.Find(id);
            if (appartements == null)
            {
                return HttpNotFound();
            }
            ViewBag.ImmeubleId = new SelectList(db.Immeubles, "Id", "NomImmeuble", appartements.ImmeubleId);
            ViewBag.TypeAppartementId = new SelectList(db.TypeAppartements, "Id", "NomTypeAppartement", appartements.TypeAppartementId);
            return View(appartements);
        }

        // POST: Appartements/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AdresseAppartement,NbSalleBain,NbEtages,ChauffeOuiNon,Annee,nbBalcons,nbStationnementsInt,nbStationnementsExt,nbGarages,Orientation,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,ImmeubleId,NumeroEtage,StatutAppartement,TypeChauffage,TypeAppartementId,Superficie")] Appartements appartements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appartements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ImmeubleId = new SelectList(db.Immeubles, "Id", "NomImmeuble", appartements.ImmeubleId);
            ViewBag.TypeAppartementId = new SelectList(db.TypeAppartements, "Id", "NomTypeAppartement", appartements.TypeAppartementId);
            return View(appartements);
        }

        // GET: Appartements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appartements appartements = db.Appartements.Find(id);
            if (appartements == null)
            {
                return HttpNotFound();
            }
            return View(appartements);
        }

        // POST: Appartements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appartements appartements = db.Appartements.Find(id);
            if (appartements == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.Appartements.Remove(appartements);
                db.SaveChanges();

                TempData["NotificationMessage"] = "Appartement supprimé avec succès.";
                TempData["NotificationType"] = "success";
            }
            catch (DbUpdateException)
            {
                // Erreur due à une contrainte de clé étrangère (baux liés, etc.)
                TempData["NotificationMessage"] = "Impossible de supprimer cet appartement car il est référencé dans des baux. Veuillez d'abord supprimer les baux associés.";
                TempData["NotificationType"] = "error";
            }
            catch (Exception ex)
            {
                // Autre erreur inattendue
                TempData["NotificationMessage"] = "Une erreur est survenue lors de la suppression : " + ex.Message;
                TempData["NotificationType"] = "error";
            }

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