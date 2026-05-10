using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace Location.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MessagesController : Controller
    {
        // GET: Messages
        public ActionResult Index()
        {
            return View();
        }

        // API pour récupérer les 20 derniers messages (résumé)
        public JsonResult GetMessages()
        {
            try
            {
                var messages = FetchRecentEmails(20); // limite à 20
                return Json(messages, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // API pour récupérer le contenu d'un message spécifique (par UID)
        public JsonResult GetMessageContent(string uid)
        {
            try
            {
                var content = FetchEmailContent(uid);
                return Json(new { success = true, content }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private List<object> FetchRecentEmails(int maxCount)
        {
            var server = ConfigurationManager.AppSettings["EmailServer"];
            var port = int.Parse(ConfigurationManager.AppSettings["EmailPort"]);
            var useSsl = bool.Parse(ConfigurationManager.AppSettings["EmailUseSsl"]);
            var username = ConfigurationManager.AppSettings["EmailUsername"];
            var password = ConfigurationManager.AppSettings["EmailPassword"];

            using (var client = new ImapClient())
            {
                client.Connect(server, port, useSsl);
                client.Authenticate(username, password);

                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                // Récupérer les UIDs des messages récents (les plus récents en premier)
                var uids = inbox.Search(SearchQuery.All).OrderByDescending(uid => uid).Take(maxCount).ToList();

                var messages = new List<object>();
                foreach (var uid in uids)
                {
                    var message = inbox.GetMessage(uid);
                    messages.Add(new
                    {
                        Uid = uid.ToString(),
                        From = message.From.ToString(),
                        Subject = message.Subject ?? "(sans objet)",
                        Date = message.Date.DateTime,
                        Preview = message.TextBody?.Length > 100 ? message.TextBody.Substring(0, 100) + "..." : message.TextBody ?? "(pas de texte)"
                    });
                }

                client.Disconnect(true);
                return messages;
            }
        }

        private string FetchEmailContent(string uidStr)
        {
            var uid = UniqueId.Parse(uidStr);
            var server = ConfigurationManager.AppSettings["EmailServer"];
            var port = int.Parse(ConfigurationManager.AppSettings["EmailPort"]);
            var useSsl = bool.Parse(ConfigurationManager.AppSettings["EmailUseSsl"]);
            var username = ConfigurationManager.AppSettings["EmailUsername"];
            var password = ConfigurationManager.AppSettings["EmailPassword"];

            using (var client = new ImapClient())
            {
                client.Connect(server, port, useSsl);
                client.Authenticate(username, password);

                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                var message = inbox.GetMessage(uid);
                // On renvoie le corps HTML s'il existe, sinon le texte brut
                var body = message.HtmlBody ?? message.TextBody ?? "(aucun contenu)";

                client.Disconnect(true);
                return body;
            }
        }
    }
}