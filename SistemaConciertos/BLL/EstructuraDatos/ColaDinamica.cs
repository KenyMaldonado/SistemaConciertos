using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Clases; // Asegúrate de que BLL.Clases.Transaccion sea la que usas
// No necesitas 'using System.Transactions;' si no lo usas específicamente.

namespace BLL.EstructuraDatos
{
    // *** IMPORTANTE: LA DEFINICIÓN DE LA CLASE NODO<T> YA NO ESTÁ AQUÍ. ***
    // *** SE ASUME QUE ESTÁ EN SU PROPIO ARCHIVO (Nodo.cs) ***

    public class ColaDinamica<T>
    {
        private Nodo<T> frente; // Ahora usa la clase Nodo<T> de Nodo.cs
        private Nodo<T> final;  // Ahora usa la clase Nodo<T> de Nodo.cs
        private int count;

        public ColaDinamica()
        {
            frente = null;
            final = null;
            count = 0;
        }

        public int Count
        {
            get { return count; }
        }

        public bool EstaVacia()
        {
            return frente == null;
        }

        public void Encolar(T item)
        {
            Nodo<T> nuevoNodo = new Nodo<T>(item); // Usa la clase Nodo<T> de Nodo.cs
            if (EstaVacia())
            {
                frente = nuevoNodo;
                final = nuevoNodo;
            }
            else
            {
                final.Siguiente = nuevoNodo;
                final = nuevoNodo;
            }
            count++;
        }

        // El método EncolarConPrioridad se mantiene comentado como se había discutido.
        /*
        public void EncolarConPrioridad(T item)
        {
            // ... (código comentado) ...
        }
        */

        public T Desencolar()
        {
            if (EstaVacia())
            {
                throw new InvalidOperationException("La cola está vacía.");
            }
            T data = frente.Data;
            frente = frente.Siguiente;
            if (frente == null)
            {
                final = null;
            }
            count--;
            return data;
        }

        public bool Desencolar(out T item)
        {
            item = default(T);
            if (EstaVacia())
            {
                return false;
            }
            item = frente.Data;
            frente = frente.Siguiente;
            if (frente == null)
            {
                final = null;
            }
            count--;
            return true;
        }

        public T Peek()
        {
            if (EstaVacia())
            {
                throw new InvalidOperationException("La cola está vacía.");
            }
            return frente.Data;
        }
    }
}