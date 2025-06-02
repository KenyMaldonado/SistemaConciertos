using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Clases // Asegúrate de que este sea el namespace correcto para tu clase Zona
{
    public class Zona
    {
        public string Nombre { get; set; } // Nombre de la zona (ej. VIP, Tribuna)
        public int CapacidadMaxima { get; set; } // Número total de asientos en la zona
        public int BoletosDisponibles { get; set; } // Cantidad de boletos actualmente disponibles
        public Zona Siguiente { get; set; } // Referencia al siguiente nodo en la lista enlazada (si usas lista enlazada)

        // *** NUEVA PROPIEDAD: EsVIP ***
        // Esta propiedad indica si esta zona es considerada VIP.
        public bool EsVIP { get; private set; } // Solo se puede establecer en el constructor o internamente

        // Para gestionar los asientos ocupados y evitar "huecos"
        private bool[] asientosOcupados;

        // Constructor de la clase Zona
        // *** CAMBIO: Nuevo parámetro 'esVIP' en el constructor ***
        public Zona(string nombre, int capacidadMaxima, bool esVIP = false) // 'esVIP' es false por defecto si no se especifica
        {
            Nombre = nombre;
            CapacidadMaxima = capacidadMaxima;
            BoletosDisponibles = capacidadMaxima; // Inicialmente, todos los boletos están disponibles
            Siguiente = null; // Si usas una lista enlazada para las zonas
            asientosOcupados = new bool[capacidadMaxima + 1]; // Los asientos pueden ser 1-based, +1 para evitar índice 0
            EsVIP = esVIP; // Asigna el valor de VIP para esta zona
        }

        // Reduce la cantidad de boletos disponibles
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

        // Aumenta la cantidad de boletos disponibles
        public void AumentarDisponibilidad(int cantidad = 1)
        {
            if (BoletosDisponibles + cantidad <= CapacidadMaxima)
            {
                BoletosDisponibles += cantidad;
            }
            else
            {
                BoletosDisponibles = CapacidadMaxima; // No exceder la capacidad máxima
            }
        }

        // Verifica la disponibilidad de boletos
        public bool VerificarDisponibilidad(int cantidad = 1)
        {
            return BoletosDisponibles >= cantidad;
        }

        // Asigna un asiento.
        // *** CAMBIO: Simplificación de la lógica de asignación de asiento. ***
        // La lógica compleja de "no dejar 1 solo boleto aislado" es mejor manejarla
        // en un nivel superior (ej. al seleccionar el asiento en la UI, o en la lógica de compra)
        // en lugar de dentro de esta función de bajo nivel.
        // Aquí simplemente buscamos el primer asiento disponible.
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
            return -1; // Esto no debería pasar si BoletosDisponibles > 0, pero es un caso de seguridad.
        }
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
        // Libera un asiento previamente ocupado
        public void LiberarAsiento(int asiento)
        {
            if (asiento > 0 && asiento <= CapacidadMaxima && asientosOcupados[asiento])
            {
                asientosOcupados[asiento] = false; // Lo marcamos como libre
                AumentarDisponibilidad();          // Aumentamos la cuenta de disponibles
            }
        }

        // Dentro de la clase Zona.cs, añade estos métodos:

        // Nuevo método para obtener los asientos ocupados como una lista de enteros
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

        // Nuevo método para restaurar los asientos ocupados desde una cadena (usado al cargar)
        public void RestaurarAsientosOcupados(string asientosOcupadosStr)
        {
            // Reinicia el arreglo de asientos a todo libre antes de restaurar
            asientosOcupados = new bool[CapacidadMaxima + 1];
            if (!string.IsNullOrEmpty(asientosOcupadosStr))
            {
                // Dividimos la cadena por '-' y convertimos a enteros
                var asientosNumeros = asientosOcupadosStr.Split('-')
                                                         .Where(s => int.TryParse(s, out _)) // Filtra entradas no válidas
                                                         .Select(int.Parse)
                                                         .ToList();
                foreach (var asientoNum in asientosNumeros)
                {
                    if (asientoNum > 0 && asientoNum <= CapacidadMaxima)
                    {
                        asientosOcupados[asientoNum] = true;
                    }
                }
            }
        }
    }
}