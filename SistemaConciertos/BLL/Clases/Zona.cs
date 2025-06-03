using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Clases
{
    public class Zona
    {
        public string Nombre { get; set; }
        public int CapacidadMaxima { get; set; }
        public int BoletosDisponibles { get; set; }
        public Zona Siguiente { get; set; } // Referencia al siguiente nodo en la lista enlazada
        public bool EsVIP { get; private set; }

        private bool[] asientosOcupados;

        public Zona(string nombre, int capacidadMaxima, bool esVIP = false)
        {
            Nombre = nombre;
            CapacidadMaxima = capacidadMaxima;
            // BoletosDisponibles se inicializará y recalculará en RestaurarAsientosOcupados al cargar.
            // Para la primera vez que se crea una Zona (sin cargar de archivo), la inicialización es correcta:
            BoletosDisponibles = capacidadMaxima;
            Siguiente = null;
            asientosOcupados = new bool[capacidadMaxima + 1]; // +1 para índices 1 a CapacidadMaxima
            EsVIP = esVIP;
        }

        public void ReducirDisponibilidad(int cantidad = 1)
        {
            if (BoletosDisponibles >= cantidad)
            {
                BoletosDisponibles -= cantidad;
            }
            else
            {
                throw new InvalidOperationException("No hay suficientes boletos disponibles en esta zona.");
            }
        }

        public void AumentarDisponibilidad(int cantidad = 1)
        {
            if (BoletosDisponibles + cantidad <= CapacidadMaxima)
            {
                BoletosDisponibles += cantidad;
            }
            else
            {
                BoletosDisponibles = CapacidadMaxima;
            }
        }

        public bool VerificarDisponibilidad(int cantidad = 1)
        {
            return BoletosDisponibles >= cantidad;
        }

        public int AsignarAsiento()
        {
            if (BoletosDisponibles == 0)
            {
                return -1; // No hay asientos disponibles
            }

            for (int i = 1; i <= CapacidadMaxima; i++)
            {
                if (!asientosOcupados[i]) // Si el asiento está libre
                {
                    asientosOcupados[i] = true; // Lo marcamos como ocupado
                    ReducirDisponibilidad();    // Reducimos la cuenta de disponibles
                    return i;                   // Devolvemos el número de asiento asignado
                }
            }
            return -1; // Esto no debería pasar si BoletosDisponibles > 0
        }

        // Ya tienes estos métodos, solo los incluyo para completar
        public List<int> ObtenerAsientosDisponibles()
        {
            List<int> disponibles = new List<int>();
            for (int i = 1; i <= CapacidadMaxima; i++)
            {
                if (!asientosOcupados[i])
                {
                    disponibles.Add(i);
                }
            }
            return disponibles;
        }

        public int ObtenerCantidadAsientosOcupados()
        {
            return ObtenerAsientosOcupados().Count;
        }

        public int ObtenerCantidadAsientosDisponibles()
        {
            return ObtenerAsientosDisponibles().Count;
        }

        // Este método ya lo tienes y es crucial.
        public void LiberarAsiento(int asiento)
        {
            if (asiento > 0 && asiento <= CapacidadMaxima && asientosOcupados[asiento])
            {
                asientosOcupados[asiento] = false; // Lo marcamos como libre
                AumentarDisponibilidad();           // Aumentamos la cuenta de disponibles
            }
        }

        // Este método ya lo tienes y es crucial.
        public List<int> ObtenerAsientosOcupados()
        {
            List<int> ocupados = new List<int>();
            for (int i = 1; i <= CapacidadMaxima; i++)
            {
                if (asientosOcupados[i])
                {
                    ocupados.Add(i);
                }
            }
            return ocupados;
        }

        // Este método ya lo tienes. Solo asegúrate de que al final de su ejecución
        // `BoletosDisponibles` refleje la cantidad correcta de asientos libres.
        public void RestaurarAsientosOcupados(string asientosOcupadosStr)
        {
            // Reinicia el arreglo de asientos a todo libre antes de restaurar
            asientosOcupados = new bool[CapacidadMaxima + 1];
            int ocupadosCount = 0; // Contador para saber cuántos asientos se ocuparon

            if (!string.IsNullOrEmpty(asientosOcupadosStr))
            {
                var asientosNumeros = asientosOcupadosStr.Split('-')
                                                         .Where(s => int.TryParse(s, out _))
                                                         .Select(int.Parse)
                                                         .ToList();
                foreach (var asientoNum in asientosNumeros)
                {
                    if (asientoNum > 0 && asientoNum <= CapacidadMaxima)
                    {
                        asientosOcupados[asientoNum] = true;
                        ocupadosCount++; // Incrementa el contador de ocupados
                    }
                }
            }
            // Recalcula BoletosDisponibles basado en los asientos realmente ocupados
            BoletosDisponibles = CapacidadMaxima - ocupadosCount;
        }
    }
}