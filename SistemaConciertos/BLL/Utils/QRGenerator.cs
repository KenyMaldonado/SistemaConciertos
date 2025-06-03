using System;
using System.IO; // Para Directory y Path
using System.Drawing; // Necesario para Bitmap
using System.Drawing.Imaging; // Necesario para ImageFormat.Png
using ZXing; // El namespace principal de ZXing.Net
using ZXing.QrCode;

namespace BLL.Utils
{
    public static class QRGenerator
    {
        // Cambia la ruta a la carpeta fija en C:
        private static string qrFolderPath = @"C:\QRCodes";

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
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = 256,
                    Width = 256,
                    Margin = 1,
                    ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.H
                }
            };

            // Genera los píxeles del código QR
            var pixelData = writer.Write(data);

            // Crea un Bitmap a partir de los píxeles generados
            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                    ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                try
                {
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
