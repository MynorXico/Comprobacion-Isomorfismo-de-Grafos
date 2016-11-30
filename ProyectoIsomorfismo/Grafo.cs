using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProyectoIsomorfismo
{
    class Grafo
    {
        public int cantidadAristas { get; set; }
        public int cantidadVertices { get; set; }
        public List<Vertice> lstVertices { get; set; }

        public Grafo(int cantidadVertices, int cantidadAristas, List<Vertice> lstVertices)
        {
            this.lstVertices = lstVertices;
            this.cantidadVertices = cantidadVertices;
            this.cantidadAristas = cantidadAristas;
        }

        public Vertice verticePorEtiqueta(string etiqueta)
        {
            Vertice nuevoVertice = new Vertice();
            foreach (Vertice v in lstVertices)
                if (v.etiqueta == etiqueta)
                    return v;
            return nuevoVertice;
        }        
        
    }
}
