using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.EstructuraDatos; // Asegúrate de que esta referencia es correcta para tu Nodo<Zona>
using BLL.Clases;

namespace BLL.Clases
{
    public class Estadio
    {
        private Nodo<Zona> cabezaZonas;

        public Estadio()
        {
            cabezaZonas = null;
            // OJO: Si vas a cargar el estado del estadio desde un archivo al iniciar,
            // no necesitas agregar estas zonas aquí directamente.
            // La carga se encargará de re-crearlas y restaurar su estado.
            // Si el archivo no existe, podrías considerar agregar estas como zonas predeterminadas.
            // Para el ejemplo, las mantendré comentadas para que veas que la carga del FileManager
            // es quien debe poblar estas zonas si vienen de persistencia.
            /*
            AgregarZona(new Zona("VIP", 50, true));
            AgregarZona(new Zona("Tribuna", 200, false));
            AgregarZona(new Zona("General", 300, false));
            AgregarZona(new Zona("Mesas Platino", 20, true));
            */
        }

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
            return null;
        }

        public int VerificarDisponibilidadZona(string nombreZona)
        {
            Zona zona = ObtenerZonaPorNombre(nombreZona);
            return zona != null ? zona.BoletosDisponibles : 0;
        }

        public bool ActualizarDisponibilidad(string nombreZona, int cantidad, bool esCompra)
        {
            Zona zona = ObtenerZonaPorNombre(nombreZona);
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

        public int ObtenerAsientoDisponible(string nombreZona)
        {
            Zona zona = ObtenerZonaPorNombre(nombreZona);
            if (zona != null)
            {
                return zona.AsignarAsiento();
            }
            return -1;
        }

        // Este método ya lo tienes y es el que usaremos desde el Formulario
        public void LiberarAsiento(string nombreZona, int asiento)
        {
            Zona zona = ObtenerZonaPorNombre(nombreZona);
            if (zona != null)
            {
                zona.LiberarAsiento(asiento);
            }
        }

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

        // Este método ya lo tienes y es el que el FileManager usará para guardar
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