using System;
using BLL.Utils;   // Para QRGenerator y FileManager
using BLL.Clases;  // Para Boleto y BoletoVIP

namespace BLL.Clases
{
    // Enumeración que define los posibles estados de una transacción
    public enum EstadoTransaccion
    {
        Pendiente,
        Procesada,
        Cancelada
    }

    public class Transaccion
    {
        // Campo estático para llevar el control del ID correlativo de las transacciones
        private static int proximoNumeroCorrelativo;

        // Propiedades de la clase Transaccion
        public string IdTransaccion { get; private set; }
        public Boleto BoletoComprado { get; private set; }
        public EstadoTransaccion Estado { get; set; }
        public DateTime FechaProcesamiento { get; set; }

        // Propiedades auxiliares para mostrar fácilmente en una interfaz como un DataGridView
        public string NombreComprador => BoletoComprado?.NombreComprador ?? "";
        public string ApellidoComprador => BoletoComprado?.ApellidoComprador ?? "";
        public string DireccionComprador => BoletoComprado?.DireccionComprador ?? "";
        public string TelefonoComprador => BoletoComprado?.TelefonoComprador ?? "";
        public string EmailComprador => BoletoComprado?.EmailComprador ?? "";
        public string ZonaAsignada => BoletoComprado?.Zona ?? "";
        public int NumeroAsiento => BoletoComprado?.Asiento ?? 0;
        public string TipoBoleto => BoletoComprado?.GetType().Name ?? "";
        public string CodigoQR => BoletoComprado?.CodigoQR ?? "";
        public bool EsVIP => BoletoComprado?.EsVIP ?? false;

        // Constructor para crear una nueva transacción
        public Transaccion(Boleto boleto)
        {
            IdTransaccion = GeneradorCorrelativo.ObtenerSiguiente();
            BoletoComprado = boleto;
            BoletoComprado.SetNumeroCorrelativo(IdTransaccion); // Asignar número correlativo al boleto
            Estado = EstadoTransaccion.Pendiente;
        }

        // Constructor usado al cargar desde archivo
        public Transaccion(string id, Boleto boleto, EstadoTransaccion estado, DateTime fechaProcesamiento)
        {
            IdTransaccion = id;
            BoletoComprado = boleto;
            Estado = estado;
            FechaProcesamiento = fechaProcesamiento;
        }

        // Establece el próximo número de transacción según el último guardado
        public static void SetProximoIdTransaccion(int ultimoIdGuardado)
        {
            if (ultimoIdGuardado >= proximoNumeroCorrelativo)
                proximoNumeroCorrelativo = ultimoIdGuardado + 1;
            else if (proximoNumeroCorrelativo == 0)
                proximoNumeroCorrelativo = 1;
        }

        // Método para crear una nueva transacción con datos completos
        public static Transaccion CrearNuevaTransaccion(
            string numeroCorrelativo, string zona, int asiento,
            string nombreComprador, string apellidoComprador,
            string direccionComprador, string telefonoComprador,
            string emailComprador, DateTime fechaHoraCompra,
            string codigoQR, bool esVIP)
        {
            Boleto boleto;

            if (esVIP)
            {
                boleto = new BoletoVIP(
                    numeroCorrelativo, zona, asiento,
                    nombreComprador, apellidoComprador,
                    direccionComprador, telefonoComprador,
                    emailComprador, fechaHoraCompra, "",
                    "Acceso preferencial, bebidas gratis");
            }
            else
            {
                boleto = new Boleto(
                    numeroCorrelativo, zona, asiento,
                    nombreComprador, apellidoComprador,
                    direccionComprador, telefonoComprador,
                    emailComprador, fechaHoraCompra, "");
            }

            return new Transaccion(boleto);
        }

        // Procesa la transacción: genera QR, guarda info, cambia estado
        public void ProcesarTransaccion()
        {
            if (Estado == EstadoTransaccion.Pendiente)
            {
                // Generar el código QR
                string qrData = $"ID: {IdTransaccion}, Zona: {BoletoComprado.Zona}, Asiento: {BoletoComprado.Asiento}, Comprador: {BoletoComprado.NombreComprador}";
                string qrFilePath = QRGenerator.GenerarQR(qrData, IdTransaccion.ToString());
                BoletoComprado.CodigoQR = qrFilePath;

                // Fecha y hora en que se procesó la transacción
                FechaProcesamiento = DateTime.Now;

                // Crear línea de texto con la información de la transacción
                string transactionInfo = $"{IdTransaccion}," +
                                         $"{BoletoComprado.Zona}," +
                                         $"{BoletoComprado.Asiento}," +
                                         $"{BoletoComprado.NombreComprador}," +
                                         $"{BoletoComprado.ApellidoComprador}," +
                                         $"{BoletoComprado.DireccionComprador}," +
                                         $"{BoletoComprado.TelefonoComprador}," +
                                         $"{BoletoComprado.EmailComprador}," +
                                         $"{BoletoComprado.FechaHoraCompra:yyyy-MM-dd HH:mm:ss}," +
                                         $"{BoletoComprado.CodigoQR}," +
                                         $"{BoletoComprado.GetType().Name}," +
                                         $"{Estado}," +
                                         $"{FechaProcesamiento:yyyy-MM-dd HH:mm:ss}";

                // Si es VIP, incluir los beneficios adicionales
                if (BoletoComprado is BoletoVIP vipBoleto)
                {
                    transactionInfo += $",{vipBoleto.BeneficiosAdicionales}";
                }

                // Guardar la transacción en un archivo
                FileManager.GuardarTransaccion(this);

                // Cambiar el estado a procesada
                Estado = EstadoTransaccion.Procesada;
            }
        }

        // Permite cancelar una transacción ya procesada
        public void CancelarTransaccion()
        {
            if (Estado == EstadoTransaccion.Procesada)
            {
                Estado = EstadoTransaccion.Cancelada;
                // Aquí podrías liberar el asiento, eliminar el QR, etc.
            }
        }

        // Reinicia el número correlativo (útil para pruebas)
        public static void ResetNumeroCorrelativo()
        {
            proximoNumeroCorrelativo = 1;
        }
    }
}
