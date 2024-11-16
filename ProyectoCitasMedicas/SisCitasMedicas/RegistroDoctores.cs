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
    public partial class RegistroDoctores : Form
    {
        public RegistroDoctores()
        {
            InitializeComponent();
        }

        private void Registro_Load(object sender, EventArgs e)
        {

            List<Especialidades> listaEspecialidades = new CN_Especialidades().Listar();
            foreach (Especialidades item in listaEspecialidades)
            {
                cboespecialidad.Items.Add(new OpcionCombo() { Valor = item.IdEspecialidad, Texto = item.NombreEspecialidad });
            }
            cboespecialidad.DisplayMember = "Texto";
            cboespecialidad.ValueMember = "Valor";
            cboespecialidad.SelectedIndex = 0;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Limpiar()
        {
            txtnombre.Text = string.Empty;
            cboespecialidad.SelectedIndex = 0;
            txttelefono.Text = string.Empty;
            txtemail.Text = string.Empty;
            txthorario.Text = string.Empty;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;


            Doctores objDoctor = new Doctores()
            {
                Id = Convert.ToInt32(txtId.Text),
                Nombre = txtnombre.Text,
                oEspecialidad = new Especialidades() { IdEspecialidad = Convert.ToInt32(((OpcionCombo)cboespecialidad.SelectedItem).Valor) },
                Telefono = txttelefono.Text,
                Email = txtemail.Text,
                HorarioAtencion = txthorario.Text
            };


            if (objDoctor.Id == 0)
            {
                int iddoctorgenerado = new CN_Doctores().Registrar(objDoctor, out mensaje);

                if (iddoctorgenerado != 0)
                {
                    MessageBox.Show("Doctor registrado correctamente.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool resultado = new CN_Doctores().Editar(objDoctor, out mensaje);

                if (resultado)
                {
                    MessageBox.Show("Doctor actualizado correctamente.", "Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
