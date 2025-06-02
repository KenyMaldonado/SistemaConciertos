namespace BLL
{
    public class Boleto
    {
        public int NumeroCorrelativo { get; protected set; } // Número único por transacción [cite: 5, 9]
        public string Zona { get; protected set; } // Zona del estadio [cite: 9]
        public int Asiento { get; protected set; } // Número de asiento asignado [cite: 5, 9]
        public string NombreComprador { get; protected set; } // Datos del comprador [cite: 5, 9]
        public DateTime FechaHoraCompra { get; protected set; } // Fecha y hora de la compra [cite: 5]
        public string CodigoQR { get; protected set; } // Ruta o contenido del código QR [cite: 5]

        // Constructor de la clase Boleto
        public Boleto(int numeroCorrelativo, string zona, int asiento, string nombreComprador, DateTime fechaHoraCompra, string codigoQR)
        {
            NumeroCorrelativo = numeroCorrelativo;
            Zona = zona;
            Asiento = asiento;
            NombreComprador = nombreComprador;
            FechaHoraCompra = fechaHoraCompra;
            CodigoQR = codigoQR;
        }

        // Método para obtener una representación de cadena del boleto
        public override string ToString()
        {
            return $"Boleto No. {NumeroCorrelativo} - Zona: {Zona}, Asiento: {Asiento}, Comprador: {NombreComprador}, Fecha: {FechaHoraCompra.ToShortDateString()} {FechaHoraCompra.ToShortTimeString()}";
        }
    }
}
