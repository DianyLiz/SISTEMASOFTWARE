using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SisCitasMedicas
{
    public partial class RegistroHistorial : Form
    {
        public RegistroHistorial()
        {
            InitializeComponent();
        }

        private void RegistroHistorial_Load(object sender, EventArgs e)
        {
            // Cargar lista de pacientes
            List<Pacientes> listaPaciente = new CN_Pacientes().Listar();
            foreach (Pacientes item in listaPaciente)
            {
                cbopaciente.Items.Add(new OpcionCombo() { Valor = item.IdPaciente, Texto = item.Nombre });
            }
            cbopaciente.DisplayMember = "Texto";
            cbopaciente.ValueMember = "Valor";
            cbopaciente.SelectedIndex = 0;

            // Configurar el evento SelectedIndexChanged
            cbopaciente.SelectedIndexChanged += cbopaciente_SelectedIndexChanged;

            // Cargar motivos de citas para el paciente seleccionado inicialmente
            CargarCitasDelPaciente();
        }

        private void cbopaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cargar citas según el paciente seleccionado
            CargarCitasDelPaciente();
        }

        private void CargarCitasDelPaciente()
        {
            // Obtener el paciente seleccionado
            var pacienteSeleccionado = cbopaciente.SelectedItem as OpcionCombo;
            if (pacienteSeleccionado == null) return;

            int idPaciente = Convert.ToInt32(pacienteSeleccionado.Valor);

            // Filtrar citas por paciente
            List<Citas> listaCita = new CN_Citas().Listar()
                .Where(c => c.oPaciente.IdPaciente == idPaciente)
                .ToList();

            // Limpiar y llenar el ComboBox de citas
            cbocita.Items.Clear();
            foreach (Citas item in listaCita)
            {
                cbocita.Items.Add(new OpcionCombo() { Valor = item.IdCita, Texto = item.Motivo });
            }
            if (cbocita.Items.Count > 0)
            {
                cbocita.DisplayMember = "Texto";
                cbocita.ValueMember = "Valor";
                cbocita.SelectedIndex = 0;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            if (string.IsNullOrEmpty(txtdiagnostico.Text) ||
                string.IsNullOrEmpty(txttratamiento.Text) ||
                string.IsNullOrEmpty(txtfecha.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime fecha;
            if (!DateTime.TryParse(txtfecha.Text, out fecha))
            {
                MessageBox.Show("La fecha ingresada no es válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Historial objHistorial = new Historial()
                {
                    IdHistorial = string.IsNullOrEmpty(txtId.Text) ? 0 : Convert.ToInt32(txtId.Text),
                    oPaciente = new Pacientes() { IdPaciente = Convert.ToInt32(((OpcionCombo)cbopaciente.SelectedItem).Valor) },
                    oCita = new Citas() { IdCita = Convert.ToInt32(((OpcionCombo)cbocita.SelectedItem).Valor) },
                    Diagnostico = txtdiagnostico.Text,
                    Tratamiento = txttratamiento.Text,
                    Fecha = fecha
                };

                if (objHistorial.IdHistorial == 0)
                {
                    int idhistorialgenerado = new CN_Historial().Registrar(objHistorial, out mensaje);

                    if (idhistorialgenerado != 0)
                    {
                        MessageBox.Show("Historial registrado correctamente.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    bool resultado = new CN_Historial().Editar(objHistorial, out mensaje);

                    if (resultado)
                    {
                        MessageBox.Show("Historial actualizado correctamente.", "Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Limpiar()
        {
            cbopaciente.SelectedIndex = 0;
            cbocita.Items.Clear();
            txtId.Text = string.Empty;
            txtdiagnostico.Text = string.Empty;
            txttratamiento.Text = string.Empty;
            txtfecha.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnbuscar_Click(object sender, EventArgs e)
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
