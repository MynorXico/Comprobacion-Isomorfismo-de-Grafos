using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIsomorfismo
{
    /// <summary>
    /// Contiene funciones necesarias para operar matrices.
    /// </summary>
    class OperacionesMatriz
    {
        internal Matriz Matriz
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        /// <summary>
        /// Multiplicación de dos matrices
        /// </summary>
        /// <param name="m1"> Primera matriz </param>
        /// <param name="m2"> Segunda matriz </param>
        /// <returns> Devuelve el producto punto entre dos matrices. </returns>
        public static Matriz multiplicar(Matriz m1, Matriz m2) {
            Matriz resultado = new Matriz();
            for(int i = 0; i < m1.matriz.Count; i++)
            {
                List<int> tmpFila = new List<int>();
                for(int j = 0; j < m1.matriz[0].Count; j++)
                {
                    tmpFila.Add(productoFilaColumna(obtenerFila(i, m1), 
                        obtenerColumna(j, m2)));
                }
                resultado.matriz.Add(tmpFila);
            }
            return resultado;
        }

        /// <summary>
        /// Mulstiplicación de tres matrices
        /// </summary>
        /// <param name="m1"> Primera matriz </param>
        /// <param name="m2"> Segudna matriz </param>
        /// <param name="m3"> Tercera matriz </param>
        /// <returns> Producto punto de las tres matrices operadas de izquierda a derecha
        /// </returns>
        public static Matriz multiplicar(Matriz m1, Matriz m2, Matriz m3)
        {
            return multiplicar(multiplicar(m1, m2), m3);
        }

        /// <summary>
        /// Fila de matriz
        /// </summary>
        /// <param name="n"> Número de fila </param>
        /// <param name="matriz"> Matriz de la cuál se obtendrá la fila </param>
        /// <returns> Lista con fila correspondiente al número ingresado </returns>
        public static List<int> obtenerFila(int n, Matriz matriz)
        {
            List<int> resultado = new List<int>();

            for (int i = 0; i < matriz.matriz[0].Count; i++)
            {
                resultado.Add(matriz.matriz[n][i]);
            }
            return resultado;
        }

        /// <summary>
        /// Columna de matriz
        /// </summary>
        /// <param name="n"> Número de columna </param>
        /// <param name="m"> Matriz de la cuál se obtendrá la columna</param>
        /// <returns> Lista con columna correspondiente al número ingresado</returns>
        public static List<int> obtenerColumna(int n, Matriz m)
        {
            List<int> resultado = new List<int>();
            for(int i = 0; i < m.matriz.Count; i++)
            {
                resultado.Add(m.matriz[i][n]);
            }
            return resultado;
        }

        /// <summary>
        /// Devuelve el producto punto de dos vectores.
        /// </summary>
        /// <param name="fila"> Primer vector </param>
        /// <param name="columna"> Segundo vector </param>
        /// <returns> Producto punto de primer vector y segundo vector </returns>
        public static int productoFilaColumna(List<int> fila, List<int> columna)
        {
            int producto = 0;
            for(int i = 0; i < fila.Count; i++)
            {
                producto += fila[i] * columna[i];
            }
            return producto;
        }

        /// <summary>
        /// Transposición de matriz
        /// </summary>
        /// <param name="m"> Matriz a transponer </param>
        /// <returns> Matriz transpuesta de m </returns>
        public static Matriz transpose(Matriz m)
        {
            Matriz resultado = new Matriz();
            for(int i = 0; i < m.matriz.Count(); i++)
            {
                List<int> tmpFila = obtenerColumna(i, m);
                resultado.matriz.Add(tmpFila);
            }
            return resultado;
        }

        /// <summary>
        /// Matriz de incidencia entre vértices de distintos grafos
        /// </summary>
        /// <param name="v1"> Vértices de primer grafo </param>
        /// <param name="v2"> Vértices de segundo grafo </param>
        /// <returns> Matriz de incidencia entre vértices de distintos grafos </returns>
        public static Matriz generarMatrizPosible(List<Vertice> v1, List<Vertice> v2)
        {
            Matriz matrix = new Matriz();
            
            // Llena la matriz con ceros
            for(int i = 0; i < v1.Count; i++)
            {
                List<int> fila = new List<int>();
                for(int j = 0; j < v2.Count; j++)
                {
                    fila.Add(0);
                }
                matrix.matriz.Add(fila);
            }
            
            // Agrega unos por cada coincidencia entre vértice del primer grafo y
            // vértice del segundo grafo.
            for(int i = 0; i < v1.Count; i++)
            {
                matrix.matriz[int.Parse(v1[i].etiqueta)][int.Parse(v2[i].etiqueta)] = 1;
            }

            return matrix;

        }
    }
}
