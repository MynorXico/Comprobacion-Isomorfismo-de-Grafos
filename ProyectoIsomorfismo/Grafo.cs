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
        
        public List<MatrizIncidencia> generarMatricesIncidencia()
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

        public MatrizIncidencia generarMatrizAdyacencia()
        {
            MatrizIncidencia m = new MatrizIncidencia();
            foreach(Vertice v in lstVertices)
            {
                List<int> lista = new List<int>();
                foreach(Vertice v1 in lstVertices)
                {
                    if( v.contains(v1.etiqueta)){
                        lista.Add(1);
                    }
                    else
                    {
                        lista.Add(0);
                    }
                }
                m.agregarFila(lista);
            }
            return m;
        }

        public MatrizIncidencia generarMatricesAdyacencia(MatrizIncidencia matrizCmp)
        {
            PermutadorVertices p = new PermutadorVertices();
            List<List<Vertice>> permutacionesVertices = p.combinar(lstVertices);

            foreach (List<Vertice> list in permutacionesVertices)
            {
                foreach (List<Vertice> list2 in permutacionesVertices)
                {
                    MatrizIncidencia matriz = new MatrizIncidencia();

                    foreach (Vertice v in list)
                    {
                        List<int> fila = new List<int>();
                        foreach(Vertice v1 in list2)
                        {
                            if (v.contains(v1.etiqueta))
                            {
                                fila.Add(1);
                            }
                            else
                            {
                                fila.Add(0);
                            }
                        }
                        matriz.agregarFila(fila);
                    }
                    if (matriz.Equivalent(matrizCmp))
                    {
                        return matriz;
                    }
                }
            }
            return null;


        }

        public Matriz matrizAdyacencia()
        {
            Matriz m = new Matriz();

            foreach (Vertice v in lstVertices)
            {                
                List<int> lista = new List<int>();
                foreach (Vertice v1 in lstVertices)
                {
                    lista.Add(0);
                }
                m.matriz.Add(lista);
            }
            for(int i = 0; i < lstVertices.Count; i++)
            {
                for(int j = 0; j < lstVertices.Count; j++)
                {
                    if (verticePorEtiqueta(i.ToString()).contains(j.ToString()))
                    {
                        m.matriz[i][j] = 1;
                    }
                }
            }
            return m;

        }
    }
        
}
