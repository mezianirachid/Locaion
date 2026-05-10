using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Location.Models; using Location.DAL; // adaptez le namespace si nécessaire
using System.Data.Entity.Validation;
using Rotativa;
using Rotativa.Options;
using System.Text;
using System.IO;

namespace Location.Controllers
{
    // ViewModel pour le renouvellement (peut être déplacé dans un fichier séparé)
 

    //[Authorize] // Décommentez si vous utilisez l'authentification
    public class BauxController : Controller
    {
        private ApplicationConnection db = new ApplicationConnection();
        private string userNameConnected = System.Web.HttpContext.Current.User.Identity.Name;

        private string GenererNumeroBail()
        {
            string annee = DateTime.Now.Year.ToString();
            string prefixe = "B" + annee + "";

            var dernierBail = db.Baux
                .Where(b => b.NumeroBail.StartsWith(prefixe))
                .OrderByDescending(b => b.NumeroBail)
                .FirstOrDefault();

            int prochainNumero = 1;
            if (dernierBail != null && !string.IsNullOrEmpty(dernierBail.NumeroBail))
            {
                string partieNumerique = dernierBail.NumeroBail.Substring(prefixe.Length);
                if (int.TryParse(partieNumerique, out int dernier))
                {
                    prochainNumero = dernier + 1;
                }
            }
            return prefixe + prochainNumero.ToString("D3");
        }

        // GET: Baux
        public ActionResult Index()
        {
            var baux = db.Baux
                .Include(b => b.Appartements)
                .Include(b => b.Locataires)          // Locataire principal
                .Include(b => b.Locataires1)         // Co-locataire
                .Include(b => b.MoyenPayements)
                .OrderByDescending(b => b.DateDebut);
            return View(baux.ToList());
        }

        // GET: Baux/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Baux baux = db.Baux
                .Include(b => b.Appartements)
                .Include(b => b.Locataires)
                .Include(b => b.Locataires1)
                .Include(b => b.MoyenPayements)
                .FirstOrDefault(b => b.Id == id);

            if (baux == null)
                return HttpNotFound();

            return View(baux);
        }

        // GET: Baux/Create
        public ActionResult Create()
        {
            var baux = new Baux();

            ViewBag.AppartementId = new SelectList(db.Appartements, "Id", "AdresseAppartement");
            ViewBag.LocataireId = new SelectList(db.Locataires.ToList().Select(l => new
            {
                Id = l.Id,
                NomComplet = l.NomLocataire + " " + l.PrenomLocataire
            }), "Id", "NomComplet");
            ViewBag.CoLocataireId = new SelectList(db.Locataires.ToList().Select(l => new
            {
                Id = l.Id,
                NomComplet = l.NomLocataire + " " + l.PrenomLocataire
            }), "Id", "NomComplet");
            ViewBag.MoyenPayementId = new SelectList(db.MoyenPayements, "Id", "NomMoyenPayement");

            return View(baux);
        }

        // POST: Baux/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NumeroBail,AppartementId,LocataireId,Prix,LieuPayement,ReglementImmeuble,DateDebut,DateFin,StationnementExt,NbPlacesExt,StationnementInt,NbPlacesInt,Emplacement,RemiseEspaceRangenment,Autre,MeublesInclus,AppareilsInclus,Deneigement,TailleGazon,MontantDepot,DateOccupation,DateRevision,Observation,DatePayement,StatutBaux,MoyenPayementId,Charges,DateDebutEffective,DateFinEffective,CoLocataireId,DateResiliation")] Baux baux)
        {
            if (ModelState.IsValid)
            {
                baux.CreatedBy = userNameConnected;
                baux.CreatedOn = DateTime.Now;
                db.Baux.Add(baux);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Recharger les listes en cas d'erreur
            ViewBag.AppartementId = new SelectList(db.Appartements, "Id", "AdresseAppartement", baux.AppartementId);
            ViewBag.LocataireId = new SelectList(db.Locataires.ToList().Select(l => new
            {
                Id = l.Id,
                NomComplet = l.NomLocataire + " " + l.PrenomLocataire
            }), "Id", "NomComplet", baux.LocataireId);
            ViewBag.CoLocataireId = new SelectList(db.Locataires.ToList().Select(l => new
            {
                Id = l.Id,
                NomComplet = l.NomLocataire + " " + l.PrenomLocataire
            }), "Id", "NomComplet", baux.CoLocataireId);
            ViewBag.MoyenPayementId = new SelectList(db.MoyenPayements, "Id", "NomMoyenPayement", baux.MoyenPayementId);
            return View(baux);
        }

        // GET: Baux/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Baux baux = db.Baux.Find(id);
            if (baux == null)
                return HttpNotFound();

            ViewBag.AppartementId = new SelectList(db.Appartements, "Id", "AdresseAppartement", baux.AppartementId);
            ViewBag.LocataireId = new SelectList(db.Locataires.ToList().Select(l => new
            {
                Id = l.Id,
                NomComplet = l.NomLocataire + " " + l.PrenomLocataire
            }), "Id", "NomComplet", baux.LocataireId);
            ViewBag.CoLocataireId = new SelectList(db.Locataires.ToList().Select(l => new
            {
                Id = l.Id,
                NomComplet = l.NomLocataire + " " + l.PrenomLocataire
            }), "Id", "NomComplet", baux.CoLocataireId);
            ViewBag.MoyenPayementId = new SelectList(db.MoyenPayements, "Id", "NomMoyenPayement", baux.MoyenPayementId);
            return View(baux);
        }

        // POST: Baux/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NumeroBail,AppartementId,LocataireId,Prix,LieuPayement,ReglementImmeuble,DateDebut,DateFin,StationnementExt,NbPlacesExt,StationnementInt,NbPlacesInt,Emplacement,RemiseEspaceRangenment,Autre,MeublesInclus,AppareilsInclus,Deneigement,TailleGazon,MontantDepot,DateOccupation,DateRevision,Observation,DatePayement,StatutBaux,MoyenPayementId,Charges,DateDebutEffective,DateFinEffective,CoLocataireId,DateResiliation")] Baux baux)
        {
            if (ModelState.IsValid)
            {
                baux.ModifiedBy = userNameConnected;
                baux.ModifiedOn = DateTime.Now;
                db.Entry(baux).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AppartementId = new SelectList(db.Appartements, "Id", "AdresseAppartement", baux.AppartementId);
            ViewBag.LocataireId = new SelectList(db.Locataires.ToList().Select(l => new
            {
                Id = l.Id,
                NomComplet = l.NomLocataire + " " + l.PrenomLocataire
            }), "Id", "NomComplet", baux.LocataireId);
            ViewBag.CoLocataireId = new SelectList(db.Locataires.ToList().Select(l => new
            {
                Id = l.Id,
                NomComplet = l.NomLocataire + " " + l.PrenomLocataire
            }), "Id", "NomComplet", baux.CoLocataireId);
            ViewBag.MoyenPayementId = new SelectList(db.MoyenPayements, "Id", "NomMoyenPayement", baux.MoyenPayementId);
            return View(baux);
        }

        // GET: Baux/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Baux baux = db.Baux
                .Include(b => b.Appartements)
                .Include(b => b.Locataires)
                .Include(b => b.Locataires1)
                .Include(b => b.MoyenPayements)
                .FirstOrDefault(b => b.Id == id);
            if (baux == null)
                return HttpNotFound();

            // Récupérer les dépendances
            ViewBag.NbPaiements = db.Payements.Count(p => p.BauxId == id);
            ViewBag.NbAppareils = db.InclusionAppareils.Count(i => i.BauxId == id);
            ViewBag.NbMeubles = db.InclusionMeubles.Count(i => i.BauxId == id);
            ViewBag.NbAutres = db.InclusionAutres.Count(i => i.BauxId == id);

            return View(baux);
        }

        // POST: Baux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Baux baux = db.Baux.Find(id);

            bool aDesPaiements = db.Payements.Any(p => p.BauxId == id);
            bool aDesAppareils = db.InclusionAppareils.Any(i => i.BauxId == id);
            bool aDesMeubles = db.InclusionMeubles.Any(i => i.BauxId == id);
            bool aDesAutres = db.InclusionAutres.Any(i => i.BauxId == id);

            if (aDesPaiements || aDesAppareils || aDesMeubles || aDesAutres)
            {
                ModelState.AddModelError("", "Impossible de supprimer ce bail car il est associé à des éléments (paiements, appareils, meubles, etc.).");
                // Recharger les données pour affichage
                baux = db.Baux
                    .Include(b => b.Appartements)
                    .Include(b => b.Locataires)
                    .Include(b => b.Locataires1)
                    .Include(b => b.MoyenPayements)
                    .FirstOrDefault(b => b.Id == id);
                return View("Delete", baux);
            }

            db.Baux.Remove(baux);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Baux/Renouveler/5
        public ActionResult Renouveler(int id)
        {
            var bailOriginal = db.Baux
                .Include(b => b.Locataires)      // Locataire principal
                .Include(b => b.Appartements)
                .FirstOrDefault(b => b.Id == id);

            if (bailOriginal == null)
            {
                return HttpNotFound();
            }

            // Vérifier la présence des entités liées
            if (bailOriginal.Locataires1 == null)
            {
                TempData["NotificationMessage"] = $"Le bail #{bailOriginal.NumeroBail} (ID {id}) ne peut pas être renouvelé car le locataire principal (ID {bailOriginal.LocataireId}) est introuvable. Vérifiez l'intégrité des données.";
                TempData["NotificationType"] = "error";
                return RedirectToAction("Index");
            }
            if (bailOriginal.Appartements == null)
            {
                TempData["NotificationMessage"] = $"Le bail #{bailOriginal.NumeroBail} ne peut pas être renouvelé car l'appartement (ID {bailOriginal.AppartementId}) est introuvable.";
                TempData["NotificationType"] = "error";
                return RedirectToAction("Index");
            }

            var viewModel = new RenouvellementBailViewModel
            {
                BailOriginalId = bailOriginal.Id,
                LocataireId = bailOriginal.Locataires1.Id,
                LocataireNomComplet = $"{bailOriginal.Locataires1.PrenomLocataire} {bailOriginal.Locataires1.NomLocataire}".Trim(),
                AppartementId = bailOriginal.Appartements.Id,
                AppartementAdresse = bailOriginal.Appartements.AdresseAppartement,
                // Nouvelle date de début : lendemain de la fin ou aujourd'hui si non définie
                NouvelleDateDebut = bailOriginal.DateFin?.AddDays(1) ?? DateTime.Today,
                // Nouvelle date de fin : +1 an par rapport à la nouvelle date de début
                NouvelleDateFin = bailOriginal.DateFin != null ? bailOriginal.DateFin.Value.AddYears(1) : DateTime.Today.AddYears(1),
                NouveauLoyer = bailOriginal.Prix ?? 0m,        // Gestion des nulls
                NouvellesCharges = bailOriginal.Charges ?? 0m,
                Commentaires = string.Empty
            };

            return View(viewModel);
        }

        // POST: Baux/Renouveler
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Renouveler(RenouvellementBailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bailOriginal = db.Baux.Find(model.BailOriginalId);
                if (bailOriginal == null)
                {
                    ModelState.AddModelError("", "Le bail original n'existe pas.");
                    return View(model);
                }

                string nouveauNumero = GenererNumeroBail();

                var nouveauBail = new Baux
                {
                    LocataireId = model.LocataireId,
                    AppartementId = model.AppartementId,
                    NumeroBail = nouveauNumero,
                    DateDebut = model.NouvelleDateDebut,
                    DateFin = model.NouvelleDateFin,
                    DateDebutEffective = model.NouvelleDateDebut,
                    DateFinEffective = model.NouvelleDateFin,
                    DateOccupation = bailOriginal.DateOccupation,
                    DateRevision = bailOriginal.DateRevision,
                    DatePayement = bailOriginal.DatePayement,
                    DateResiliation = null,
                    Prix = model.NouveauLoyer,
                    Charges = model.NouvellesCharges,
                    MontantDepot = bailOriginal.MontantDepot,
                    LieuPayement = bailOriginal.LieuPayement,
                    MoyenPayementId = bailOriginal.MoyenPayementId,
                    ReglementImmeuble = bailOriginal.ReglementImmeuble,
                    StationnementExt = bailOriginal.StationnementExt,
                    NbPlacesExt = bailOriginal.NbPlacesExt,
                    StationnementInt = bailOriginal.StationnementInt,
                    NbPlacesInt = bailOriginal.NbPlacesInt,
                    Emplacement = bailOriginal.Emplacement,
                    RemiseEspaceRangenment = bailOriginal.RemiseEspaceRangenment,
                    MeublesInclus = bailOriginal.MeublesInclus,
                    AppareilsInclus = bailOriginal.AppareilsInclus,
                    Deneigement = bailOriginal.Deneigement,
                    TailleGazon = bailOriginal.TailleGazon,
                    Autre = bailOriginal.Autre,
                    Observation = model.Commentaires,
                    CoLocataireId = bailOriginal.CoLocataireId,
                    CreatedBy = User.Identity.Name,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = User.Identity.Name,
                    ModifiedOn = DateTime.Now,
                    StatutBaux = "A"
                };

                try
                {
                    db.Baux.Add(nouveauBail);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = nouveauBail.Id });
                }
                catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => $"Propriété : {x.PropertyName}, Erreur : {x.ErrorMessage}");
                    ModelState.AddModelError("", "Erreur de validation : " + string.Join("; ", errorMessages));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Erreur inattendue : " + ex.Message);
                }
            }

            // En cas d'erreur, recharger les infos pour affichage
            var bailOrig = db.Baux
                .Include(b => b.Locataires)
                .Include(b => b.Appartements)
                .FirstOrDefault(b => b.Id == model.BailOriginalId);
            if (bailOrig != null && bailOrig.Locataires != null && bailOrig.Appartements != null)
            {
                model.LocataireNomComplet = $"{bailOrig.Locataires.PrenomLocataire} {bailOrig.Locataires.NomLocataire}".Trim();
                model.AppartementAdresse = bailOrig.Appartements.AdresseAppartement;
            }
            else
            {
                model.LocataireNomComplet = "Inconnu";
                model.AppartementAdresse = "Inconnue";
            }

            return View(model);
        }

        // GET: Bails/LettreRenouvellement/5
        public ActionResult LettreRenouvellement(int id)
        {
            var bail = db.Baux
                .Include(b => b.Locataires)
                .Include(b => b.Appartements)
                .FirstOrDefault(b => b.Id == id);
            if (bail == null)
                return HttpNotFound();

            return View(bail);
        }

        // GET: Baux/ImprimerRenouvellement/5
        public ActionResult ImprimerRenouvellement(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Baux nouveauBail = db.Baux
                .Include(b => b.Locataires)
                .Include(b => b.Locataires1)
                .Include(b => b.Appartements)
                .Include(b => b.Appartements.Immeubles)
                .FirstOrDefault(b => b.Id == id);
            if (nouveauBail == null)
                return HttpNotFound();

            return new Rotativa.ViewAsPdf("Renouvellement", nouveauBail)
            {
                FileName = "Renouvellement_Bail_" + nouveauBail.NumeroBail + ".pdf",
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Portrait,
                CustomSwitches = "--disable-smart-shrinking"
            };
        }

        // GET: Bails/GeneratePDF/5
        public ActionResult GeneratePDF(int id)
        {
            var bail = db.Baux.Find(id);
            if (bail == null)
                return HttpNotFound();

            return new ViewAsPdf("LettreRenouvellement", bail)
            {
                FileName = $"LettreRenouvellement_{bail.NumeroBail}.pdf",
                PageSize = Size.Letter,
                PageOrientation = Orientation.Portrait,
                CustomSwitches = "--footer-center \"Page [page]/[pageCount]\""
            };
        }

        // Helper pour générer une chaîne HTML à partir d'une vue
        private string RenderViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        // GET: Bails/GenerateWord/5
        public ActionResult GenerateWord(int id)
        {
            var bail = db.Baux.Find(id);
            if (bail == null)
                return HttpNotFound();

            string htmlContent = RenderViewToString("LettreRenouvellement", bail);
            return File(Encoding.UTF8.GetBytes(htmlContent), "application/msword", $"LettreRenouvellement_{bail.NumeroBail}.doc");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}