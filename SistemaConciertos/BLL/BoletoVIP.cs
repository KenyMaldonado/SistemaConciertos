using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BoletoVIP : Boleto
    {
        public string BeneficiosAdicionales { get; private set; }

        // Constructor de la clase BoletoVIP
        public BoletoVIP(int numeroCorrelativo, string zona, int asiento, string nombreComprador, DateTime fechaHoraCompra, string codigoQR, string beneficiosAdicionales)
            : base(numeroCorrelativo, zona, asiento, nombreComprador, fechaHoraCompra, codigoQR)
        {
            BeneficiosAdicionales = beneficiosAdicionales;
        }

        
        public override string ToString()
        {
            return $"{base.ToString()}, Beneficios VIP: {BeneficiosAdicionales}";
        }
    }
}
