using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIsomorfismo
{
    class PermutadorUtilities
    {
        //Variables para las utilidades de todas las permutaciones
        private static PermutadorUtilities instancia;
        public List<Vertice> listaVertices { get; set; }
        List<List<Vertice>> listasProbabilidades = new List<List<Vertice>>();
        List<Vertice> numeros = new List<Vertice>();
        public List<funcion> funcionIsomorfica { get; set; }

        
        /// <summary>
        /// Singleton de las utilidads para mentener una sola instancia
        /// </summary>
        /// <returns></returns>
        public static PermutadorUtilities getInstancia()
        {
            if (instancia == null)
            {
                instancia = new PermutadorUtilities();

            }
            return instancia;
        }

        /// <summary>
        /// Agrega un vertice a la lista según su ID
        /// </summary>
        /// <param name="numero"></param>
        public void agregarNumeros(Vertice numero)
        {
            numeros.Add(numero);
        }

        /// <summary>
        /// Limpia la lista de vertices
        /// </summary>
        public void clearNumeros()
        {
            numeros = new List<Vertice>();
        }

        /// <summary>
        /// Obtiene cada caracter de la cadena y según el ID así ingresa los vertices
        /// a una lista
        /// </summary>
        /// <param name="permutacion"></param>
        public void agregarListaProb(string permutacion, List<Vertice> vertices1, Matriz matrizAdyacencia1, Matriz matrizAdyacencia2, ref bool encuentraRelacion, ref List<funcion> listaFunciones)
        {
            for (int i = 0; i < permutacion.Length; i++)
            {
                for (int j = 0;j < listaVertices.Count;j++)
                {
                    if ((int)permutacion[i] == listaVertices[j].ID)
                    {
                        // Si el ID de la cadena es igual al ID de un vertice se agrega
                        // ese vertice al array
                        numeros.Add(listaVertices[j]);
                        break;
                    }
                }
            }


            List<Vertice> vertices2 = new List<Vertice>(numeros);
            if (vertices1.Count > 9)
            {
                // En caso de que la cantidad de vértices sea mayor a nueve, solo se 
                // genera una función de isomorfismo.
                Matriz posibleMatriz = OperacionesMatriz.generarMatrizPosible(vertices1,
                        vertices2);
                if (Isomorfismo.gradosListasCoinciden(vertices1, vertices2) &&
                        // Es verdadero si paras el producto de
                        // (posibleMatriz)*(matrizAdyacencia2)*(matryzAdyacencia2Transpuesta)
                        // se genera la matriz de adyacencia del primer grafo.
                        matrizAdyacencia1.equivalent(OperacionesMatriz.multiplicar(
                            posibleMatriz, matrizAdyacencia2, OperacionesMatriz.transpose(
                                posibleMatriz))))
                {
                    encuentraRelacion = true;
                    funcion f = new funcion();
                    f.V1 = vertices1;
                    f.V2 = vertices2;
                    listaFunciones.Add(f);
                    funcionIsomorfica = listaFunciones;
                }
            }
            else
            {
                listasProbabilidades.Add(vertices2);
            }
        }

        /// <summary>
        /// Se reinician las utilidades
        /// </summary>
        public void reiniciar()
        {
            listasProbabilidades.Clear();
            
        }

        /// <summary>
        /// Retorna todas las permutaciones
        /// </summary>
        /// <returns></returns>
        public List<List<Vertice>> obtenerListasPermutadas()
        {
            return listasProbabilidades;
        }
    }
}
