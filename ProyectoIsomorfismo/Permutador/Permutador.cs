using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIsomorfismo
{
    class PermutadorVertices
    {
        /// <summary>
        /// Funcion que permuta una lista de vertices
        /// </summary>
        /// <param name="listaPermutada"></param>
        /// <returns></returns>
        public List<List<Vertice>> combinar(List<Vertice> listaPermutada, List<Vertice> 
            vertices1, Matriz matrizAdyacencia1, Matriz matrizAdyacencia2, 
            ref bool encuentraRelacion, ref List<funcion> listaFunciones)
        {
            /* Se reinician las permutaciones por si alguna lista estaba llena en las uti
            lidades a través de Singleton. */
            PermutadorUtilities.getInstancia().reiniciar();

            /* Se agrega la lista de vertices en las utilidaddes para que cuando se reci-
            ba un stringpermutado con sus ID sepa cual ingresar en que posicion. */
            PermutadorUtilities.getInstancia().listaVertices = listaPermutada;

            /* Se crea una lista de lista de vertices para guardar todas las 
            permutaciones. */
            List<List<Vertice>> lista = new List<List<Vertice>>();
            string permutarCadena = "";

            /* Como lo que se permuta es un string se crea una cadena con los ID de los 
            vertices. */
            for (int i = 0;i < listaPermutada.Count;i++)
            {
                permutarCadena = permutarCadena + (char)listaPermutada[i].ID;
            }

            // Se manda a combinar la cadena. 
            Combinacion(permutarCadena, vertices1, matrizAdyacencia1, matrizAdyacencia2, 
                ref encuentraRelacion, ref listaFunciones);

            // Se reciben todas las combinaciones en una lista.
            lista = PermutadorUtilities.getInstancia().obtenerListasPermutadas();
            return lista;

        }

        /// <summary>
        /// Funcion que manda a combinar el string con un array booleano 
        /// </summary>
        /// <param name="s"></param>
        public void Combinacion(string s, List<Vertice> vertices1, Matriz matrizAdyacencia1
            , Matriz matrizAdyacencia2, ref bool encuentraRelacion, 
            ref List<funcion> listaFunciones)
        {
            bool[] esIgual = new bool[s.Length];
            Combinaciones(s, "", esIgual, vertices1, matrizAdyacencia1, matrizAdyacencia2, 
                ref encuentraRelacion, ref listaFunciones);
        }

        /// <summary>
        /// Funcion que combina el string y devuelve todas las combinaciones posibles 
        /// del mismo tamaño del string que recibe.
        /// </summary>
        /// <param name="cadena"></param>
        /// <param name="combinacion"></param>
        /// <param name="iguales"></param>
        public void Combinaciones(string cadena, string combinacion, bool[] iguales,
            List<Vertice> vertices1, Matriz matrizAdyacencia1, Matriz matrizAdyacencia2,
            ref bool encuentraRelacion, ref List<funcion> listaFunciones)
        {
            if (encuentraRelacion)
            {
                return;
            }

            if (cadena.Length == combinacion.Length)
            {
                // Se manda a agregar la combinación en las utilidades del permutador.
                PermutadorUtilities.getInstancia().agregarListaProb(combinacion, vertices1, 
                    matrizAdyacencia1, matrizAdyacencia2, ref encuentraRelacion,
                    ref listaFunciones);

                /* Se limpia el array que se utilizo para agregar cada vertice según
                su ID en la cadena enviada. */
                PermutadorUtilities.getInstancia().clearNumeros();
            }

            //Se generan todas las combinaciones y si son iguales se descartan
            for (int i = 0; i < iguales.Length; i++)
            {
                if (encuentraRelacion)
                    return;
                if (!iguales[i])
                {
                    if (encuentraRelacion)
                        break;

                    iguales[i] = true;
                    Combinaciones(cadena, combinacion + cadena[i], iguales, vertices1, 
                        matrizAdyacencia1, matrizAdyacencia2, ref encuentraRelacion,
                        ref listaFunciones);
                    iguales[i] = false;
                }
            }
        }
    }
}
