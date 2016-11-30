using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIsomorfismo
{
    public class Arista
    {
        public string from { get; set; }
        public string to { get; set; }
        public int ID { get; set; }
        public Arista(string[] arreglo, int id)
        {
            ID = id;
            from = arreglo[0];
            to = arreglo[1];
        }
    }
}
