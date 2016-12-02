using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ProyectoIsomorfismo
{
    /// <summary>
    /// Modela el vértice de un grafo
    /// </summary>
    class Vertice
    {
        // Propiedades de la clase vértice
        /// <summary>
        /// Etiqueta del vértice
        /// </summary>
        public string etiqueta { get; set; }
        /// <summary>
        /// Lista de vértices con los cuáles tiene conexión.
        /// </summary>
        public List<Vertice> verticesConectados { get; set;}
        /// <summary>
        /// Grado del vértice
        /// </summary>
        public int grado { get; set; }
        /// <summary>
        /// ID del vértice
        /// </summary>
        public int ID { get; set; }


        /// <summary>
        /// Constructor de la clase Vertice
        /// </summary>
        public Vertice()
        {
            verticesConectados = new List<Vertice>();
            grado = 0;
        }

        /// <summary>
        /// Asigna un ID al Vertice
        /// </summary>
        public void asignarID()
        {
            ID = Convert.ToInt32(etiqueta) + 65;
        } 

        /// <summary>
        /// Asigna grado al Vertice
        /// </summary>
        public void calcularGrado()
        {
            grado = verticesConectados.Count();
        }

        /// <summary>
        /// Lista de Vertice por etiqueta 
        /// </summary>
        /// <param name="v"> Vértice del cuál se desean obtener las conexiones. </param>
        /// <param name="g"> Grafo en el que están contenidos los vértices conectados a v
        /// </param>
        /// <returns> Lista con Vertice's conectados a v</returns>
        public List<Vertice> listaVerticesPorEtiqueta(Vertice v, Grafo g)
        {
            List<Vertice> resultado = new List<Vertice>();
            foreach(Vertice vertice in v.verticesConectados)
            {
                resultado.Add(g.verticePorEtiqueta(vertice.etiqueta));
            }
            return resultado;
        }

        /// <summary>
        /// Verifica si un vértice tiene a s como etiqueta de uno de sus vértices
        /// conectados
        /// </summary>
        /// <param name="s"> Cadena que se buscará entre las etiquetas de los vértices 
        /// conectados</param>
        /// <returns> Verdadero si la etiqueta existe entre las conexiones de los vértices</returns>
        public bool contains(string s)
        {
            foreach(Vertice v in verticesConectados)
            {
                if(v.etiqueta == s)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
