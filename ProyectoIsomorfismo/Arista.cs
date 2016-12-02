using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ProyectoIsomorfismo
{
    /// <summary>
    /// Clase que modela la arista de un grafo
    /// </summary>
    public class Arista
    {

        /// <summary>
        /// De donde viene la arista
        /// </summary>
        public string from { get; set; }
        /// <summary>
        /// Hacia donde va la arista
        /// </summary>
        public string to { get; set; }

        /// <summary>
        /// Identificador de la arista
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Constructor de la clase Arista
        /// </summary>
        /// <param name="arreglo"> Contiene información de donde viene y hacia donde va.</param>
        /// <param name="id"> Identificación de la arista</param>
        public Arista(string[] arreglo, int id)
        {
            ID = id;
            from = arreglo[0];
            // Maneja el error que ocurre cuando se intenta crear una arista con información vacía
            try
            {
                to = arreglo[1];
            }
            catch
            {
                MessageBox.Show("Verifique que el archivo de texto no tenga espacios en"+
                    "blanco.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
