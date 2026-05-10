using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Globalization;

namespace Location.Controllers
{

    [Authorize(Roles = "Admin, Super utilisateur, Utilisateur")]
    //[Authorize(Roles = "Admin, Super Utilisateur, Responsable de stages, Responsable CAR")]
    public class RapportsController : Controller
    {
        // GET: Rapports
        public ActionResult Index()
        {
            return View("Index");
        }              

        public ActionResult Etiquettes(DateTime? dateDebut, DateTime? dateFin, int site)
        {
            try
            {
                string idTypeDoc = "PDF";
                LocalReport lr = new LocalReport();

                //string path = Path.Combine(Server.MapPath("~/Reporting/Views/"), "ListStagiaires.rdlc");
                string path = Path.Combine(Server.MapPath("~/RPTReport/"), "RDLCReport.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "ImportExcel");
                }  
                // List<Documents> cm = new List<Documents>();
                BDCovidCEMTLEntities db = new BDCovidCEMTLEntities();
                DateTime dd = dateDebut.HasValue ? dateDebut.Value.Date : DateTime.Now.AddYears(-1000);
                DateTime df = dateFin.HasValue ? dateFin.Value.Date.AddDays(+1) : DateTime.Now.AddYears(+1000);


                var demandesFiltred = db.Demandes.Where(x => x.CreatedOn > dd && x.CreatedOn < df && x.Statut != "I");

                if (site != 0) demandesFiltred = demandesFiltred.Where(x => x.SiteID == site);


                var cm = (from e in demandesFiltred
                           select new
                           {
                               ID = e.ID,
                               Nom = e.Nom.ToUpper(),
                               Prenom = e.Prenom.ToUpper(),
                               Telephone = e.Telephone != null ? "Tél:" + e.Telephone: String.Empty,
                               Courriel = e.Courriel,
                               Ramq = e.Ramq,
                               Autre = e.Sites.NomSite,
                               Matricule = e.TypeUsagerID == Enum.TypeUsagerIDEnum.EmployéCEMTL ? "Employé Cemtl: " + e.Matricule : String.Empty,
                               CodePostal = e.CodePostal,
                               DateNaissance = e.DateNaissance != null ?  "Né(e) le: " + e.DateNaissance.Value.Day + "-" + e.DateNaissance.Value.Month + "-" + e.DateNaissance.Value.Year : String.Empty,
                               TypeUsagerId = e.TypeUsagerID,
                               NomTypeUsager = (e.TypeUsagerID == Enum.TypeUsagerIDEnum.EmployéCEMTL ? "Employé Cemtl: " + e.Matricule : e.TypeUsagerID == Enum.TypeUsagerIDEnum.Militaire ? "Militaire: " + e.Matricule : e.TypeUsagers.NomTypeUsager),
                               Sexe = e.Sexe,
                               PrioriteID = e.PrioriteID,
                               NomPriorite = e.PrioriteID != null ? "Priorité:" + e.Priorites.NomPriorite + "(" + e.Priorites.CouleurPriorite + ")" : String.Empty,
                               CouleurPriorite = e.Priorites.CouleurPriorite,
                               SiteId = e.SiteID,
                               NomSite = e.Sites.NomSite,
                               NomTypePrelevement = e.TypePrelevements.NomTypePrelevement,
                               Voyage = e.Voyage == "O" ? "Oui" : "Non",
                               Destination = e.Destination,
                               Description = e.Voyage == "O" ? "Voyage : " + e.Pays.Description + (e.Destination != null ? "/" + e.Destination : "")  : "Voyage : Non"
                           });              

                var NombreEtiquetteParUsager = System.Configuration.ConfigurationManager.AppSettings["NombreEtiquetteParUsager"];
                var result = cm;
                if (NombreEtiquetteParUsager.ToString() == "2")
                    result = result.Concat(cm);
                else if (NombreEtiquetteParUsager.ToString() == "3")
                    result = result.Concat(cm).Concat(cm);
                 else if (NombreEtiquetteParUsager.ToString() == "4")
                    result = result.Concat(cm).Concat(cm).Concat(cm);
                else if (NombreEtiquetteParUsager.ToString() == "5")
                   result = result.Concat(cm).Concat(cm).Concat(cm).Concat(cm);

                ReportDataSource rd = new ReportDataSource("DataSetCovid", result.OrderBy(x => x.Nom).ToList());
                lr.DataSources.Add(rd);
                string reportType = idTypeDoc;
                string mimeType;
                string encoding;
                string fileNameExtension;
                string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>" + idTypeDoc + "</OutputFormat>" +
                "  <PageWidth>4in</PageWidth>" +
                "  <PageHeight>2in</PageHeight>" +
                "  <MarginTop>0.1in</MarginTop>" +
                "  <MarginLeft>0.1in</MarginLeft>" +
                "  <MarginRight>0.1in</MarginRight>" +
                "  <MarginBottom>0.1in</MarginBottom>" +
                "</DeviceInfo>";
                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;
                try { 
                    renderedBytes = lr.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);
                    return File(renderedBytes, mimeType);
                }
                catch(Exception ex)
                {
                    throw (ex); 
                }
               
            }
            catch (Exception ex)
            {
                throw (ex);
               
            }
        }

    }
}