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
    public partial class ListaCitas : Form
    {
        public ListaCitas()
        {
            InitializeComponent();
        }
        private void frmCitas_Load(object sender, EventArgs e)
        {
            cboestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Cancelada" });
            cboestado.DisplayMember = "Texto";
            cboestado.ValueMember = "Valor";
            cboestado.SelectedIndex = 0;

            List<CapaEntidad.Pacientes> listaPaciente = new CN_Pacientes().Listar();
            foreach (CapaEntidad.Pacientes item in listaPaciente)
            {
                cbopaciente.Items.Add(new OpcionCombo() { Valor = item.IdPaciente, Texto = item.Nombre });
            }
            cbopaciente.DisplayMember = "Texto";
            cbopaciente.ValueMember = "Valor";
            cbopaciente.SelectedIndex = 0;

            // Llenar el ComboBox de Doctores
            List<CapaEntidad.Doctores> listaDoctor = new CN_Doctores().Listar();
            foreach (CapaEntidad.Doctores item in listaDoctor)
            {
                cbodoctor.Items.Add(new OpcionCombo() { Valor = item.Id, Texto = item.Nombre });
            }
            cbodoctor.DisplayMember = "Texto";
            cbodoctor.ValueMember = "Valor";
            cbodoctor.SelectedIndex = 0;


            List<Consultorios> listaConsultorio = new CN_Consultorios().Listar();
            foreach (Consultorios item in listaConsultorio)
            {
                cboconsultorio.Items.Add(new OpcionCombo() { Valor = item.IdConsultorio, Texto = item.Consultorio });
            }
            cboconsultorio.DisplayMember = "Texto";
            cboconsultorio.ValueMember = "Valor";

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

            // Llenar el DataGridView de citas
            List<CapaEntidad.Citas> listaCitas = new CN_Citas().Listar();
            foreach (CapaEntidad.Citas item in listaCitas)
            {
                dgvdata.Rows.Add(new object[]
                {
                    "", // Botón seleccionar
                    item.IdCita,
                    item.oPaciente.IdPaciente,
                    item.oPaciente.Nombre,
                    item.oDoctor.Id,
                    item.oDoctor.Nombre,
                    item.FechaCita.ToString("yyyy-MM-dd HH:mm"), // Formatear la fecha
                    item.Motivo,
                    item.Estado == true ? 1 : 0 ,
                    item.Estado == true ? "Activo" : "No Activo",
                    item.oConsultorio.IdConsultorio,
                    item.oConsultorio.Consultorio // Mostrar el texto del consultorio
                });
            
        }
    }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Citas objcita = new Citas()
            {
                IdCita = Convert.ToInt32(txtid.Text),
                oPaciente = new Pacientes() { IdPaciente = Convert.ToInt32(((OpcionCombo)cbopaciente.SelectedItem).Valor) },
                oDoctor = new Doctores() { Id = Convert.ToInt32(((OpcionCombo)cbodoctor.SelectedItem).Valor) },
                FechaCita = Convert.ToDateTime(txtfecha.Text),
                Motivo = txtmotivo.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cboestado.SelectedItem).Valor) == 1,
                oConsultorio = new Consultorios() { IdConsultorio = Convert.ToInt32(((OpcionCombo)cboconsultorio.SelectedItem).Valor) },
            };

            bool resultado = new CN_Citas().Editar(objcita, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtindice.Text)];
                    row.Cells["Id"].Value = txtid.Text;
                    row.Cells["IdPaciente"].Value = ((OpcionCombo)cbopaciente.SelectedItem).Valor.ToString();
                    row.Cells["Paciente"].Value = ((OpcionCombo)cbopaciente.SelectedItem).Texto.ToString();
                    row.Cells["IdDoctor"].Value = ((OpcionCombo)cbodoctor.SelectedItem).Valor.ToString();
                    row.Cells["Doctor"].Value = ((OpcionCombo)cbodoctor.SelectedItem).Texto.ToString();
                    row.Cells["fecha"].Value = txtfecha.Text;
                    row.Cells["Motivo"].Value = txtmotivo.Text;
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cboestado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cboestado.SelectedItem).Texto.ToString();
                    row.Cells["IdConsultorio"].Value = ((OpcionCombo)cboconsultorio.SelectedItem).Valor.ToString();
                    row.Cells["Consultorio"].Value = ((OpcionCombo)cboconsultorio.SelectedItem).Texto.ToString();
              
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
            cbopaciente.SelectedIndex = 0;
            cbodoctor.SelectedIndex = 0;
            txtfecha.Text = "";
            txtmotivo.Text = "";
            cboestado.SelectedIndex = 0;
            cboconsultorio.SelectedIndex = 0;

            txtindice.Select();
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
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
            if (e.RowIndex >= 0 && dgvdata.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                int indice = e.RowIndex;

                txtindice.Text = indice.ToString();
                txtid.Text = dgvdata.Rows[indice].Cells["Id"].Value.ToString();

                foreach (OpcionCombo oc in cbopaciente.Items)
                {
                    if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["IdPaciente"].Value))
                    {
                        int indice_combo = cbopaciente.Items.IndexOf(oc);
                        cbopaciente.SelectedIndex = indice_combo;
                        break;
                    }
                }

                foreach (OpcionCombo oc in cbodoctor.Items)
                {
                    if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["IdDoctor"].Value))
                    {
                        int indice_combo = cbodoctor.Items.IndexOf(oc);
                        cbodoctor.SelectedIndex = indice_combo;
                        break;
                    }
                }

                txtfecha.Text = dgvdata.Rows[indice].Cells["fecha"].Value.ToString();
                txtmotivo.Text = dgvdata.Rows[indice].Cells["Motivo"].Value.ToString();
                foreach (OpcionCombo oc in cboestado.Items)
                {
                    if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["EstadoValor"].Value))
                    {
                        int indice_combo = cboestado.Items.IndexOf(oc);
                        cboestado.SelectedIndex = indice_combo;
                        break;
                    }
                };
                foreach (OpcionCombo oc in cboconsultorio.Items)
                {
                    if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["IdConsultorio"].Value))
                    {
                        int indice_combo = cboconsultorio.Items.IndexOf(oc);
                        cboconsultorio.SelectedIndex = indice_combo;
                        break;
                    }
                }

            }

        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar la Cita", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    CapaEntidad.Citas objcita = new CapaEntidad.Citas()
                    {
                        IdCita = Convert.ToInt32(txtid.Text)
                    };

                    bool respuesta = new CN_Citas().Eliminar(objcita, out mensaje);

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
