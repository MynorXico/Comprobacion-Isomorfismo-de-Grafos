﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoIsomorfismo
{
    class Isomorfismo
    {

        struct relacion
        {
            public Vertice v1;
            public Vertice v2;
        }

        static int posicionEnLista(List<relacion> lista, Vertice v1)
        {
            for(int i = 0; i < lista.Count; i++)
            {
                if(lista[i].v1.etiqueta == v1.etiqueta)
                {
                    return i;
                }
            }
            return -1;
        }

        public static bool comprobarIsomorfismo(Grafo g1, Grafo g2)
        {
            if (!(mismaCantidadAristas(g1, g2) & mismaCantidadVertices(g1, g2) & mismosGradosVertices(g1, g2) & funcionIsomorfisoXMatriz(g1, g2))){
                return false;
            }
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

        static List<relacion> encontrarFuncion(Grafo g1, Grafo g2)
        {
            List<relacion> listaFuncion = new List<relacion>();
            List<Vertice> v1 = g1.lstVertices;
            List<Vertice> v2 = g2.lstVertices;
            List<List<Vertice>> permutacionesV2 = generarCombinaciones(v2, g2);
            foreach(List<Vertice> list in permutacionesV2)
            {
                for (int i = 0; i < v1.Count; i++)
                {
                    if (funcionRecursiva(v1[i], list[i], g1, g2, listaFuncion))
                    {
                        return listaFuncion;
                    }
                }
            }
            return null;
        }


        static bool funcionRecursiva(Vertice v1, Vertice v2, Grafo g1, Grafo g2, List<relacion> listaFuncion)
        {
       
            List<Vertice> lista1 = v1.listaVerticesPorEtiqueta(v1, g1);
            List<List<Vertice>> permutacionesListaVertice2 = generarCombinaciones(v2.listaVerticesPorEtiqueta(v2, g2), g2);
            for(int i = 0; i < permutacionesListaVertice2.Count; i++)
            {
                if(gradosListasCoinciden(lista1, permutacionesListaVertice2[i]))
                {
                    for (int j = 0; j < lista1.Count; j++)
                    {
                        relacion cmp = new relacion();
                        cmp.v1 = v1;
                        cmp.v2 = v2;
                        if (posicionEnLista(listaFuncion, cmp.v1) != -1 && listaFuncion[posicionEnLista(listaFuncion, cmp.v1)].v2.etiqueta == v2.etiqueta)
                        {
                           
                            return true;
                            
                        }
                        if (posicionEnLista(listaFuncion, cmp.v1) != -1 && listaFuncion[posicionEnLista(listaFuncion, cmp.v1)].v2.etiqueta != v2.etiqueta)
                        {
                            listaFuncion.Remove(listaFuncion.Last<relacion>());
                            return false;
                        }
                        else
                        {
                            listaFuncion.Add(cmp);
                            if (funcionRecursiva(lista1[j], permutacionesListaVertice2[i][j], g1, g2, listaFuncion)){
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }        
       
        static List<List<Vertice>> generarCombinaciones(List<Vertice> listaPermutar, Grafo g)
        {
            PermutadorVertices permuta = new PermutadorVertices();
            List<List<Vertice>> lista = permuta.combinar(listaPermutar);
            return lista;
        }

        /// <summary>
        /// Verifica que dos listas tengan los mismos grados en el mismo orden
        /// </summary>
        /// <param name="lista1"> Primera lista</param>
        /// <param name="lista2"> Segunda lista</param>
        /// <returns> Verdadero si coinciden los grados en el mismo orden en la listas. </returns>
        private static bool gradosListasCoinciden(List<Vertice> lista1, List<Vertice> lista2)
        {
            if (lista1.Count != lista2.Count)
                return false;
            int k = 0;
            foreach (Vertice v in lista1) {
                if (v.grado != lista2[k++].grado)
                {
                    return false;
                }
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

        


        static bool coincidenciaMatrices(MatrizIncidencia matriz1, MatrizIncidencia matriz2)
        {
            for(int i = 0; i < matriz1.matriz.Count; i++)
            {
                for(int j = 0; j < matriz1.matriz[0].Count; j++)
                {
                    if (matriz1.matriz[j][i] != matriz2.matriz[j][i])
                        return false;
                }
            }
            return true;
        }

        static bool funcionIsomorfisoXMatriz(Grafo g1, Grafo g2)
        {
            List<MatrizIncidencia> matrices1 = g1.generarMatrices();
            List<MatrizIncidencia> matrices2 = g2.generarMatrices();

            foreach(MatrizIncidencia matriz1 in matrices1)
            {
                foreach(MatrizIncidencia matriz2 in matrices2)
                {
                    if (matriz1.Equivalent(matriz2))
                        return true;
                }
            }
            return false;
        }
    }
}
