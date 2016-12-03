using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIsomorfismo
{
    public class PermutadorAristas
    {

        /// <summary>
        /// Funcion que permuta una lista de vertices
        /// </summary>
        /// <param name="listaPermutada"></param>
        /// <returns></returns>
        public List<List<Arista>> combinar(List<Arista> listaPermutada)
        {
            /* Se reinician las permutaciones por si alguna lista estaba llena en las uti
            lidades a través de Singleton. */
            PermutadorUtilitiesAristas.getInstancia().reiniciar();

            /* Se agrega la lista de vertices en las utilidaddes para que cuando se reci-
            ba un stringpermutado con sus ID sepa cual ingresar en que posicion. */
            PermutadorUtilitiesAristas.getInstancia().listaAristas = listaPermutada;

            /* Se crea una lista de lista de vertices para guardar todas las 
            permutaciones. */
            List<List<Arista>> lista = new List<List<Arista>>();
            string permutarCadena = "";

            /* Como lo que se permuta es un string se crea una cadena con los ID de los 
            vertices. */
            for (int i = 0; i < listaPermutada.Count; i++)
            {
                permutarCadena = permutarCadena + (char)listaPermutada[i].ID;

            }

            // Se manda a combinar la cadena. 
            Combinacion(permutarCadena);

            // Se reciben todas las combinaciones en una lista.
            lista = PermutadorUtilitiesAristas.getInstancia().obtenerListasPermutadas();
            return lista;

        }

        /// <summary>
        /// Funcion que manda a combinar el string con un array booleano 
        /// </summary>
        /// <param name="s"></param>
        public void Combinacion(string s)
        {
            bool[] igualacion = new bool[s.Length];
            Combinaciones(s, "", igualacion);
        }

        /// <summary>
        /// Funcion que combina el string y devuelve todas las combinaciones posibles 
        /// del mismo tamaño del string que recibe.
        /// </summary>
        /// <param name="cadena"></param>
        /// <param name="combinacion"></param>
        /// <param name="iguales"></param>
        public void Combinaciones(string cadena, string combinacion, bool[] iguales)
        {

            if (cadena.Length == combinacion.Length)
            {
                // Se manda a agregar la combinación en las utilidades del permutador.
                PermutadorUtilitiesAristas.getInstancia().agregarListaProb(combinacion);
                /* Se limpia el array que se utilizo para agregar cada vertice según
                su ID en la cadena enviada. */
                PermutadorUtilitiesAristas.getInstancia().clearNumeros();
            }

            //Se generan todas las combinaciones y si son iguales se descartan
            for (int i = 0; i < iguales.Length; i++)
            {
                if (!iguales[i])
                {
                    iguales[i] = true;
                    Combinaciones(cadena, combinacion + cadena[i], iguales);
                    iguales[i] = false;
                }
            }
        }
    }
}
