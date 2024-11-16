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
    public partial class ListaDoctores : Form
    {
        public ListaDoctores()
        {
            InitializeComponent();
        }
        private void frmDoctores_Load(object sender, EventArgs e)
        {
            List<Especialidades> listaEspecialidad = new CN_Especialidades().Listar();

            foreach (Especialidades item in listaEspecialidad)
            {
                cboespecialidad.Items.Add(new OpcionCombo() { Valor = item.IdEspecialidad, Texto = item.NombreEspecialidad });
            }
            cboespecialidad.DisplayMember = "Texto";
            cboespecialidad.ValueMember = "Valor";
            cboespecialidad.SelectedIndex = 0;


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

            List<Doctores> listaDoctores = new CN_Doctores().Listar();

            foreach (Doctores item in listaDoctores)
            {

                dgvdata.Rows.Add(new object[] {"",item.Id,item.Nombre,
                    item.oEspecialidad.IdEspecialidad,
                    item.oEspecialidad.NombreEspecialidad,
                    item.Telefono,item.Email,item.HorarioAtencion
                });
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {

            string mensaje = string.Empty;

            Doctores objDoctor = new Doctores()
            {
                Id = Convert.ToInt32(txtid.Text),
                Nombre = txtnombre.Text,
                oEspecialidad = new Especialidades() { IdEspecialidad = Convert.ToInt32(((OpcionCombo)cboespecialidad.SelectedItem).Valor) },
                Telefono = txttelefono.Text,
                Email = txtemail.Text,
                HorarioAtencion = txthorario.Text
            };

            bool resultado = new CN_Doctores().Editar(objDoctor, out mensaje);

            if (resultado)

            {
                MessageBox.Show("El Doctor ha sido editado correctamente.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtindice.Text)];
                row.Cells["Id"].Value = txtid.Text;
                row.Cells["nombre"].Value = txtnombre.Text;
                row.Cells["IdEspecialidad"].Value = ((OpcionCombo)cboespecialidad.SelectedItem).Valor.ToString();
                row.Cells["Especialidad"].Value = ((OpcionCombo)cboespecialidad.SelectedItem).Texto.ToString();
                row.Cells["telefono"].Value = txttelefono.Text;
                row.Cells["email"].Value = txtemail.Text;
                row.Cells["horario"].Value = txthorario.Text;
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
            cboespecialidad.SelectedIndex = 0;
            txttelefono.Text = "";
            txtemail.Text = "";
            txthorario.Text = "";

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
                    
                    foreach (OpcionCombo oc in cboespecialidad.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["IdEspecialidad"].Value))
                        {
                            int indice_combo = cboespecialidad.Items.IndexOf(oc);
                            cboespecialidad.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                    txttelefono.Text = dgvdata.Rows[indice].Cells["telefono"].Value.ToString();
                    txtemail.Text = dgvdata.Rows[indice].Cells["email"].Value.ToString();
                    txthorario.Text = dgvdata.Rows[indice].Cells["horario"].Value.ToString();
                    
                }
            }

        }
        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el Doctor?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Doctores objDoctor = new Doctores()
                    {
                        Id = Convert.ToInt32(txtid.Text)
                    };

                    bool respuesta = new CN_Doctores().Eliminar(objDoctor, out mensaje);

                    if (respuesta)
                    {
                        dgvdata.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
            }
        }
        private void btnbuscar_Click(object sender, EventArgs e)
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
