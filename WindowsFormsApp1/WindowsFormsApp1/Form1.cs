using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Capa_Entidad;
using Capa_Negocio;
using QuickSea;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        ClassEntidad objent = new ClassEntidad();
        ClassNegocio objneg = new ClassNegocio();
        public string FILE;
        public string ultimo_codigo;
        
        public Form1()
        {
            InitializeComponent();
        }

        void mantenimiento(String accion)
        {
            double t;
            objent.codigo = txtcodigo.Text;
            objent.nombre = txtnombre.Text;
            objent.vneto = txtedad.Text;

            t = Convert.ToInt32(objent.vneto);
            t = t * 1.19;
            t = Convert.ToInt32(t);
            txttotal.Text = Convert.ToString(t);

            objent.vtotal = txttotal.Text;
            objent.descripcion = txttelefono.Text;
            objent.accion = accion;
            String men = objneg.N_mantenimiento_productos(objent);
            MessageBox.Show(men, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void limpiar()
        {
            txtcodigo.Text = "";
            txtnombre.Text = "";
            txtedad.Text = "";
            txttotal.Text = "";
            txttelefono.Text = "";
            txtbuscar.Text = "";
            dataGridView1.DataSource = objneg.N_listar_productos();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = objneg.N_listar_productos();

            int ultima_fila = dataGridView1.Rows.Count - 1;
            ultimo_codigo = dataGridView1.Rows[ultima_fila].Cells[0].Value.ToString();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void registrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i;
            bool result = int.TryParse(txtedad.Text, out i);
            if (txtcodigo.Text != "")
            {
                MessageBox.Show("Debe registrar un NUEVO producto", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(result == false)
            {
                MessageBox.Show("El valor neto es INVALIDO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(txtnombre.Text == "")
            {
                MessageBox.Show("Ingrese un NOMBRE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(MessageBox.Show("¿Deseas registrar a " +txtnombre.Text+ "?","Mensaje",
                    MessageBoxButtons.YesNo,MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    mantenimiento("1");
                    limpiar();
                }          
            }  
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i;
            bool result = int.TryParse(txtedad.Text, out i);
            if (txtcodigo.Text == "")
            {
                MessageBox.Show("Debe seleccionar un PRODUCTO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (result == false)
            {
                MessageBox.Show("El valor neto es INVALIDO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtnombre.Text == "")
            {
                MessageBox.Show("Ingrese un NOMBRE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("¿Deseas modicar a " + txtnombre.Text + "?", "Mensaje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    mantenimiento("2");
                    limpiar();
                }

            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ultima_fila = dataGridView1.Rows.Count - 1;
            ultimo_codigo = dataGridView1.Rows[ultima_fila].Cells[0].Value.ToString();

            if (txtcodigo.Text == "")
            {
                MessageBox.Show("Debe seleccionar un PRODUCTO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtcodigo.Text != ultimo_codigo)
            {
                if (MessageBox.Show("¿Deseas eliminar a " + txtnombre.Text + "?", "Mensaje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    txtnombre.Text = "NULL";
                    txtedad.Text = "0";
                    txttelefono.Text = "";
                    mantenimiento("2");
                    limpiar();
                }
            }
            else
            {
                if (MessageBox.Show("¿Deseas eliminar a " + txtnombre.Text + "?", "Mensaje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    mantenimiento("3");
                    limpiar();
                }

            }
        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            if(txtbuscar.Text != "")
            {
                objent.nombre = txtbuscar.Text;
                DataTable dt = new DataTable();
                dt = objneg.N_buscar_productos(objent);
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = objneg.N_listar_productos();
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int fila = dataGridView1.CurrentCell.RowIndex;

            txtcodigo.Text = dataGridView1[0, fila].Value.ToString();
            txtnombre.Text = dataGridView1[1, fila].Value.ToString();
            txtedad.Text = dataGridView1[2, fila].Value.ToString();
            txtedad.Text = txtedad.Text.Replace("$", "");
            txttotal.Text = dataGridView1[3, fila].Value.ToString();
            txttotal.Text = txttotal.Text.Replace("$", "");
            txttelefono.Text = dataGridView1[4, fila].Value.ToString();
        }

        public void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grupos gr = new Grupos();
            gr.ShowDialog();
            while(gr.error == "1")
            {
                gr.ShowDialog();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void txttotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtedad_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
