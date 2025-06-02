using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.EstructuraDatos
{
    public class Pila<T>
    {
        private Nodo<T> cima;

        public Pila()
        {
            cima = null;
        }

        // Verifica si la pila está vacía
        public bool EstaVacia()
        {
            return cima == null;
        }

        // Añade un elemento a la cima de la pila [cite: 8]
        public void Push(T item)
        {
            Nodo<T> nuevoNodo = new Nodo<T>(item);
            nuevoNodo.Siguiente = cima;
            cima = nuevoNodo;
        }

        // Elimina y retorna el elemento de la cima de la pila [cite: 8]
        public T Pop()
        {
            if (EstaVacia())
            {
                throw new InvalidOperationException("La pila está vacía.");
            }
            T data = cima.Data;
            cima = cima.Siguiente;
            return data;
        }

        // Retorna el elemento de la cima de la pila sin eliminarlo
        public T Peek()
        {
            if (EstaVacia())
            {
                throw new InvalidOperationException("La pila está vacía.");
            }
            return cima.Data;
        }
    }

}
