using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIsomorfismo
{
    class PermutadorUtilities
    {
        private static PermutadorUtilities instancia;
        public List<Vertice> listaVertices { get; set; }
        List<List<Vertice>> listasProbabilidades = new List<List<Vertice>>();
        List<Vertice> numeros = new List<Vertice>();

        public static PermutadorUtilities getInstancia()
        {
            if (instancia == null)
            {
                instancia = new PermutadorUtilities();

            }
            return instancia;
        }

        public void agregarNumeros(Vertice numero)
        {
            numeros.Add(numero);
        }

        public void clearNumeros()
        {
            numeros = new List<Vertice>();
        }

        public void agregarListaProb(string permutacion)
        {
            for (int i = 0; i < permutacion.Length; i++)
            {
                for (int j = 0;j < listaVertices.Count;j++)
                {
                    if ((int)permutacion[i] == listaVertices[j].ID)
                    {
                        numeros.Add(listaVertices[j]);
                        /////////
                        break;
                    }
                }
            }
           listasProbabilidades.Add(new List<Vertice>(numeros));
        }

        public void reiniciar()
        {
            listasProbabilidades.Clear();
            
        }

        public List<List<Vertice>> obtenerListasPermutadas()
        {
            return listasProbabilidades;
        }
    }
}
