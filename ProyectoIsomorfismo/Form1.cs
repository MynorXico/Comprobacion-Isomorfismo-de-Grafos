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
        const int ANGULO_CIRCULO = 360;
        private Coordenadas[] coordenadasVerticesG1;
        private Coordenadas[] coordenadasVerticesG2;       
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
                    gBGrafos.Enabled = true;
                    pBG1.Enabled = true;
                    pBG2.Enabled = true;
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
                    gBGrafos.Enabled = true;
                    pBG1.Enabled = true;
                    pBG2.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("El grafo no se cargó corectamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Reiniciar()
        {
            pbLoading.Value = 0;
            pbLoading.Maximum = 0;
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
            btnComprobar.Enabled = false;
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

        //METODOS AGREGADOS 

        private void DeterminarCoordenadas(int cantidadVerticesG1, int cantidadVerticesG2)
        {
            //Arreglo de coordenadas tanto del Grafo 1 como del Grafo 2
            coordenadasVerticesG1 = new Coordenadas[cantidadVerticesG1];
            coordenadasVerticesG2 = new Coordenadas[cantidadVerticesG2];

            //Operaciones para determinar las coordenadas para el grafo 1
            double angulo = ANGULO_CIRCULO / cantidadVerticesG1;
            double anguloActual = 0;
            int almacenarX = 0;
            int almacenarY = 0;
            int radio = DeterminarRadio(1);
            int centroX = DeterminarCentroX(1);
            int centroY = DeterminarCentroY(1);

            for (int i = 0; i < cantidadVerticesG1; i++)
            {
                almacenarX = CoordenadaX(radio, anguloActual, centroX);
                almacenarY = CoordenadaY(radio, anguloActual, centroY);
                coordenadasVerticesG1[i] = new Coordenadas(almacenarX, almacenarY);
                anguloActual += angulo;
            }

            //Operaciones para determinar las coordenadas para el grafo 2
            angulo = ANGULO_CIRCULO / cantidadVerticesG2;
            anguloActual = 0;
            almacenarX = 0;
            almacenarY = 0;
            radio = DeterminarRadio(2);
            centroX = DeterminarCentroX(2);
            centroY = DeterminarCentroY(2);

            for (int j = 0; j < cantidadVerticesG2; j++)
            {
                almacenarX = CoordenadaX(radio, anguloActual, centroX);
                almacenarY = CoordenadaY(radio, anguloActual, centroY);
                coordenadasVerticesG2[j] = new Coordenadas(almacenarX, almacenarY);
                anguloActual += angulo;
            }


        }

        private int CoordenadaY(int radio, double angulo, int centroY)
        {
            //Almacena la coordenada y del vertice 
            int coordenadaY = 0;

            switch ((int)angulo)
            {
                case 0:
                    coordenadaY = centroY;
                    break;
                case 90:
                    coordenadaY = centroY - radio;
                    break;
                case 180:
                    coordenadaY = centroY;
                    break;
                case 270:
                    coordenadaY = centroY + radio;
                    break;

                default:

                    //Primer cuadrante
                    if (angulo < 90 && angulo > 0)
                    {
                        coordenadaY = centroY;
                        coordenadaY = coordenadaY - (int)(radio * Math.Sin((angulo) * (Math.PI / 180)));
                        break;
                    }

                    //Segundo cuadrante
                    if (angulo < 180 && angulo > 90)
                    {
                        coordenadaY = centroY;
                        coordenadaY = coordenadaY - (int)(radio * Math.Sin((180 - angulo) * (Math.PI / 180)));
                        break;
                    }

                    //Tercer cuadrante
                    if (angulo < 270 && angulo > 180)
                    {
                        coordenadaY = centroY;
                        coordenadaY = coordenadaY + (int)(radio * Math.Sin((angulo - 180) * (Math.PI / 180)));
                        break;
                    }

                    //Cuarto cuadrante
                    if (angulo < 360 && angulo > 270)
                    {
                        coordenadaY = centroY;
                        coordenadaY = coordenadaY + (int)(radio * Math.Sin((360 - angulo) * (Math.PI / 180)));
                        break;
                    }
                    break;
            }

            return coordenadaY;
        }

        private int CoordenadaX(int radio, double angulo, int centroX)
        {
            //Almacena el valor de la coordenada
            int coordenadaX = 0;

            switch ((int)angulo)
            {
                case 0:
                    coordenadaX = centroX + radio;
                    break;
                case 90:
                    coordenadaX = centroX;
                    break;
                case 180:
                    coordenadaX = centroX - radio;
                    break;
                case 270:
                    coordenadaX = centroX;
                    break;

                default:

                    //Primer cuadrante
                    if (angulo < 90 && angulo > 0)
                    {
                        coordenadaX = centroX;
                        coordenadaX = coordenadaX + (int)(radio * Math.Sin((90 - angulo) * (Math.PI / 180)));
                        break;
                    }

                    //Segundo cuadrante
                    if (angulo < 180 && angulo > 90)
                    {
                        coordenadaX = centroX;
                        coordenadaX = coordenadaX - (int)(radio * Math.Sin((90 - (180 - angulo)) * (Math.PI / 180)));
                        break;
                    }

                    //Tercer cuadrante
                    if (angulo < 270 && angulo > 180)
                    {
                        coordenadaX = centroX;
                        coordenadaX = coordenadaX - (int)(radio * Math.Sin((90 - (angulo - 180)) * (Math.PI / 180)));
                        break;
                    }

                    //Cuarto cuadrante
                    if (angulo < 360 && angulo > 270)
                    {
                        coordenadaX = centroX;
                        coordenadaX = coordenadaX + (int)(radio * Math.Sin((90 - (360 - angulo)) * (Math.PI / 180)));
                        break;
                    }
                    break;
            }

            return coordenadaX;
        }

        private void DibujarGrafos()
        {
            Bitmap a;
            Bitmap b;
            Graphics grafo1;
            Graphics grafo2;
            Pen redPen = new Pen(Color.Red, 9);
            Pen greenPen = new Pen(Color.Green, 9);
            Pen blackPen = new Pen(Color.Black, 4);
            Font arial = new Font("Arial", 9);
            char auxiliar = ' ';

            a = new Bitmap(pBG1.Width, pBG1.Height);
            b = new Bitmap(pBG2.Width, pBG2.Height);
            pBG1.Image = (Image)a;
            pBG2.Image = (Image)b;
            grafo1 = Graphics.FromImage(a);
            grafo2 = Graphics.FromImage(b);

            #region Dibujar Aristas:

            int aux = 0;
            int to = 0;

            for (int z = 0; z < g1.cantidadAristas; z++)
            {
                aux = Convert.ToInt32(g1.lstAristas[z].from);
                to = Convert.ToInt32(g1.lstAristas[z].to);

                //Analiza si existe un lazo...
                if (aux == to)
                {
                    //Si en caso exista un lazo.. lo dibuja utilizando una elipse..
                    Rectangle r = new Rectangle(coordenadasVerticesG1[aux].X + 4, coordenadasVerticesG1[aux].Y + 10, -30, -30);
                    grafo1.DrawEllipse(blackPen, r);
                }
                else
                {
                    grafo1.DrawLine(blackPen, new Point(coordenadasVerticesG1[aux].X, coordenadasVerticesG1[aux].Y), new Point(coordenadasVerticesG1[to].X, coordenadasVerticesG1[to].Y));
                }
            }

            aux = 0;
            to = 0;

            for (int w = 0; w < g2.cantidadAristas; w++)
            {
                aux = Convert.ToInt32(g2.lstAristas[w].from);
                to = Convert.ToInt32(g2.lstAristas[w].to);

                //Analiza si existe un lazo...
                if (aux == to)
                {
                    //Si en caso exista un lazo.. lo dibuja utilizando una elipse..
                    Rectangle r = new Rectangle(coordenadasVerticesG2[aux].X + 4, coordenadasVerticesG2[aux].Y + 10, -30, -30);
                    grafo2.DrawEllipse(blackPen, r);

                }
                else
                {
                    grafo2.DrawLine(blackPen, new Point(coordenadasVerticesG2[aux].X, coordenadasVerticesG2[aux].Y), new Point(coordenadasVerticesG2[to].X, coordenadasVerticesG2[to].Y));
                }
            }

            #endregion

            #region Dibujar Vertices:
            //Dibuja los vertices del grafo 1
            for (int i = 0; i < coordenadasVerticesG1.Length; i++)
            {  
                Rectangle r = new Rectangle(coordenadasVerticesG1[i].X, coordenadasVerticesG1[i].Y, 8, 8);
                grafo1.DrawEllipse(redPen, r);
                auxiliar = Convert.ToChar(i + 65);
                grafo1.DrawString(Convert.ToString(auxiliar), arial, Brushes.Blue, new PointF(coordenadasVerticesG1[i].X + 12, coordenadasVerticesG1[i].Y -6));
            }

            //Dibuja los vertices del grafo 2
            for(int j = 0; j < coordenadasVerticesG2.Length; j++)
            {
                Rectangle r = new Rectangle(coordenadasVerticesG2[j].X, coordenadasVerticesG2[j].Y, 8, 8);
                grafo2.DrawEllipse(greenPen, r);
                auxiliar = Convert.ToChar(j + 65);
                grafo2.DrawString(Convert.ToString(auxiliar), arial, Brushes.Blue, new PointF(coordenadasVerticesG2[j].X + 12, coordenadasVerticesG2[j].Y -6));
            }
            #endregion
        }

        private int DeterminarRadio(int numeroGrafo)
        {
            int radio = 0;

            switch(numeroGrafo)
            {
                case 1:
                    radio = (pBG1.Height - 40) / 2;
                    break;
                case 2:
                    radio = (pBG2.Height - 40) / 2;
                    break;
                default:
                    //Si en caso se comete el error de ingresar un numero mayor  a 2
                    //En nuestro caso nunca sucedera asumiendo porque simpre tenemos 2 grafos.
                    radio = -1;
                    break;
            }

            return radio;
        }

        private int DeterminarCentroX(int numeroGrafo)
        {
            int centroX = 0;

            switch (numeroGrafo)
            {
                case 1:
                    centroX = pBG1.Width / 2;
                    break;
                case 2:
                    centroX = pBG2.Width / 2;
                    break;
                default:
                    //Si en caso se comete el error de ingresar un numero mayor  a 2
                    //En nuestro caso nunca sucedera asumiendo porque simpre tenemos 2 grafos.
                    centroX = -1;
                    break;
            }

            return centroX;
        }

        private int DeterminarCentroY(int numeroGrafo)
        {
            int centroY = 0;

            switch (numeroGrafo)
            {
                case 1:
                    centroY = pBG1.Height / 2;
                    break;
                case 2:
                    centroY = pBG2.Height / 2;
                    break;
                default:
                    //Si en caso se comete el error de ingresar un numero mayor  a 2
                    //En nuestro caso nunca sucedera asumiendo porque simpre tenemos 2 grafos.
                    centroY = -1;
                    break;
            }

            return centroY;
        }

        private void btnVerGrafos_Click(object sender, EventArgs e)
        {
            DeterminarCoordenadas(g1.cantidadVertices, g2.cantidadVertices);
            DibujarGrafos();
        }
    }
}
