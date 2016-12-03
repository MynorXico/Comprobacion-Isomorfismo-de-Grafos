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
        public void agregarListaProb(string permutacion)
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
           listasProbabilidades.Add(new List<Vertice>(numeros));
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
