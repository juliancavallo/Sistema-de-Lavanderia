using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace Servicios
{
    public class ServicioMail
    {
        public void EnviarMail(string destino, string asunto, string mensaje, string mailSoporte, string contraseñaMailSoporte)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(mailSoporte, "Sistema de Lavandería - Soporte");
                mail.To.Add(destino);
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(mailSoporte, contraseñaMailSoporte);
                smtp.EnableSsl = true;
                smtp.Send(mail);

                mail.Dispose();
                smtp.Dispose();
            }
            catch
            {
                throw new Exception("No se pudo enviar el mail de recupero de contraseña. Por favor, contacte al administrador del sistema");
            }
        }
    }
}
