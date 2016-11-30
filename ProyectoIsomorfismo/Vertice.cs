using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ProyectoIsomorfismo
{
    class Vertice
    {
        public string etiqueta { get; set; }
        public List<Vertice> verticesConectados { get; set;}
        public int grado { get; set; }

        public Vertice()
        {
            verticesConectados = new List<Vertice>();
            grado = 0;
        }

        public void calcularGrado()
        {
            grado = verticesConectados.Count();
        }
    }
}
