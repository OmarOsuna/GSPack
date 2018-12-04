using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Practica.Nucleo.Negocio
{
    public class Email
    {
        public static void EnviarEmail(string correoDestinatario, string nombreDestinatario, string fechaSalida, string fechaEntrega, string folio, string domicilio, string referencia, string cec, string telefono )
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("gspackguaymas@gmail.com", "itsonguaymas");
                MailMessage mmsg = new MailMessage();


                mmsg.To.Add(correoDestinatario);
                mmsg.Subject = "Detalle del envío";
                mmsg.SubjectEncoding = Encoding.UTF8;

                mmsg.Body = body.Replace("[NOMBRE_DESTINATARIO]", nombreDestinatario).Replace("[FECHA_ENTREGA]", fechaEntrega).Replace("[FECHA_SALIDA]", fechaSalida).Replace("[DOMICILIO]", domicilio).Replace("[REFERENCIA]", referencia).Replace("[CEC]", cec).Replace("[TELEFONO]", telefono).Replace("[FOLIO]", folio);
                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                mmsg.IsBodyHtml = true;
                mmsg.From = new MailAddress("gspackguaymas@gmail.com");
                Thread t1 = new Thread(delegate ()
                {
                    client.Send(mmsg);
                });
                t1.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static string body = "<html><head> <meta charset=\"utf-8\"/> <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"> <style type=\"text/css\"> .btn,a{text-decoration:none}.header p,p.footer{text-transform:uppercase}body{Margin:0!important;padding:15px;background-color:#FFF}.wrapper{width:100%;table-layout:fixed}.wrapper-inner{width:100%;background-color:#eee;max-width:670px;Margin:0 auto}table{border-spacing:0;font-family:sans-serif;color:#727f80}.outer-table{width:100%;max-width:670px;margin:0 auto;background-color:#FFF}td{padding:0}.header{background-color:#C2C1C1;border-bottom:3px solid #81B9C3}p{Margin:0}.header p{text-align:center;padding:1%;font-weight:500;font-size:11px}.btn,.h2{font-weight:600}a{color:#F1F1F1}.main-table-first{width:100%;max-width:610px;Margin:0 auto;background-color:#286090;border-radius:6px;margin-top:25px}.two-column{text-align:center;font-size:0;padding:5px 0 10px}.one-column .inner-td,.two-column .content{font-size:16px;line-height:20px;text-align:justify}.two-column .section{width:100%;max-width:300px;display:inline-block;vertical-align:top}.content{width:100%;padding-top:20px}.center{display:table;Margin:0 auto}img{border:0}img.logo{float:left;Margin-left:5%;max-width:200px!important}#callout{float:right;Margin:4% 5% 2% 0;height:auto;overflow:hidden}#callout img{max-width:20px}.social{list-style-type:none;Margin-top:1%;padding:0}.social li{display:inline-block}.social li img{max-width:15px;Margin-bottom:0;padding-bottom:0}.image img{width:100%;max-width:670px;height:auto}.main-table{width:100%;max-width:610px;margin:0 auto;background-color:#FFF;border-radius:6px}.h2,.h3,.three-column,p.center{text-align:center}.inner-td{padding:10px}.h2{font-size:23px;line-height:45px;Margin:12px;color:#4A4A4A}p.center{max-width:580px;line-height:24px}.button-holder-center{text-align:center;Margin:5% 2% 3% 0}.button-holder{float:right;Margin:5% 0 3% 0}.btn{font-size:15px;background:#81BAC6;color:#FFF;padding:9px 16px;border-radius:28px}.outer-table-2,.outer-table-3{background-color:#C2C1C1;width:100%}.two-column img{width:100%;max-width:280px;height:auto}.two-column .text{padding:10px 0}.outer-table-2{max-width:670px;margin:22px auto}.three-column{font-size:0;padding:10px 0 30px}.three-column .section{width:100%;max-width:200px;display:inline-block;vertical-align:top}.three-column .content{font-size:16px;line-height:20px}.three-column img{width:100%;max-width:125px;height:auto}.outer-table-2 p{margin-top:6px;color:#FFF;font-size:18px;font-weight:500;line-height:23px}.h1,.h3{font-weight:600;color:#4A4A4A}.outer-table-3{max-width:670px;margin:22px auto}.h3{font-size:21px;Margin-bottom:8px}.inner-bottom{padding:22px}.h1{text-align:center!important;font-size:25px!important;line-height:45px;Margin:12px 0 20px 0}.inner-bottom p{font-size:16px;line-height:24px;text-align:justify}.footer{width:100%;background-color:#e2ae00;Margin:0 auto;color:#FFF}.footer img{max-width:135px;Margin:0 auto;display:block;padding:4% 0 1%}p.footer{text-align:center;color:#FFF!important;line-height:30px;padding-bottom:4%}@media screen and (max-width:400px){.h1{font-size:22px}.three-column .column,.two-column .column{max-width:100%!important}.two-column img{width:100%!important}.three-column img{max-width:60%!important}}@media screen and (min-width:401px) and (max-width:400px){.two-column .column{max-width:50%!important}.three-column .column{max-width:33%!important}}@media screen and (max-width:768px){#callout,img.logo{float:none!important}img.logo{margin-left:0!important;max-width:200px!important}#callout{margin:0;height:auto;text-align:center;overflow:hidden}.two-column img,img.img-responsive{height:auto!important}#callout img{max-width:26px!important}.two-column .section,img.img-responsive{width:100%!important;max-width:100%!important}.two-column .section{display:inline-block;vertical-align:top}.two-column img{width:100%!important}.content{width:100%;padding-top:0!important}}</style> </head><body> <div class=\"wrapper\"> <div class=\"wrapper-inner\"> <table class=\"outer-table\"> </table> <table class=\"main-table-first\"> <tr> <td class=\"two-column\"> <div class=\"section\"> <table width=\"100%\"> <tr> <td class=\"inner-td\"> <table class=\"content\"> <tr> <td align=\"center\"> <img src=\"~/Content/img/logoChico.png\" class=\"logo\"> </td></tr></table> </td></tr></table> </div><div class=\"section\"> <table width=\"100%\"> <tr> <td class=\"inner-td\"> <table class=\"content\"> <tr> <td> <div id=\"callout\"> <ul class=\"social\"> <li><a href=\"index.html#\" target=\"_blank\"><img src=\"~/Content/img/facebook.png\"></a></li><li><a href=\"index.html#\" target=\"_blank\"><img src=\"~/Content/img/googleplus.png\"></a></li><li><a href=\"index.html#\" target=\"_blank\"><img src=\"~/Content/img/twitter.png\"></a></li><li><a href=\"index.html#\" target=\"_blank\"><img src=\"~/Content/img/youtube.png\"></a></li><li><a href=\"index.html#\" target=\"_blank\"><img src=\"~/Content/img/instagram.png\"></a></li></ul> </div></td></tr></table> </td></tr></table> </div></td></tr><table class=\"main-table\"> <tr> <td class=\"one-column\"> <table width=\"100%\"> <tr> <td class=\"inner-td\"> <p class=\"h2\">GSPack Guaymas</p><p class=\"center\">Hola [NOMBRE_DESTINATARIO] , tienes un paquete en camino.</p></td></tr></table> </td></tr></table> <table class=\"outer-table-2\" style=\"background-color: #C2C1C1;\"> <tr> <td class=\"one-column\"> <table width=\"100%\"> <tr> <td class=\"footer\"> <img src=\"~/Content/img/logistic.png\"> <p class=\"footer\">Salio de nuestras oficinas el [FECHA_SALIDA]<br>te llegara aproximadamente el [FECHA_ENTREGA] .</p></td></tr></table> </td></tr></table> <table class=\"main-table\"> <tr> <td class=\"two-column\"> <div class=\"section\"> <table width=\"100%\"> <tr> <td class=\"inner-td\"> <table class=\"content\"> <tr> <td align=\"center\" valign=\"middle\" > <a href=\" http://www.gspack.azurewebsites.net\"><img style=\"width: 30%; max-width: 280px; height: auto;\" src=\"~/Content/img/ubicacion.png\" class=\"img-responsive\"></a> </td></tr><tr> <td class=\"text\"> <p class=\"h3\">Codigo de Seguimiento</p><p class=\"h3\"> [FOLIO] </p></td></tr></table> </td></tr></table> </div><!--[if (gte mso 9)|(IE)]> </td><td width=\"50%\" valign=\"top\"><![endif]--> </td></tr></table> <table class=\"outer-table-3\"> <tr> <td class=\"one-column\"> <table width=\"100%\"> <tr> <td class=\"footer\"> <br><p style=\"color: white; text-align: center; font-size: 25px;\">Detalles de Envio:</p><br><p class=\"footer\"> [DOMICILIO] <br> [REFERENCIA] <br> [CEC] <br>Tel: [TELEFONO] </p></td></tr></table> </td></tr></table> </div></div><br></body>";
    }
}
