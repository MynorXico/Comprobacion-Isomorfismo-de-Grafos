using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
namespace ProyectoIsomorfismo
{
    /// <summary>
    /// Contiene métodos relacionados con la carga de los datos de los grafos.
    /// </summary>
    class CargaDatos
    {
        /// <summary>
        /// Lista de vértices del grafo
        /// </summary>
        static List<Vertice> listaVertices;

        /// <summary>
        /// Lista de aristas del grafo
        /// </summary>
        static List<Arista> listaAristas;

        /// <summary>
        /// Función encargada de la lectura de datos del grafo desde un archivo de texto.
        /// </summary>
        /// <param name="g"> Grafo que se creará.</param>
        /// <returns> Verdadero si se creó el grafo con éxito</returns>
        public bool cargarGrafo(ref Grafo g) {
            // Instancia las listas para el almacenamiento de los vértices y las aristas.
            listaVertices = new List<Vertice>();
            listaAristas = new List<Arista>();
            // Muestra un openFileDialog para que el usuario pueda navegar y seleccionar
            // el archivo que desea abrir
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivo de texto | *.txt";
            ofd.ShowDialog();

            if(ofd.FileName == null) // Comprueba que se haya abierto un archivo.
            {
                MessageBox.Show("El nombre del archivo no es válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Grafo g1;
            string[] lineas; // Arreglo que contiene todas las líneas del archivo
            try
            {
                lineas = File.ReadAllLines(ofd.FileName);
            }
            catch(ArgumentException e)
            {
                return false;
            }
            int numeroVertices = 0;
            try { // Asegura que el archivo se encuentra en el formato adecuado.

                numeroVertices = Convert.ToInt32(lineas[0].Split(' ')[0]);
            }
            catch(FormatException e)
            {
                MessageBox.Show("El archivo ingresado no se encuentra en el formato cor"+
                    "recto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            int numeroAristas = 0;
            
            // Recorre todas las lineas del archivo
            for(int i = 1; i < lineas.Length; i++)
            {
                // Se crea una arista por cada línea leída.
                Arista a = new Arista(lineas[i].Split(','), numeroAristas+65);
                listaAristas.Add(a);
                numeroAristas++;
                for(int j = 0; j < 2; j++)
                {
                    // Verifica que el vértice no exista para añadirlo
                    if (!verticeExiste(lineas[i].Split(',')[j]))
                    {
                        Vertice v = new Vertice();
                        v.etiqueta = lineas[i].Split(',')[j];
                        listaVertices.Add(v);
                    }
                    // En caso de que exista, añade únicamente el vértice con el cual
                    // conecta
                    foreach(Vertice v2 in listaVertices)
                    {
                        if(v2.etiqueta == lineas[i].Split(',')[j])
                        {
                            if (j == 0) {
                                Vertice v = new Vertice();
                                try {
                                    // Asegura que el archivo se encuentre en el formato
                                    // esperado
                                    v.etiqueta = lineas[i].Split(',')[1];
                                }
                                catch(IndexOutOfRangeException e)
                                {
                                    return false;
                                }
                                v2.verticesConectados.Add(v);
                            }
                            else
                            {
                                Vertice v = new Vertice();
                                v.etiqueta = lineas[i].Split(',')[0];
                                v2.verticesConectados.Add(v);
                            }
                        }
                    }
                }
            }
            // Asigna el grado y ID a cada vértice
            foreach(Vertice v in listaVertices)
            {
                v.calcularGrado();
                v.asignarID();
            }
            try {
                // Maneja el error en el que se ingresan letras en lugar de números como
                // identificador de los vértices
                g1 = new Grafo(numeroVertices, numeroAristas, listaVertices, listaAristas);
            }
            catch
            {
                MessageBox.Show("Verifique que no existan espacios en blanco en el archivo de texto.", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            // Asigna el grafo generado al grafo pasado por referencia a la función
            g = g1;
            return true;
        }

        /// <summary>
        /// Comprueba si un vértice existe en el listado de vértices.
        /// </summary>
        /// <param name="s"> Etiqueta del vértice que se busca. </param>
        /// <returns> Verdadero si el vértice que se busca existe</returns>
        private bool verticeExiste(string s)
        {
            // Por cada vértice, verifica que la etiqueta coincida con la búsqueda.
            foreach(Vertice v in listaVertices)
            {
                if(v.etiqueta == s)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
