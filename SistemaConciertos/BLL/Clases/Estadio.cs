using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.EstructuraDatos; // Asegúrate de que esta referencia es correcta para tu Nodo<Zona>
using BLL.Clases; // Asegúrate de que tus clases Zona, Boleto, etc. estén en este namespace

namespace BLL.Clases
{
    // Clase Estadio que gestiona las zonas y la disponibilidad de asientos
    public class Estadio
    {
        // La lista enlazada para representar las zonas del estadio
        private Nodo<Zona> cabezaZonas;

        public Estadio()
        {
            cabezaZonas = null;
            // Inicializar zonas del estadio al crear el objeto Estadio
            // *** CAMBIO CRÍTICO AQUÍ: Pasa el parámetro 'esVIP' al constructor de Zona ***
            // Ajusta las capacidades y el estado VIP según tus necesidades reales
            AgregarZona(new Zona("VIP", 50, true)); // Esta zona es VIP
            AgregarZona(new Zona("Tribuna", 200, false)); // Esta zona NO es VIP
            AgregarZona(new Zona("General", 300, false)); // Esta zona NO es VIP
            AgregarZona(new Zona("Mesas Platino", 20, true)); // Esta zona es VIP
        }

        // Agrega una zona a la lista enlazada
        public void AgregarZona(Zona nuevaZona)
        {
            if (cabezaZonas == null)
            {
                cabezaZonas = new Nodo<Zona>(nuevaZona);
            }
            else
            {
                Nodo<Zona> actual = cabezaZonas;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = new Nodo<Zona>(nuevaZona);
            }
        }

        // Obtiene una zona por su nombre
        // Renombrado de ObtenerZona a ObtenerZonaPorNombre para mayor claridad en el UI
        public Zona ObtenerZonaPorNombre(string nombreZona)
        {
            Nodo<Zona> actual = cabezaZonas;
            while (actual != null)
            {
                if (actual.Data.Nombre.Equals(nombreZona, StringComparison.OrdinalIgnoreCase))
                {
                    return actual.Data;
                }
                actual = actual.Siguiente;
            }
            return null; // Zona no encontrada
        }

        // Retorna la cantidad de boletos disponibles para una zona específica
        public int VerificarDisponibilidadZona(string nombreZona)
        {
            Zona zona = ObtenerZonaPorNombre(nombreZona); // Usamos el método renombrado
            return zona != null ? zona.BoletosDisponibles : 0;
        }

        // Actualiza la disponibilidad de boletos de una zona
        public bool ActualizarDisponibilidad(string nombreZona, int cantidad, bool esCompra)
        {
            Zona zona = ObtenerZonaPorNombre(nombreZona); // Usamos el método renombrado
            if (zona != null)
            {
                if (esCompra)
                {
                    if (zona.VerificarDisponibilidad(cantidad))
                    {
                        zona.ReducirDisponibilidad(cantidad);
                        return true;
                    }
                }
                else // Es una devolución/cancelación
                {
                    zona.AumentarDisponibilidad(cantidad);
                    return true;
                }
            }
            return false;
        }

        // Obtiene el siguiente asiento disponible para una zona
        // La lógica de "no dejar 1 solo boleto" debe ser gestionada a nivel de la UI o antes de llamar aquí.
        public int ObtenerAsientoDisponible(string nombreZona)
        {
            Zona zona = ObtenerZonaPorNombre(nombreZona); // Usamos el método renombrado
            if (zona != null)
            {
                return zona.AsignarAsiento();
            }
            return -1; // Zona no encontrada o sin asientos
        }

        // Libera un asiento específico en una zona (para cancelaciones/devoluciones)
        public void LiberarAsiento(string nombreZona, int asiento)
        {
            Zona zona = ObtenerZonaPorNombre(nombreZona); // Usamos el método renombrado
            if (zona != null)
            {
                zona.LiberarAsiento(asiento);
            }
        }

        // Método para obtener una lista de nombres de zonas (útil para ComboBox en UI)
        public List<string> ObtenerNombresZonas()
        {
            List<string> nombres = new List<string>();
            Nodo<Zona> actual = cabezaZonas;
            while (actual != null)
            {
                nombres.Add(actual.Data.Nombre);
                actual = actual.Siguiente;
            }
            return nombres;
        }

        // Dentro de la clase Estadio.cs, añade este método:
        public List<Zona> ObtenerTodasLasZonas()
        {
            List<Zona> todasLasZonas = new List<Zona>();
            Nodo<Zona> actual = cabezaZonas;
            while (actual != null)
            {
                todasLasZonas.Add(actual.Data);
                actual = actual.Siguiente;
            }
            return todasLasZonas;
        }
    }
}