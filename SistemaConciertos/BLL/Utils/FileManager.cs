using System;
using System.Collections.Generic;
using System.IO; // Asegúrate de tener este 'using' para operaciones de archivo
using System.Linq;
using System.Text;
using BLL.Clases; // Para usar Transaccion, Boleto, Zona, etc.

namespace BLL.Utils // Tu namespace actual
{
    public static class FileManager
    {
        private static string dataFolderPath; // Ruta de la carpeta donde se guardarán los archivos
        private static string transactionsFilePath;
        private static string stadiumStateFilePath;

        // Guarda una transacción en el archivo
        public static void GuardarTransaccion(string transactionData)
        {
            try
            {
                // Abre el archivo en modo append y escribe la nueva línea
                using (StreamWriter sw = new StreamWriter(transactionsFilePath, true, Encoding.UTF8))
                {
                    sw.WriteLine(transactionData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar la transacción: {ex.Message}");
                // Aquí podrías agregar un log de errores o mostrar un mensaje al usuario
            }
        }
        static FileManager()
        {
            // Obtiene la ruta del directorio donde se está ejecutando el ensamblado (TuApp.exe)
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Navega hacia arriba para llegar a la raíz de la solución o del proyecto.
            // Esto asume una estructura como:
            // TuSolucion/TuProyectoGUI/bin/Debug/TuApp.exe
            // Queremos llegar a TuSolucion/MiCarpetaDeDatos/
            // Subimos 3 niveles para ir de "Debug" -> "bin" -> "TuProyectoGUI" -> "TuSolucion"
            // string projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\")); // Subir 3 niveles para llegar a la raíz de la solución

            // Opción más simple si quieres una carpeta dentro de la carpeta donde está el .exe, por ejemplo:
            // TuApp.exe
            // Data/
            //   transactions.csv
            //   stadium_state.csv
            // En este caso, solo necesitas:
            dataFolderPath = Path.Combine(baseDirectory, "Data"); // Esto crea una carpeta "Data" junto al .exe

            // Si realmente quieres que los archivos estén en una carpeta a nivel de la solución
            // (fuera de la carpeta bin/Debug), la ruta `projectRoot` es más compleja y depende de tu estructura exacta.
            // Para simplificar y que funcione universalmente, vamos a crear una carpeta "Data"
            // en la misma ubicación que el ejecutable.
            // Si necesitas una ruta diferente, dime la estructura de carpetas exacta.


            // Asegúrate de que la carpeta de datos exista
            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }

            transactionsFilePath = Path.Combine(dataFolderPath, "transactions.csv");
            stadiumStateFilePath = Path.Combine(dataFolderPath, "stadium_state.csv");
        }


        // Carga todas las transacciones desde el archivo
        // Ahora retornará una lista de objetos Transaccion, no solo cadenas
        public static List<Transaccion> CargarTransacciones()
        {
            List<Transaccion> transaccionesCargadas = new List<Transaccion>();

            if (!File.Exists(transactionsFilePath))
                return transaccionesCargadas;

            try
            {
                using (StreamReader sr = new StreamReader(transactionsFilePath, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] partes = line.Split(',');

                        if (partes.Length >= 7)
                        {
                            int id = int.Parse(partes[0]);
                            string zona = partes[1];
                            int asiento = int.Parse(partes[2]);
                            string nombre = partes[3];
                            DateTime fecha = DateTime.Parse(partes[4]);
                            string rutaQR = partes[5];
                            string tipo = partes[6].Trim();

                            Boleto boleto;

                            if (tipo.Equals("BoletoVIP", StringComparison.OrdinalIgnoreCase))
                            {
                                string beneficios = partes.Length >= 8 ? partes[7].Trim() : "Sin beneficios";
                                boleto = new BoletoVIP(id, zona, asiento, nombre, fecha, rutaQR, beneficios);
                            }
                            else
                            {
                                boleto = new Boleto(id, zona, asiento, nombre, fecha, rutaQR);
                            }

                            Transaccion transaccion = new Transaccion(id, boleto, EstadoTransaccion.Procesada, fecha);
                            transaccionesCargadas.Add(transaccion);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar transacciones: {ex.Message}");
            }

            return transaccionesCargadas;
        }


        // Método para procesar una línea del archivo y convertirla en un objeto Boleto
        // *** TU MÉTODO ParseBoletoDesdeLinea YA ES MUY BUENO, LO MANTENEMOS ASÍ ***
        public static Boleto ParseBoletoDesdeLinea(string line)
        {
            string[] parts = line.Split(',');
            if (parts.Length >= 7) // Mínimo de campos para Boleto
            {
                // Manejo seguro de la conversión de tipos
                if (!int.TryParse(parts[0], out int id) ||
                    !int.TryParse(parts[2], out int asiento) ||
                    !DateTime.TryParse(parts[4], out DateTime fechaHoraCompra))
                {
                    Console.WriteLine($"Error al parsear datos de boleto en línea: {line}");
                    return null; // O lanza una excepción específica
                }

                string zona = parts[1];
                string nombreComprador = parts[3];
                string codigoQRPath = parts[5];
                string tipoBoleto = parts[6];

                if (tipoBoleto == "BoletoVIP" && parts.Length >= 8)
                {
                    string beneficios = parts[7];
                    return new BoletoVIP(id, zona, asiento, nombreComprador, fechaHoraCompra, codigoQRPath, beneficios);
                }
                else
                {
                    return new Boleto(id, zona, asiento, nombreComprador, fechaHoraCompra, codigoQRPath);
                }
            }
            return null; // La línea no tiene el formato esperado
        }

        // --- NUEVOS MÉTODOS PARA EL ESTADO DEL ESTADIO ---

        // Guarda el estado actual de todas las zonas del estadio
        public static void GuardarEstadoEstadio(Estadio estadio)
        {
            try
            {
                List<string> lineas = new List<string>();
                List<Zona> zonas = estadio.ObtenerTodasLasZonas(); // Necesitarás este método en Estadio

                foreach (var zona in zonas)
                {
                    // Formato: NombreZona,CapacidadMaxima,BoletosDisponibles,EsVIP,AsientosOcupados (ej. "1-5-7-10")
                    string asientosOcupadosStr = string.Join("-", zona.ObtenerAsientosOcupados()); // Necesitarás este método en Zona
                    lineas.Add($"{zona.Nombre},{zona.CapacidadMaxima},{zona.BoletosDisponibles},{zona.EsVIP},{asientosOcupadosStr}");
                }
                File.WriteAllLines(stadiumStateFilePath, lineas, Encoding.UTF8); // Sobreescribe el archivo cada vez
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar estado del estadio: {ex.Message}");
            }
        }

        // Carga el estado del estadio y lo aplica a una instancia de Estadio existente
        public static void CargarEstadoEstadio(Estadio estadio)
        {
            if (!File.Exists(stadiumStateFilePath))
            {
                return; // No hay estado guardado
            }

            try
            {
                var lineas = File.ReadAllLines(stadiumStateFilePath, Encoding.UTF8);
                foreach (var linea in lineas)
                {
                    var partes = linea.Split(',');
                    if (partes.Length == 5)
                    {
                        string nombreZona = partes[0];
                        // Convertimos de string a los tipos correctos
                        if (!int.TryParse(partes[1], out int capacidadMaxima) ||
                            !int.TryParse(partes[2], out int boletosDisponibles) ||
                            !bool.TryParse(partes[3], out bool esVIP))
                        {
                            Console.WriteLine($"Error al parsear estado de zona en línea: {linea}");
                            continue;
                        }
                        string asientosOcupadosStr = partes[4];

                        // Obtener la zona existente del estadio y actualizar su estado
                        Zona zonaExistente = estadio.ObtenerZonaPorNombre(nombreZona);
                        if (zonaExistente != null)
                        {
                            // Restaurar los asientos ocupados
                            zonaExistente.RestaurarAsientosOcupados(asientosOcupadosStr);
                            // Actualizar la disponibilidad de boletos
                            zonaExistente.BoletosDisponibles = boletosDisponibles;
                            // La CapacidadMaxima y EsVIP ya deberían estar configuradas en la inicialización del Estadio
                            // si las zonas son fijas, pero podrías actualizarlo si el diseño lo permite.
                            // zonaExistente.CapacidadMaxima = capacidadMaxima; // Si la capacidad puede cambiar dinámicamente
                            // zonaExistente.EsVIP = esVIP; // Si el estado VIP de la zona puede cambiar dinámicamente
                        }
                        else
                        {
                            Console.WriteLine($"Advertencia: Zona '{nombreZona}' encontrada en archivo guardado, pero no existe en la configuración actual del estadio. Saltando carga de esta zona.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar estado del estadio: {ex.Message}");
            }
        }
    }
}