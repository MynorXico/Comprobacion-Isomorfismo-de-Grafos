using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace ProyectoIsomorfismo
{
    public partial class Form1 : Form
    {
        Grafo g1;
        Grafo g2;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGrafo1_Click(object sender, EventArgs e)
        {
            CargaDatos c1 = new CargaDatos();
            if(c1.cargarGrafo(ref g1))
            {
                MessageBox.Show("El grafo fue cargado correctamente.", "Carga exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnGrafo1.Enabled = false;
                if (!btnGrafo2.Enabled)
                {
                    btnComprobar.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("El grafo no se cargó corectamente", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnGrafo2_Click(object sender, EventArgs e)
        {
            CargaDatos c1 = new CargaDatos();
            if (c1.cargarGrafo(ref g2))
            {
                MessageBox.Show("El grafo fue cargado correctamente.", "Carga exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnGrafo2.Enabled = false;
                if (!btnGrafo1.Enabled)
                {
                    btnComprobar.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("El grafo no se cargó corectamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Reiniciar()
        {
            btnGrafo1.Enabled = true;
            btnGrafo2.Enabled = true;
            btnComprobar.Enabled = false;
        }

        private void btnComprobar_Click(object sender, EventArgs e)
        {
            if(!Isomorfismo.comprobarIsomorfismo(g1, g2))
            {
                MessageBox.Show("Los grafos no son isomorfos", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reiniciar();
            }
            else
            {
                MessageBox.Show("ajsldñflkajskldpflakñjsldkjfñlkj");
                Reiniciar();
            }
        }
    }
}
