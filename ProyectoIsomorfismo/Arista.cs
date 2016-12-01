using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ProyectoIsomorfismo
{
    public class Arista
    {
        public string from { get; set; }
        public string to { get; set; }
        public int ID { get; set; }
        public Arista(string[] arreglo, int id)
        {
            ID = id;
            from = arreglo[0];
            try
            {
                to = arreglo[1];
            }
            catch
            {
                MessageBox.Show("Verifique que el archivo de texto no tenga espacios en blanco.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
