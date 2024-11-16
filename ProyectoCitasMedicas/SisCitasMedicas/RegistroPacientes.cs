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
    public partial class RegistroPacientes : Form
    {
        public RegistroPacientes()
        {
            InitializeComponent();
        }
        private void RegistroP_Load(object sender, EventArgs e)
        {

            List<Pacientes> lista = new CN_Pacientes().Listar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Limpiar()
        {
            txtnombre.Text = string.Empty;
            txtfechaNac.Text = string.Empty;
            txtgenero.Text = string.Empty;
            txtdireccion.Text = string.Empty;
            txttelefono.Text = string.Empty;
            txtemail.Text = string.Empty;
            txtfecha.Text = string.Empty;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;


            Pacientes objPaciente = new Pacientes()
            {
                IdPaciente = Convert.ToInt32(txtId.Text),
                Nombre = txtnombre.Text,
                FechaNacimiento = Convert.ToDateTime(txtfechaNac.Text),
                Genero = txtgenero.Text,
                Direccion = txtdireccion.Text,
                Telefono = txttelefono.Text,
                Email = txtemail.Text,
                FechaRegistro = Convert.ToDateTime(txtfecha.Text),
            };


            if (objPaciente.IdPaciente == 0)
            {
                int idpacientegenerado = new CN_Pacientes().Registrar(objPaciente, out mensaje);

                if (idpacientegenerado != 0)
                {
                    MessageBox.Show("Paciente registrado correctamente.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool resultado = new CN_Pacientes().Editar(objPaciente, out mensaje);

                if (resultado)
                {
                    MessageBox.Show("Paciente actualizado correctamente.", "Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
