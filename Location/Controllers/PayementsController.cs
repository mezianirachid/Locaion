using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Location.DAL; 
 

namespace Location.Controllers
{
    [Authorize]
    public class PayementsController : Controller
    {
        private ApplicationConnection db = new ApplicationConnection();
        private string userNameConnected = System.Web.HttpContext.Current.User.Identity.Name;

        // GET: Payements
        public ActionResult Index(int? annee, int? mois, int? bailId)
        {
            var paiements = db.Payements.Include(p => p.Baux).Include(p => p.MoyenPayements).AsQueryable();

            if (annee.HasValue)
                paiements = paiements.Where(p => p.Annee == annee.Value);
            if (mois.HasValue)
                paiements = paiements.Where(p => p.Mois == mois.Value);
            if (bailId.HasValue)
                paiements = paiements.Where(p => p.BauxId == bailId.Value);

            paiements = paiements.OrderByDescending(p => p.Annee).ThenByDescending(p => p.Mois);

            // Pour les listes déroulantes de filtres
            ViewBag.Annees = new SelectList(db.Payements.Select(p => p.Annee).Distinct().OrderByDescending(a => a));
            ViewBag.Mois = new SelectList(Enumerable.Range(1, 12).Select(m => new { Value = m, Text = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(m) }), "Value", "Text", mois);
            ViewBag.Baux = new SelectList(db.Baux.Include(b => b.Locataires).ToList()
                .Select(b => new { Id = b.Id, Display = "Bail n°" + b.NumeroBail + " - " + (b.Locataires != null ? b.Locataires.NomLocataire + " " + b.Locataires.PrenomLocataire : "") }), "Id", "Display", bailId);

            return View(paiements.ToList());
        }

        // GET: Payements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Payements paiement = db.Payements.Include(p => p.Baux).Include(p => p.Baux.Appartements)
                                             .Include(p => p.MoyenPayements)
                                             .FirstOrDefault(p => p.Id == id);
            if (paiement == null)
                return HttpNotFound();

            return View(paiement);
        }

        // GET: Payements/Create
        public ActionResult Create(int? bailId)
        {
            // Préparer les listes déroulantes
            ViewBag.BauxId = new SelectList(db.Baux.Include(b => b.Locataires).ToList()
                .Select(b => new { Id = b.Id, Display = "Bail n°" + b.NumeroBail + " - " + (b.Locataires != null ? b.Locataires.NomLocataire + " " + b.Locataires.PrenomLocataire : "") }), "Id", "Display", bailId);
            ViewBag.MoyenPayementId = new SelectList(db.MoyenPayements, "Id", "NomMoyenPayement");

            // Si bailId est fourni, pré-remplir l'année et le mois courants
            var paiement = new Payements();
            if (bailId.HasValue)
            {
                paiement.BauxId = bailId.Value;
                paiement.DatePayement = DateTime.Today;
                paiement.Annee = DateTime.Today.Year;
                paiement.Mois = DateTime.Today.Month;
                // Optionnel : récupérer le loyer du bail pour le montant par défaut
                var bail = db.Baux.Find(bailId.Value);
                if (bail != null)
                    paiement.Montant = bail.Prix ?? 0;
            }
            return View(paiement);
        }

        // POST: Payements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Annee,Mois,DatePayement,LieuPayement,Description,ModePayement,BauxId,Montant,Statut,MoyenPayementId")] Payements paiement)
        {
            if (ModelState.IsValid)
            {
                paiement.CreatedBy = userNameConnected;
                paiement.CreatedOn = DateTime.Now;
                db.Payements.Add(paiement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Recharger les listes en cas d'erreur
            ViewBag.BauxId = new SelectList(db.Baux.Include(b => b.Locataires).ToList()
                .Select(b => new { Id = b.Id, Display = "Bail n°" + b.NumeroBail + " - " + (b.Locataires != null ? b.Locataires.NomLocataire + " " + b.Locataires.PrenomLocataire : "") }), "Id", "Display", paiement.BauxId);
            ViewBag.MoyenPayementId = new SelectList(db.MoyenPayements, "Id", "NomMoyenPayement", paiement.MoyenPayementId);
            return View(paiement);
        }

        // GET: Payements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Payements paiement = db.Payements.Find(id);
            if (paiement == null)
                return HttpNotFound();

            ViewBag.BauxId = new SelectList(db.Baux.Include(b => b.Locataires).ToList()
                .Select(b => new { Id = b.Id, Display = "Bail n°" + b.NumeroBail + " - " + (b.Locataires != null ? b.Locataires.NomLocataire + " " + b.Locataires.PrenomLocataire : "") }), "Id", "Display", paiement.BauxId);
            ViewBag.MoyenPayementId = new SelectList(db.MoyenPayements, "Id", "NomMoyenPayement", paiement.MoyenPayementId);
            return View(paiement);
        }

        // POST: Payements/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Annee,Mois,DatePayement,LieuPayement,Description,ModePayement,BauxId,Montant,Statut,MoyenPayementId")] Payements paiement)
        {
            if (ModelState.IsValid)
            {
                paiement.ModifiedBy = userNameConnected;
                paiement.ModifiedOn = DateTime.Now;
                db.Entry(paiement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BauxId = new SelectList(db.Baux.Include(b => b.Locataires).ToList()
                .Select(b => new { Id = b.Id, Display = "Bail n°" + b.NumeroBail + " - " + (b.Locataires != null ? b.Locataires.NomLocataire + " " + b.Locataires.PrenomLocataire : "") }), "Id", "Display", paiement.BauxId);
            ViewBag.MoyenPayementId = new SelectList(db.MoyenPayements, "Id", "NomMoyenPayement", paiement.MoyenPayementId);
            return View(paiement);
        }

        // GET: Payements/Renouveler/5 (basé sur un bailId ou un paiementId)
        // Ici, on reçoit l'ID du bail pour lequel on veut créer un nouveau paiement
        // GET: Payements/Renouveler/5
        public ActionResult Renouveler(int? bailId)
        {
            if (!bailId.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Trouver le dernier paiement de ce bail pour pré-remplir
            var dernierPaiement = db.Payements.Where(p => p.BauxId == bailId.Value)
                                              .OrderByDescending(p => p.Annee)
                                              .ThenByDescending(p => p.Mois)
                                              .FirstOrDefault();

            var bail = db.Baux.Find(bailId.Value);
            if (bail == null)
                return HttpNotFound();

            // Créer un nouveau paiement avec des valeurs par défaut
            var nouveau = new Payements
            {
                BauxId = bailId.Value,
                Montant = dernierPaiement?.Montant ?? bail.Prix ?? 0,
                MoyenPayementId = dernierPaiement?.MoyenPayementId,
                LieuPayement = dernierPaiement?.LieuPayement,
                ModePayement = dernierPaiement?.ModePayement,
                Statut = "A" // Actif par défaut
            };

            // Déterminer le mois/année suivant
            if (dernierPaiement != null && dernierPaiement.Mois.HasValue && dernierPaiement.Annee.HasValue)
            {
                int mois = dernierPaiement.Mois.Value;
                int annee = dernierPaiement.Annee.Value;
                // Mois suivant
                int moisSuivant = mois == 12 ? 1 : mois + 1;
                int anneeSuivante = mois == 12 ? annee + 1 : annee;
                nouveau.Mois = moisSuivant;
                nouveau.Annee = anneeSuivante;
            }
            else
            {
                // Aucun paiement existant ou mois/année manquants : mois et année courants
                nouveau.Mois = DateTime.Today.Month;
                nouveau.Annee = DateTime.Today.Year;
            }

            nouveau.DatePayement = DateTime.Today;

            // Préparer les listes pour la vue Create
            ViewBag.BauxId = new SelectList(db.Baux.Include(b => b.Locataires).ToList()
                .Select(b => new { Id = b.Id, Display = "Bail n°" + b.NumeroBail + " - " + (b.Locataires != null ? b.Locataires.NomLocataire + " " + b.Locataires.PrenomLocataire : "") }), "Id", "Display", nouveau.BauxId);
            ViewBag.MoyenPayementId = new SelectList(db.MoyenPayements, "Id", "NomMoyenPayement", nouveau.MoyenPayementId);

            return View("Create", nouveau);
        }

        // GET: Payements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Payements paiement = db.Payements.Include(p => p.Baux).Include(p => p.MoyenPayements)
                                             .FirstOrDefault(p => p.Id == id);
            if (paiement == null)
                return HttpNotFound();

            return View(paiement);
        }

        // POST: Payements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payements paiement = db.Payements.Find(id);
            db.Payements.Remove(paiement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}