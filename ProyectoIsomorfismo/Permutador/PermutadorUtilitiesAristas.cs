using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIsomorfismo
{
    public class PermutadorUtilitiesAristas
    {
        private static PermutadorUtilitiesAristas instancia;
        public List<Arista> listaAristas { get; set; }
        List<List<Arista>> listasProbabilidades = new List<List<Arista>>();
        List<Arista> numeros = new List<Arista>();

        public static PermutadorUtilitiesAristas getInstancia()
        {
            if (instancia == null)
            {
                instancia = new PermutadorUtilitiesAristas();

            }
            return instancia;
        }

        public void agregarNumeros(Arista numero)
        {
            numeros.Add(numero);
        }

        public void clearNumeros()
        {
            numeros = new List<Arista>();
        }

        public void agregarListaProb(string permutacion)
        {
            for (int i = 0; i < permutacion.Length; i++)
            {
                for (int j = 0; j < listaAristas.Count; j++)
                {
                    if ((int)permutacion[i] == listaAristas[j].ID)
                    {
                        numeros.Add(listaAristas[j]);
                        break;
                    }
                }
            }
            listasProbabilidades.Add(new List<Arista>(numeros));
        }

        public void reiniciar()
        {
            listasProbabilidades.Clear();

        }

        public List<List<Arista>> obtenerListasPermutadas()
        {
            return listasProbabilidades;
        }
    }
}
