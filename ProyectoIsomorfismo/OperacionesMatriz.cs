using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIsomorfismo
{
    class OperacionesMatriz
    {   
        public static Matriz multiplicar(Matriz m1, Matriz m2) {
            Matriz resultado = new Matriz();
            for(int i = 0; i < m1.matriz.Count; i++)
            {
                List<int> tmpFila = new List<int>();
                for(int j = 0; j < m1.matriz[0].Count; j++)
                {
                    tmpFila.Add(productoFilaColumna(obtenerFila(i, m1), obtenerColumna(j, m2)));
                }
                resultado.matriz.Add(tmpFila);
            }
            return resultado;
        }

        public static Matriz multiplicar(Matriz m1, Matriz m2, Matriz m3)
        {
            return multiplicar(multiplicar(m1, m2), m3);
        }

        public static List<int> obtenerFila(int n, Matriz matriz)
        {
            List<int> resultado = new List<int>();

            for (int i = 0; i < matriz.matriz[0].Count; i++)
            {
                resultado.Add(matriz.matriz[n][i]);
            }
            return resultado;
        }

        public static List<int> obtenerColumna(int n, Matriz m)
        {
            List<int> resultado = new List<int>();
            for(int i = 0; i < m.matriz.Count; i++)
            {
                resultado.Add(m.matriz[i][n]);
            }
            return resultado;
        }

        public static int productoFilaColumna(List<int> fila, List<int> columna)
        {
            int producto = 0;

            for(int i = 0; i < fila.Count; i++)
            {
                producto += fila[i] * columna[i];
            }

            return producto;
        }

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

        public static Matriz generarMatrizPosible(List<Vertice> v1, List<Vertice> v2)
        {
            Matriz matrix = new Matriz();
            
            for(int i = 0; i < v1.Count; i++)
            {
                List<int> fila = new List<int>();
                for(int j = 0; j < v2.Count; j++)
                {
                    fila.Add(0);
                }
                matrix.matriz.Add(fila);
            }

            for(int i = 0; i < v1.Count; i++)
            {
                matrix.matriz[int.Parse(v1[i].etiqueta)][int.Parse(v2[i].etiqueta)] = 1;
            }

            return matrix;

        }
    }
}
