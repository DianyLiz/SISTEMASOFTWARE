using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SisCitasMedicas
{
    public partial class ListaHistorial : Form
    {
        public ListaHistorial()
        {
            InitializeComponent();
            cbopaciente.SelectedIndexChanged += cbopaciente_SelectedIndexChanged; // Asociar el evento
        }

        private void frmHistorial_Load(object sender, EventArgs e)
        {
            // Cargar pacientes
            List<CapaEntidad.Pacientes> listaPaciente = new CN_Pacientes().Listar();
            foreach (CapaEntidad.Pacientes item in listaPaciente)
            {
                cbopaciente.Items.Add(new OpcionCombo() { Valor = item.IdPaciente, Texto = item.Nombre });
            }
            cbopaciente.DisplayMember = "Texto";
            cbopaciente.ValueMember = "Valor";
            cbopaciente.SelectedIndex = 0;

            // Cargar citas iniciales (opcional)
            CargarCitasPorPaciente(Convert.ToInt32(((OpcionCombo)cbopaciente.SelectedItem).Valor));

            // Configuración del DataGridView para las columnas visibles
            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                if (columna.Visible && columna.Name != "btnseleccionar")
                {
                    cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cbobusqueda.SelectedIndex = 0;

            // Cargar historial
            List<CapaEntidad.Historial> listahistorial = new CN_Historial().Listar();
            foreach (CapaEntidad.Historial item in listahistorial)
            {
                dgvdata.Rows.Add(new object[]
                {
                    "",
                    item.IdHistorial,
                    item.oPaciente.IdPaciente,
                    item.oPaciente.Nombre,
                    item.oCita.IdCita,
                    item.oCita.Motivo,
                    item.Diagnostico,
                    item.Tratamiento,
                    item.Fecha.ToString("yyyy-MM-dd HH:mm"),
                });
            }
        }

        private void cbopaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el Id del paciente seleccionado
            int idPacienteSeleccionado = Convert.ToInt32(((OpcionCombo)cbopaciente.SelectedItem).Valor);

            // Cargar citas asociadas al paciente
            CargarCitasPorPaciente(idPacienteSeleccionado);
        }

        private void CargarCitasPorPaciente(int idPaciente)
        {
            // Limpia el ComboBox de citas
            cbocita.Items.Clear();

            // Obtén las citas asociadas al paciente
            List<CapaEntidad.Citas> listaCitas = new CN_Citas().Listar()
                .Where(c => c.oPaciente.IdPaciente == idPaciente) // Filtrar por Id del paciente
                .ToList();

            // Cargar citas en el ComboBox
            foreach (CapaEntidad.Citas item in listaCitas)
            {
                cbocita.Items.Add(new OpcionCombo() { Valor = item.IdCita, Texto = item.Motivo });
            }

            cbocita.DisplayMember = "Texto";
            cbocita.ValueMember = "Valor";

            // Seleccionar el primer elemento si hay citas disponibles
            if (cbocita.Items.Count > 0)
            {
                cbocita.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Este paciente no tiene citas asociadas.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Historial objhistorial = new Historial()
            {
                IdHistorial = Convert.ToInt32(txtid.Text),
                oPaciente = new Pacientes() { IdPaciente = Convert.ToInt32(((OpcionCombo)cbopaciente.SelectedItem).Valor) },
                oCita = new Citas() { IdCita = Convert.ToInt32(((OpcionCombo)cbocita.SelectedItem).Valor) },
                Diagnostico = txtdiagnostico.Text,
                Tratamiento = txttratamiento.Text,
                Fecha = Convert.ToDateTime(txtfecha.Text),
            };

            bool resultado = new CN_Historial().Editar(objhistorial, out mensaje);

            if (resultado)
            {
                DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtindice.Text)];
                row.Cells["Id"].Value = txtid.Text;
                row.Cells["IdPaciente"].Value = ((OpcionCombo)cbopaciente.SelectedItem).Valor.ToString();
                row.Cells["Paciente"].Value = ((OpcionCombo)cbopaciente.SelectedItem).Texto.ToString();
                row.Cells["IdCita"].Value = ((OpcionCombo)cbocita.SelectedItem).Valor.ToString();
                row.Cells["cita"].Value = ((OpcionCombo)cbocita.SelectedItem).Texto.ToString();
                row.Cells["diagnostico"].Value = txtdiagnostico.Text;
                row.Cells["tratamiento"].Value = txttratamiento.Text;
                row.Cells["fecha"].Value = txtfecha.Text;

                MessageBox.Show("El historial ha sido actualizado exitosamente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
            }
            else
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Limpiar()
        {
            txtindice.Text = "-1";
            txtid.Text = "0";
            cbopaciente.SelectedIndex = 0;
            cbocita.SelectedIndex = 0;
            txtdiagnostico.Text = "";
            txttratamiento.Text = "";
            txtfecha.Text = "";

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

                foreach (OpcionCombo oc in cbocita.Items)
                {
                    if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["IdCita"].Value))
                    {
                        int indice_combo = cbocita.Items.IndexOf(oc);
                        cbocita.SelectedIndex = indice_combo;
                        break;
                    }
                }

                txtdiagnostico.Text = dgvdata.Rows[indice].Cells["diagnostico"].Value.ToString();
                txttratamiento.Text = dgvdata.Rows[indice].Cells["tratamiento"].Value.ToString();
                txtfecha.Text = dgvdata.Rows[indice].Cells["fecha"].Value.ToString();
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el historial?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    CapaEntidad.Historial objHistorial = new CapaEntidad.Historial()
                    {
                        IdHistorial = Convert.ToInt32(txtid.Text)
                    };

                    bool respuesta = new CN_Historial().Eliminar(objHistorial, out mensaje);

                    if (respuesta)
                    {
                        dgvdata.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
                        MessageBox.Show("El historial ha sido eliminado exitosamente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un historial válido para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void CargarPacientes()
        {
            cbopaciente.Items.Clear();

            List<Pacientes> listaPaciente = new CN_Pacientes().Listar();
            foreach (Pacientes item in listaPaciente)
            {
                cbopaciente.Items.Add(new OpcionCombo() { Valor = item.IdPaciente, Texto = item.Nombre });
            }
            cbopaciente.DisplayMember = "Texto";
            cbopaciente.ValueMember = "Valor";

            if (cbopaciente.Items.Count > 0) cbopaciente.SelectedIndex = 0;
        }

        private void btnbuscar_Click_1(object sender, EventArgs e)
        {
            string textoBusqueda = txtbuscar.Text.Trim();

            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                
                CargarPacientes();
                MessageBox.Show("Lista de pacientes restaurada.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
            
                List<Pacientes> listaFiltrada = new CN_Pacientes().Listar()
                    .Where(p => p.Nombre != null && p.Nombre.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                if (listaFiltrada.Any())
                {
                    cbopaciente.Items.Clear();
                    foreach (Pacientes item in listaFiltrada)
                    {
                        cbopaciente.Items.Add(new OpcionCombo() { Valor = item.IdPaciente, Texto = item.Nombre });
                    }
                    cbopaciente.DisplayMember = "Texto";
                    cbopaciente.ValueMember = "Valor";
                    cbopaciente.SelectedIndex = 0;

                    MessageBox.Show($"{listaFiltrada.Count} paciente(s) encontrado(s).", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontraron pacientes con ese criterio de búsqueda.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
