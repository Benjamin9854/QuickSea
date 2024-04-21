using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using static System.Net.WebRequestMethods;

namespace QuickSea
{
    public partial class Grupos : Form
    {
        private string allgrupos = "";
        private string escribir = "";
        private string grupito = "";
        private string escribirGrupito = "";
        public string file = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string error = "";
        public Grupos()
        {
            InitializeComponent();
        }

        public void Grupos_Load(object sender, EventArgs e)
        {
            error = "";
            escribir = "";
            cmbGrupo.Items.Clear();

            if ((System.IO.File.Exists(file + "\\grupos.txt")))
            {

                    StreamReader sr = new StreamReader(file + "\\grupos.txt");
                    allgrupos = sr.ReadLine();
                    while (allgrupos != null)
                    {
                        cmbGrupo.Items.Add(allgrupos);
                        if(allgrupos != "")
                        {
                            escribir += allgrupos + "\n";
                        }
                        allgrupos = sr.ReadLine();
                    }
                    sr.Close();
            }        
        }

        private void btnG_Click(object sender, EventArgs e)
        {
            if(txtGrupo.Text == "")
            {
                MessageBox.Show("INGRESE UN GRUPO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string nuevo = txtGrupo.Text + "\n";
                StreamWriter sw = new StreamWriter(file + "\\grupos.txt");

                escribir += nuevo;
                cmbGrupo.Items.Add(nuevo);
                sw.Write(escribir);
                sw.Close();

                StreamWriter pw = new StreamWriter(file + "\\grupito" + txtGrupo.Text + ".txt");
                pw.Write("");
                pw.Close();
                MessageBox.Show("Grupo Guardado");
                error = "1";
                this.Close();
            }            
        }

        private void btnP_Click(object sender, EventArgs e)
        {
            if(txtCP.Text.Length != 6)
            {
                MessageBox.Show("CODIGO INVALIDO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(txtNP.Text == "")
            {
                MessageBox.Show("INGRESE UN NOMBRE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (System.IO.File.Exists(file + "\\grupito" + cmbGrupo.Text + ".txt"))
                {

                    StreamReader sr = new StreamReader(file + "\\grupito" + cmbGrupo.Text + ".txt");
                    txtTodo.Clear();

                    grupito = sr.ReadLine();
                    while (grupito != null)
                    {
                        txtTodo.Text += grupito + Environment.NewLine;
                        if (grupito != "")
                        {
                            escribirGrupito += grupito + "\n";
                        }
                        grupito = sr.ReadLine();
                    }
                    sr.Close();
                }
                string nuevoPr = txtNP.Text + "----" + txtCP.Text + "\n";
                StreamWriter pw = new StreamWriter(file + "\\grupito" + cmbGrupo.Text + ".txt");

                escribirGrupito += nuevoPr;
                txtTodo.Text += nuevoPr + Environment.NewLine;
                pw.Write(escribirGrupito);
                pw.Close();
                MessageBox.Show("Producto guardado");
                escribirGrupito = "";
            }           
        }

        private void txtMostrar_Click(object sender, EventArgs e)
        {
            txtTodo.Clear();

            if(cmbGrupo.Text == "")
            {
                MessageBox.Show("GRUPO NO SELECCIONADO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (System.IO.File.Exists(file + "\\grupito" + cmbGrupo.Text + ".txt"))
            {

                StreamReader sr = new StreamReader(file + "\\grupito" + cmbGrupo.Text + ".txt");

                grupito = sr.ReadLine();
                while (grupito != null)
                {
                    txtTodo.Text += grupito + Environment.NewLine;
                    grupito = sr.ReadLine();
                }
                sr.Close();
            }
        }

        private void todoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtCP.Clear();
            txtNP.Clear();
            txtGrupo.Clear();
            txtTodo.Clear();
        }

        private void formulariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtCP.Clear();
            txtNP.Clear();
            txtGrupo.Clear();
        }

        private void tablaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtTodo.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int indice = cmbGrupo.FindString(cmbGrupo.Text);
            if(indice != -1 && cmbGrupo.Text != "")
            {
                System.IO.File.Delete(file + "\\grupito" + cmbGrupo.Text + ".txt");
                cmbGrupo.Items.RemoveAt(indice);

                int i = 0, j = cmbGrupo.Items.Count;
                escribir = "";
                for(i = 0; i < j; i++)
                {
                    escribir += cmbGrupo.Items[i];
                    escribir += "\n";
                }
                StreamWriter sw = new StreamWriter(file + "\\grupos.txt");
                sw.Write(escribir);
                sw.Close();
            }  
            else
            {
                MessageBox.Show("NO SE PUDO ENCONTRAR EL GRUPO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
