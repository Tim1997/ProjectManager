using template__.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace template__.Helpers
{
    public class EmailHelper : EmailConfig
    {
        public static bool SendMail(string subject, string body, IEnumerable<string> to, IEnumerable<string> cc, IEnumerable<string> bcc, IEnumerable<Attachment> attachments)
        {
            try
            {
                using (var smtpClient = new SmtpClient
                {
                    Host = _smtpServer,
                    EnableSsl = bool.Parse(_enableSsl),
                })
                {

                    if (!string.IsNullOrEmpty(_smtpUser) && !string.IsNullOrEmpty(_smtpPass))
                    {
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                    }
                    else
                    {
                        smtpClient.UseDefaultCredentials = true;
                    }

                    if (!string.IsNullOrEmpty(_smtpPort))
                    {
                        smtpClient.Port = int.Parse(_smtpPort);
                    }

                    MailMessage message = PrepareMailMessage(subject, body, to, cc, bcc, attachments);

                    smtpClient.Send(message);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static MailMessage PrepareMailMessage(string subject, string body, IEnumerable<string> to, IEnumerable<string> cc, IEnumerable<string> bcc, IEnumerable<Attachment> attachments)
        {
            var message = new MailMessage();
            var fromAddress = new MailAddress(_adminEmail);

            message.From = fromAddress;
            if (to != null)
            {
                foreach (var item in to.Where(item => !string.IsNullOrWhiteSpace(item)))
                {
                    message.To.Add(item);
                }
            }

            if (cc != null)
            {
                foreach (var item in cc.Where(item => !string.IsNullOrWhiteSpace(item)))
                {
                    message.CC.Add(item);
                }
            }
            if (bcc != null)
            {
                foreach (var item in bcc.Where(item => !string.IsNullOrWhiteSpace(item)))
                {
                    message.Bcc.Add(item);
                }
            }

            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    message.Attachments.Add(attachment);
                }
            }

            return message;
        }
    }
}
