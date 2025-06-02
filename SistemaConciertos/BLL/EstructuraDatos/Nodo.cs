using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.EstructuraDatos
{
    public class Nodo<T>
    {
        public T Data { get; set; }
        public Nodo<T> Siguiente { get; set; }

        public Nodo(T data)
        {
            Data = data;
            Siguiente = null;
        }
    }
}
