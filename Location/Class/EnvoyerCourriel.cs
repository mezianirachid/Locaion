using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Location.Class
{

    public class EnvoyerCourriel
    {
        public string GmailUsername { get; set; }
        public string GmailPassword { get; set; }
        public string GmailHost { get; set; }
        public int GmailPort { get; set; }
        public bool GmailSSL { get; set; }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }

        public EnvoyerCourriel()
        {
            GmailHost = "smtp.gmail.com";
            GmailPort = 587; // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
            GmailSSL = true;
        }

        public void Send()
        {
            using (MailMessage mailMessage = new MailMessage(new MailAddress(ToEmail), new MailAddress(ToEmail)))
            {
                mailMessage.Body = Body;
                mailMessage.Subject = Subject;
                try
                {
                    SmtpClient SmtpServer = new SmtpClient();
                    SmtpServer.Credentials =
                        new System.Net.NetworkCredential(GmailUsername, GmailPassword);
                    SmtpServer.Port = GmailPort;
                    SmtpServer.Host = GmailHost;
                    SmtpServer.EnableSsl = true;
                    MailMessage mail = new MailMessage();
                    String[] addr = ToEmail.Split(','); // toemail is a string which contains many email address separated by comma
                    mail.From = new MailAddress(GmailUsername);
                    Byte i;
                    for (i = 0; i < addr.Length; i++)
                        mail.To.Add(addr[i]);
                    mail.Subject = Subject;
                    mail.Body = Body;
                    mail.IsBodyHtml = false;
                    mail.DeliveryNotificationOptions =
                        DeliveryNotificationOptions.OnFailure;
                    //   mail.ReplyTo = new MailAddress(toemail);
                    mail.ReplyToList.Add(ToEmail);
                    SmtpServer.Send(mail);

                }
                catch (Exception ex)
                {
                    throw (new Exception("Mail send failed to loginId " + ToEmail + ", though registration done." + ex.ToString() + "\n" + ex.StackTrace));
                }
            }
        }
    }
}
               