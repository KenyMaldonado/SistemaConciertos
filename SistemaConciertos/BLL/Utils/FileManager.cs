using System;
using System.Collections.Generic;
using System.IO; // Asegúrate de tener este 'using' para operaciones de archivo
using System.Linq;
using System.Text;
using BLL.Clases; // Para usar Transaccion, Boleto, Zona, etc.
using Microsoft.VisualBasic.FileIO; // Necesario para TextFieldParser

namespace BLL.Utils // Tu namespace actual
{
    public static class FileManager
    {
        private static string dataFolderPath; // Ruta de la carpeta donde se guardarán los archivos
        private static string transactionsFilePath;
        private static string stadiumStateFilePath;

        // Constructor estático para inicializar las rutas de los archivos una sola vez
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

        /// <summary>
        /// Guarda una transacción en el archivo CSV de transacciones.
        /// </summary>
        /// <param name="t">La transacción a guardar.</param>
        public static void GuardarTransaccion(Transaccion t)
        {
            try
            {
                // El 'true' en StreamWriter indica que se abrirá en modo de adjuntar (append),
                // para agregar nuevas líneas al final del archivo.
                using (StreamWriter sw = new StreamWriter(transactionsFilePath, true, Encoding.UTF8))
                {
                    string linea = ConvertirTransaccionALineaCSV(t);
                    sw.WriteLine(linea);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar la transacción: {ex.Message}");
                // Considera agregar un log más robusto o relanzar la excepción si es un error crítico.
            }
        }

        /// <summary>
        /// Escapa un campo para su inclusión en un archivo CSV, manejando comas y comillas.
        /// </summary>
        /// <param name="campo">El valor del campo a escapar.</param>
        /// <returns>El campo escapado para CSV.</returns>
        private static string EscaparCampo(string campo)
        {
            if (campo == null)
                return "\"\""; // Un campo nulo se representa como comillas vacías en CSV

            // Escapa las comillas dobles existentes duplicándolas y encierra el campo entre comillas dobles.
            return $"\"{campo.Replace("\"", "\"\"")}\"";
        }

        /// <summary>
        /// Convierte un objeto Transaccion en una línea de texto con formato CSV.
        /// </summary>
        /// <param name="t">La transacción a convertir.</param>
        /// <returns>Una cadena representando la transacción en formato CSV.</returns>
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
                EscaparCampo(boleto.GetType().Name), // Tipo de boleto (Boleto o BoletoVIP)
                EscaparCampo(t.Estado.ToString()),
                EscaparCampo(t.FechaProcesamiento.ToString("yyyy-MM-dd HH:mm:ss"))
            };

            // Si es un BoletoVIP, agrega los beneficios adicionales
            if (boleto is BoletoVIP vip)
            {
                campos.Add(EscaparCampo(vip.BeneficiosAdicionales));
            }

            return string.Join(",", campos);
        }

        /// <summary>
        /// Carga todas las transacciones desde el archivo CSV de transacciones.
        /// </summary>
        /// <returns>Una lista de objetos Transaccion cargados desde el archivo.</returns>
        public static List<Transaccion> CargarTransacciones()
        {
            List<Transaccion> transaccionesCargadas = new List<Transaccion>();

            if (!File.Exists(transactionsFilePath))
                return transaccionesCargadas; // Retorna una lista vacía si el archivo no existe

            try
            {
                // TextFieldParser es ideal para leer archivos CSV de forma robusta
                using (TextFieldParser parser = new TextFieldParser(transactionsFilePath, Encoding.UTF8))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(","); // Define la coma como el delimitador de campos
                    parser.HasFieldsEnclosedInQuotes = true; // Importante para campos que contienen comas o comillas

                    while (!parser.EndOfData)
                    {
                        string[] partes = parser.ReadFields();

                        // Asegúrate de que haya suficientes partes para una transacción básica (13 campos + opcional VIP)
                        if (partes.Length >= 13)
                        {
                            // Desescapar campos si es necesario (TextFieldParser ya maneja esto)
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
                            string tipo = partes[10].Trim(); // Asegúrate de quitar espacios en blanco
                            EstadoTransaccion estado = Enum.Parse<EstadoTransaccion>(partes[11]);
                            DateTime fechaProcesamiento = DateTime.Parse(partes[12]);

                            Boleto boleto;

                            // Instancia el tipo de boleto correcto (Boleto normal o BoletoVIP)
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
                // Considera un mejor manejo de errores para producción.
            }

            return transaccionesCargadas;
        }

        /// <summary>
        /// Actualiza el estado de una transacción existente en el archivo CSV de transacciones.
        /// Sobrescribe todo el archivo con los datos actualizados.
        /// </summary>
        /// <param name="transaccionActualizada">La transacción con los datos actualizados (especialmente el estado).</param>
        /// <returns>True si la transacción fue encontrada y actualizada, false en caso contrario.</returns>
        public static bool ActualizarEstadoTransaccion(Transaccion transaccionActualizada)
        {
            if (!File.Exists(transactionsFilePath))
                return false;

            try
            {
                // Carga todas las transacciones existentes en memoria
                var transacciones = CargarTransacciones();

                bool encontrada = false;
                // Busca la transacción a actualizar por su IdTransaccion
                for (int i = 0; i < transacciones.Count; i++)
                {
                    if (transacciones[i].IdTransaccion == transaccionActualizada.IdTransaccion)
                    {
                        // Actualiza solo las propiedades relevantes (estado y fecha de procesamiento)
                        transacciones[i].Estado = transaccionActualizada.Estado;
                        transacciones[i].FechaProcesamiento = transaccionActualizada.FechaProcesamiento;
                        encontrada = true;
                        break; // Sale del bucle una vez que la transacción ha sido actualizada
                    }
                }

                if (!encontrada)
                    return false; // Si la transacción no se encontró, no hay nada que actualizar.

                // Sobrescribe el archivo completo con la lista de transacciones actualizada.
                // El 'false' en StreamWriter indica que se sobrescribirá el archivo.
                using (StreamWriter sw = new StreamWriter(transactionsFilePath, false, Encoding.UTF8))
                {
                    foreach (var t in transacciones)
                    {
                        string linea = ConvertirTransaccionALineaCSV(t);
                        sw.WriteLine(linea);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar transacción: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Guarda el estado actual de todas las zonas del estadio en un archivo CSV.
        /// Este método sobrescribe el archivo cada vez que se llama.
        /// </summary>
        /// <param name="estadio">La instancia del Estadio que contiene las zonas a guardar.</param>
        public static void GuardarEstadoEstadio(Estadio estadio)
        {
            try
            {
                List<string> lineas = new List<string>();
                // Obtiene todas las zonas del objeto Estadio
                List<Zona> zonas = estadio.ObtenerTodasLasZonas();

                foreach (var zona in zonas)
                {
                    // Obtiene los asientos ocupados como una cadena "1-5-7-10"
                    string asientosOcupadosStr = string.Join("-", zona.ObtenerAsientosOcupados());
                    // Formato de la línea: NombreZona,CapacidadMaxima,BoletosDisponibles,EsVIP,AsientosOcupados
                    lineas.Add($"{zona.Nombre},{zona.CapacidadMaxima},{zona.BoletosDisponibles},{zona.EsVIP},{asientosOcupadosStr}");
                }
                // Escribe todas las líneas al archivo, sobrescribiendo el contenido anterior.
                File.WriteAllLines(stadiumStateFilePath, lineas, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar estado del estadio: {ex.Message}");
            }
        }

        /// <summary>
        /// Carga el estado del estadio desde el archivo CSV y lo aplica a la instancia de Estadio proporcionada.
        /// </summary>
        /// <param name="estadio">La instancia de Estadio a la que se le aplicará el estado cargado.</param>
        public static void CargarEstadoEstadio(Estadio estadio)
        {
            if (!File.Exists(stadiumStateFilePath))
            {
                // Si el archivo no existe, simplemente retorna. 
                // El estadio se mantendrá con su estado inicial (definido en su constructor o por defecto).
                return;
            }

            try
            {
                var lineas = File.ReadAllLines(stadiumStateFilePath, Encoding.UTF8);
                foreach (var linea in lineas)
                {
                    var partes = linea.Split(',');
                    // Se esperan 5 partes: NombreZona,CapacidadMaxima,BoletosDisponibles,EsVIP,AsientosOcupados
                    if (partes.Length == 5)
                    {
                        string nombreZona = partes[0];
                        // Intenta parsear los valores. Si falla, se imprime un error y se salta la línea.
                        if (!int.TryParse(partes[1], out int capacidadMaxima) ||
                            !int.TryParse(partes[2], out int boletosDisponiblesDelArchivo) || // Este valor ya no se usa directamente para Zona.BoletosDisponibles
                            !bool.TryParse(partes[3], out bool esVIP))
                        {
                            Console.WriteLine($"Error al parsear estado de zona en línea: {linea}. Formato incorrecto.");
                            continue;
                        }
                        string asientosOcupadosStr = partes[4];

                        // Intenta obtener la zona existente del objeto Estadio en memoria
                        Zona zonaExistente = estadio.ObtenerZonaPorNombre(nombreZona);
                        if (zonaExistente != null)
                        {
                            // Si la zona existe, actualiza sus asientos ocupados y esto recalculará sus BoletosDisponibles.
                            zonaExistente.RestaurarAsientosOcupados(asientosOcupadosStr);
                        }
                        else
                        {
                            // Si la zona no existe en la instancia de Estadio (por ejemplo, es una nueva zona
                            // o el estadio se inicializó vacío), la crea y la agrega.
                            Console.WriteLine($"Advertencia: Zona '{nombreZona}' encontrada en archivo guardado, pero no existe en la configuración actual del estadio. Creándola y cargando su estado.");
                            Zona nuevaZona = new Zona(nombreZona, capacidadMaxima, esVIP);
                            nuevaZona.RestaurarAsientosOcupados(asientosOcupadosStr); // Restaura los asientos en la nueva zona
                            estadio.AgregarZona(nuevaZona); // Agrega la nueva zona al estadio
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Advertencia: Línea con formato inesperado en stadium_state.csv: {linea}");
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