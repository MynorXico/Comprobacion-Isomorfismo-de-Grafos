using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIsomorfismo
{
    public class PermutadorUtilitiesAristas
    {
        //Variables para las utilidades de todas las permutaciones
        private static PermutadorUtilitiesAristas instancia;
        public List<Arista> listaAristas { get; set; }
        List<List<Arista>> listasProbabilidades = new List<List<Arista>>();
        List<Arista> numeros = new List<Arista>();

        /// <summary>
        /// Singleton de las utilidads para mentener una sola instancia
        /// </summary>
        /// <returns></returns>
        public static PermutadorUtilitiesAristas getInstancia()
        {
            if (instancia == null)
            {
                instancia = new PermutadorUtilitiesAristas();

            }
            return instancia;
        }

        /// <summary>
        /// Agrega un vertice a la lista según su ID
        /// </summary>
        /// <param name="numero"></param>
        public void agregarNumeros(Arista numero)
        {
            numeros.Add(numero);
        }

        /// <summary>
        /// Limpia la lista de vertices
        /// </summary>
        public void clearNumeros()
        {
            numeros = new List<Arista>();
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
                for (int j = 0; j < listaAristas.Count; j++)
                {
                    if ((int)permutacion[i] == listaAristas[j].ID)
                    {
                        // Si el ID de la cadena es igual al ID de un vertice se agrega
                        // ese vertice al array
                        numeros.Add(listaAristas[j]);
                        break;
                    }
                }
            }
            listasProbabilidades.Add(new List<Arista>(numeros));
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
        public List<List<Arista>> obtenerListasPermutadas()
        {
            return listasProbabilidades;
        }
    }
}
