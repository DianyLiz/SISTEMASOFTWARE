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
    public partial class RegistroConsultoriocs : Form
    {
        public RegistroConsultoriocs()
        {
            InitializeComponent();
        }

        private void Registro_Load(object sender, EventArgs e)
        {

            List<Consultorios> lista = new CN_Consultorios().Listar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;


            Consultorios objConsultorio = new Consultorios()
            {
                IdConsultorio = Convert.ToInt32(txtId.Text),
                Consultorio = txtconsultorio.Text,
                Ubicacion = txtubicacion.Text,
                Capacidad = Convert.ToInt32(txtcapacidad.Text),
            };


            if (objConsultorio.IdConsultorio == 0)
            {
                int idconsultoriogenerado = new CN_Consultorios().Registrar(objConsultorio, out mensaje);

                if (idconsultoriogenerado != 0)
                {
                    MessageBox.Show("Consultorio registrado correctamente.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool resultado = new CN_Consultorios().Editar(objConsultorio, out mensaje);

                if (resultado)
                {
                    MessageBox.Show("Consultorio actualizado correctamente.", "Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtindice.Text = "-1";
            txtId.Text = "0";
            txtconsultorio.Text = "";
            txtubicacion.Text = "";
            txtcapacidad.Text = "";

            txtconsultorio.Select();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
