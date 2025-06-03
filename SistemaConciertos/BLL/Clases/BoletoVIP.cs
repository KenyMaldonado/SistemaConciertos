using System;
using System.Text; // Necesario para StringBuilder en MostrarInformacion()

namespace BLL.Clases
{
    public class BoletoVIP : Boleto
    {
        public string BeneficiosAdicionales { get; private set; }

        // Constructor de la clase BoletoVIP, con datos completos del comprador y beneficios
        public BoletoVIP(string numeroCorrelativo, string zona, int asiento,
                         string nombreComprador, string apellidoComprador,
                         string direccionComprador, string telefonoComprador,
                         string emailComprador, DateTime fechaHoraCompra,
                         string codigoQR, string beneficiosAdicionales)
            : base(numeroCorrelativo, zona, asiento, nombreComprador, apellidoComprador, direccionComprador, telefonoComprador, emailComprador, fechaHoraCompra, codigoQR)
        {
            BeneficiosAdicionales = beneficiosAdicionales;
            EsVIP = true; // Marca este boleto como VIP
        }

        // Sobrescribe el método ToString() para incluir beneficios VIP
        public override string ToString()
        {
            return $"{base.ToString()}, Beneficios VIP: {BeneficiosAdicionales}";
        }

        // Sobrescribe MostrarInformacion() para añadir beneficios VIP al detalle
        public override string MostrarInformacion()
        {
            StringBuilder info = new StringBuilder(base.MostrarInformacion());
            info.AppendLine($"Beneficios Adicionales VIP: {BeneficiosAdicionales}");
            return info.ToString();
        }
    }
}
