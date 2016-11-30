using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoIsomorfismo
{
    class Isomorfismo
    {
        public static bool comprobarIsomorfismo(Grafo g1, Grafo g2)
        {
            if (!(mismaCantidadAristas(g1,g2) & mismaCantidadVertices(g1, g2) & mismosGradosVertices(g1, g2)))
                return false;

            return true;
        }

        static bool mismaCantidadVertices(Grafo g1, Grafo g2)
        {
            bool pruebaSuperada = g1.cantidadVertices == g2.cantidadVertices;
            if (!pruebaSuperada)
            {
                MessageBox.Show("Los grafos no tienen la misma cantidad de vértices");
                return false;
            }
            return true;
        }

        static bool mismaCantidadAristas(Grafo g1, Grafo g2)
        {
            bool pruebaSuperada = g1.cantidadAristas == g2.cantidadAristas;
            if (!pruebaSuperada)
            {
                MessageBox.Show("Los grafos no tienen la misma cantidad de aristas.");
                return false;
            }
            return true;
        }

        static bool mismosGradosVertices(Grafo g1, Grafo g2)
        {
            List<int> grados1 = new List<int>();
            List<int> grados2 = new List<int>();

            foreach (Vertice v in g1.lstVertices)
                grados1.Add(v.grado);
            foreach (Vertice v in g2.lstVertices)
                grados2.Add(v.grado);

            foreach (int i in grados2)
            {
                if (grados1.Contains(i))
                    grados1.Remove(i);
            }

            if (grados1.Count != 0)
            {
                MessageBox.Show("Los grados de los vértices de los grafos no coinciden.");
                return false;
            }
            return true;
        }

        static bool encontrarFuncion(Grafo g1, Grafo g2)
        {
               
        }

        static bool funcionRecursiva(Vertice v1, Vertice v2)
        {
            if (v1.grado != v2.grado)
                return false;
            List<string> vertices1 = new List<string>();
            foreach(Vertice v in v1.verticesConectados)
            {
                vertices1.Add(v.etiqueta);
            }
            List<string> vertices2 = new List<string>();
            foreach (Vertice v in v2.verticesConectados)
            {
                vertices1.Add(v.etiqueta);
            }

            return true;
        }  
       


        private static bool gradoCoincide(Vertice v1, Vertice v2)
        {
            return v1.grado == v2.grado;
        }

        public static bool verticeExiste(string s, List<Vertice> listaVertices)
        {
            foreach (Vertice v in listaVertices)
            {
                if (v.etiqueta == s)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
