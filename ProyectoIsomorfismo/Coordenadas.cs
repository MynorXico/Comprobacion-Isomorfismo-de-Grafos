using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIsomorfismo
{
    class Coordenadas
    {
        //Coordenadas en enteros
        private int x;
        private int y;

        //COORDENADAS
        public Coordenadas()
        {

        }

        public Coordenadas(int coordenadaX, int coordenadasY)
        {
            x = coordenadaX;
            y = coordenadasY;
        }

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

    }
}
