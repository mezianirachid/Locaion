using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Location.Models; 
using Location.DAL;  
using System.Data.Entity;

namespace Location.Controllers
{
    [Authorize] // Seuls les utilisateurs authentifiés peuvent accéder au tableau de bord
    public class DashboardController : Controller
    {
        private ApplicationConnection db = new ApplicationConnection(); // Votre contexte

        // GET: Dashboard
        public ActionResult Index()
        {
            // Récupération des statistiques
            ViewBag.ImmeublesCount = db.Immeubles.Count();
            ViewBag.AppartementsCount = db.Appartements.Count();
            ViewBag.LocatairesCount = db.Locataires.Count();
            ViewBag.BauxCount = db.Baux.Count();
            ViewBag.PaiementsCount = db.Payements.Count();

            // Récupération des derniers baux (5 derniers)
            var derniersBaux = db.Baux
                .Include(b => b.Appartements)
                .Include(b => b.Locataires)
                .OrderByDescending(b => b.DateDebut)
                .Take(5)
                .ToList();
            ViewBag.DerniersBaux = derniersBaux;

            // Récupération des derniers paiements (5 derniers)
            var derniersPaiements = db.Payements
                .Include(p => p.Baux)
                .OrderByDescending(p => p.DatePayement)
                .Take(5)
                .ToList();
            ViewBag.DerniersPaiements = derniersPaiements;

            return View();
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