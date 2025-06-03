using System;
using System.IO;
using System.Security.Cryptography;

namespace BLL.Utils
{
    public static class GeneradorCorrelativo
    {
        private static string archivoRuta = "correlativos_usados.txt";
        private static HashSet<string> correlativosUsados = new HashSet<string>();

        static GeneradorCorrelativo()
        {
            if (File.Exists(archivoRuta))
            {
                var lineas = File.ReadAllLines(archivoRuta);
                foreach (var linea in lineas)
                {
                    correlativosUsados.Add(linea.Trim());
                }
            }
        }

        public static string ObtenerSiguiente(string prefijo = "GEN")
        {
            string nuevoCorrelativo;
            do
            {
                // Generar número aleatorio de 8 dígitos
                int numero = GenerarNumeroAleatorio(10000000, 99999999);
                nuevoCorrelativo = $"{prefijo}-{numero}";
            } while (correlativosUsados.Contains(nuevoCorrelativo));

            correlativosUsados.Add(nuevoCorrelativo);
            File.AppendAllText(archivoRuta, nuevoCorrelativo + Environment.NewLine);
            return nuevoCorrelativo;
        }

        private static int GenerarNumeroAleatorio(int min, int max)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[4];
                int resultado;

                do
                {
                    rng.GetBytes(bytes);
                    resultado = BitConverter.ToInt32(bytes, 0) & int.MaxValue; // Asegura positivo
                } while (resultado < min || resultado > max);

                return resultado;
            }
        }
    }
}
