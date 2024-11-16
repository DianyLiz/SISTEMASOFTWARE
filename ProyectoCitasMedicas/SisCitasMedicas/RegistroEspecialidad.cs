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
    public partial class RegistroEspecialidad : Form
    {
        public RegistroEspecialidad()
        {
            InitializeComponent();
        }
        private void Registro_Load(object sender, EventArgs e)
        {
            List<Especialidades> lista = new CN_Especialidades().Listar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;


            Especialidades objEspecialidad = new Especialidades()
            {
                IdEspecialidad = Convert.ToInt32(txtId.Text),
                NombreEspecialidad = txtnombre.Text,
                Descripcion = txtdescripcion.Text
            };


            if (objEspecialidad.IdEspecialidad == 0)
            {
                int idespecialidadgenerado = new CN_Especialidades().Registrar(objEspecialidad, out mensaje);

                if (idespecialidadgenerado != 0)
                {
                    MessageBox.Show("Especialidad registrada correctamente.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool resultado = new CN_Especialidades().Editar(objEspecialidad, out mensaje);

                if (resultado)
                {
                    MessageBox.Show("Especialidad actualizada correctamente.", "Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtnombre.Text = string.Empty;
            txtdescripcion.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
