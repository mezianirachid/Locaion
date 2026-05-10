using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using Gnostice.StarDocsSDK;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Location.Models; using Location.DAL;
using Microsoft.Owin;
using System.Text.RegularExpressions;
using System.Data.Entity.SqlServer;
using System.Threading;



//Go to Tools menu (If using VS 2019 Extensions menu will be available on top)
//Select Extensions and Updates.
//Choose Online option
//Search for: rdlc report in search window as below
//Select Microsoft RDLC Report Designer and click download as shown below image link


namespace Location.Controllers
{
    [Authorize]
    //[Authorize(Roles = "Admin, Super Utilisateur, Responsable de stages, Responsable CAR")]
    public class ReportController : Controller
    {
        // GET: Rapports
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Locataires()
        {
            using (ApplicationConnection dc = new ApplicationConnection())
            {
                var v = dc.Locataires.ToList();
                return View(v);
            }
        }

        public ActionResult Meubles()
        {
            using (ApplicationConnection dc = new ApplicationConnection())
            {
                var v = dc.Meubles.ToList();
                return View(v);
            }
        }

        public ActionResult Payements()
        {          
            using (ApplicationConnection db = new ApplicationConnection())
            {
                var v = (from a in db.Payements
                                 join b in db.Baux on a.BauxId equals b.Id into r1
                                 from results1 in r1.DefaultIfEmpty()
                                 join c in db.Locataires on results1.LocataireId equals c.Id into r2
                                 from results2 in r2.DefaultIfEmpty()
                                 join d in db.Locataires on results1.CoLocataireId equals d.Id into r3
                                 from results3 in r3.DefaultIfEmpty()
                                 join e in db.Appartements on results1.AppartementId equals e.Id into r4
                                 from results4 in r4.DefaultIfEmpty()
                                 join g in db.Immeubles on results4.ImmeubleId equals g.Id into r5
                                 from results5 in r5.DefaultIfEmpty()
                                 join h in db.MoyenPayements on results1.MoyenPayementId equals h.Id into r6
                                 from results6 in r6.DefaultIfEmpty()
                                 select new PayementsVM
                                 {
                                     NumeroFacture = a.Id,
                                     Adresse = results4.AdresseAppartement + ", " + results5.AdresseImmeuble,
                                     NomCompletLocataire = results2.PrenomLocataire + ", " + results2.NomLocataire,
                                     NomCompletColocataire = results3.PrenomLocataire != null ? results3.PrenomLocataire + ", " + results3.NomLocataire : "",
                                     Annee = a.Annee,
                                     Mois = a.Mois,
                                     DatePayement = a.DatePayement.Value,                                    
                                     LieuPayement = a.LieuPayement,
                                     Description = a.Description,
                                     Prix = results1.Prix,
                                     Montant = a.Montant,
                                     NomMoyenPayement = results6.NomMoyenPayement,
                                 }
                             ).ToList();
                return View(v);
            }
        }

        public ActionResult Immeubles()
        {
            using (ApplicationConnection dc = new ApplicationConnection())
            {
                var v = dc.Immeubles.ToList();
                return View(v);
            }
        }
        public ActionResult Report(string typeRapport, string nomRapport, int idPayement)
        {
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            string reportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo;

           

            if (nomRapport == "Locataires")
            {
                LocalReport lr = new LocalReport();
                string path = Path.Combine(Server.MapPath("~/Report"), "Report_Locataires.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }          
                using (ApplicationConnection db = new ApplicationConnection())
                {
                    var v = (from a in db.Locataires
                             join b in db.Baux on a.Id equals b.LocataireId into r1
                             from results1 in r1.DefaultIfEmpty()
                             join e in db.Appartements on results1.AppartementId equals e.Id into r4
                             from results4 in r4.DefaultIfEmpty()
                             join g in db.Immeubles on results4.ImmeubleId equals g.Id into r5
                             from results5 in r5.DefaultIfEmpty()
                             select new LocatairesVM
                             {
                                 Nom = a.NomLocataire,
                                 Prenom = a.PrenomLocataire,
                                 Telephone = a.TeLocataire,
                                 Courriel = a.CourrielLocataire,
                                 Adresse = results4.AdresseAppartement + ", " + results5.AdresseImmeuble
                             }
                             );


                    List<LocatairesVM> cm = new List<LocatairesVM>();
                    cm = v.ToList();
                    ReportDataSource rd = new ReportDataSource("DataSet_Locataires", cm);
                    lr.DataSources.Add(rd);
                    reportType = typeRapport;
                    deviceInfo =
                    "<DeviceInfo>" +
                    "  <OutputFormat>" + typeRapport + "</OutputFormat>" +
                    "  <PageWidth>8.5in</PageWidth>" +
                    "  <PageHeight>11in</PageHeight>" +
                    "  <MarginTop>0.5in</MarginTop>" +
                    "  <MarginLeft>1in</MarginLeft>" +
                    "  <MarginRight>1in</MarginRight>" +
                    "  <MarginBottom>0.5in</MarginBottom>" +
                    "</DeviceInfo>";

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
            }
            else
            if (nomRapport == "Meubles")
            {
                LocalReport lr = new LocalReport();
                string path = Path.Combine(Server.MapPath("~/Report"), "Report_Meubles.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    return View("Meubles");
                }
                List<Meubles> cm = new List<Meubles>();
                using (ApplicationConnection dc = new ApplicationConnection())
                {
                    cm = dc.Meubles.ToList();
                }
                ReportDataSource rd = new ReportDataSource("DataSet_Meubles", cm);
                lr.DataSources.Add(rd);
                reportType = typeRapport;
                deviceInfo =
               "<DeviceInfo>" +
               "  <OutputFormat>" + typeRapport + "</OutputFormat>" +
               "  <PageWidth>8.5in</PageWidth>" +
               "  <PageHeight>11in</PageHeight>" +
               "  <MarginTop>0.5in</MarginTop>" +
               "  <MarginLeft>1in</MarginLeft>" +
               "  <MarginRight>1in</MarginRight>" +
               "  <MarginBottom>0.5in</MarginBottom>" +
               "</DeviceInfo>";
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
            else
            if (nomRapport == "Immeubles")
            {
                LocalReport lr = new LocalReport();
                string path = Path.Combine(Server.MapPath("~/Report"), "Report_Immeubles.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    return View("Immeubles");
                }
                List<Immeubles> cm = new List<Immeubles>();
                using (ApplicationConnection dc = new ApplicationConnection())
                {
                    cm = dc.Immeubles.ToList();
                }
                ReportDataSource rd = new ReportDataSource("DataSet_Immeubles", cm);
                lr.DataSources.Add(rd);
                reportType = typeRapport;
                deviceInfo =
               "<DeviceInfo>" +
               "  <OutputFormat>" + typeRapport + "</OutputFormat>" +
               "  <PageWidth>8.5in</PageWidth>" +
               "  <PageHeight>11in</PageHeight>" +
               "  <MarginTop>0.5in</MarginTop>" +
               "  <MarginLeft>1in</MarginLeft>" +
               "  <MarginRight>1in</MarginRight>" +
               "  <MarginBottom>0.5in</MarginBottom>" +
               "</DeviceInfo>";
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
            else
                if (nomRapport == "Payements")
            {               
                LocalReport lr = new LocalReport();               

                string path = Path.Combine(Server.MapPath("~/Report"), "Report_Payements.rdlc");
                if (System.IO.File.Exists(path))
                {                                       
                    lr.ReportPath = path;
                }
                else
                {
                    return View("Payements");
                }
                List<PayementsVM> cm = new List<PayementsVM>();
                using (ApplicationConnection db = new ApplicationConnection())
                {
                    var v = (from a in db.Payements
                             join b in db.Baux on a.BauxId equals b.Id into r1
                             from results1 in r1.DefaultIfEmpty()
                             join c in db.Locataires on results1.LocataireId equals c.Id into r2
                             from results2 in r2.DefaultIfEmpty()
                             join d in db.Locataires on results1.CoLocataireId equals d.Id into r3
                             from results3 in r3.DefaultIfEmpty()
                             join e in db.Appartements on results1.AppartementId equals e.Id into r4
                             from results4 in r4.DefaultIfEmpty()
                             join g in db.Immeubles on results4.ImmeubleId equals g.Id into r5
                             from results5 in r5.DefaultIfEmpty()
                             join h in db.MoyenPayements on results1.MoyenPayementId equals h.Id into r6
                             from results6 in r6.DefaultIfEmpty()
                             select new PayementsVM
                             {
                                 NumeroFacture = a.Id,
                                 Adresse = results4.AdresseAppartement + ", " + results5.AdresseImmeuble,
                                 NomCompletLocataire = results2.PrenomLocataire + ", " + results2.PrenomLocataire,
                                 NomCompletColocataire = results3.PrenomLocataire + ", " + results3.PrenomLocataire,
                                 Annee = a.Annee,
                                 Mois = a.Mois,
                                 DatePayement = a.DatePayement.Value,                                
                                 LieuPayement = a.LieuPayement,
                                 Description = a.Description,
                                 Prix = results1.Prix,
                                 Montant = a.Montant,
                                 NomMoyenPayement = results6.NomMoyenPayement,
                             }
                                    );
                    cm = v.ToList();


                    ReportDataSource rd = new ReportDataSource("DataSet_Payements", cm);
                    lr.DataSources.Add(rd);
                    reportType = typeRapport;
                    deviceInfo =
                   "<DeviceInfo>" +
                   "  <OutputFormat>" + typeRapport + "</OutputFormat>" +
                   "  <PageWidth>27.94cm</PageWidth>" +
                   "  <PageHeight>8.5in</PageHeight>" +
                   "  <MarginTop>0.5in</MarginTop>" +
                   "  <MarginLeft>1cmn</MarginLeft>" +
                   "  <MarginRight>1cm</MarginRight>" +
                   "  <MarginBottom>1cm</MarginBottom>" +
                   "</DeviceInfo>";
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
            }
            else 
                if (nomRapport == "Facture")
                {
                    LocalReport lr = new LocalReport();

                    string path = Path.Combine(Server.MapPath("~/Report"), "Report_Facture_Locataire.rdlc");
                    if (System.IO.File.Exists(path))
                    {
                        lr.ReportPath = path;
                    }
                    else
                    {
                        return View("Payements");
                    }
                    List<PayementsVM> cm = new List<PayementsVM>();
                    using (ApplicationConnection db = new ApplicationConnection())
                    {

                    var v = (from a in db.Payements.Where(x => x.Id == idPayement)
                             join b in db.Baux on a.BauxId equals b.Id into r1
                             from results1 in r1.DefaultIfEmpty()
                             join c in db.Locataires on results1.LocataireId equals c.Id into r2
                             from results2 in r2.DefaultIfEmpty()
                             join d in db.Locataires on results1.CoLocataireId equals d.Id into r3
                             from results3 in r3.DefaultIfEmpty()
                             join e in db.Appartements on results1.AppartementId equals e.Id into r4
                             from results4 in r4.DefaultIfEmpty()
                             join g in db.Immeubles on results4.ImmeubleId equals g.Id into r5
                             from results5 in r5.DefaultIfEmpty()
                             join h in db.MoyenPayements on results1.MoyenPayementId equals h.Id into r6
                             from results6 in r6.DefaultIfEmpty()
                             select new PayementsVM
                             {
                                 NumeroFacture = a.Id,
                                 Adresse = results4.AdresseAppartement + ", " + results5.AdresseImmeuble,
                                 NomCompletLocataire = results2.PrenomLocataire + ", " + results2.PrenomLocataire,
                                 NomCompletColocataire = results3.PrenomLocataire + ", " + results3.PrenomLocataire,
                                 Annee = a.Annee,
                                 Mois = a.Mois,
                                 DatePayement = a.DatePayement.Value,
                                 LieuPayement = a.LieuPayement,
                                 DatePayementFormated = (a.DatePayement.Value.Day.ToString().Length == 1 ? "0" + a.DatePayement.Value.Day.ToString(): a.DatePayement.Value.Day.ToString()) + "-" + (a.DatePayement.Value.Month.ToString().Length == 1 ? "0" + a.DatePayement.Value.Month.ToString() : a.DatePayement.Value.Month.ToString()) + "-" + a.DatePayement.Value.Year.ToString(),
                                 Description = a.Description,
                                 Prix = results1.Prix,
                                 Montant = a.Montant,
                                 NomMoyenPayement = results6.NomMoyenPayement,
                             }
                             );
                        cm = v.ToList();

                        ReportDataSource rd = new ReportDataSource("DataSet_Facture_Locataire", cm);
                        lr.DataSources.Add(rd);
                        reportType = typeRapport;
                        deviceInfo =
                       "<DeviceInfo>" +
                       "  <OutputFormat>" + typeRapport + "</OutputFormat>" +
                       "  <PageWidth>27.94cm</PageWidth>" +
                       "  <PageHeight>8.5in</PageHeight>" +
                       "  <MarginTop>0.5in</MarginTop>" +
                       "  <MarginLeft>1cmn</MarginLeft>" +
                       "  <MarginRight>1cm</MarginRight>" +
                       "  <MarginBottom>1cm</MarginBottom>" +
                       "</DeviceInfo>";
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
                }
                else
                    return File("", "");
            }
        }
    }