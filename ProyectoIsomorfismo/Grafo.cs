using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProyectoIsomorfismo
{
    class Grafo
    {
        // Propiedades del grafo
        /// <summary>
        /// Número de aristas en el grafo
        /// </summary>
        public int cantidadAristas { get; set; }
        /// <summary>
        /// Número de aristas en el vértice
        /// </summary>
        public int cantidadVertices { get; set; }
        /// <summary>
        /// Lista con todos los vértices del grafo
        /// </summary>
        public List<Vertice> lstVertices { get; set; }
        /// <summary>
        /// Lista con todas las aristas del grafo
        /// </summary>
        public List<Arista> lstAristas { get; set; }

        /// <summary>
        /// Constructor de la clase Grafo
        /// </summary>
        /// <param name="cantidadVertices"> Número de vértices que tendrá el grafo. </param>
        /// <param name="cantidadAristas"> Número de aristas que tendrá el grafo. </param>
        /// <param name="lstVertices"> Lista de vértices que tendrá el grafo. </param>
        /// <param name="lstAristas"> Lista de aristas que tendrá el grafo </param>
        public Grafo(int cantidadVertices, int cantidadAristas, List<Vertice> lstVertices
            , List<Arista> lstAristas)
        {
            this.lstVertices = lstVertices;
            this.cantidadVertices = cantidadVertices;
            this.cantidadAristas = cantidadAristas;
            this.lstAristas = lstAristas;
        }

        /// <summary>
        /// Obtiene un vértice a partir de una etiqueta.
        /// </summary>
        /// <param name="etiqueta"> Etiqueta del vértice a obtener. </param>
        /// <returns> Vértice con la etiqueta pasada por parámetro. </returns>
        public Vertice verticePorEtiqueta(string etiqueta)
        {
            Vertice nuevoVertice = new Vertice();
            foreach (Vertice v in lstVertices)
                if (v.etiqueta == etiqueta)
                    return v;
            return nuevoVertice;
        }              
        
        /// <summary>
        /// Genera la matriz de adyacencia del grafo.
        /// </summary>
        /// <returns> Matriz de adyacencia del grafo </returns>
        public Matriz matrizAdyacencia()
        {
            // Crea una matriz
            Matriz m = new Matriz();

            // Llena la matriz con ceros
            foreach (Vertice v in lstVertices)
            {                
                List<int> lista = new List<int>();
                foreach (Vertice v1 in lstVertices)
                {
                    lista.Add(0);
                }
                m.matriz.Add(lista);
            }

            // En caso de encontrar una coincidencia entre vértices, añade un uno en esa
            // posición.
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
