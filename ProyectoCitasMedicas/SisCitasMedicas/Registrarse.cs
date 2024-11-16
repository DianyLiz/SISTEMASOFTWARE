using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CapaEntidad;
using CapaNegocios;

namespace SisCitasMedicas
{
    public partial class Registrarse : Form
    {
        public Registrarse()
        {
            InitializeComponent();
        }

        private void Registrarse_Load(object sender, EventArgs e)
        {
            cboestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboestado.DisplayMember = "Texto";
            cboestado.ValueMember = "Valor";
            cboestado.SelectedIndex = 0;

            List<Rol> listaRol = new CN_Rol().Listar();
            foreach (Rol item in listaRol)
            {
                cborol.Items.Add(new OpcionCombo() { Valor = item.IdRol, Texto = item.NombreRol });
            }
            cborol.DisplayMember = "Texto";
            cborol.ValueMember = "Valor";
            cborol.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            
            Usuario objusuario = new Usuario()
            {
                IdUsuario = Convert.ToInt32(txtId.Text),
                Nombre = txtnombre.Text,
                Contraseña = txtcontraseña.Text,
                Email = txtemail.Text,
                Telefono = txttelefono.Text,
                oRol = new Rol() { IdRol = Convert.ToInt32(((OpcionCombo)cborol.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionCombo)cboestado.SelectedItem).Valor) == 1,
                FechaCreacion = Convert.ToDateTime(txtFecha.Text)
            };

            
            if (objusuario.IdUsuario == 0)
            {
                int idusuariogenerado = new CN_Usuario().Registrar(objusuario, out mensaje);

                if (idusuariogenerado != 0)
                {
                    MessageBox.Show("Usuario registrado correctamente.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool resultado = new CN_Usuario().Editar(objusuario, out mensaje);

                if (resultado)
                {
                    MessageBox.Show("Usuario actualizado correctamente.", "Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtcontraseña.Text = string.Empty;
            txtemail.Text = string.Empty;
            txttelefono.Text = string.Empty;
            cborol.SelectedIndex = 0;
            cboestado.SelectedIndex = 0;
            txtFecha.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private static Form FormularioActivo = null;


        private void AbrirFormulario(Form formulario)
        {
            if (FormularioActivo != null)
            {
                FormularioActivo.Close();
            }
            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.SteelBlue;

            
            panelcontainer.Controls.Clear();  
            panelcontainer.Controls.Add(formulario);
            formulario.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new ListaUsuarios());
        }

        private void txtemail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
