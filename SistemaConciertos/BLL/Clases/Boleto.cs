using System;
using System.Text; // Necesario para StringBuilder en MostrarInformacion()

namespace BLL.Clases
{
    public enum EstadoBoleto
    {
        Vendido,
        Usado,
        Cancelado
    }

    public class Boleto
    {
        public string NumeroCorrelativo { get; private set; }
        public string Zona { get; protected set; }
        public int Asiento { get; protected set; }

        // Datos completos del comprador
        public string NombreComprador { get; protected set; }
        public string ApellidoComprador { get; protected set; }
        public string DireccionComprador { get; protected set; }
        public string TelefonoComprador { get; protected set; }
        public string EmailComprador { get; protected set; }
        public string TipoBoleto {  get; protected set; }
        public DateTime FechaHoraCompra { get; protected set; }
        public string CodigoQR { get; set; }
        public EstadoBoleto Estado { get; set; }
        public bool EsVIP { get; protected set; }

        // Constructor extendido para datos completos del comprador
        public Boleto(string numeroCorrelativo, string zona, int asiento,
                      string nombreComprador, string apellidoComprador,
                      string direccionComprador, string telefonoComprador,
                      string emailComprador, DateTime fechaHoraCompra,
                      string codigoQR)
        {
            NumeroCorrelativo = numeroCorrelativo;
            Zona = zona;
            Asiento = asiento;

            NombreComprador = nombreComprador;
            ApellidoComprador = apellidoComprador;
            DireccionComprador = direccionComprador;
            TelefonoComprador = telefonoComprador;
            EmailComprador = emailComprador;

            FechaHoraCompra = fechaHoraCompra;
            CodigoQR = codigoQR;

            Estado = EstadoBoleto.Vendido;
            EsVIP = false;
        }

        public void SetNumeroCorrelativo(string numero)
        {
            NumeroCorrelativo = numero;
        }

        public override string ToString()
        {
            return $"Boleto No. {NumeroCorrelativo} - Zona: {Zona}, Asiento: {Asiento}, Comprador: {NombreComprador} {ApellidoComprador}, Fecha: {FechaHoraCompra.ToShortDateString()} {FechaHoraCompra.ToShortTimeString()}, Tipo: {(EsVIP ? "VIP" : "Normal")}";
        }

        public virtual string MostrarInformacion()
        {
            StringBuilder info = new StringBuilder();
            info.AppendLine($"ID Boleto: {NumeroCorrelativo}");
            info.AppendLine($"Comprador: {NombreComprador} {ApellidoComprador}");
            info.AppendLine($"Dirección: {DireccionComprador}");
            info.AppendLine($"Teléfono: {TelefonoComprador}");
            info.AppendLine($"Email: {EmailComprador}");
            info.AppendLine($"Zona: {Zona}");
            info.AppendLine($"Asiento: {Asiento}");
            info.AppendLine($"Tipo de Boleto: {(EsVIP ? "VIP" : "Normal")}");
            info.AppendLine($"Fecha/Hora Compra: {FechaHoraCompra:yyyy-MM-dd HH:mm:ss}");
            info.AppendLine($"Estado del Boleto: {Estado}");
            info.AppendLine($"Ruta Código QR: {CodigoQR}");
            return info.ToString();
        }
    }

}