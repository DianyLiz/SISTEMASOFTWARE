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
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void sistemaDeCitasMedicasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dOCTORESToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lISTADEUToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aSIGNACIONESToolStripMenuItem_Click(object sender, EventArgs e)
        {

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


            panel1.Controls.Clear();
            panel1.Controls.Add(formulario);
            formulario.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new Registrarse());
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new ListaUsuarios());
        }

        private void registrarDoctorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new RegistroDoctores());
        }

        private void listaDeDoctoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new ListaDoctores());
        }

        private void registrarRolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new RegistrarRoles());
        }

        private void listaDeRolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new ListaRoles());
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new RegistroPacientes());
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new ListaPaciente());
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new RegistrarCita());
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new ListaCitas());
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new RegistroConsultoriocs());
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new ListaConsultorio());
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new RegistroEspecialidad());
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new ListaEspecialidad());
        }

        private void registrarHistorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new RegistroHistorial());
        }

        private void listaDeHistorialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new ListaHistorial());
        }
    }
}
