using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer.Servises
{
    public class EmailService
    {
        public static void SendMail(string Email, string token)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("rupsam0607@gmail.com","Rups123#");

                MailMessage msgObj = new MailMessage();
                msgObj.To.Add(Email);
                msgObj.From = new MailAddress("rupsam0607@gmail.com");
                msgObj.Subject = "Password Reset Link";
                msgObj.Body = $"www.FundooNotes.com/reset-password/{token}";
                client.Send(msgObj);


            }
        }
    }
}
