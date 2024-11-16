using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using CapaEntidad;
using CapaNegocios;

namespace SisCitasMedicas
{
    public partial class ListaPaciente : Form
    {
        public ListaPaciente()
        {
            InitializeComponent();
        }
        private void frmPacientes_Load(object sender, EventArgs e)
        {

            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {

                if (columna.Visible == true && columna.Name != "btnseleccionar")
                {
                    cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cbobusqueda.SelectedIndex = 0;

            List<Pacientes> listaPacientes = new CN_Pacientes().Listar();

            foreach (Pacientes item in listaPacientes)
            {

                dgvdata.Rows.Add(new object[] {"",item.IdPaciente,item.Nombre,
                    item.FechaNacimiento,item.Genero,item.Direccion,
                    item.Telefono,item.Email,item.FechaRegistro
                });
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Pacientes objPaciente = new Pacientes()
            {
                IdPaciente = Convert.ToInt32(txtid.Text),
                Nombre = txtnombre.Text,
                FechaNacimiento = Convert.ToDateTime(txtfechanac.Text),
                Genero = txtgenero.Text,
                Direccion  = txtdireccion.Text,
                Telefono = txttelefono.Text,
                Email = txtemail.Text,
                FechaRegistro = Convert.ToDateTime(txtfecha.Text)
            };

            bool resultado = new CN_Pacientes().Editar(objPaciente, out mensaje);

            if (resultado)

            {
                MessageBox.Show("El Paciente ha sido editado correctamente.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtindice.Text)];
                row.Cells["Id"].Value = txtid.Text;
                row.Cells["nombre"].Value = txtnombre.Text;
                row.Cells["fechanac"].Value = txtfechanac.Text;
                row.Cells["genero"].Value = txtgenero.Text;
                row.Cells["direccion"].Value = txtdireccion.Text;
                row.Cells["telefono"].Value = txttelefono.Text;
                row.Cells["email"].Value = txtemail.Text;
                row.Cells["fecha"].Value = txtfecha.Text;
                Limpiar();
            }
            else
            {
                MessageBox.Show(mensaje);
            }
        }
        private void Limpiar()
        {

            txtindice.Text = "-1";
            txtid.Text = "0";
            txtnombre.Text = "";
            txtfechanac.Text = "";
            txtgenero.Text = "";
            txtdireccion.Text = "";
            txttelefono.Text = "";
            txtemail.Text = "";
            txtfecha.Text = "";

            txtnombre.Select();

        }
        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 1)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check20.Width;
                var h = Properties.Resources.check20.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check20, new Rectangle(x, y, w, h));
                e.Handled = true;

            }

        }
        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtindice.Text = indice.ToString();
                    txtid.Text = dgvdata.Rows[indice].Cells["Id"].Value.ToString();
                    txtnombre.Text = dgvdata.Rows[indice].Cells["nombre"].Value.ToString();
                    txtfechanac.Text = dgvdata.Rows[indice].Cells["fechanac"].Value.ToString();
                    txtgenero.Text = dgvdata.Rows[indice].Cells["genero"].Value.ToString();
                    txtdireccion.Text = dgvdata.Rows[indice].Cells["direccion"].Value.ToString();
                    txttelefono.Text = dgvdata.Rows[indice].Cells["telefono"].Value.ToString();
                    txtemail.Text = dgvdata.Rows[indice].Cells["email"].Value.ToString();
                    txtfecha.Text = dgvdata.Rows[indice].Cells["fecha"].Value.ToString();

                }
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el Paciente?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Pacientes objPaciente = new Pacientes()
                    {
                        IdPaciente = Convert.ToInt32(txtid.Text)
                    };

                    bool respuesta = new CN_Pacientes().Eliminar(objPaciente, out mensaje);

                    if (respuesta)
                    {
                        dgvdata.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    Limpiar();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cbobusqueda.SelectedItem).Valor.ToString();

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {

                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }
        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtbusqueda.Text = "";
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
        }
        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        
    }
}
