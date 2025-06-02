using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    // Clase Estadio que gestiona las zonas y la disponibilidad de asientos [cite: 10]
    public class Estadio
    {
        // La lista enlazada para representar las zonas del estadio [cite: 6]
        private Nodo<Zona> cabezaZonas;

        public Estadio()
        {
            cabezaZonas = null;
            // Inicializar zonas del estadio al crear el objeto Estadio
            // Puedes ajustar las capacidades según tus necesidades
            AgregarZona(new Zona("VIP", 50)); // Capacidad de ejemplo
            AgregarZona(new Zona("Tribuna", 200)); // Capacidad de ejemplo
            AgregarZona(new Zona("General", 300)); // Capacidad de ejemplo
            AgregarZona(new Zona("Mesas Platino", 20)); // Capacidad de ejemplo
        }

        // Agrega una zona a la lista enlazada [cite: 6]
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

        // Obtiene una zona por su nombre [cite: 6]
        public Zona ObtenerZona(string nombreZona)
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

        // Retorna la cantidad de boletos disponibles para una zona específica [cite: 4]
        public int VerificarDisponibilidadZona(string nombreZona)
        {
            Zona zona = ObtenerZona(nombreZona);
            return zona != null ? zona.BoletosDisponibles : 0;
        }

        // Actualiza la disponibilidad de boletos de una zona
        public bool ActualizarDisponibilidad(string nombreZona, int cantidad, bool esCompra)
        {
            Zona zona = ObtenerZona(nombreZona);
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
            Zona zona = ObtenerZona(nombreZona);
            if (zona != null)
            {
                return zona.AsignarAsiento();
            }
            return -1; // Zona no encontrada o sin asientos
        }

        // Libera un asiento específico en una zona (para cancelaciones/devoluciones)
        public void LiberarAsiento(string nombreZona, int asiento)
        {
            Zona zona = ObtenerZona(nombreZona);
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
    }
}
