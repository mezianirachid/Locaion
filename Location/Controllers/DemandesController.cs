using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Location.Models;
using System.IO;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace Location.Controllers
{
    [Authorize]
    public class DemandesController : Controller
    {
        private BDCovidCEMTLEntities db = new BDCovidCEMTLEntities();
        private ApplicationDbContext dbRoles = new ApplicationDbContext();
        private string userNameConnected = System.Web.HttpContext.Current.User.Identity.Name;
        [HttpPost]
        public JsonResult GetTimeNow()
        {
            bool status = true;
            string HeureServeur = DateTime.Now.ToString();
            return new JsonResult { Data = new { status = status, message = HeureServeur } };
        }       
        // GET: Demandes
        //[OutputCache(CacheProfile = "Cache1Minute")]
        [Authorize(Roles = "Utilisateur-SST, Admin")]
        public ActionResult Employees()
        {
            var demandes = db.Demandes.Where(x => x.TypeUsagerID == Enum.TypeUsagerIDEnum.EmployéCEMTL).Include(d => d.Sites);
            return View(demandes);
        }

        //[OutputCache(CacheProfile = "Cache1Minute")]
        [Authorize(Roles = "Admin, Super utilisateur, Utilisateur")]
        public ActionResult ListUsagers()
        {
            ViewBag.ShowResuts = true;
            return View();
        }

        //[OutputCache(CacheProfile = "Cache1Minute")]
        [Authorize(Roles = "Utilisateur-SST, Admin")]

        public ActionResult ListEmployees()
        {
            ViewBag.ShowResuts = true;
            return View();
        }

        //il faut installer NuGet  "Microsoft.jQuery.Unobtrusive.Ajax"
        // Return all students
        //[OutputCache(CacheProfile = "Cache1Minute")]
        public PartialViewResult All()
        {
            System.Threading.Thread.Sleep(3000);
            List<Demandes> model = db.Demandes.OrderByDescending(m => m.ID).ToList();
            if (model.Count() > 0) ViewBag.ShowResuts = true; else ViewBag.Resuts = false;
            return PartialView("_ListUsagers", model);
        }

        //[OutputCache(CacheProfile = "Cache1Minute")]
        public PartialViewResult Derniers()
        {
            System.Threading.Thread.Sleep(3000);
            var der1 = DateTime.Today.AddDays(-2);
            var der2 = DateTime.Now.AddDays(-2);
            List<Demandes> model = db.Demandes.Where(m => m.CreatedOn.Value > der1).OrderByDescending(m => m.ID).ToList();
            //List<Demandes> model1 = db.Demandes.Where(m => m.CreatedOn > DateTime.Now.AddDays(-12)).OrderByDescending(m => m.ID).ToList();
            if (model.Count() > 0) ViewBag.ShowResuts = true; else ViewBag.Resuts = false;
            return PartialView("_ListUsagers", model);
        }
        public PartialViewResult EntreUneDateEtUneDate(DateTime dateDebut, DateTime dateFin)
        {
            System.Threading.Thread.Sleep(3000);
            DateTime dd = dateDebut.Date;
            DateTime df = dateFin.Date.AddDays(1);
            List<Demandes> model = db.Demandes.Where(m => m.CreatedOn >= dd && m.CreatedOn < df).OrderByDescending(m => m.ID).ToList();
            //List<Demandes> model1 = db.Demandes.Where(m => m.CreatedOn > DateTime.Now.AddDays(-12)).OrderByDescending(m => m.ID).ToList();
            if (model.Count() > 0) ViewBag.ShowResuts = true; else ViewBag.Resuts = false;
            return PartialView("_ListUsagers", model);
        }

        public PartialViewResult EntreUneDateEtUneDateEmployees(DateTime dateDebut, DateTime dateFin)
        {
            System.Threading.Thread.Sleep(3000);
            DateTime dd = dateDebut.Date;
            DateTime df = dateFin.Date.AddDays(1);
            List<Demandes> model = db.Demandes.Where(m => m.TypeUsagerID == 1 && m.CreatedOn >= dd && m.CreatedOn < df && m.Statut != "I").OrderByDescending(m => m.ID).ToList();
            if (model.Count() > 0) ViewBag.ShowResuts = true; else ViewBag.Resuts = false;
            return PartialView("_ListEmployees", model);
        }
        // GET: Demandes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demandes demandes = db.Demandes.Find(id);
            if (demandes == null)
            {
                return HttpNotFound();
            }
            return View(demandes);
        }

        // GET: Demandes/Create
        [Authorize(Roles = "Admin, Super utilisateur, Utilisateur")]
        public ActionResult Create()
        {
            int SiteId = 0;
            var Site = db.Sites.FirstOrDefault();
            var siteCourant = System.Configuration.ConfigurationManager.AppSettings["SiteCourant"];
            if (siteCourant == "CHAUVEAU")
            {
                Site = db.Sites.Where(m => m.NomSite == "CHAUVEAU").FirstOrDefault();
            }
            else if (siteCourant == "SITE MOBILE")
            {
                Site = db.Sites.Where(m => m.NomSite.ToUpper() == "SITE MOBILE").FirstOrDefault();
            }


            if (Site != null)
            {
                SiteId = Site.ID;
                ViewBag.SiteID = new SelectList(db.Sites, "ID", "NomSite", SiteId).OrderBy(x => x.Text); }
            else
            {
                ViewBag.SiteID = new SelectList(db.Sites, "ID", "NomSite"); }

            ViewBag.PrioriteID = new SelectList(db.Priorites, "ID", "NomPriorite").OrderBy(x => x.Text);
            //ViewBag.TypeUsagerID = new SelectList(db.TypeUsagers, "ID", "NomTypeUsager");


            var listTypeUsager = new SelectList(db.TypeUsagers, "ID", "NomTypeUsager").ToList();         
            listTypeUsager.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.TypeUsagerID = listTypeUsager.OrderBy(x => x.Text);


            var listTypePrelevement = new SelectList(db.TypePrelevements, "ID", "NomTypePrelevement").ToList();
            listTypePrelevement.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.TypePrelevementID = listTypePrelevement.OrderBy(x => x.Text);

            var listPays = new SelectList(db.Pays, "ID", "Description").ToList();
            listPays.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.PaysID = listPays.OrderBy(x => x.Text);

            Demandes model = new Demandes();
            ViewBag.CurrentDate = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
            return View(model);

        }

        // POST: Demandes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Super utilisateur, Utilisateur")]
        public ActionResult Create(Demandes demandes)
        {
            bool erreur = false;
            ViewBag.ErrorRamq = "";
            ViewBag.ErrorTel = "";
            ViewBag.ErrorDateNaissance = "";
            ViewBag.ErrorDateEntree = "";
            ViewBag.ErrorVoyage = "";

            ViewBag.SiteID = new SelectList(db.Sites, "ID", "NomSite", demandes.SiteID);
            ViewBag.PrioriteID = new SelectList(db.Priorites, "ID", "NomPriorite", demandes.PrioriteID);
            //ViewBag.TypeUsagerID = new SelectList(db.TypeUsagers, "ID", "NomTypeUsager", demandes.TypeUsagerID);

            var listTypeUsager = new SelectList(db.TypeUsagers, "ID", "NomTypeUsager", demandes.TypeUsagerID).ToList();
            listTypeUsager.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.TypeUsagerID = listTypeUsager.OrderBy(m => m.Value);

            var listTypePrelevement = new SelectList(db.TypePrelevements, "ID", "NomTypePrelevement", demandes.TypePrelevementID).ToList();
            listTypePrelevement.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.TypePrelevementID = listTypePrelevement.OrderBy(m => m.Value);

            var listPays = new SelectList(db.Pays, "ID", "Description", demandes.PaysID).ToList();
            listPays.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.PaysID = listPays.OrderBy(m => m.Text);

            if (ModelState.IsValid)
            {
                if (demandes.Ramq == null && demandes.DateNaissance == null)
                {
                    ModelState.AddModelError("error", "En l'absence du NAM, vous devez renseigner la date de naissance");
                    ViewBag.ErrorRamq = "En l'absence du NAM, vous devez renseigner la date de naissance";
                    erreur = true;
                }

                if (demandes.Telephone == null && demandes.Courriel == null)
                {
                    ModelState.AddModelError("error", "En l'absence d'un numéro de téléphone, vous devez renseigner le courriel");
                    ViewBag.ErrorTel = "En l'absence d'un numéro de téléphone, vous devez renseigner le courriel";
                    erreur = true;

                }
                if (demandes.DateNaissance > DateTime.Now)
                {
                    ModelState.AddModelError("error", "La date de naissance doit être antérieure à la date d'aujourd'hui");
                    @ViewBag.ErrorDateNaissance = "La date de naissance doit être antérieure à la date d'aujourd'hui";
                    erreur = true;
                }
                if (demandes.DateNaissance < DateTime.Now.AddYears(-150))
                {
                    ModelState.AddModelError("error", "La date de naissance semble incorrecte.");
                    @ViewBag.ErrorDateNaissance = "La date de naissance semble incorrecte.";
                    erreur = true;
                }
                if (demandes.DateEntree > DateTime.Now)
                {
                    ModelState.AddModelError("error", "La date de retour doit être antérieure à la date d'aujourd'hui");
                    ViewBag.ErrorDateEntree = "La date de retour doit être antérieure à la date d'aujourd'hui";
                    erreur = true;
                }

                if (demandes.DateEntree < DateTime.Now.AddYears(-150))
                {
                    ModelState.AddModelError("error", "La date de retour semble incorrecte.");
                    ViewBag.ErrorDateEntree = "La date de retour semble incorrecte.";
                    erreur = true;
                }

                if (demandes.Voyage == "O" && demandes.PaysID == null)
                {
                    ModelState.AddModelError("error", "Le pays est obligatoire lorsque le voyage hors du Québec est selectionné.");
                    ViewBag.ErrorVoyage = "Le pays est obligatoire lorsque le voyage hors du Québec est selectionné.";
                    erreur = true;
                }                
                if (erreur) return View();

                var resultat = from recordset in dbRoles.Users
                               where ((recordset.UserName == userNameConnected))
                               select recordset;
                string idAspNetUser = resultat.First().Id;
                if (idAspNetUser != null)
                {
                    demandes.IdUser = idAspNetUser;
                }
                demandes.CreatedBy = userNameConnected;
                demandes.CreatedOn = DateTime.Now;
                demandes.Statut = "A";
                demandes.CodePostal = demandes.CodePostal.ToUpper();
                demandes.Nom = demandes.Nom.ToUpper();
                demandes.Prenom = demandes.Prenom.ToUpper();


                if (demandes.Ramq != null) demandes.Ramq = demandes.Ramq.ToUpper();
                db.Demandes.Add(demandes);

                try
                {
                    db.SaveChanges();
                    ViewBag.succes = "Vos modifications ont été enregistrées avec succès.";
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("Cannot insert duplicate"))
                    {
                        ViewBag.Exception = "Le NAM et la date du Rendez-vous entrées existent dèjà dans le système.";
                    }
                    else
                    {
                        ViewBag.Exception = "Veuillez contacter l'administrateur de l'application.";
                        ModelState.AddModelError("", ex.InnerException.InnerException.Message);
                    }
                    return View(demandes);
                }
                int lastDemandeId = db.Demandes.Max(item => item.ID);
                Demandes dem = db.Demandes.Find(lastDemandeId);
                if (dem == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("Edit", "Demandes", new { id = dem.ID, param = "ok" });
                //return RedirectToAction("Index");
            }
            return View(demandes);
        }
        // GET: Demandes/Edit/5
        [Authorize(Roles = "Admin, Super utilisateur, Utilisateur")]
        public ActionResult Edit(int? id, string param)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ViewBag.SiteList = db.Sites.Select(x => new { x.ID, x.NomSite }).ToList();
            Demandes demandes = db.Demandes.Find(id);
            if (demandes == null)
            {
                return HttpNotFound();
            }
            if (demandes.TypeUsagerID != 1 && demandes.TypeUsagerID != 6)
            {
                demandes.Matricule = null;
            }
            ViewBag.SiteID = new SelectList(db.Sites, "ID", "NomSite", demandes.SiteID).OrderBy(x => x.Text);
            ViewBag.PrioriteID = new SelectList(db.Priorites, "ID", "NomPriorite", demandes.PrioriteID).OrderBy(x => x.Text);
            //ViewBag.TypeUsagerID = new SelectList(db.TypeUsagers, "ID", "NomTypeUsager", demandes.TypeUsagerID);

            var listTypeUsager = new SelectList(db.TypeUsagers, "ID", "NomTypeUsager", demandes.TypeUsagerID).ToList();
            listTypeUsager.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.TypeUsagerID = listTypeUsager.OrderBy(x => x.Text);


            var listTypePrelevement = new SelectList(db.TypePrelevements, "ID", "NomTypePrelevement", demandes.TypePrelevementID).ToList();
            listTypePrelevement.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.TypePrelevementID = listTypePrelevement.OrderBy(x => x.Text);

            var listPays = new SelectList(db.Pays, "ID", "Description", demandes.PaysID).ToList();
            listPays.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.PaysID = listPays.OrderBy(x => x.Text);


            if (param == "ok") ViewBag.succes = "La fiche a été enregistrée avec succès.";
            return View(demandes);
        }

        // POST: Demandes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.





        /******************************************************************************************************************/
        // GET: Demandes/Edit/5
        public JsonResult CreateEdit(int? id, string action)      
        {            
            //ViewBag.SiteList = db.Sites.Select(x => new { x.ID, x.NomSite }).ToList();
            Demandes demandes = db.Demandes.Find(id);           
            if (demandes.TypeUsagerID != 1 && demandes.TypeUsagerID != 6)
            {
                demandes.Matricule = null;
            }
            ViewBag.SiteID = new SelectList(db.Sites, "ID", "NomSite", demandes.SiteID).OrderBy(x => x.Text);
            ViewBag.PrioriteID = new SelectList(db.Priorites, "ID", "NomPriorite", demandes.PrioriteID).OrderBy(x => x.Text);
            //ViewBag.TypeUsagerID = new SelectList(db.TypeUsagers, "ID", "NomTypeUsager", demandes.TypeUsagerID);
            var listTypeUsager = new SelectList(db.TypeUsagers, "ID", "NomTypeUsager", demandes.TypeUsagerID).ToList();
            listTypeUsager.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.TypeUsagerID = listTypeUsager.OrderBy(x => x.Text);

            var listTypePrelevement = new SelectList(db.TypePrelevements, "ID", "NomTypePrelevement", demandes.TypePrelevementID).ToList();
            listTypePrelevement.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.TypePrelevementID = listTypePrelevement.OrderBy(x => x.Text);

            var listPays = new SelectList(db.Pays, "ID", "Description", demandes.PaysID).ToList();
            listPays.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.PaysID = listPays.OrderBy(x => x.Text);

            var Data = new { ok = true, modify = true, action, id = id };
            return Json(Data, JsonRequestBehavior.AllowGet);
           
        }

        // GET
        public ActionResult Creat(int id)
        {   
            //ViewBag.SiteList = db.Sites.Select(x => new { x.ID, x.NomSite }).ToList();
            Demandes demandes = db.Demandes.Find(id);
            if (demandes == null)
            {
                return HttpNotFound();
            }
            if (demandes.TypeUsagerID != 1 && demandes.TypeUsagerID != 6)
            {
                demandes.Matricule = null;
            }


            ViewBag.SiteID = new SelectList(db.Sites, "ID", "NomSite", demandes.SiteID).OrderBy(x => x.Text);
            ViewBag.PrioriteID = new SelectList(db.Priorites, "ID", "NomPriorite", demandes.PrioriteID).OrderBy(x => x.Text);
            //ViewBag.TypeUsagerID = new SelectList(db.TypeUsagers, "ID", "NomTypeUsager", demandes.TypeUsagerID);

            var listTypeUsager = new SelectList(db.TypeUsagers, "ID", "NomTypeUsager", demandes.TypeUsagerID).ToList();
            listTypeUsager.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.TypeUsagerID = listTypeUsager.OrderBy(x => x.Text);


            var listTypePrelevement = new SelectList(db.TypePrelevements, "ID", "NomTypePrelevement", demandes.TypePrelevementID).ToList();
            listTypePrelevement.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.TypePrelevementID = listTypePrelevement.OrderBy(x => x.Text);

            var listPays = new SelectList(db.Pays, "ID", "Description", demandes.PaysID).ToList();
            listPays.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.PaysID = listPays.OrderBy(x => x.Text);

            return View(demandes);
        }
        /*****************************************************************************************************************/

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Super utilisateur, Utilisateur")]
        public ActionResult Creat(Demandes demandes)
        {
         //   demandes.ID = demandes.ID + 1;
            demandes.LastModifiedOn = null;
            demandes.ModifiedBy = null;
            demandes.CreatedBy = userNameConnected;
            demandes.CreatedOn = DateTime.Now;
            int lastDemandeId = demandes.ID;
            bool erreur = false;
            ViewBag.ErrorRamq = "";
            ViewBag.ErrorTel = "";
            ViewBag.ErrorDateNaissance = "";           
            ViewBag.ErrorDateEntree = "";
            ViewBag.ErrorVoyage = "";

            ViewBag.SiteID = new SelectList(db.Sites, "ID", "NomSite", demandes.SiteID);
            ViewBag.PrioriteID = new SelectList(db.Priorites, "ID", "NomPriorite");
            //ViewBag.TypeUsagerID = new SelectList(db.TypeUsagers, "ID", "NomTypeUsager", demandes.TypeUsagerID);

            var listTypeUsager = new SelectList(db.TypeUsagers, "ID", "NomTypeUsager", demandes.TypeUsagerID).ToList();
            listTypeUsager.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.TypeUsagerID = listTypeUsager.OrderBy(m => m.Value);

            var listTypePrelevement = new SelectList(db.TypePrelevements, "ID", "NomTypePrelevement", demandes.TypePrelevementID).ToList();
            listTypePrelevement.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.TypePrelevementID = listTypePrelevement.OrderBy(m => m.Value);
            // ModelState.Remove("DateNaissance");

            var listPays = new SelectList(db.Pays, "ID", "Description", demandes.PaysID).ToList();
            listPays.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.PaysID = listPays.OrderBy(x => x.Text);


            if (ModelState.IsValid)
            {
                if (demandes.Ramq == null && demandes.DateNaissance == null)
                {
                    ModelState.AddModelError("error", "En l'absence du NAM, vous devez renseigner la date de naissance");
                    ViewBag.ErrorRamq = "En l'absence du NAM, vous devez renseigner la date de naissance";
                    erreur = true;
                }

                if (demandes.Telephone == null && demandes.Courriel == null)
                {
                    ModelState.AddModelError("error", "En l'absence d'un numéro de téléphone, vous devez renseigner le courriel");
                    ViewBag.ErrorTel = "En l'absence d'un numéro de téléphone, vous devez renseigner le courriel";
                    erreur = true;

                }

                if (demandes.DateNaissance > DateTime.Now)
                {
                    ModelState.AddModelError("error", "La date de naissance doit être antérieure à la date d'aujourd'hui");
                    ViewBag.ErrorDateNaissance = "La date de naissance doit être antérieure à la date d'aujourd'hui";
                    erreur = true;
                }

                if (demandes.DateNaissance < DateTime.Now.AddYears(-150))
                {
                    ModelState.AddModelError("error", "La date de naissance semble incorrecte.");
                    ViewBag.ErrorDateNaissance = "La date de naissance semble incorrecte.";
                    erreur = true;
                }

                if (demandes.DateEntree > DateTime.Now)
                {
                    ModelState.AddModelError("error", "La date de naissance doit être antérieure à la date d'aujourd'hui");
                    ViewBag.DateEntree = "La date de naissance doit être antérieure à la date d'aujourd'hui";
                    erreur = true;
                }

                if (demandes.DateEntree < DateTime.Now.AddYears(-150))
                {
                    ModelState.AddModelError("error", "La date de naissance semble incorrecte.");
                    ViewBag.DateEntree = "La date de naissance semble incorrecte.";
                    erreur = true;
                }

                if (demandes.Voyage == "O" && demandes.PaysID == null)
                {
                    ModelState.AddModelError("error", "Le pays est obligatoire lorsque le voyage hors du Québec est selectionné.");
                    ViewBag.ErrorVoyage = "Le pays est obligatoire lorsque le voyage hors du Québec est selectionné.";
                    erreur = true;
                }
                if (erreur) return View();
                var resultat = from recordset in dbRoles.Users
                               where ((recordset.UserName == userNameConnected))
                               select recordset;
                string idAspNetUser = resultat.First().Id;                          

                    try
                    {
                        db.Demandes.Add(demandes);
                        db.SaveChanges();
                        lastDemandeId = db.Demandes.Max(item => item.ID);
                        ViewBag.succes = "la fiche a été été créee avec succès.";

                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.InnerException.Message.Contains("Cannot insert duplicate"))
                        {
                            ViewBag.Exception = "Le NAM et la date du Rendez-vous entrées existent dèjà dans le système.";
                        }
                        else
                        {
                            ViewBag.Exception = "Veuillez contacter l'administrateur de l'application.";
                            ModelState.AddModelError("", ex.InnerException.InnerException.Message);
                        }
                    }                
            }
            
            return RedirectToAction("Edit", "Demandes", new { id = lastDemandeId, param = "ok" });
            //return View(demandes);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Super utilisateur, Utilisateur")]
        public ActionResult Edit(Demandes demandes)
        {
            bool erreur = false;
            ViewBag.ErrorRamq = "";
            ViewBag.ErrorTel = "";
            ViewBag.ErrorDateNaissance = "";
            ViewBag.ErrorDateEntree = "";
            ViewBag.ErrorVoyage = "";

            if (demandes.Voyage == "N")
            {
                demandes.Destination = null;
                demandes.DateEntree = null;
            }

            ViewBag.SiteID = new SelectList(db.Sites, "ID", "NomSite", demandes.SiteID);
            ViewBag.PrioriteID = new SelectList(db.Priorites, "ID", "NomPriorite", demandes.PrioriteID);
            //ViewBag.TypeUsagerID = new SelectList(db.TypeUsagers, "ID", "NomTypeUsager", demandes.TypeUsagerID);

            var listTypeUsager = new SelectList(db.TypeUsagers, "ID", "NomTypeUsager", demandes.TypeUsagerID).ToList();
            listTypeUsager.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.TypeUsagerID = listTypeUsager.OrderBy(m => m.Value);


            var listTypePrelevement = new SelectList(db.TypePrelevements, "ID", "NomTypePrelevement", demandes.TypePrelevementID).ToList();
            listTypePrelevement.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.TypePrelevementID = listTypePrelevement.OrderBy(m => m.Value);

            var listPays = new SelectList(db.Pays, "ID", "Description", demandes.PaysID).ToList();
            listPays.Add(new SelectListItem() { Value = "", Text = "--- Veuillez choisir ---", Selected = true });
            ViewBag.PaysID = listPays.OrderBy(x => x.Text);

            if (ModelState.IsValid)
            {
                if (demandes.Ramq == null && demandes.DateNaissance == null)
                {
                    ModelState.AddModelError("error", "En l'absence du NAM, vous devez renseigner la date de naissance");
                    ViewBag.ErrorRamq = "En l'absence du NAM, vous devez renseigner la date de naissance";
                    erreur = true;
                }

                if (demandes.Telephone == null && demandes.Courriel == null)
                {
                    ModelState.AddModelError("error", "En l'absence d'un numéro de téléphone, vous devez renseigner le courriel");
                    ViewBag.ErrorTel = "En l'absence d'un numéro de téléphone, vous devez renseigner le courriel";
                    erreur = true;

                }

                if (demandes.DateNaissance > DateTime.Now)
                {
                    ModelState.AddModelError("error", "La date de naissance doit être antérieure à la date d'aujourd'hui");
                    ViewBag.ErrorDateNaissance = "La date de naissance doit être antérieure à la date d'aujourd'hui";
                    erreur = true;
                }

                if (demandes.DateNaissance < DateTime.Now.AddYears(-150))
                {
                    ModelState.AddModelError("error", "La date de naissance semble incorrecte.");
                    ViewBag.ErrorDateNaissance = "La date de naissance semble incorrecte.";
                    erreur = true;
                }


                if (demandes.DateEntree > DateTime.Now)
                {
                    ModelState.AddModelError("error", "La date de retour doit être antérieure à la date d'aujourd'hui");
                    ViewBag.ErrorDateEntree = "La date de retour doit être antérieure à la date d'aujourd'hui";
                    erreur = true;
                }

                if (demandes.DateEntree < DateTime.Now.AddYears(-150))
                {
                    ModelState.AddModelError("error", "La date de retour semble incorrecte.");
                    ViewBag.ErrorDateEntree = "La date de retour semble incorrecte.";
                    erreur = true;
                }

                if (demandes.Voyage == "O" && demandes.PaysID == null)
                {
                    ModelState.AddModelError("error", "Le pays est obligatoire lorsque le voyage hors du Québec est selectionné.");
                    ViewBag.ErrorVoyage = "Le pays est obligatoire lorsque le voyage hors du Québec est selectionné.";
                    erreur = true;
                }



                if (erreur) return View();
                var resultat = from recordset in dbRoles.Users
                               where ((recordset.UserName == userNameConnected))
                               select recordset;
                string idAspNetUser = resultat.First().Id;
                if (idAspNetUser != null)
                {
                    demandes.IdUser = idAspNetUser;
                }


                if (ModelState.IsValid)
                {
                    db.Entry(demandes).State = EntityState.Modified;
                    demandes.CodePostal = demandes.CodePostal.ToUpper();
                    demandes.Nom = demandes.Nom.ToUpper();
                    demandes.Prenom = demandes.Prenom.ToUpper();

                    if (demandes.TypeUsagerID != 1 && demandes.TypeUsagerID != 6)
                    {
                        demandes.Matricule = null;
                    }

                    if (demandes.Ramq != null) demandes.Ramq = demandes.Ramq.ToUpper();
                    demandes.ModifiedBy = userNameConnected;
                    demandes.LastModifiedOn = DateTime.Now;
                    try
                    {
                        db.SaveChanges();
                        ViewBag.succes = "Vos modifications ont été enregistrées avec succès.";
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.InnerException.Message.Contains("Cannot insert duplicate"))
                        {
                            ViewBag.Exception = "Le NAM et la date du Rendez-vous entrées existent dèjà dans le système.";
                        }
                        else
                        {
                            ViewBag.Exception = "Veuillez contacter l'administrateur de l'application.";
                            ModelState.AddModelError("", ex.InnerException.InnerException.Message);
                        }
                    }
                }
            }
            return View(demandes);
        }

        // GET: Demandes/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demandes demandes = db.Demandes.Find(id);
            if (demandes == null)
            {
                return HttpNotFound();
            }
            return View(demandes);
        }

        // POST: Demandes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Demandes demandes = db.Demandes.Find(id);
            db.Demandes.Remove(demandes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, Super utilisateur, Utilisateur")]
        public ActionResult Activer(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demandes demandes = db.Demandes.Find(id);
            if (demandes == null)
            {
                return HttpNotFound();
            }
            else
            {
                demandes.Statut = "A";
                demandes.ModifiedBy = userNameConnected;
                demandes.LastModifiedOn = DateTime.Now;
                db.SaveChanges();
            }

            return RedirectToAction("../Home/ListUsagers");
        }

        [Authorize(Roles = "Admin, Super utilisateur, Utilisateur")]
        public ActionResult Desactiver(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demandes demandes = db.Demandes.Find(id);
            if (demandes == null)
            {
                return HttpNotFound();
            }
            else
            {
                demandes.Statut = "I";
                demandes.ModifiedBy = userNameConnected;
                demandes.LastModifiedOn = DateTime.Now;
                db.SaveChanges();
            }

            return RedirectToAction("../Home/ListUsagers");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        protected void ExportToExcel<T>(List<T> data, string fileName)
        {
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment;  filename=Contact.xlsx");
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        [Authorize(Roles = "Admin, Super utilisateur, Utilisateur")]
        public ActionResult ExportToExcel(DateTime? dateDebut, DateTime? dateFin)
        {
            DateTime dd = dateDebut.HasValue ? dateDebut.Value.Date : DateTime.Now.AddYears(-100);
            DateTime df = dateFin.HasValue ? dateFin.Value.Date.AddDays(+1) : DateTime.Now.AddDays(+1);
            var entities = db.Demandes.Where(m => m.CreatedOn > dd && m.CreatedOn < df && m.Statut != "I").OrderByDescending(x => x.ID).ToList();
            //var entities = from d in db.Demandes.OrderByDescending(x => x.ID)
            //                select new { d};
            var data = entities.Select(x => new
            {
                Reference = x.ID.ToString("0000000"),
                NAM = x.Ramq,
                Nom = x.Nom,
                Prenom = x.Prenom,
                Sexe = x.Sexe == 1 ? "Masculin" : x.Sexe == 2 ? "Féminin" : x.Sexe == 3 ? "Inconnu" : " ",
                DateNaissance = x.DateNaissance.HasValue ? x.DateNaissance.Value.Day.ToString("00") + "-" + x.DateNaissance.Value.Month.ToString("00") + "-" + x.DateNaissance.Value.Year.ToString("0000") : "",
                CodePostal = x.CodePostal,
                Telephone = x.Telephone,
                Courriel = x.Courriel,
                DateRdv = x.DateRdv.HasValue ? x.DateRdv.Value.Day.ToString("00") + "-" + x.DateRdv.Value.Month.ToString("00") + "-" + x.DateRdv.Value.Year.ToString("0000") : "",
                HeureRdv = x.HeureRdv.HasValue ? x.HeureRdv.Value.Hour.ToString("00") + ":" + x.HeureRdv.Value.Minute.ToString("00") : "",
                Surnombre = x.SurNombre == "O" ? "Oui" : "Non",
                Site = x.Sites.NomSite,
                Langue = x.Langue == "F" ? "Français" : x.Langue == "A" ? "Anglais" : " ",
                Present = x.Presence == "O" ? "Oui" : x.Presence == "N" ? "Non" : " ",
                TypeUsager = x.TypeUsagerID != null ? x.TypeUsagers.NomTypeUsager : " ",
                Matricule = x.Matricule,
                Priorite = x.PrioriteID != null ? x.Priorites.NomPriorite : "",
                TypePrelevement = x.TypePrelevementID != null ? x.TypePrelevements.NomTypePrelevement : " ",
                Voyage = x.Voyage == "O" ? "Oui" : "Non",
                Pays = x.Voyage == "O" ? x.Pays.Description : String.Empty,
                Region = x.Voyage == "O" ? x.Destination : String.Empty,
                CreePar = x.CreatedBy.ToUpper(),
                CreeLe = x.CreatedOn,
                ModifiePar = x.ModifiedBy != null ? x.ModifiedBy.ToUpper() : "",
                ModifieLe = x.LastModifiedOn.HasValue ? x.LastModifiedOn.Value.ToString() : "",
            });


            // .ToList();
            //     ExportToExcel(data, "Liste_usagers");
            //return null;
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable("Lites usagers");
                dt.Columns.AddRange(new DataColumn[26] {
                                            new DataColumn("Réfence"),
                                            new DataColumn("NAM"),
                                            new DataColumn("Nom"),
                                            new DataColumn("Prénom"),
                                            new DataColumn("Sexe"),
                                            new DataColumn("Date Naissance"),
                                            new DataColumn("Code Postal"),
                                            new DataColumn("Téléphone"),
                                            new DataColumn("Courriel"),
                                            new DataColumn("Date Rendez-vous"),
                                            new DataColumn("Heure Rendez-vous"),
                                            new DataColumn("Surnombre"),
                                            new DataColumn("Site"),
                                            new DataColumn("Langue"),
                                            new DataColumn("Présent"),
                                            new DataColumn("Type usager"),
                                            new DataColumn("Matricule"),
                                            new DataColumn("#Priorité"),
                                            new DataColumn("#Crée par"),
                                            new DataColumn("#Crée le"),
                                            new DataColumn("#Modifié par"),
                                            new DataColumn("#Modifié le"),
                                            new DataColumn("Type prélèvement "),
                                            new DataColumn("a voyagé ?"),
                                            new DataColumn("Pays"),
                                            new DataColumn("Région "),                                           

                });
                foreach (var item in data)
                {
                    dt.Rows.Add(item.Reference, item.NAM, item.Nom, item.Prenom, item.Sexe, item.DateNaissance, item.CodePostal, item.Telephone, item.Courriel, item.DateRdv, item.HeureRdv, item.Surnombre, item.Site, item.Langue, item.Present, item.TypeUsager, item.Matricule, item.Priorite, item.CreePar, item.CreeLe, item.ModifiePar, item.ModifieLe, item.TypePrelevement, item.Voyage, item.Pays, item.Region);
                }

                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Liste usagers.xlsx");
                }
            }
        }
        [Authorize(Roles = "Utilisateur-SST, Admin")]
        public ActionResult ExportToExcelEmployees(DateTime? dateDebut, DateTime? dateFin, int? typeUsager)
        {
            DateTime dd = dateDebut.HasValue ? dateDebut.Value.Date : DateTime.Now.AddYears(-100);
            DateTime df = dateDebut.HasValue ? dateFin.Value.Date.AddDays(+1) : DateTime.Now.AddYears(+1);
            //var entities = from d in db.Demandes.OrderByDescending(x => x.ID)
            //                select new { d};

            var entities = db.Demandes.Where(m => m.ID == 0 ).OrderByDescending(x => x.ID).ToList(); //Initialisation


            if (typeUsager != null && typeUsager != 0)
            {
                 entities = db.Demandes.Where(m => m.CreatedOn > dd && m.CreatedOn < df && m.Statut != "I" && m.TypeUsagerID == typeUsager).OrderByDescending(x => x.ID).ToList();
            }
            else
            {
                 entities = db.Demandes.Where(m => m.CreatedOn > dd && m.CreatedOn < df && m.Statut != "I" && (m.TypeUsagerID == 1 || m.TypeUsagerID == 2)).OrderByDescending(x => x.ID).ToList();
            }

           

            var data = entities.Select(x => new
            {
                Reference = x.ID.ToString("0000000"),
                NAM = x.Ramq,
                Nom = x.Nom,
                Prenom = x.Prenom,
                Sexe = x.Sexe == 1 ? "Masculin" : x.Sexe == 2 ? "Féminin" : x.Sexe == 3 ? "Inconnu" : " ",
                DateNaissance = x.DateNaissance.HasValue ? x.DateNaissance.Value.Day.ToString("00") + "-" + x.DateNaissance.Value.Month.ToString("00") + "-" + x.DateNaissance.Value.Year.ToString("0000") : "",
                CodePostal = x.CodePostal,
                Telephone = x.Telephone,
                Courriel = x.Courriel,
                DateRdv = x.DateRdv.HasValue ? x.DateRdv.Value.Day.ToString("00") + "-" + x.DateRdv.Value.Month.ToString("00") + "-" + x.DateRdv.Value.Year.ToString("0000") : "",
                HeureRdv = x.HeureRdv.HasValue ? x.HeureRdv.Value.Hour.ToString("00") + ":" + x.HeureRdv.Value.Minute.ToString("00") : "",
                Surnombre = x.SurNombre == "O" ? "Oui" : "Non",
                Site = x.Sites.NomSite,
                Langue = x.Langue == "F" ? "Français" : x.Langue == "A" ? "Anglais" : " ",
                Present = x.Presence == "O" ? "Oui" : x.Presence == "N" ? "Non" : " ",
                TypeUsager = x.TypeUsagerID != null ? x.TypeUsagers.NomTypeUsager : " ",
                Matricule = x.Matricule,
                Priorite = x.PrioriteID != null ? x.Priorites.NomPriorite : "",
                TypePrelevement = x.TypePrelevementID != null ? x.TypePrelevements.NomTypePrelevement : " ",
            });


            // .ToList();
            //     ExportToExcel(data, "Liste_usagers");
            //return null;
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable("Lites employers");
                dt.Columns.AddRange(new DataColumn[19] {
                                            new DataColumn("Matricule"),
                                            new DataColumn("Réfence"),
                                            new DataColumn("NAM"),
                                            new DataColumn("Nom"),
                                            new DataColumn("Prénom"),
                                            new DataColumn("Sexe"),
                                            new DataColumn("Date Naissance"),
                                            new DataColumn("Code Postal"),
                                            new DataColumn("Téléphone"),
                                            new DataColumn("Courriel"),
                                            new DataColumn("Date Rendez-vous"),
                                            new DataColumn("Heure Rendez-vous"),
                                            new DataColumn("Surnombre"),
                                            new DataColumn("Site"),
                                            new DataColumn("Langue"),
                                            new DataColumn("Présent"),
                                            new DataColumn("Type usager"),
                                            new DataColumn("#Priorité"),
                                            new DataColumn("Type prélèvement "),
                });
                foreach (var item in data)
                {
                    dt.Rows.Add(item.Matricule, item.Reference, item.NAM, item.Nom, item.Prenom, item.Sexe, item.DateNaissance, item.CodePostal, item.Telephone, item.Courriel, item.DateRdv, item.HeureRdv, item.Surnombre, item.Site, item.Langue, item.Present, item.TypeUsager, item.Priorite, item.TypePrelevement);
                }

                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Liste usagers.xlsx");
                }
            }
        }


    }
}
