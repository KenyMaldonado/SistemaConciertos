using System;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Clases;

namespace BLL.Utils
{
    public static class EmailSender
    {
        private const string SenderEmail = "ticketsexpressmeso@gmail.com";
        private const string SenderPassword = "qbfa qmvy rzxv ejmx";
        private const string SmtpHost = "smtp.gmail.com";
        private const int SmtpPort = 587;

        const string QrFolderPath = @"C:\QRCodes";

        public static async Task<bool> SendOrderNotificationEmailAsync(List<Transaccion> transacciones)
        {
            return await EnviarCorreoAsync(transacciones, false);
        }

        public static async Task<bool> SendTicketConfirmationWithQrEmailAsync(List<Transaccion> transacciones)
        {
            return await EnviarCorreoAsync(transacciones, true);
        }

        private static async Task<bool> EnviarCorreoAsync(List<Transaccion> transacciones, bool adjuntarQr)
        {
            Console.WriteLine("Iniciando envío de correo...");

            if (transacciones == null || transacciones.Count == 0)
            {
                Console.WriteLine("No hay transacciones para enviar.");
                return false;
            }

            try
            {
                using var client = new SmtpClient(SmtpHost)
                {
                    Port = SmtpPort,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(SenderEmail, SenderPassword),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                using var mail = new MailMessage
                {
                    From = new MailAddress(SenderEmail, "Sistema de Conciertos"),
                    Subject = adjuntarQr ? $"🎟️ Confirmación de Compra - {transacciones.Count} Boleto(s)" : "🕒 Orden Recibida - Sistema de Conciertos",
                    IsBodyHtml = true
                };

                mail.To.Add(transacciones[0].EmailComprador);

                var body = $@"
<div style='font-family: Arial, sans-serif; background-color: #f9f9f9; padding: 20px;'>
  <div style='max-width: 600px; margin: auto; background: white; border-radius: 10px; box-shadow: 0 2px 8px rgba(0,0,0,0.1); padding: 30px;'>
    <h2 style='color: #2e7d32; text-align: center;'>{(adjuntarQr ? "Confirmación de Compra" : "Orden Recibida")}</h2>
    <p>Hola <strong>{transacciones[0].NombreComprador} {transacciones[0].ApellidoComprador}</strong>,</p>
    <p>{(adjuntarQr ? "Tu compra ha sido procesada exitosamente. Aquí están los detalles de tus boletos:" : "Hemos recibido tu orden. Estamos procesando tus boletos, aquí los detalles:")}</p>
    <ul style='list-style-type: none; padding-left: 0;'>";

                foreach (var t in transacciones)
                {
                    body += $@"
      <li style='border-bottom: 1px solid #eee; margin-bottom: 15px; padding-bottom: 10px;'>
        <b>ID de Boleto:</b> {t.IdTransaccion} <br/>
        <b>Zona:</b> {t.ZonaAsignada} <br/>
        <b>Asiento:</b> {t.NumeroAsiento} <br/>
        <b>Fecha:</b> {DateTime.Now:dd/MM/yyyy HH:mm} <br/>
        <b>Tipo:</b> {(t.EsVIP ? "VIP" : "General")}
      </li>";
                }

                body += @"
    </ul>";

                if (adjuntarQr)
                {
                    body += "<p>Adjunto encontrarás los códigos QR de tus boletos.</p>";
                }
                else
                {
                    body += "<p>Te enviaremos un segundo correo con los boletos adjuntos en breve.</p>";
                }

                body += @"
    <p>Gracias por confiar en nosotros.<br/>Equipo del Estadio</p>
    <hr style='margin-top: 30px; border-top: 1px solid #ccc;'/>
    <p style='font-size: 12px; color: #999; text-align: center;'>© " + DateTime.UtcNow.Year + @" Sistema de Conciertos. Todos los derechos reservados.</p>
  </div>
</div>";

                mail.Body = body;

                // Adjuntar QR si aplica
                if (adjuntarQr)
                {
                    foreach (var t in transacciones)
                    {
                        if (!string.IsNullOrEmpty(t.CodigoQR))
                        {
                            string fullPath = Path.Combine(QrFolderPath, t.CodigoQR);
                            Console.WriteLine($"Adjuntando QR: {fullPath}");

                            if (File.Exists(fullPath))
                            {
                                var attachment = new Attachment(fullPath);
                                attachment.Name = $"QR_Boleto_{t.IdTransaccion}.png";
                                mail.Attachments.Add(attachment);
                            }
                            else
                            {
                                Console.WriteLine($"Archivo no encontrado: {fullPath}");
                            }
                        }
                    }
                }

                await client.SendMailAsync(mail);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
                return false;
            }
        }
    }

}
