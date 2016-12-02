using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIsomorfismo
{
    /// <summary>
    /// Modela las coordenadas rectangulares de cada uno de los vértices en un plano
    /// </summary>
    class Coordenadas
    {
        //Coordenadas en enteros
        int x;
        int y;

        // Propiedades de la coordenadas
        /// <summary>
        /// Coordenada en el eje de las abscisas
        /// </summary>
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
        /// <summary>
        /// Coordenada en el eje de las ordenadas
        /// </summary>
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

        // Constructores de la clase
        /// <summary>
        /// Constructor de la clase Coordenadas
        /// </summary>
        public Coordenadas(){}
        /// <summary>
        /// Constructor de la clase Coordenadas
        /// </summary>
        /// <param name="coordenadaX"> Coordenada en el eje de las abscisas. </param>
        /// <param name="coordenadasY"> Coordenada en el eje de las ordenadas. </param>
        public Coordenadas(int coordenadaX, int coordenadasY)
        {
            x = coordenadaX;
            y = coordenadasY;
        }
    }
}
