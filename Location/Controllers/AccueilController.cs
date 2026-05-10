//using DocumentFormat.OpenXml.Vml;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using Location.Models; using Location.DAL;

namespace Location.Controllers
{
    public class AccueilController : Controller
    {
        private ApplicationConnection db = new ApplicationConnection();
        private string userNameConnected = System.Web.HttpContext.Current.User.Identity.Name;




        public ActionResult Accueil()
        {
            string message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text1").FirstOrDefault().Description;
            ViewBag.Text1 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text2").FirstOrDefault().Description;
            ViewBag.Text2 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text3").FirstOrDefault().Description;
            ViewBag.Text3 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text4").FirstOrDefault().Description;
            ViewBag.Text4 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text5").FirstOrDefault().Description;
            ViewBag.Text5 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text6").FirstOrDefault().Description;
            ViewBag.Text6 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text7").FirstOrDefault().Description;
            ViewBag.Text7 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text8").FirstOrDefault().Description;
            ViewBag.Text8 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text9").FirstOrDefault().Description;
            ViewBag.Text9 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text10").FirstOrDefault().Description;
            ViewBag.Text10 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text11").FirstOrDefault().Description;
            ViewBag.Text11 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text12").FirstOrDefault().Description;
            ViewBag.Text12 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text13").FirstOrDefault().Description;
            ViewBag.Text13 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text14").FirstOrDefault().Description;
            ViewBag.Text14 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text15").FirstOrDefault().Description;
            ViewBag.Text15 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text16").FirstOrDefault().Description;
            ViewBag.Text16 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text17").FirstOrDefault().Description;
            ViewBag.Text17 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text18").FirstOrDefault().Description;
            ViewBag.Text18 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text19").FirstOrDefault().Description;
            ViewBag.Text19 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text20").FirstOrDefault().Description;
            ViewBag.Text20 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text21").FirstOrDefault().Description;
            ViewBag.Text21 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text22").FirstOrDefault().Description;
            ViewBag.Text22 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text23").FirstOrDefault().Description;
            ViewBag.Text23 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text24").FirstOrDefault().Description;
            ViewBag.Text24 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text25").FirstOrDefault().Description;
            ViewBag.Text25 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text26").FirstOrDefault().Description;
            ViewBag.Text26 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text27").FirstOrDefault().Description;
            ViewBag.Text27 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text28").FirstOrDefault().Description;
            ViewBag.Text28 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text29").FirstOrDefault().Description;
            ViewBag.Text29 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text30").FirstOrDefault().Description;
            ViewBag.Text30 = message;
            return View(); //Vue partielle pour ignorer le css de Laout.cshtml
            //return Redirect("/Accueil/Index.html");           
        }
        public ActionResult Index()
        {
            string message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text1").FirstOrDefault().Description;
            ViewBag.Text1 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text2").FirstOrDefault().Description;
            ViewBag.Text2 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text3").FirstOrDefault().Description;
            ViewBag.Text3 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text4").FirstOrDefault().Description;
            ViewBag.Text4 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text5").FirstOrDefault().Description;
            ViewBag.Text5 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text6").FirstOrDefault().Description;
            ViewBag.Text6 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text7").FirstOrDefault().Description;
            ViewBag.Text7 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text8").FirstOrDefault().Description;
            ViewBag.Text8 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text9").FirstOrDefault().Description;
            ViewBag.Text9 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text10").FirstOrDefault().Description;
            ViewBag.Text10 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text11").FirstOrDefault().Description;
            ViewBag.Text11 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text12").FirstOrDefault().Description;
            ViewBag.Text12 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text13").FirstOrDefault().Description;
            ViewBag.Text13 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text14").FirstOrDefault().Description;
            ViewBag.Text14 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text15").FirstOrDefault().Description;
            ViewBag.Text15 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text16").FirstOrDefault().Description;
            ViewBag.Text16 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text17").FirstOrDefault().Description;
            ViewBag.Text17 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text18").FirstOrDefault().Description;
            ViewBag.Text18 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text19").FirstOrDefault().Description;
            ViewBag.Text19 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text20").FirstOrDefault().Description;
            ViewBag.Text20 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text21").FirstOrDefault().Description;
            ViewBag.Text21 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text22").FirstOrDefault().Description;
            ViewBag.Text22 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text23").FirstOrDefault().Description;
            ViewBag.Text23 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text24").FirstOrDefault().Description;
            ViewBag.Text24 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text25").FirstOrDefault().Description;
            ViewBag.Text25 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text26").FirstOrDefault().Description;
            ViewBag.Text26 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text27").FirstOrDefault().Description;
            ViewBag.Text27 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text28").FirstOrDefault().Description;
            ViewBag.Text28 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text29").FirstOrDefault().Description;
            ViewBag.Text29 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text30").FirstOrDefault().Description;
            ViewBag.Text30 = message;
            return View(); //Vue partielle pour ignorer le css de Laout.cshtml
                           //return Redirect("/Accueil/Index.html");      

        }
        // GET: AccueilE
        public ActionResult Achat()
        {
            string message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text61").FirstOrDefault().Description;
            ViewBag.Text61 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text62").FirstOrDefault().Description;
            ViewBag.Text62 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text63").FirstOrDefault().Description;
            ViewBag.Text63 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text64").FirstOrDefault().Description;
            ViewBag.Text64 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text65").FirstOrDefault().Description;
            ViewBag.Text65 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text66").FirstOrDefault().Description;
            ViewBag.Text66 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text67").FirstOrDefault().Description;
            ViewBag.Text67 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text68").FirstOrDefault().Description;
            ViewBag.Text68 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text69").FirstOrDefault().Description;
            ViewBag.Text69 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text70").FirstOrDefault().Description;
            ViewBag.Text70 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text71").FirstOrDefault().Description;
            ViewBag.Text71 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text72").FirstOrDefault().Description;
            ViewBag.Text72 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text73").FirstOrDefault().Description;
            ViewBag.Text73 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text74").FirstOrDefault().Description;
            ViewBag.Text74 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text75").FirstOrDefault().Description;
            ViewBag.Text75 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text76").FirstOrDefault().Description;
            ViewBag.Text76 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text77").FirstOrDefault().Description;
            ViewBag.Text77 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text78").FirstOrDefault().Description;
            ViewBag.Text78 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text79").FirstOrDefault().Description;
            ViewBag.Text79 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text80").FirstOrDefault().Description;
            ViewBag.Text80 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text81").FirstOrDefault().Description;
            ViewBag.Text81 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text82").FirstOrDefault().Description;
            ViewBag.Text82 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text83").FirstOrDefault().Description;
            ViewBag.Text83 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text84").FirstOrDefault().Description;
            ViewBag.Text84 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text85").FirstOrDefault().Description;
            ViewBag.Text85 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text86").FirstOrDefault().Description;
            ViewBag.Text86 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text87").FirstOrDefault().Description;
            ViewBag.Text87 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text88").FirstOrDefault().Description;
            ViewBag.Text88 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text89").FirstOrDefault().Description;
            ViewBag.Text89 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text90").FirstOrDefault().Description;
            ViewBag.Text90 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text91").FirstOrDefault().Description;
            ViewBag.Text91 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text92").FirstOrDefault().Description;
            ViewBag.Text92 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text93").FirstOrDefault().Description;
            ViewBag.Text93 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text94").FirstOrDefault().Description;
            ViewBag.Text94 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text95").FirstOrDefault().Description;
            ViewBag.Text95 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text96").FirstOrDefault().Description;
            ViewBag.Text96 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text97").FirstOrDefault().Description;
            ViewBag.Text97 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text98").FirstOrDefault().Description;
            ViewBag.Text98 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text99").FirstOrDefault().Description;
            ViewBag.Text99 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text100").FirstOrDefault().Description;
            ViewBag.Text100 = message;
            return View();//Vue partielle  pour ignorer le css de Laout.cshtml
        }
        // GET: Accueil
        public ActionResult Location()
        {
        
            string message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text31").FirstOrDefault().Description;
            ViewBag.Text31 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text32").FirstOrDefault().Description;
            ViewBag.Text32 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text33").FirstOrDefault().Description;
            ViewBag.Text33 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text34").FirstOrDefault().Description;
            ViewBag.Text34 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text35").FirstOrDefault().Description;
            ViewBag.Text35 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text36").FirstOrDefault().Description;
            ViewBag.Text36 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text37").FirstOrDefault().Description;
            ViewBag.Text37 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text38").FirstOrDefault().Description;
            ViewBag.Text38 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text39").FirstOrDefault().Description;
            ViewBag.Text39 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text40").FirstOrDefault().Description;
            ViewBag.Text40 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text41").FirstOrDefault().Description;
            ViewBag.Text41 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text42").FirstOrDefault().Description;
            ViewBag.Text42 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text43").FirstOrDefault().Description;
            ViewBag.Text43 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text44").FirstOrDefault().Description;
            ViewBag.Text44 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text45").FirstOrDefault().Description;
            ViewBag.Text45 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text46").FirstOrDefault().Description;
            ViewBag.Text46 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text47").FirstOrDefault().Description;
            ViewBag.Text47 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text48").FirstOrDefault().Description;
            ViewBag.Text48 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text49").FirstOrDefault().Description;
            ViewBag.Text49 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text50").FirstOrDefault().Description;
            ViewBag.Text50 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text51").FirstOrDefault().Description;
            ViewBag.Text51 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text52").FirstOrDefault().Description;
            ViewBag.Text52 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text53").FirstOrDefault().Description;
            ViewBag.Text53 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text54").FirstOrDefault().Description;
            ViewBag.Text54 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text55").FirstOrDefault().Description;
            ViewBag.Text55 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text56").FirstOrDefault().Description;
            ViewBag.Text56 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text57").FirstOrDefault().Description;
            ViewBag.Text57 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text58").FirstOrDefault().Description;
            ViewBag.Text58 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text59").FirstOrDefault().Description;
            ViewBag.Text59 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text60").FirstOrDefault().Description;
            ViewBag.Text60 = message;
            return View();//Vue partielle pour ignorer le css de Laout.cshtml
            //return Redirect("/Accueil/Index.html");      
        }
        // GET: Accueil
        public ActionResult Presentation()
        {
            string message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text101").FirstOrDefault().Description;
            ViewBag.Text101 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text102").FirstOrDefault().Description;
            ViewBag.Text102 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text103").FirstOrDefault().Description;
            ViewBag.Text103 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text104").FirstOrDefault().Description;
            ViewBag.Text104 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text105").FirstOrDefault().Description;
            ViewBag.Text105 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text106").FirstOrDefault().Description;
            ViewBag.Text106 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text107").FirstOrDefault().Description;
            ViewBag.Text107 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text108").FirstOrDefault().Description;
            ViewBag.Text108 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text109").FirstOrDefault().Description;
            ViewBag.Text109 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text110").FirstOrDefault().Description;
            ViewBag.Text110 = message;

            return View();//Vue partielle pour ignorer le css de Laout.cshtml
        }

        public ActionResult Contact()
        {
            string message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text101").FirstOrDefault().Description;
            ViewBag.Text101 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text102").FirstOrDefault().Description;
            ViewBag.Text102 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text103").FirstOrDefault().Description;
            ViewBag.Text103 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text104").FirstOrDefault().Description;
            ViewBag.Text104 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text105").FirstOrDefault().Description;
            ViewBag.Text105 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text106").FirstOrDefault().Description;
            ViewBag.Text106 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text107").FirstOrDefault().Description;
            ViewBag.Text107 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text108").FirstOrDefault().Description;
            ViewBag.Text108 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text109").FirstOrDefault().Description;
            ViewBag.Text109 = message;
            message = db.RefMessages.Where(m => m.Statut != "I" && m.Code == "Text110").FirstOrDefault().Description;
            ViewBag.Text110 = message;
            return View();//Vue partielle pour ignorer le css de Laout.cshtml
        }

        [HttpPost]
        public async Task<JsonResult> EnvoyerFormulaire(Contact contact)
        //public async Task<JsonResult> EnvoyerFormulaire(string Nom,  string Prenom, string Courriel, string Telephone, string Sujet, string Body, HttpPostedFileBase fileContact)
        {
            string fileName = "";
            string path = "";
            bool status = false;
            string result = "Erreur de telechargement";
            string nom = contact.Nom;
            string prenom = contact.Prenom;
            string courriel = contact.Courriel;
            string telephone = contact.Telephone;
            string sujet = contact.Sujet;
            string body = contact.Body;
            HttpPostedFileBase fileContact = contact.FileContact;
            if (fileContact != null && fileContact.ContentLength > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    fileName = Path.GetFileName(fileContact.FileName);
                    path = HttpContext.Server.MapPath("~/UploadedFiles/") + fileName;
                    //ajouter le  fichier dans le repertoire
                    fileContact.SaveAs(path);
                }
                catch (Exception e)
                {
                    status = false;
                    result = "Votre message n'a pas été envoyé à cause de l'erreur suivante:" + e.Message.ToString();
                    return new JsonResult { Data = new { statut = status, data = result } };
                }
            }

            body = "Nom: " + nom + "<br />"
                 + "Prenom: " + prenom + "<br />"
                 + "Courriel: " + courriel + "<br />"
                 + "Telephone: " + telephone + "<br />" + "<br />"

                 + body + "<br />"
                 ;

            string destinataire = ConfigurationManager.AppSettings["Destinataire"];
            string fromUsername = ConfigurationManager.AppSettings["FromEmailAddress"];
            //string fromAlias = ConfigurationManager.AppSettings["FromEmailDisplayName"];
            string fromPassword = ConfigurationManager.AppSettings["FromEmailPassword"];
            string smtpHost = ConfigurationManager.AppSettings["SMTPHost"];
            int smtpPort = Int32.Parse(ConfigurationManager.AppSettings["SMTPPort"]);

            SmtpClient smtp = new SmtpClient();
            smtp.Host = smtpHost;
            smtp.Port = smtpPort; // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Timeout = 30000;
            smtp.Credentials = new NetworkCredential(fromUsername, fromPassword);
            try
            {
                //smtp.Send(fromUsername, destinataire, sujet, body);
                MailMessage msg = new MailMessage(fromUsername, destinataire, sujet, body);

                if (fileContact != null && fileContact.ContentLength > 0)
                {
                    // Create  the file attachment for this email message.
                    Attachment data = new Attachment(path, MediaTypeNames.Application.Octet);
                    // Add time stamp information for the file.
                    ContentDisposition disposition = data.ContentDisposition;
                    disposition.CreationDate = System.IO.File.GetCreationTime(path);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(path);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(path);
                    // Add the file attachment to this email message.
                    msg.Attachments.Add(data);
                }
                msg.IsBodyHtml = true;
                await smtp.SendMailAsync(msg);
                status = true;
                result = "Votre message été envoyé avec succès";
                return new JsonResult { Data = new { statut = status, data = result } };
            }
            catch (Exception e)
            {
                status = false;
                result = "Votre message n'a pas été envoyé à cause de l'erreur suivante:" + e.Message.ToString();
                return new JsonResult { Data = new { statut = status, data = result } };
            }
        }
    }
}