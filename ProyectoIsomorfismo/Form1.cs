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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Font = System.Drawing.Font;
using Image = System.Drawing.Image;
using Rectangle = System.Drawing.Rectangle;


namespace ProyectoIsomorfismo
{
    public partial class Form1 : Form
    {
        // Elementos para la gráfica de los grafos.
        const int ANGULO_CIRCULO = 360;
        private Coordenadas[] coordenadasVerticesG1;
        private Coordenadas[] coordenadasVerticesG2;

        // Grafos       
        Grafo g1;
        Grafo g2;
        // Lista de funciones de isomorfismo
        List<funcion> listaFunciones;

        public Form1()
        {
            listaFunciones = new List<funcion>();
            InitializeComponent();
        }

        /// <summary>
        /// Carga del primer grafo
        /// </summary>
        private void btnGrafo1_Click(object sender, EventArgs e)
        {
            // Realiza la carga de datos
            CargaDatos c1 = new CargaDatos();
            if(c1.cargarGrafo(ref g1))
            {
                MessageBox.Show("El grafo fue cargado correctamente.", "Carga exitosa", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnGrafo1.Enabled = false;
                if (!btnGrafo2.Enabled)
                {
                    // Habilita los botones que se utilizarán
                    btnComprobar.Enabled = true;
                    gBGrafos.Enabled = true;
                    pBG1.Enabled = true;
                    pBG2.Enabled = true;
                    btnVerGrafos.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("El grafo no se cargó corectamente", "Error", 
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Carag del segundo grafo
        /// </summary>
        private void btnGrafo2_Click(object sender, EventArgs e)
        {
            // Realiza la carga de datos   
            CargaDatos c1 = new CargaDatos();
            if (c1.cargarGrafo(ref g2))
            {
                MessageBox.Show("El grafo fue cargado correctamente.", "Carga exitosa", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnGrafo2.Enabled = false;
                if (!btnGrafo1.Enabled)
                {
                    // Habilita los botones que se utilizarán
                    btnComprobar.Enabled = true;
                    gBGrafos.Enabled = true;
                    pBG1.Enabled = true;
                    pBG2.Enabled = true;
                    btnVerGrafos.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("El grafo no se cargó corectamente", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Restablece todos los controles a su estado previo a ser utilizado por el
        /// usuario
        /// </summary>
        private void Reiniciar()
        {
            pbLoading.Value = 0;
            pbLoading.Maximum = 0;
            btnGrafo1.Enabled = true;
            btnGrafo2.Enabled = true;
            btnGenerarPdf.Enabled = false;
            btnVerGrafos.Enabled = false;
            pBG1.Image = null;
            pBG2.Image = null;
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

        

        /// <summary>
        /// Comprueba isomorfismo entre los dos grafos cargados
        /// </summary>
        private void btnComprobar_Click(object sender, EventArgs e)
        {
           
            if(!Isomorfismo.comprobarIsomorfismo(g1, g2, pbLoading, cbFunciones, 
                ref listaFunciones))
            {
                MessageBox.Show("Los grafos no son isomorfos.", "Resultado", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                cbFunciones.Enabled = true;
                dgvMostrarFuncion.Enabled = true;
                MessageBox.Show("Los grafos son isomorfos.", "Resultado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnGenerarPdf.Enabled = true;
            }
            btnComprobar.Enabled = false;
        }

        /// <summary>
        /// Muestra la información de las distintas funciones encontradas
        /// </summary>
        private void cbFunciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Agrega filas al dataGridView en caso de que no haya la cantidad necesaria
            if(dgvMostrarFuncion.Rows.Count != g1.cantidadVertices)
            {
                for(int i = 0; i < g1.cantidadVertices; i++)
                {
                    dgvMostrarFuncion.Rows.Add();
                }
            }

            if(g1.lstVertices.Count > 9)
                listaFunciones = PermutadorUtilities.getInstancia().funcionIsomorfica;

            // Llena el dataGridView con la información de la función seleccionada
            for(int i = 0; i < listaFunciones[cbFunciones.SelectedIndex].V1.Count; i++)
            {
                dgvMostrarFuncion[0, i].Value = Convert.ToChar(Convert.ToInt16(
                    listaFunciones[cbFunciones.SelectedIndex].V1[i].etiqueta)+65);
                dgvMostrarFuncion[1, i].Value = Convert.ToChar(Convert.ToInt16(
                    listaFunciones[cbFunciones.SelectedIndex].V2[i].etiqueta)+97);
            }
        }

        /// <summary>
        /// Restablece todos los controles a su estado previo a ser utilizado el programa
        /// </summary>
        private void cargarDatosDeNuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reiniciar();            
        }

        /// <summary>
        /// Realiza la gráfica de los dos grafos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVerGrafos_Click(object sender, EventArgs e)
        {
            DeterminarCoordenadas(g1.cantidadVertices, g2.cantidadVertices);
            DibujarGrafos();
        }

        // Métodos para graficar los grafos 

        /// <summary>
        /// Indica las coordenadas en las que se agregarán los vértices
        /// </summary>
        /// <param name="cantidadVerticesG1"> Vértices de primer grafo </param>
        /// <param name="cantidadVerticesG2"> Vértices de segundo grafo </param>
        private void DeterminarCoordenadas(int cantidadVerticesG1,  int cantidadVerticesG2)
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

            /*Recorre la lista de vertices para asignar las coordenadas correspondientes
            a cada uno */
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

            /*Recorre la lista de vertices para asignar las coordenadas correspondientes
           a cada uno */
            for (int j = 0; j < cantidadVerticesG2; j++)
            {
                almacenarX = CoordenadaX(radio, anguloActual, centroX);
                almacenarY = CoordenadaY(radio, anguloActual, centroY);
                coordenadasVerticesG2[j] = new Coordenadas(almacenarX, almacenarY);
                anguloActual += angulo;
            }


        }

        /// <summary>
        /// Coordenada y del vértice
        /// Lo calcula en base al angulo y al radio, con la ley de senos,
        /// para determinar la altura y el ancho, en nuestro caso la coordenada en Y
        /// en base al angulo que tenga asigando.
        /// </summary>
        /// <param name="radio"> Radio de cada vértice </param>
        /// <param name="angulo"> Ángulo del vértice (360) conveniemtemente</param>
        /// <param name="centroY"> Centro del vértice en y </param>
        /// <returns></returns>
        private int CoordenadaY(int radio, double angulo, int centroY)
        {
            //Almacena la coordenada y del vertice 
            int coordenadaY = 0;

            switch ((int)angulo)
            {
                //Estos 4 casos aplican cuando el angulo es recto u obtuso..
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

                    // Math.Sin(), opera con radianes por lo que se realizo la conversion
                    // del angulo en grados a radianes utilizando la operacion Math.PI/180

                    //Primer cuadrante
                    if (angulo < 90 && angulo > 0)
                    {
                        coordenadaY = centroY;
                        coordenadaY = coordenadaY - (int)(radio * Math.Sin((angulo) * 
                            (Math.PI / 180)));
                        break;
                    }

                    //Segundo cuadrante
                    if (angulo < 180 && angulo > 90)
                    {
                        coordenadaY = centroY;
                        coordenadaY = coordenadaY - (int)(radio * Math.Sin((180 - angulo) * 
                            (Math.PI / 180)));
                        break;
                    }

                    //Tercer cuadrante
                    if (angulo < 270 && angulo > 180)
                    {
                        coordenadaY = centroY;
                        coordenadaY = coordenadaY + (int)(radio * Math.Sin((angulo - 180) * 
                            (Math.PI / 180)));
                        break;
                    }

                    //Cuarto cuadrante
                    if (angulo < 360 && angulo > 270)
                    {
                        coordenadaY = centroY;
                        coordenadaY = coordenadaY + (int)(radio * Math.Sin((360 - angulo) *
                            (Math.PI / 180)));
                        break;
                    }
                    break;
            }

            return coordenadaY;
        }
        /// <summary>
        /// Coordenada x del vértice 
        /// Lo calcula en base al angulo y al radio, con la ley de senos,
        /// para determinar la altura y el ancho, en nuestro caso la coordenada en X
        /// en base al angulo que tenga asigando.
        /// </summary>
        /// <param name="radio"> Radio del vértice </param>
        /// <param name="angulo"> Ángulo del vértice (Convenientemente 360)</param>
        /// <param name="centroX"> Coordenada x del centro del vértice </param>
        /// <returns></returns>
        private int CoordenadaX(int radio, double angulo, int centroX)
        {
            //Almacena el valor de la coordenada
            int coordenadaX = 0;

            switch ((int)angulo)
            {
                //Estos 4 casos aplican cuando el angulo es recto u obtuso..
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

                    // Math.Sin(), opera con radianes por lo que se realizo la conversion 
                    // del angulo en grados a radianes utilizando la operacion Math.PI/180 */
                    
                    //Primer cuadrante
                    if (angulo < 90 && angulo > 0)
                    {
                        coordenadaX = centroX;
                        coordenadaX = coordenadaX + (int)(radio * Math.Sin((90 - angulo) * 
                            (Math.PI / 180)));
                        break;
                    }

                    //Segundo cuadrante
                    if (angulo < 180 && angulo > 90)
                    {
                        coordenadaX = centroX;
                        coordenadaX = coordenadaX - (int)(radio * Math.Sin((90 - (180 - 
                            angulo)) * (Math.PI / 180)));
                        break;
                    }

                    //Tercer cuadrante
                    if (angulo < 270 && angulo > 180)
                    {
                        coordenadaX = centroX;
                        coordenadaX = coordenadaX - (int)(radio * Math.Sin((90 - (angulo -
                            180)) * (Math.PI / 180)));
                        break;
                    }

                    //Cuarto cuadrante
                    if (angulo < 360 && angulo > 270)
                    {
                        coordenadaX = centroX;
                        coordenadaX = coordenadaX + (int)(radio * Math.Sin((90 - (360 - 
                            angulo)) * (Math.PI / 180)));
                        break;
                    }
                    break;
            }

            return coordenadaX;
        }
        /// <summary>
        /// Crea el dibujo de los dos grafos
        /// </summary>
        private void DibujarGrafos()
        {
            /* Permiten operar sobre un pictureBox como si fuera un graficador.
               Manipulando las coordenadas de dicho pictureBox*/

            //Mapa de bits para el grafo 1 y 2
            Bitmap a;
            Bitmap b;

            /*Permite acceder a funciones de dibujo para crear la figura deseada,
              va amarrada del mapa de bits*/
            Graphics grafo1;
            Graphics grafo2;

            //Pinceles para dibujar vertices
            Pen redPen = new Pen(Color.Red, 5);
            Pen greenPen = new Pen(Color.Green, 5);

            //Pinceles para dibujar aristas
            Pen blackPen = new Pen(Color.Black, 2);
            Pen redLinesPen = new Pen(Color.Red, 1);
            Pen yellowLinesPen = new Pen(Color.Yellow, 1);
            Pen blueLinesPen = new Pen(Color.Blue, 1);

            //Tamaño de fuente para el nombre de las aristas
            Font arial = new Font("Arial", 9);

            //Almacena las aristas que se repitan en el grafo
            //Obtiene el arreglo que determina cuantas aristas repetidas existen
            int[] repeticiones = AristasRepetidas(1);

            //Se inicializan los mapas de bits del tamaño de los pictureBox
            a = new Bitmap(pBG1.Width, pBG1.Height);
            b = new Bitmap(pBG2.Width, pBG2.Height);

            //Se asignan los mapas de bits como si fueran imagenes a los pictureBox
            pBG1.Image = (Image)a;
            pBG2.Image = (Image)b;

            /*Inicializa los graficadores e indican sobre que imagen(Mapa de bits) 
              trabajará.*/
            grafo1 = Graphics.FromImage(a);
            grafo2 = Graphics.FromImage(b);

            /* Parte del codigo que se encarga de dibujar las aristas del grafo 1 
               y grafo 2*/
            #region Dibujar Aristas:

            //Dibuja las aristas del  grafo 1
            int aux = 0;
            int to = 0;

            //Recorre las lista de aristas..
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
                    switch (repeticiones[z])
                    {
                        case 1:
                            //En caso que la arista sea única en la lista.
                            grafo1.DrawLine(blackPen, new Point(coordenadasVerticesG1[aux].X, coordenadasVerticesG1[aux].Y), new Point(coordenadasVerticesG1[to].X, coordenadasVerticesG1[to].Y));
                            break;
                        case 2:
                            //Indica que se ha repetido 1 vez la arista...
                            //Asigna un nuevo color a esa arista..
                            //Manipula el grosor y la ubicacion de la arista...
                            grafo1.DrawLine(yellowLinesPen, new Point(coordenadasVerticesG1[aux].X + 3, coordenadasVerticesG1[aux].Y + 2), new Point(coordenadasVerticesG1[to].X + 3, coordenadasVerticesG1[to].Y + 2));
                            break;
                        case 3:
                            //Indica que se ha repetido 2 veces la arista...
                            //Asigna un nuevo color a esa arista..
                            //Manipula el grosor y la ubicacion de la arista..
                            grafo1.DrawLine(blueLinesPen, new Point(coordenadasVerticesG1[aux].X - 3, coordenadasVerticesG1[aux].Y + 2), new Point(coordenadasVerticesG1[to].X - 3, coordenadasVerticesG1[to].Y + 2));
                            break;
                        case 4:
                            //Indica que se ha repetido 3 veces la arista...
                            //Asigna un nuevo color a esa arista..
                            //Manipula el grosor y la ubicacion de la arista..
                            grafo1.DrawLine(redLinesPen, new Point(coordenadasVerticesG1[aux].X + 6, coordenadasVerticesG1[aux].Y + 2), new Point(coordenadasVerticesG1[to].X + 6, coordenadasVerticesG1[to].Y + 2));
                            break;
                    }
                }
            }


            //Dibuja las aristas del grafo 2
            aux = 0;
            to = 0;

            //Obtiene el arreglo que determina cuantas aristas repetidas existen
            int[] repeticiones2 = AristasRepetidas(2);

            //Recorre las lista de aristas..
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
                    /* Solo puede analizar hasta 4 aristas repetidas, en caso de existir mas, no se podra ver visualmente
                      en el graficador.*/
                    switch (repeticiones2[w])
                    {
                        case 1:
                            //En caso que la arista sea única en la lista.
                            grafo2.DrawLine(blackPen, new Point(coordenadasVerticesG2[aux].X, coordenadasVerticesG2[aux].Y), new Point(coordenadasVerticesG2[to].X, coordenadasVerticesG2[to].Y));
                            break;
                        case 2:
                            //Indica que se ha repetido 1 vez la arista...
                            //Asigna un nuevo color a esa arista..
                            //Manipula el grosor y la ubicacion de la arista...
                            grafo2.DrawLine(yellowLinesPen, new Point(coordenadasVerticesG2[aux].X + 3, coordenadasVerticesG2[aux].Y + 2), new Point(coordenadasVerticesG2[to].X + 3, coordenadasVerticesG2[to].Y + 2));
                            break;
                        case 3:

                            //Indica que se ha repetido 2 veces la arista...
                            //Asigna un nuevo color a esa arista..
                            //Manipula el grosor y la ubicacion de la arista...
                            grafo2.DrawLine(blueLinesPen, new Point(coordenadasVerticesG2[aux].X - 3, coordenadasVerticesG2[aux].Y + 2), new Point(coordenadasVerticesG2[to].X - 3, coordenadasVerticesG2[to].Y + 2));
                            break;
                        case 4:
                            //Indica que se ha repetido 3 veces la arista...
                            //Asigna un nuevo color a esa arista..
                            //Manipula el grosor y la ubicacion de la arista...
                            grafo2.DrawLine(redLinesPen, new Point(coordenadasVerticesG2[aux].X + 6, coordenadasVerticesG2[aux].Y + 2), new Point(coordenadasVerticesG2[to].X + 6, coordenadasVerticesG2[to].Y + 2));
                            break;
                    }
                }
            }

            #endregion


            /* Parte del codigo que se encarga de dibujar los vertices del grafo 1 
               y grafo 2*/
            #region Dibujar Vertices:
            //Dibuja los vertices del grafo 1
            for (int i = 0; i < coordenadasVerticesG1.Length; i++)
            {  
                /* Genera un rectangulo sobre el cual se dibujará la elipse rellena que 
                  representará un vertice del grafo.*/
                Rectangle r = new Rectangle(coordenadasVerticesG1[i].X, coordenadasVerticesG1[i].Y, 5, 5);
                grafo1.DrawEllipse(redPen, r);

                /* Asigna un identificador a cada vertice del grafo*/
                grafo1.DrawString(Convert.ToString(i), arial, Brushes.Blue, new PointF(coordenadasVerticesG1[i].X + 8 , coordenadasVerticesG1[i].Y -4));
            }

            //Dibuja los vertices del grafo 2
            for(int j = 0; j < coordenadasVerticesG2.Length; j++)
            {
                /* Genera un rectangulo sobre el cual se dibujará la elipse rellena que 
                  representará un vertice del grafo.*/
                Rectangle r = new Rectangle(coordenadasVerticesG2[j].X, coordenadasVerticesG2[j].Y, 5, 5);
                grafo2.DrawEllipse(greenPen, r);

                /* Asigna un identificador a cada vertice del grafo*/
                grafo2.DrawString(Convert.ToString(j), arial, Brushes.Blue, new PointF(coordenadasVerticesG2[j].X + 8 , coordenadasVerticesG2[j].Y -4));
            }
            #endregion
        }

        /// <summary>
        /// Crea un arreglo de enteros que indica cuantas veces se ha repetido
        /// una arista en la lista.
        /// </summary>
        /// <param name="grafo"> Indica el numero del grafo sobre el cual va a operar.</param>
        /// <returns> Retorna el arreglo de enteros lleno con 
        /// datos analizados. </returns>
        private int[] AristasRepetidas(int grafo)
        {
            //Crea el arreglo del mismo tamaño que la lista de aristas..
            int[] repeticiones = new int[1];
            int count = 1;

            //Elige sobre cual grafo va operar..
            //En nuestro caso tenemos solo 2 grafos..
            switch (grafo)
            {
                case 1:
                    //Grafo 1

                    repeticiones = new int[g1.cantidadAristas];

                    //Llena el arreglo con 1 para indicar que existe una vez esa arista en la lista..
                    for (int x = 0; x < repeticiones.Length; x++)
                    {
                        repeticiones[x] = count;
                    }

                    //Recorre la lista de aristas buscando aristas repetidas...
                    for (int j = 0; j < g1.cantidadAristas; j++)
                    {
                        //Ayuda a indicar si existe una o mas aristas iguales...
                        count = 2;

                        for (int i = j + 1; i < g1.cantidadAristas; i++)
                        {
                            if (g1.lstAristas[j].from == g1.lstAristas[i].from ||
                                g1.lstAristas[j].from == g1.lstAristas[i].to)
                            {
                                if (g1.lstAristas[j].to == g1.lstAristas[i].to || 
                                    g1.lstAristas[j].to == g1.lstAristas[i].from)
                                {
                                    //Si encuentra una arista repetida le asigna el valor que tenga count...
                                    //Por ejemplo si fuera la tercera arista igual, el valor de count será 3...
                                    if (repeticiones[i] < count)
                                    {
                                        repeticiones[i] = count;
                                        count++;
                                    }
                                }
                            }
                        }
                    }

                    break;
                case 2:
                    //Grafo 2

                    repeticiones = new int[g2.cantidadAristas];

                    //Llena el arreglo con 1 para indicar que existe una vez esa arista en la lista..
                    for (int x = 0; x < repeticiones.Length; x++)
                    {
                        repeticiones[x] = count;
                    }

                    //Recorre la lista de aristas buscando aristas repetidas...
                    for (int j = 0; j < g2.cantidadAristas; j++)
                    {
                        //Ayuda a indicar si existe una o mas aristas iguales...
                        count = 2;

                        for (int i = j + 1; i < g2.cantidadAristas; i++)
                        {
                            if (g2.lstAristas[j].from == g2.lstAristas[i].from || 
                                g2.lstAristas[j].from == g2.lstAristas[i].to)
                            {
                                if (g2.lstAristas[j].to == g2.lstAristas[i].to || 
                                    g2.lstAristas[j].to == g2.lstAristas[i].from)
                                {
                                    //Si encuentra una arista repetida le asigna el valor que tenga count...
                                    //Por ejemplo si fuera la tercera arista igual, el valor de count será 3...
                                    if (repeticiones[i] < count)
                                    {
                                        repeticiones[i] = count;
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

            return repeticiones;
        }

        /// <summary>
        /// Determina el radio de un grafo determinado,
        /// En base a la altura del pictureBox asignado.
        /// </summary>
        /// <param name="numeroGrafo"> Grafo sobre el cual se realizaran operaciones </param>
        /// <returns> El radio calculado en base al grafo elegido </returns>
        private int DeterminarRadio(int numeroGrafo)
        {
            int radio = 0;

            //En nuestro caso solamente se esta trabajando con 2 grafos..
            switch (numeroGrafo)
            {
                case 1:
                    //Grafo 1
                    radio = (pBG1.Height - 40) / 2;
                    break;
                case 2:
                    //Grafo 2
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
        /// <summary>
        /// Determina el centro en X de un grafo determinado
        /// </summary>
        /// <param name="numeroGrafo"> Grafo sobre el cual se realizaran operaciones </param>
        /// <returns> Retorna el centro en el eje X del PictureBox 
        /// correspondiente al grafo elegido </returns>
        private int DeterminarCentroX(int numeroGrafo)
        {
            int centroX = 0;

            //En nuestro caso solamente se esta trabajando con 2 grafos..
            switch (numeroGrafo)
            {
                case 1:
                    //Grafo 1
                    centroX = pBG1.Width / 2;
                    break;
                case 2:
                    //Grafo 2
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
        /// <summary>
        /// Determina el centro en Y de un grafo determinado
        /// </summary>
        /// <param name="numeroGrafo"> Grafo sobre el cual se realizaran operaciones </param>
        /// <returns> Retorna el centro en el eje Y del PictureBox 
        /// correspondiente al grafo elegido. </returns>
        private int DeterminarCentroY(int numeroGrafo)
        {
            int centroY = 0;

            //En nuestro caso solamente se esta trabajando con 2 grafos..
            switch (numeroGrafo)
            {
                case 1:
                    //Grafo 1
                    centroY = pBG1.Height / 2;
                    break;
                case 2:
                    //Grafo 2
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

        

        /// <summary>
        /// Funcion que genera el reporte de las funciones isomórficas en PDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //Variables para escribir en el pdf
            PdfWriter writer;
            Document doc = new Document(PageSize.LETTER);

            //Se llenan la lista de funciones cuando el número de vertices es > 9
            if (g1.lstVertices.Count > 9)
                listaFunciones = PermutadorUtilities.getInstancia().funcionIsomorfica;

            try
            {
                //Path en donde se escribira el archivo
                writer = PdfWriter.GetInstance(doc,
                    new FileStream(Directory.GetCurrentDirectory() + "\\Funciones de " +
                                   "Isomorfismo.pdf", FileMode.Create));

                //Autores del archivo
                doc.AddAuthor("Matemática Discreta II");
                doc.AddCreator("Alumnos de Matemática Discreta II");
                doc.Open();

                //Fuente del archivo
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(
                    iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL
                    , BaseColor.BLACK);
                doc.Add(new Paragraph("Lista de funciones isomórficas - Matemática " +
                                      "Discreta II"));
                doc.Add(Chunk.NEWLINE);

                /* Se escriben todas las funciones isomorficas recorriendo la lista de 
                funciones con un for. */

                for (int i = 0; i < listaFunciones.Count; i++)
                {
                    doc.Add(new Paragraph("Función Isomórfica No. " + (i + 1)));
                    PdfPTable tblIsomorfismo = new PdfPTable(2);
                    tblIsomorfismo.WidthPercentage = 100;

                    PdfPCell clGrafo1 = new PdfPCell(new Phrase("Grafo 1", _standardFont));
                    clGrafo1.BorderWidth = 0;
                    clGrafo1.BorderWidthBottom = 0.75f;

                    PdfPCell clGrafo2 = new PdfPCell(new Phrase("Grafo 2", _standardFont));
                    clGrafo2.BorderWidth = 0;
                    clGrafo2.BorderWidthBottom = 0.75f;

                    tblIsomorfismo.AddCell(clGrafo1);
                    tblIsomorfismo.AddCell(clGrafo2);

                    for (int j = 0; j < listaFunciones[i].V1.Count; j++)
                    {
                        clGrafo1 =
                            new PdfPCell(new Phrase(((char) listaFunciones[i].V1[j].ID).
                            ToString(), _standardFont));
                        clGrafo1.BorderWidth = 0;

                        clGrafo2 =
                            new PdfPCell(new Phrase(((char) listaFunciones[i].V2[j].ID).
                            ToString().ToLower(),
                                _standardFont));
                        clGrafo2.BorderWidth = 0;
                        tblIsomorfismo.AddCell(clGrafo1);
                        tblIsomorfismo.AddCell(clGrafo2);
                    }

                    doc.Add(tblIsomorfismo);
                    doc.Add(new LineSeparator());
                    doc.Add(new LineSeparator());
                    doc.Add(Chunk.NEWLINE);
                }

                //Se cierra el documento y el escribidor.
                doc.Close();
                writer.Close();
                btnGenerarPdf.Enabled = false;
            }
            catch (IOException file)
            {
                //Si se intenta generar un archivo cuando este esta abierto marca error
                MessageBox.Show("Cierre el archivo de isomórfismo que tiene abierto " +
                                "antes de generar uno nuevo.");
            }
            catch (UnauthorizedAccessException sinPermisos)
            {
                MessageBox.Show("Por favor para generar el PDF ejecuta la aplicación " +
                                "como administrador");
            }
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            Reiniciar();
        }
    }
}
