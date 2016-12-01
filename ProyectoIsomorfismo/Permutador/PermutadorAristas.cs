using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIsomorfismo
{
    public class PermutadorAristas
    {


        public List<List<Arista>> combinar(List<Arista> listaPermutada)
        {
            PermutadorUtilitiesAristas.getInstancia().reiniciar();
            PermutadorUtilitiesAristas.getInstancia().listaAristas = listaPermutada;
            List<List<Arista>> lista = new List<List<Arista>>();
            string cadenaPermutada = "";

            for (int i = 0; i < listaPermutada.Count; i++)
            {
                cadenaPermutada = cadenaPermutada + (char)listaPermutada[i].ID;

            }

            Combinacion(cadenaPermutada);
            lista = PermutadorUtilitiesAristas.getInstancia().obtenerListasPermutadas();
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
                PermutadorUtilitiesAristas.getInstancia().agregarListaProb(combinacion);
                PermutadorUtilitiesAristas.getInstancia().clearNumeros();
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
