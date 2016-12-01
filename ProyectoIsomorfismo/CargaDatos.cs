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
    class CargaDatos
    {
        static List<Vertice> listaVertices;
        static List<Arista> listaAristas;
        public bool cargarGrafo(ref Grafo g) {
            listaVertices = new List<Vertice>();
            listaAristas = new List<Arista>();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivo de texto | *.txt";
            ofd.ShowDialog();
            if(ofd.FileName == null)
            {
                MessageBox.Show("El nombre del archivo no es válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Grafo g1;
            
            string[] lineas = File.ReadAllLines(ofd.FileName);
            int numeroVertices = 0;
            try {
                numeroVertices = Convert.ToInt32(lineas[0].Split(' ')[0]);
            }
            catch(FormatException e)
            {
                MessageBox.Show("El archivo ingresado no se encuentra en el formato correcto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            int numeroAristas = 0;
            for(int i = 1; i < lineas.Length; i++)
            {
                Arista a = new Arista(lineas[i].Split(','), numeroAristas+65);
                listaAristas.Add(a);
                numeroAristas++;
                for(int j = 0; j < 2; j++)
                {
                    if (!verticeExiste(lineas[i].Split(',')[j]))
                    {
                        Vertice v = new Vertice();
                        v.etiqueta = lineas[i].Split(',')[j];
                        listaVertices.Add(v);
                    }
                    foreach(Vertice v2 in listaVertices)
                    {
                        if(v2.etiqueta == lineas[i].Split(',')[j])
                        {
                            if (j == 0) {
                                Vertice v = new Vertice();
                                try {
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
            foreach(Vertice v in listaVertices)
            {
                v.calcularGrado();
                v.asignarID();
            }

            try {
                g1 = new Grafo(numeroVertices, numeroAristas, listaVertices, listaAristas);
            }
            catch
            {
                MessageBox.Show("Verifique que no existan espacios en blanco en el archivo de texto.", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            g = g1;
            return true;
        }

        private bool verticeExiste(string s)
        {
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
