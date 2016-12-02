using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIsomorfismo
{
    /// <summary>
    /// Modela una matriz matemática
    /// </summary>
    class Matriz
    {
        /// <summary>
        /// Constructor de la clase matriz
        /// </summary>
        public Matriz()
        {
            matriz = new List<List<int>>();
        }

        /// <summary>
        /// Lista de lista de enteros que almacena los datos contenidos en la matriz
        /// </summary>
        public List<List<int>> matriz { get; set; }

        /// <summary>
        /// Verifica que dos matrices sean equivaltnes
        /// </summary>
        /// <param name="cmp"> Matriz a comparar </param>
        /// <returns> Verdadero si las matrices contienen los mismos datos en el mismo
        /// orden</returns>
        public bool equivalent(Matriz cmp)
        {
            // Compara cada una de las posiciones en las dos matrices.
            for(int i = 0; i < matriz.Count; i++)
            {
                for(int j = 0; j < matriz[0].Count; j++)
                {
                    if(matriz[i][j] != cmp.matriz[i][j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
    }
}
