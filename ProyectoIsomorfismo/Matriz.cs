using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIsomorfismo
{
    class Matriz
    {
        public List<List<int>> matriz { get; set; }

        public bool equivalent(Matriz cmp)
        {
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
        public Matriz()
        {
            matriz = new List<List<int>>();
        }
    }
}
