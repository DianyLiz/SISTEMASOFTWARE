using CapaDatos;
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
    public partial class RegistrarCita : Form
    {
        public RegistrarCita()
        {
            InitializeComponent();
        }

        private void Registrar_Load(object sender, EventArgs e)
        {
            cboestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Cancelada" });
            cboestado.DisplayMember = "Texto";
            cboestado.ValueMember = "Valor";
            cboestado.SelectedIndex = 0;

            List<Pacientes> listaPaciente = new CN_Pacientes().Listar();
            foreach (Pacientes item in listaPaciente)
            {
                cbopaciente.Items.Add(new OpcionCombo() { Valor = item.IdPaciente, Texto = item.Nombre });
            }
            cbopaciente.DisplayMember = "Texto";
            cbopaciente.ValueMember = "Valor";
            cbopaciente.SelectedIndex = 0;

            List<Doctores> listaDoctor = new CN_Doctores().Listar();
            foreach (Doctores item in listaDoctor)
            {
                cboDoctor.Items.Add(new OpcionCombo() { Valor = item.Id, Texto = item.Nombre });
            }
            cboDoctor.DisplayMember = "Texto";
            cboDoctor.ValueMember = "Valor";
            cboDoctor.SelectedIndex = 0;

            List<Consultorios> listaConsultorio = new CN_Consultorios().Listar();
            foreach (Consultorios item in listaConsultorio)
            {
                cboconsultorio.Items.Add(new OpcionCombo() { Valor = item.IdConsultorio, Texto = item.Consultorio });
            }
            cboconsultorio.DisplayMember = "Texto";
            cboconsultorio.ValueMember = "Valor";
        }


        private void btnRegistro_Click(object sender, EventArgs e)
        {
            //RegistroPacientes formularioRegistro = new RegistroPacientes();
            //formularioRegistro.Show();

            using (RegistroPacientes formularioRegistro = new RegistroPacientes())
            {
                formularioRegistro.ShowDialog();
            }
            CargarPacientes();
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
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;


            Citas objcita = new Citas()
            {
                IdCita = Convert.ToInt32(txtId.Text),
                oPaciente = new Pacientes() { IdPaciente = Convert.ToInt32(((OpcionCombo)cbopaciente.SelectedItem).Valor) },
                oDoctor = new Doctores() { Id = Convert.ToInt32(((OpcionCombo)cboDoctor.SelectedItem).Valor) },
                FechaCita = Convert.ToDateTime(txtfecha.Text),
                Motivo = txtmotivo.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cboestado.SelectedItem).Valor) == 1,
                oConsultorio = new Consultorios() { IdConsultorio = Convert.ToInt32(((OpcionCombo)cboconsultorio.SelectedItem).Valor) },
            };


            if (objcita.IdCita == 0)
            {
                int idcitagenerado = new CN_Citas().Registrar(objcita, out mensaje);

                if (idcitagenerado != 0)
                {
                    MessageBox.Show("Cita registrada correctamente.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool resultado = new CN_Citas().Editar(objcita, out mensaje);

                if (resultado)
                {
                    MessageBox.Show("Cita actualizada correctamente.", "Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Limpiar()
        {
            cbopaciente.SelectedIndex = 0;
            cboDoctor.SelectedIndex = 0;
            txtfecha.Text = string.Empty;
            txtmotivo.Text = string.Empty;
            cboestado.SelectedIndex = 0;
            cboconsultorio.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
                string textoBusqueda = txtbuscar.Text.Trim();

                if (string.IsNullOrWhiteSpace(textoBusqueda))
                {
                    // Si el texto está vacío, cargar todos los pacientes
                    CargarPacientes();
                    MessageBox.Show("Lista de pacientes restaurada.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Filtrar pacientes según el texto de búsqueda
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


