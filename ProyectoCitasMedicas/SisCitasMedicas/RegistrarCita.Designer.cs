namespace SisCitasMedicas
{
    partial class RegistrarCita
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrarCita));
            this.txtindice = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtmotivo = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboestado = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbopaciente = new System.Windows.Forms.ComboBox();
            this.panelcontainer = new System.Windows.Forms.Panel();
            this.btnbuscar = new System.Windows.Forms.Button();
            this.txtbuscar = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboconsultorio = new System.Windows.Forms.ComboBox();
            this.txtfecha = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDoctor = new System.Windows.Forms.ComboBox();
            this.btnRegistro = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelcontainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtindice
            // 
            this.txtindice.Location = new System.Drawing.Point(4, 12);
            this.txtindice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtindice.Name = "txtindice";
            this.txtindice.Size = new System.Drawing.Size(36, 26);
            this.txtindice.TabIndex = 29;
            this.txtindice.Text = "-1";
            this.txtindice.Visible = false;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(52, 12);
            this.txtId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(36, 26);
            this.txtId.TabIndex = 28;
            this.txtId.Text = "0";
            this.txtId.Visible = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Highlight;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.button2.ForeColor = System.Drawing.Color.AliceBlue;
            this.button2.Location = new System.Drawing.Point(748, 357);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(193, 46);
            this.button2.TabIndex = 10;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtmotivo
            // 
            this.txtmotivo.Location = new System.Drawing.Point(176, 328);
            this.txtmotivo.Multiline = true;
            this.txtmotivo.Name = "txtmotivo";
            this.txtmotivo.Size = new System.Drawing.Size(526, 36);
            this.txtmotivo.TabIndex = 17;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.btnGuardar.ForeColor = System.Drawing.Color.Transparent;
            this.btnGuardar.Location = new System.Drawing.Point(748, 305);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(193, 46);
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Text = "Aceptar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(208, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(438, 63);
            this.label1.TabIndex = 0;
            this.label1.Text = "Registro de Citas";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label8.ForeColor = System.Drawing.Color.MediumBlue;
            this.label8.Location = new System.Drawing.Point(46, 332);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 32);
            this.label8.TabIndex = 16;
            this.label8.Text = "Motivo:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label7.ForeColor = System.Drawing.Color.MediumBlue;
            this.label7.Location = new System.Drawing.Point(48, 382);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 32);
            this.label7.TabIndex = 14;
            this.label7.Text = "Estado:";
            // 
            // cboestado
            // 
            this.cboestado.FormattingEnabled = true;
            this.cboestado.Location = new System.Drawing.Point(176, 386);
            this.cboestado.Name = "cboestado";
            this.cboestado.Size = new System.Drawing.Size(526, 28);
            this.cboestado.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label6.ForeColor = System.Drawing.Color.MediumBlue;
            this.label6.Location = new System.Drawing.Point(46, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 32);
            this.label6.TabIndex = 12;
            this.label6.Text = "Paciente:";
            // 
            // cbopaciente
            // 
            this.cbopaciente.FormattingEnabled = true;
            this.cbopaciente.Location = new System.Drawing.Point(176, 157);
            this.cbopaciente.Name = "cbopaciente";
            this.cbopaciente.Size = new System.Drawing.Size(245, 28);
            this.cbopaciente.TabIndex = 13;
            // 
            // panelcontainer
            // 
            this.panelcontainer.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelcontainer.Controls.Add(this.btnbuscar);
            this.panelcontainer.Controls.Add(this.txtbuscar);
            this.panelcontainer.Controls.Add(this.label4);
            this.panelcontainer.Controls.Add(this.cboconsultorio);
            this.panelcontainer.Controls.Add(this.txtfecha);
            this.panelcontainer.Controls.Add(this.label3);
            this.panelcontainer.Controls.Add(this.label2);
            this.panelcontainer.Controls.Add(this.cboDoctor);
            this.panelcontainer.Controls.Add(this.btnRegistro);
            this.panelcontainer.Controls.Add(this.txtindice);
            this.panelcontainer.Controls.Add(this.txtId);
            this.panelcontainer.Controls.Add(this.button2);
            this.panelcontainer.Controls.Add(this.txtmotivo);
            this.panelcontainer.Controls.Add(this.btnGuardar);
            this.panelcontainer.Controls.Add(this.label1);
            this.panelcontainer.Controls.Add(this.label8);
            this.panelcontainer.Controls.Add(this.pictureBox1);
            this.panelcontainer.Controls.Add(this.label7);
            this.panelcontainer.Controls.Add(this.cboestado);
            this.panelcontainer.Controls.Add(this.label6);
            this.panelcontainer.Controls.Add(this.cbopaciente);
            this.panelcontainer.Location = new System.Drawing.Point(1, 3);
            this.panelcontainer.Name = "panelcontainer";
            this.panelcontainer.Size = new System.Drawing.Size(965, 635);
            this.panelcontainer.TabIndex = 31;
            // 
            // btnbuscar
            // 
            this.btnbuscar.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnbuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnbuscar.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.btnbuscar.ForeColor = System.Drawing.Color.Transparent;
            this.btnbuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnbuscar.Image")));
            this.btnbuscar.Location = new System.Drawing.Point(776, 153);
            this.btnbuscar.Name = "btnbuscar";
            this.btnbuscar.Size = new System.Drawing.Size(112, 32);
            this.btnbuscar.TabIndex = 38;
            this.btnbuscar.UseVisualStyleBackColor = false;
            this.btnbuscar.Click += new System.EventHandler(this.btnbuscar_Click);
            // 
            // txtbuscar
            // 
            this.txtbuscar.Location = new System.Drawing.Point(427, 157);
            this.txtbuscar.Multiline = true;
            this.txtbuscar.Name = "txtbuscar";
            this.txtbuscar.Size = new System.Drawing.Size(290, 28);
            this.txtbuscar.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label4.ForeColor = System.Drawing.Color.MediumBlue;
            this.label4.Location = new System.Drawing.Point(48, 437);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 32);
            this.label4.TabIndex = 35;
            this.label4.Text = "Consultorio:";
            // 
            // cboconsultorio
            // 
            this.cboconsultorio.FormattingEnabled = true;
            this.cboconsultorio.Location = new System.Drawing.Point(219, 441);
            this.cboconsultorio.Name = "cboconsultorio";
            this.cboconsultorio.Size = new System.Drawing.Size(483, 28);
            this.cboconsultorio.TabIndex = 36;
            // 
            // txtfecha
            // 
            this.txtfecha.Location = new System.Drawing.Point(205, 267);
            this.txtfecha.Multiline = true;
            this.txtfecha.Name = "txtfecha";
            this.txtfecha.Size = new System.Drawing.Size(497, 36);
            this.txtfecha.TabIndex = 34;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(46, 271);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 32);
            this.label3.TabIndex = 33;
            this.label3.Text = "Fecha Cita:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(46, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 32);
            this.label2.TabIndex = 31;
            this.label2.Text = "Doctor:";
            // 
            // cboDoctor
            // 
            this.cboDoctor.FormattingEnabled = true;
            this.cboDoctor.Location = new System.Drawing.Point(176, 215);
            this.cboDoctor.Name = "cboDoctor";
            this.cboDoctor.Size = new System.Drawing.Size(526, 28);
            this.cboDoctor.TabIndex = 32;
            // 
            // btnRegistro
            // 
            this.btnRegistro.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnRegistro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistro.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.btnRegistro.ForeColor = System.Drawing.Color.Transparent;
            this.btnRegistro.Location = new System.Drawing.Point(748, 250);
            this.btnRegistro.Name = "btnRegistro";
            this.btnRegistro.Size = new System.Drawing.Size(193, 49);
            this.btnRegistro.TabIndex = 30;
            this.btnRegistro.Text = "Registrarse";
            this.btnRegistro.UseVisualStyleBackColor = false;
            this.btnRegistro.Click += new System.EventHandler(this.btnRegistro_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SisCitasMedicas.Properties.Resources.calendario;
            this.pictureBox1.Location = new System.Drawing.Point(789, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(112, 112);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // RegistrarCita
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 638);
            this.Controls.Add(this.panelcontainer);
            this.Name = "RegistrarCita";
            this.Text = "RegistrarCita";
            this.Load += new System.EventHandler(this.Registrar_Load);
            this.panelcontainer.ResumeLayout(false);
            this.panelcontainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtindice;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtmotivo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboestado;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbopaciente;
        private System.Windows.Forms.Panel panelcontainer;
        private System.Windows.Forms.Button btnRegistro;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboDoctor;
        private System.Windows.Forms.TextBox txtfecha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboconsultorio;
        private System.Windows.Forms.Button btnbuscar;
        private System.Windows.Forms.TextBox txtbuscar;
    }
}