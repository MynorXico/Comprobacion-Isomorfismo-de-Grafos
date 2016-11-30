using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIsomorfismo
{
    class Permutador
    {

        public List<List<Vertice>> combinar(List<Vertice> listaPermutada )
        {
            PermutadorUtilities.getInstancia().reiniciar();
            PermutadorUtilities.getInstancia().listaVertices = listaPermutada;
            List<List<Vertice>> lista = new List<List<Vertice>>();
            string cadenaPermutada = "";

            for (int i = 0;i < listaPermutada.Count;i++)
            {
                cadenaPermutada = cadenaPermutada + (char)listaPermutada[i].ID;

            }

            Combinacion(cadenaPermutada);
            lista = PermutadorUtilities.getInstancia().obtenerListasPermutadas();
            return lista;

        }

        public void Combinacion(string s)
        {
            bool[] igualacion = new bool[s.Length];
            Combinaciones(s, "", igualacion);
        }

        public void Combinaciones(string cadena, string combinacion, bool[] iguales)
        {

            if (cadena.Length == combinacion.Length)
            {
                PermutadorUtilities.getInstancia().agregarListaProb(combinacion);
                PermutadorUtilities.getInstancia().clearNumeros();
            }
            
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
