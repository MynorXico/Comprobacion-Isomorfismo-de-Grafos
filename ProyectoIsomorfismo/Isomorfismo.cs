using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
namespace ProyectoIsomorfismo
{
    /// <summary>
    /// Estructura que permite guardar dos listados de vértices que se relacionan.
    /// </summary>
    struct funcion
    {
        public List<Vertice> V1;
        public List<Vertice> V2;
    }

    /// <summary>
    /// Se encarga de realizar las validaciones para determinar si existe una re-
    /// lación de isomorfismo entre dos grafos.
    /// </summary>
    class Isomorfismo
    {        
        /// <summary>
        /// Función que realiza las llamadas a las funciones que realizan las va-
        /// lidaciones que verifican la coincidencia en cantidad de aristas, can-
        /// tidad de vértices, si existe al menos una combinación para que coin-
        /// cidan los grados de los vértices en ambos grafos y en caso de que 
        /// exista una función, generar todas las funciones de isomorfismo en los 
        /// grafos.
        /// </summary>
        /// <param name="g1"> Primer grafo </param>
        /// <param name="g2"> Segundo grafo</param>
        /// <param name="barraProgreso"> Barra que muestra el progreso en la bús-
        /// queda de una función de isomorfismo</param>
        /// <param name="cbFunciones"> comboBox en el que se guardan las funciones de
        /// isomorfismo encontradas (en caso de existir).
        /// encontradas</param>
        /// <param name="listaFunciones"> Lista en la que se guardarán las funciones
        /// de isomorfismo encontradas por la función. </param>
        /// <returns></returns>
        public static bool comprobarIsomorfismo(Grafo g1, Grafo g2, 
            ProgressBar barraProgreso, ComboBox cbFunciones,
            ref List<funcion> listaFunciones)
        {
            if (!(mismaCantidadAristas(g1, g2) & mismaCantidadVertices(g1, g2) &
                mismosGradosVertices(g1, g2) && encuentraFuncionAdyacencia(g1, g2, 
                barraProgreso, cbFunciones, ref listaFunciones))){
                barraProgreso.Maximum = 1;
                barraProgreso.Value++;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verifica si la cantidad de vertices en g1 y g2 coincide
        /// </summary>
        /// <param name="g1"> Primer grafo</param>
        /// <param name="g2"> Segundo grafo</param>
        /// <returns> Verdadero si hay coincidencia entre la cantidad de vértices del
        /// primer grafo y del segundo</returns>
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

        /// <summary>
        /// Verifica si la cantidad de aristas en g1 y g2 coincide
        /// </summary>
        /// <param name="g1"> Primer grafo </param>
        /// <param name="g2"> Segundo grafo</param>
        /// <returns> Verdadero si existe coincidencia entre la cantidad de aristas del
        /// primer grafo y del segundo</returns>
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

        /// <summary>
        /// Verifica si existe al menos una manera de emparejar los vértices para que
        /// los grados de los vértices del grafo g1 coincidan con los del grafo g2
        /// </summary>
        /// <param name="g1"> Primer grafo </param>
        /// <param name="g2"> Segundo grafo</param>
        /// <returns> Verdadero si existe coincidencia entre los grados de los vertices
        /// del primer grafo y los del segundo </returns>
        static bool mismosGradosVertices(Grafo g1, Grafo g2)
        {
            List<int> grados1 = new List<int>();
            List<int> grados2 = new List<int>();

            // Llena dos listas con los grados de cada vértice en el primero y segundo
            // grafo
            foreach (Vertice v in g1.lstVertices)
                grados1.Add(v.grado);
            foreach (Vertice v in g2.lstVertices)
                grados2.Add(v.grado);

            // Por cada elemento en la lista grados2, busca una coincidencia en la lista
            // de grados de los vértices de g2; de encontralos elimina el elemento de la
            // lista de grados de vértices del grafo g1
            foreach (int i in grados2)
            {
                if (grados1.Contains(i))
                    grados1.Remove(i);
            }
            // En caso de no coincidan, la lista de grados de vértices del primer grafo
            // no quedará vacía
            if (grados1.Count != 0)
            {
                MessageBox.Show("Los grados de los vértices de los grafos no coinciden.");
                return false;
            }
            return true;
        }    

        /// <summary>
        /// Verifica que dos listas tengan los mismos grados en el mismo orden
        /// </summary>
        /// <param name="lista1"> Primera lista</param>
        /// <param name="lista2"> Segunda lista</param>
        /// <returns> Verdadero si coinciden los grados en el mismo orden en la listas. </returns>
        private static bool gradosListasCoinciden(List<Vertice> lista1, 
            List<Vertice> lista2)
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

        /// <summary>
        /// Comprueba que lo grados de dos vértices coincidan
        /// </summary>
        /// <param name="v1"> Primer vértice </param>
        /// <param name="v2"> Segundo vértice</param>
        /// <returns></returns>
        private static bool gradoCoincide(Vertice v1, Vertice v2)
        {
            return v1.grado == v2.grado;
        }       

       /// <summary>
       /// Función encargada de buscar y, en caso de encontrar, generar tantas funciones
       /// de isomorfismo como sean posibles.
       /// </summary>
       /// <param name="g1"> Primer grafo </param>
       /// <param name="g2"> Segundo grafo </param>
       /// <param name="barraProgreso"> Barra que indicará el progreso de la búsqueda de
       /// las funciones de isomorfismo </param>
       /// <param name="cbFunciones"> comboBox en el que se almacenarán los nombres de
       /// las funciones a medida que vayan siendo encontradas </param>
       /// <param name="listaFunciones"> Lista en la que se almacenará la información de
       /// cada una de las funciones encontradas</param>
       /// <returns></returns>
        static bool encuentraFuncionAdyacencia(Grafo g1, Grafo g2, ProgressBar barraProgreso, ComboBox cbFunciones, ref List<funcion> listaFunciones)
        {
            // Variable booleana que indica si se encontró o no al menos una función de
            // isomorfismo para los dos grafos dados
            bool pruebaSuperada = false;

            // Instancia e inicia un stopWatch para verificar el tiempo de que el algo-
            // ritmo tarda en encontrar las funciones
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // Lista que almacena las funciones generadas.
            List<funcion> lstFuncionesEncontradas = new List<funcion>();

            // Instancia que servirá para obtener todas las permutaciones de vértices en
            // un grafo
            PermutadorVertices p = new PermutadorVertices();

            // Lista que almacena los vértices del primer grafo
            List<Vertice> vertices1 = g1.lstVertices;

            // Lista de que almacena todas las posibles permutaciones de vértices del
            // primer grafo
            List<List<Vertice>> permutacionesV2= p.combinar(g2.lstVertices);

            // Se crean las matrices de adyacencia de los dos grafos
            Matriz matrizAdyacencia1 = g1.matrizAdyacencia();
            Matriz matrizAdyacencia2 = g2.matrizAdyacencia();

            barraProgreso.Maximum = permutacionesV2.Count;
            int numeroFunciones = 0;

            // Realiza el proceso por cada una de las permutaciones realizadas para el
            // listado de vértices del segundo grafo
            foreach (List<Vertice> vertices2 in permutacionesV2)
            {                
                barraProgreso.Value++;
                // Se genera una posible matriz con los dos listados de vértices
                Matriz posibleMatriz = OperacionesMatriz.generarMatrizPosible(vertices1, 
                    vertices2);
                // Verifica que en la permutación actual almenos coincidan los grados de
                // los vértices, de lo contrario es inútil continuar buscando la matriz.
                if(gradosListasCoinciden(vertices1, vertices2) &&
                    // Es verdadero si para el producto de
                    // (posibleMatriz)*(matrizAdyacencia2)*(matryzAdyacencia2Transpuesta)
                    // se genera la matriz de adyacencia del primer grafo.
                    matrizAdyacencia1.equivalent(OperacionesMatriz.multiplicar(posibleMatriz,matrizAdyacencia2, OperacionesMatriz.transpose(posibleMatriz)))){
                    funcion funcionNueva = new funcion(); // Se crea una nueva función
                    funcionNueva.V1 = vertices1;
                    funcionNueva.V2 = vertices2;
                    // La función creada se añade a la lista de funciones
                    lstFuncionesEncontradas.Add(funcionNueva);
                    numeroFunciones++;
                    cbFunciones.Items.Add(string.Format("Funcion #{0}", numeroFunciones));
                    pruebaSuperada = true;
                }
            }
            listaFunciones = lstFuncionesEncontradas;
            sw.Stop();
            // Muestra información del proceso al usuario
            string msg = String.Format("El programa tardó {0}ms en encontrar las funciones", sw.ElapsedMilliseconds);
            MessageBox.Show(msg, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return pruebaSuperada;
        }
    }
}
