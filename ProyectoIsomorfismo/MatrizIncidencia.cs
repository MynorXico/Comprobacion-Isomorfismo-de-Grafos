using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIsomorfismo
{
    class MatrizIncidencia
    {
        public List<List<int>> matriz = new List<List<int>>();

        public bool Equivalent(MatrizIncidencia m)
        {
            for(int i = 0; i < matriz.Count; i++)
            {
                for(int j = 0; j < matriz[0].Count; j++)
                {
                    if(matriz[i][j] != m.matriz[i][j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void agregarFila(List<int> lista) {
            matriz.Add(lista);
        }
    }
}
