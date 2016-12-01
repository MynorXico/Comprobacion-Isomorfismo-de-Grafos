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
        List<funcion> listaFunciones;
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
            cbFunciones.Enabled = false;
            cbFunciones.Items.Clear();
            dgvMostrarFuncion.Enabled = false;
            dgvMostrarFuncion.DataSource = null;
            while(dgvMostrarFuncion.Rows.Count != 0)
            {
                dgvMostrarFuncion.Rows.RemoveAt(0);
            }
        }

        private void btnComprobar_Click(object sender, EventArgs e)
        {
            if(!Isomorfismo.comprobarIsomorfismo(g1, g2, pbLoading, cbFunciones, ref listaFunciones))
            {
                MessageBox.Show("Los grafos no son isomorfos.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                cbFunciones.Enabled = true;
                dgvMostrarFuncion.Enabled = true;
                MessageBox.Show("Los grafos son isomorfos.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cbFunciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(dgvMostrarFuncion.Rows.Count != g1.cantidadVertices)
            {
                for(int i = 0; i < g1.cantidadVertices; i++)
                {
                    dgvMostrarFuncion.Rows.Add();
                }
            }

            for(int i = 0; i < listaFunciones[cbFunciones.SelectedIndex].V1.Count; i++)
            {
                dgvMostrarFuncion[0, i].Value = listaFunciones[cbFunciones.SelectedIndex].V1[i].etiqueta;
                dgvMostrarFuncion[1, i].Value = listaFunciones[cbFunciones.SelectedIndex].V2[i].etiqueta;
            }


        }

        private void cargarDatosDeNuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reiniciar();
        }
    }
}
