using System;
using System.Collections.Generic;
using System.IO; // Asegúrate de tener este 'using' para operaciones de archivo
using System.Linq;
using System.Text;
using BLL.Clases; // Para usar Transaccion, Boleto, Zona, etc.
using Microsoft.VisualBasic.FileIO;

namespace BLL.Utils // Tu namespace actual
{
    public static class FileManager
    {
        private static string dataFolderPath; // Ruta de la carpeta donde se guardarán los archivos
        private static string transactionsFilePath;
        private static string stadiumStateFilePath;

        // Guarda una transacción en el archivo
        public static void GuardarTransaccion(Transaccion t)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(transactionsFilePath, true, Encoding.UTF8))
                {
                    string linea = ConvertirTransaccionALineaCSV(t);
                    sw.WriteLine(linea);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar la transacción: {ex.Message}");
            }
        }

        private static string EscaparCampo(string campo)
        {
            if (campo == null)
                return "\"\"";

            // Escapa las comillas dobles existentes y encierra el campo entre comillas dobles
            return $"\"{campo.Replace("\"", "\"\"")}\"";
        }

        public static string ConvertirTransaccionALineaCSV(Transaccion t)
        {
            var boleto = t.BoletoComprado;

            List<string> campos = new List<string>
    {
        EscaparCampo(t.IdTransaccion),
        EscaparCampo(boleto.Zona),
        boleto.Asiento.ToString(),
        EscaparCampo(boleto.NombreComprador),
        EscaparCampo(boleto.ApellidoComprador),
        EscaparCampo(boleto.DireccionComprador),
        EscaparCampo(boleto.TelefonoComprador),
        EscaparCampo(boleto.EmailComprador),
        EscaparCampo(boleto.FechaHoraCompra.ToString("yyyy-MM-dd HH:mm:ss")),
        EscaparCampo(boleto.CodigoQR),
        EscaparCampo(boleto.GetType().Name),
        EscaparCampo(t.Estado.ToString()),
        EscaparCampo(t.FechaProcesamiento.ToString("yyyy-MM-dd HH:mm:ss"))
    };

            if (boleto is BoletoVIP vip)
            {
                campos.Add(EscaparCampo(vip.BeneficiosAdicionales));
            }

            return string.Join(",", campos);
        }


        static FileManager()
        {

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            dataFolderPath = Path.Combine(baseDirectory, "Data"); 

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
                using (TextFieldParser parser = new TextFieldParser(transactionsFilePath, Encoding.UTF8))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        string[] partes = parser.ReadFields();

                        if (partes.Length >= 13)
                        {
                            string id = partes[0];
                            string zona = partes[1];
                            int asiento = int.Parse(partes[2]);
                            string nombre = partes[3];
                            string apellido = partes[4];
                            string direccion = partes[5];
                            string telefono = partes[6];
                            string email = partes[7];
                            DateTime fechaCompra = DateTime.Parse(partes[8]);
                            string codigoQR = partes[9];
                            string tipo = partes[10].Trim();
                            EstadoTransaccion estado = Enum.Parse<EstadoTransaccion>(partes[11]);
                            DateTime fechaProcesamiento = DateTime.Parse(partes[12]);

                            Boleto boleto;

                            if (tipo.Equals("BoletoVIP", StringComparison.OrdinalIgnoreCase))
                            {
                                string beneficios = partes.Length >= 14 ? partes[13].Trim() : "Sin beneficios";
                                boleto = new BoletoVIP(id, zona, asiento, nombre, apellido, direccion, telefono, email, fechaCompra, codigoQR, beneficios);
                            }
                            else
                            {
                                boleto = new Boleto(id, zona, asiento, nombre, apellido, direccion, telefono, email, fechaCompra, codigoQR);
                            }

                            Transaccion transaccion = new Transaccion(id, boleto, estado, fechaProcesamiento);
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