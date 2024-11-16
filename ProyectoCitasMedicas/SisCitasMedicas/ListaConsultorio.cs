using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SisCitasMedicas
{
    public partial class ListaConsultorio : Form
    {
        public ListaConsultorio()
        {
            InitializeComponent();
        }
        private void frmConsultorio_Load(object sender, EventArgs e)
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

            List<Consultorios> listaConsultorio = new CN_Consultorios().Listar();

            foreach (Consultorios item in listaConsultorio)
            {

                dgvdata.Rows.Add(new object[] {"",item.IdConsultorio,item.Consultorio,
                    item.Ubicacion, item.Capacidad
                });
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Consultorios objConsultorio = new Consultorios()
            {
                IdConsultorio = Convert.ToInt32(txtid.Text),
                Consultorio = txtconsultorio.Text,
                Ubicacion = txtubicacion.Text,
                Capacidad = Convert.ToInt32(txtcapacidad.Text),
            };

            bool resultado = new CN_Consultorios().Editar(objConsultorio, out mensaje);

            if (resultado)

            {
                MessageBox.Show("El Consultorio ha sido editado correctamente.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtindice.Text)];
                row.Cells["Id"].Value = txtid.Text;
                row.Cells["consultorio"].Value = txtconsultorio.Text;
                row.Cells["ubicacion"].Value = txtubicacion.Text;
                row.Cells["capacidad"].Value = txtcapacidad.Text;
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
            txtconsultorio.Text = "";
            txtubicacion.Text = "";
            txtcapacidad.Text = "";

            txtconsultorio.Select();

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
                    txtconsultorio.Text = dgvdata.Rows[indice].Cells["consultorio"].Value.ToString();
                    txtubicacion.Text = dgvdata.Rows[indice].Cells["ubicacion"].Value.ToString();
                    txtcapacidad.Text = dgvdata.Rows[indice].Cells["capacidad"].Value.ToString();
                }
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el Consultorio?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Consultorios objConsultorio = new Consultorios()
                    {
                        IdConsultorio = Convert.ToInt32(txtid.Text)
                    };

                    bool respuesta = new CN_Consultorios().Eliminar(objConsultorio, out mensaje);

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
