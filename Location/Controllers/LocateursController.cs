using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Location.DAL;  

namespace Location.Controllers
{
    public class LocateursController : Controller
    {
        private ApplicationConnection db = new ApplicationConnection(); // Votre contexte

        // GET: Locateur
        public ActionResult Index()
        {
            return View(db.Locateur.ToList());
        }

        // GET: Locateur/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locateur locateur = db.Locateur.Find(id);
            if (locateur == null)
            {
                return HttpNotFound();
            }
            return View(locateur);
        }

        // GET: Locateur/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locateur/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nom,Prenom,Sexe,Adresse,Ville,CodePostal,DateNaissance,NAS,TelPrincipal,TelSecondaire,Courriel,Statut,Signature")] Locateur locateur)
        {
            if (ModelState.IsValid)
            {
                // Ajouter les métadonnées de création
                locateur.CreatedBy = User.Identity.Name ?? "Systeme";
                locateur.CreatedOn = DateTime.Now;
                locateur.ModifiedBy = User.Identity.Name ?? "Systeme";
                locateur.ModifiedOn = DateTime.Now;
                // Valeur par défaut pour Statut si non fournie
                if (string.IsNullOrEmpty(locateur.Statut))
                    locateur.Statut = "A"; // Actif

                db.Locateur.Add(locateur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locateur);
        }

        // GET: Locateur/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locateur locateur = db.Locateur.Find(id);
            if (locateur == null)
            {
                return HttpNotFound();
            }
            return View(locateur);
        }

        // POST: Locateur/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,Prenom,Sexe,Adresse,Ville,CodePostal,DateNaissance,NAS,TelPrincipal,TelSecondaire,Courriel,Statut,Signature")] Locateur locateur)
        {
            if (ModelState.IsValid)
            {
                // Mettre à jour les métadonnées de modification
                locateur.ModifiedBy = User.Identity.Name ?? "Systeme";
                locateur.ModifiedOn = DateTime.Now;
                // Ne pas modifier CreatedBy / CreatedOn

                db.Entry(locateur).State = EntityState.Modified;
                // Empêcher l'écrasement des champs de création
                db.Entry(locateur).Property(x => x.CreatedBy).IsModified = false;
                db.Entry(locateur).Property(x => x.CreatedOn).IsModified = false;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locateur);
        }

        // GET: Locateur/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locateur locateur = db.Locateur.Find(id);
            if (locateur == null)
            {
                return HttpNotFound();
            }
            return View(locateur);
        }

        // POST: Locateur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Locateur locateur = db.Locateur.Find(id);
            // Suppression physique (ou logique selon votre besoin)
            db.Locateur.Remove(locateur);
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