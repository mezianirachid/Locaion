using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;  
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Location.Models; 
using Location.DAL;

namespace Location.Controllers
{
    public class ImmeublesController : Controller
    {
        private ApplicationConnection db = new ApplicationConnection();

        // GET: Immeubles
        public ActionResult Index()
        {
            return View(db.Immeubles.ToList());
        }

        // GET: Immeubles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immeubles immeubles = db.Immeubles.Find(id);
            if (immeubles == null)
            {
                return HttpNotFound();
            }
            return View(immeubles);
        }

        // GET: Immeubles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Immeubles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NomImmeuble,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,DescriptionImmeuble,AdresseImmeuble,StatutImmeuble,Orientation,nbEtages,nbStationnementsInt,nbStationnementsExt,Annee,Autre")] Immeubles immeubles)
        {
            if (ModelState.IsValid)
            {
                db.Immeubles.Add(immeubles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(immeubles);
        }

        // GET: Immeubles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immeubles immeubles = db.Immeubles.Find(id);
            if (immeubles == null)
            {
                return HttpNotFound();
            }
            return View(immeubles);
        }

        // POST: Immeubles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NomImmeuble,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,DescriptionImmeuble,AdresseImmeuble,StatutImmeuble,Orientation,nbEtages,nbStationnementsInt,nbStationnementsExt,Annee,Autre")] Immeubles immeubles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(immeubles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(immeubles);
        }

        // GET: Immeubles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immeubles immeubles = db.Immeubles.Find(id);
            if (immeubles == null)
            {
                return HttpNotFound();
            }
            return View(immeubles);
        }

        // POST: Immeubles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Immeubles immeubles = db.Immeubles.Find(id);
            if (immeubles == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.Immeubles.Remove(immeubles);
                db.SaveChanges();

                TempData["NotificationMessage"] = "Immeuble supprimé avec succès.";
                TempData["NotificationType"] = "success";
            }
            catch (DbUpdateException)
            {
                // Erreur due à une contrainte de clé étrangère (appartements liés)
                TempData["NotificationMessage"] = "Impossible de supprimer cet immeuble car il contient encore des appartements. Veuillez d'abord supprimer les appartements associés.";
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