using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using FixMens.Models;

namespace FixmensIntegrationApi.BLL
{
    public class InvoicesBLL
    {
        public static void sendEmailToCreateInvoice(FacturaModel model)
        {
            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("martin.nv6@gmail.com", "Said2011");

            var body = "Nombre Fiscal: " + model.nombreFiscal + "\n" +
                       "Calle y número: " + model.calleynumero + "\n" +
                       "Colonia: " + model.colonia + "\n" +
                       "Municipio: " + model.municipio + "\n" +
                       "Estado: " + model.estado + "\n" +
                       "CP: " + model.cp + "\n" +
                       "RFC: " + model.rfc + "\n" +
                       "Teléfono: " + model.telefono + "\n" +
                       "Correo: " + model.email + "\n" +
                       "Ticket/Orden: " + model.ticket + "\n" +
                       "Comentario: " + model.comentario + "\n";




            MailMessage mm = new MailMessage("donotreply@domain.com", "martin_nv6@hotmail.com", "FixMens - Comprobante Fiscal en Proceso", "La factura con los datos: \n\n" + body + "\n\nSe encuentra en proceso con nuestro personal de contabilidad, cualquier duda haremos contacto con usted \n\n\n http://www.fixmens.com.mx (01) 828-284-0220");
            mm.To.Add(new MailAddress(model.email, model.nombreFiscal));
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }
    }
}