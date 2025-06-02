using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Utils; // Asegúrate de que tus utilidades (QRGenerator, FileManager) estén en este namespace
using BLL.Clases; // Asegúrate de que Boleto y BoletoVIP estén en este namespace

namespace BLL.Clases // Asegúrate de que este sea el namespace correcto para Transaccion
{
    // Enumeración para el estado de la transacción
    public enum EstadoTransaccion
    {
        Pendiente,
        Procesada,
        Cancelada
    }

    // Clase Transaccion que maneja el procesamiento y generación de QR
    public class Transaccion
    {
        // *** CAMBIO CLAVE 1: proximoNumeroCorrelativo no se inicializa a 1 aquí. ***
        // Se inicializará desde el FileManager al cargar los datos.
        private static int proximoNumeroCorrelativo;

        public int IdTransaccion { get; private set; }
        public Boleto BoletoComprado { get; private set; } // O si solo necesitas sus propiedades:
        public string CodigoQR { get; set; }
        public DateTime FechaProcesamiento { get; private set; }
        public EstadoTransaccion Estado { get; set; }

        // ====================================================================
        // === AGREGA ESTAS PROPIEDADES SOLO LECTURA PARA EL DataGridView ===
        // ====================================================================

        public string NombreComprador => BoletoComprado.NombreComprador; // El ? evita NullReferenceException
        public string ZonaAsignada => BoletoComprado.Zona;
        public int NumeroAsiento => BoletoComprado != null ? BoletoComprado.Asiento : 0; // Manejo de null para int
        public string TipoBoleto => BoletoComprado is BoletoVIP ? "VIP" : "Normal"; // Propiedad calculada


        // Propiedad para acceder fácilmente si el boleto es VIP desde la transacción
        public bool EsVIP
        {
            get { return BoletoComprado != null ? BoletoComprado.EsVIP : false; }
        }

        // *** CAMBIO CLAVE 2: Constructor principal que asigna un nuevo ID ***
        // Este es el constructor que se usa cuando se crea una NUEVA transacción en el sistema.
        public Transaccion(Boleto boleto)
        {
            // Usa el ID asignado al boleto, que a su vez se basa en el proximoNumeroCorrelativo global.
            // Para que esto funcione, el Boleto DEBE recibir el proximoNumeroCorrelativo en su constructor
            // o tu sistema de IDs debe ser diferente.
            // Dada tu implementación actual, el IdTransaccion es lo que genera Boleto.NumeroCorrelativo.
            // Entonces, se sigue usando proximoNumeroCorrelativo para el ID de Transaccion.
            IdTransaccion = proximoNumeroCorrelativo++; // Asigna y luego incrementa para el siguiente

            BoletoComprado = boleto;
            // Asegúrate de que el NumeroCorrelativo del Boleto también se establezca correctamente.
            // Si el IdTransaccion es el mismo que Boleto.NumeroCorrelativo, puedes hacer:
            BoletoComprado.SetNumeroCorrelativo(IdTransaccion); // Necesitarás este método en Boleto

            Estado = EstadoTransaccion.Pendiente;
        }

        // *** CAMBIO CLAVE 3: NUEVO Constructor para CARGAR Transacciones existentes desde archivo ***
        // Este constructor se usa SÓLO cuando se reconstruye una transacción desde los datos guardados.
        public Transaccion(int id, Boleto boleto, EstadoTransaccion estado, DateTime fechaProcesamiento)
        {
            IdTransaccion = id;
            BoletoComprado = boleto;
            Estado = estado;
            FechaProcesamiento = fechaProcesamiento;
        }


        // *** CAMBIO CLAVE 4: MÉTODO ESTÁTICO para establecer el próximo ID al cargar el programa ***
        // Este método será llamado por MainForm después de cargar todas las transacciones históricas
        // para asegurarse de que los nuevos IDs no se dupliquen.
        public static void SetProximoIdTransaccion(int ultimoIdGuardado)
        {
            // Si el último ID guardado es mayor o igual al actual 'proximoNumeroCorrelativo' (que podría ser 0 o 1 si no se inicializó),
            // lo actualizamos para que el siguiente ID sea mayor que el último guardado.
            if (ultimoIdGuardado >= proximoNumeroCorrelativo)
            {
                proximoNumeroCorrelativo = ultimoIdGuardado + 1;
            }
            // Si no hay transacciones guardadas (ultimoIdGuardado es 0), se inicializa a 1.
            else if (proximoNumeroCorrelativo == 0) // Si por alguna razón no se inicializó y es la primera vez.
            {
                proximoNumeroCorrelativo = 1;
            }
        }


        // Método estático para crear una nueva transacción y encolarla para procesamiento
        public static Transaccion CrearNuevaTransaccion(string nombreComprador, string zonaSeleccionada, int asientoAsignado, bool esVIP)
        {
            Boleto boleto;
            // NOTA: El primer parámetro '0' en el constructor de Boleto/BoletoVIP
            // es el NumeroCorrelativo. Este debería ser el IdTransaccion.
            // Lo estableceremos después de crear la transacción.
            if (esVIP)
            {
                boleto = new BoletoVIP(0, zonaSeleccionada, asientoAsignado, nombreComprador, DateTime.Now, "", "Acceso preferencial, bebidas gratis");
            }
            else
            {
                boleto = new Boleto(0, zonaSeleccionada, asientoAsignado, nombreComprador, DateTime.Now, "");
            }

            // Creamos una instancia de Transacción con el boleto (IdTransaccion se asigna aquí)
            Transaccion nuevaTransaccion = new Transaccion(boleto); // Usa el constructor que asigna IdTransaccion

            // El Boleto ya tendrá su NumeroCorrelativo establecido por el constructor de Transaccion
            // si agregamos Boleto.SetNumeroCorrelativo(IdTransaccion) en el constructor de Transaccion.

            return nuevaTransaccion;
        }

        // Procesa la transacción: genera QR y almacena la información
        public void ProcesarTransaccion()
        {
            if (Estado == EstadoTransaccion.Pendiente)
            {
                // Generar el código QR
                string qrData = $"ID: {IdTransaccion}, Zona: {BoletoComprado.Zona}, Asiento: {BoletoComprado.Asiento}, Comprador: {BoletoComprado.NombreComprador}";
                string qrFilePath = QRGenerator.GenerarQR(qrData, IdTransaccion.ToString()); // Usa el ID como nombre de archivo para el QR
                BoletoComprado.CodigoQR = qrFilePath; // Almacena la ruta del archivo QR

                // Almacenar la información del comprador y boleto en un archivo
                // Asegúrate que el formato coincide con lo que FileManager.ParseBoletoDesdeLinea espera
                string transactionInfo = $"{IdTransaccion},{BoletoComprado.Zona},{BoletoComprado.Asiento},{BoletoComprado.NombreComprador},{BoletoComprado.FechaHoraCompra:yyyy-MM-dd HH:mm:ss},{BoletoComprado.CodigoQR},{BoletoComprado.GetType().Name}";
                if (BoletoComprado is BoletoVIP vipBoleto)
                {
                    transactionInfo += $",{vipBoleto.BeneficiosAdicionales}";
                }
                FileManager.GuardarTransaccion(transactionInfo); // Asegúrate que FileManager esté en BLL.Utils

                Estado = EstadoTransaccion.Procesada;
            }
        }

        // Cancela una compra, marcando el estado de la transacción
        public void CancelarTransaccion()
        {
            if (Estado == EstadoTransaccion.Procesada) // Solo se puede cancelar si ya fue procesada
            {
                Estado = EstadoTransaccion.Cancelada;
                // Si necesitas "liberar" el asiento en el estadio, esto se haría en el Estadio.
                // o si necesitas eliminar el archivo QR, esto se haría aquí o en FileManager.
            }
        }

        // Permite restablecer el número correlativo (útil para pruebas)
        public static void ResetNumeroCorrelativo()
        {
            proximoNumeroCorrelativo = 1;
        }
    }
}