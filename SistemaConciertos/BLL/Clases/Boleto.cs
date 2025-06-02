using System;
using System.Text; // Necesario para StringBuilder en MostrarInformacion()

namespace BLL.Clases
{
    // Enumeración para el estado del boleto en sí (diferente del estado de la transacción)
    public enum EstadoBoleto
    {
        Vendido,  // El boleto ha sido comprado y está activo
        Usado,    // El boleto ya fue validado y utilizado
        Cancelado // El boleto fue cancelado (ej. por devolución)
    }

    public class Boleto
    {
        public int NumeroCorrelativo { get; private set; } // Número único por transacción
        public string Zona { get; protected set; } // Zona del estadio
        public int Asiento { get; protected set; } // Número de asiento asignado
        public string NombreComprador { get; protected set; } // Datos del comprador
        public DateTime FechaHoraCompra { get; protected set; } // Fecha y hora de la compra
        public string CodigoQR { get; set; } // Ruta o contenido del código QR
        public EstadoBoleto Estado { get; set; } // Estado del boleto (Vendido, Usado, Cancelado)

        // Esta propiedad indica si el boleto es de una zona VIP.
        // Se define como 'protected set' para que las clases derivadas (como BoletoVIP) puedan establecerla.
        public bool EsVIP { get; protected set; }

        // Constructor de la clase Boleto
        public Boleto(int numeroCorrelativo, string zona, int asiento, string nombreComprador, DateTime fechaHoraCompra, string codigoQR)
        {
            NumeroCorrelativo = numeroCorrelativo;
            Zona = zona;
            Asiento = asiento;
            NombreComprador = nombreComprador;
            FechaHoraCompra = fechaHoraCompra;
            CodigoQR = codigoQR;
            Estado = EstadoBoleto.Vendido; // Estado inicial por defecto al crear un boleto
            EsVIP = false; // Por defecto, un Boleto normal no es VIP
        }

        // *** NUEVO MÉTODO: SetNumeroCorrelativo ***
        // Este método permite establecer el NumeroCorrelativo después de la creación inicial,
        // lo que es útil cuando la Transacción asigna su propio IdTransaccion al boleto.
        public void SetNumeroCorrelativo(int numero)
        {
            NumeroCorrelativo = numero;
        }

        // Método para obtener una representación de cadena del boleto
        public override string ToString()
        {
            return $"Boleto No. {NumeroCorrelativo} - Zona: {Zona}, Asiento: {Asiento}, Comprador: {NombreComprador}, Fecha: {FechaHoraCompra.ToShortDateString()} {FechaHoraCompra.ToShortTimeString()}, Tipo: {(EsVIP ? "VIP" : "Normal")}";
        }

        // Método para obtener una cadena con toda la información detallada del boleto.
        // Se define como 'virtual' para que BoletoVIP pueda sobrescribirlo y añadir más detalles.
        public virtual string MostrarInformacion()
        {
            StringBuilder info = new StringBuilder();
            info.AppendLine($"ID Boleto: {NumeroCorrelativo}");
            info.AppendLine($"Comprador: {NombreComprador}");
            info.AppendLine($"Zona: {Zona}");
            info.AppendLine($"Asiento: {Asiento}");
            info.AppendLine($"Tipo de Boleto: {(EsVIP ? "VIP" : "Normal")}");
            info.AppendLine($"Fecha/Hora Compra: {FechaHoraCompra:yyyy-MM-dd HH:mm:ss}");
            info.AppendLine($"Estado del Boleto: {Estado.ToString()}");
            info.AppendLine($"Ruta Código QR: {CodigoQR}");
            return info.ToString();
        }
    }
}