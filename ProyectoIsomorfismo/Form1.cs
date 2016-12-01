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

        private Bitmap grafo1;
        private Bitmap grafo2;
        private Graphics a;
        private Graphics b;
        private Coordenadas[] coordenadasVerticesG1;
        private Coordenadas[] coordenadasVerticesG2;
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

        //Agrega MACA
        private void DeterminarCoordenadas(int cantidadVertices, int radio, int centroY, int centroX)
        {
            coordenadasVertices = new Coordenadas[cantidadVertices];
            double angulo = ANGULO_CIRCULO / cantidadVertices;
            double anguloActual = 0;
            int almacenarX = 0;
            int almacenarY = 0;

            for (int i = 0; i < cantidadVertices; i++)
            {
                almacenarX = CoordenadaX(radio, anguloActual, centroX);
                almacenarY = CoordenadaY(radio, anguloActual, centroY);
                coordenadasVertices[i] = new Coordenadas(almacenarX, almacenarY);
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

    }
}
