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
    public partial class RegistrarRoles : Form
    {
        public RegistrarRoles()
        {
            InitializeComponent();
        }
        private void Registrarse_Load(object sender, EventArgs e)
        {
            List<Rol> lista = new CN_Rol().Listar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;


            Rol objRol = new Rol()
            {
                IdRol = Convert.ToInt32(txtId.Text),
                NombreRol = txtnombre.Text,
                Descripcion = txtdescripcion.Text
            };


            if (objRol.IdRol == 0)
            {
                int idrolgenerado = new CN_Rol().Registrar(objRol, out mensaje);

                if (idrolgenerado != 0)
                {
                    MessageBox.Show("Rol registrado correctamente.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool resultado = new CN_Rol().Editar(objRol, out mensaje);

                if (resultado)
                {
                    MessageBox.Show("Rol actualizado correctamente.", "Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
