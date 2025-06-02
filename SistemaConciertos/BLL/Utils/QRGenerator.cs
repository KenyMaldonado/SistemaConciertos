using System;
using System.IO; // Para Directory y Path
using System.Drawing; // Necesario para Bitmap
using System.Drawing.Imaging; // Necesario para ImageFormat.Png
using ZXing; // El namespace principal de ZXing.Net
using ZXing.QrCode;

namespace BLL.Utils // Asegúrate de que este namespace coincida con el de tu proyecto BLL
{
    public static class QRGenerator
    {
        private static string qrFolderPath = "QRCodes"; // Carpeta donde se guardarán los QRs

        public static string GenerarQR(string data, string fileName)
        {
            // Crea la carpeta si no existe
            if (!Directory.Exists(qrFolderPath))
            {
                Directory.CreateDirectory(qrFolderPath);
            }

            // Configura el generador de códigos QR
            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE, // Especifica el formato QR
                Options = new QrCodeEncodingOptions
                {
                    Height = 256, // Altura de la imagen QR en píxeles
                    Width = 256,  // Ancho de la imagen QR en píxeles
                    Margin = 1,   // Margen alrededor del código QR (en "módulos" del QR)
                    ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.H // Nivel de corrección de errores (L, M, Q, H)
                }
            };

            // Genera los píxeles del código QR
            var pixelData = writer.Write(data);

            // Crea un Bitmap a partir de los píxeles generados
            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb))
            {
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                    ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
                try
                {
                    // Copia los píxeles generados al Bitmap
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }

                // Define la ruta completa donde se guardará la imagen PNG
                string filePath = Path.Combine(qrFolderPath, $"{fileName}.png");

                // Guarda la imagen del QR en el archivo
                bitmap.Save(filePath, ImageFormat.Png);

                // Devuelve la ruta completa del archivo QR generado
                return filePath;
            }
        }
    }
}