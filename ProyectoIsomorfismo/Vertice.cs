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
        public int ID { get; set; }
        public Vertice()
        {
            verticesConectados = new List<Vertice>();
            grado = 0;
        }

        public void asignarID()
        {
            ID = Convert.ToInt32(etiqueta) + 65;
        }
        public void calcularGrado()
        {
            grado = verticesConectados.Count();
        }

        public List<Vertice> listaVerticesPorEtiqueta(Vertice v, Grafo g)
        {
            List<Vertice> resultado = new List<Vertice>();
            foreach(Vertice vertice in v.verticesConectados)
            {
                resultado.Add(g.verticePorEtiqueta(vertice.etiqueta));
            }
            return resultado;
        }
    }
}
