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
        public List<Arista> lstAristas { get; set; }

        public Grafo(int cantidadVertices, int cantidadAristas, List<Vertice> lstVertices, List<Arista> lstAristas)
        {
            this.lstVertices = lstVertices;
            this.cantidadVertices = cantidadVertices;
            this.cantidadAristas = cantidadAristas;
            this.lstAristas = lstAristas;
        }

        public Vertice verticePorEtiqueta(string etiqueta)
        {
            Vertice nuevoVertice = new Vertice();
            foreach (Vertice v in lstVertices)
                if (v.etiqueta == etiqueta)
                    return v;
            return nuevoVertice;
        }      
        
        public List<MatrizIncidencia> generarMatrices()
        {
            PermutadorVertices p = new PermutadorVertices();
            PermutadorAristas pAristas = new PermutadorAristas();
            List<List<Vertice>> permutacionesVertices = p.combinar(lstVertices);
            List<List<Arista>> permutacionesAristas = pAristas.combinar(lstAristas);

            List<MatrizIncidencia> lstMatrices = new List<MatrizIncidencia>();
            
            foreach(List<Vertice> lstVertice in permutacionesVertices)
            {
                foreach(List<Arista> lstArista in permutacionesAristas)
                {
                    MatrizIncidencia m = new MatrizIncidencia();
                    List<int> lista = new List<int>();

                    foreach (Vertice v in lstVertice)
                    {
                        foreach (Arista a in lstArista)
                        {
                            if (a.to == v.etiqueta || a.from == v.etiqueta)
                            {
                                lista.Add(1);
                            }
                            else
                            {
                                lista.Add(0);
                            }
                        }
                        m.agregarFila(lista);
                    }
                    lstMatrices.Add(m);
                }
            }            

            return lstMatrices;
        }  
        
    }
}
