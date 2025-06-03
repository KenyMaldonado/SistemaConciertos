namespace GUI
{
    partial class BuyTicketsForm
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
            grpComprador = new GroupBox();
            txtNombreComprador = new TextBox();
            lblNombreComprador = new Label();
            grpSeleccionBoletos = new GroupBox();
            lblAsientoAsignado = new Label();
            lblEtiquetaAsiento = new Label();
            lblDisponibilidad = new Label();
            lblEtiquetaDisponibilidad = new Label();
            cmbZonas = new ComboBox();
            lblSeleccionarZona = new Label();
            btnComprarBoleto = new Button();
            pictureBox1 = new PictureBox();
            pnlAsientos = new Panel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtApellidoComprador = new TextBox();
            txtDireccion = new TextBox();
            txtTelefono = new TextBox();
            txtCorreo = new TextBox();
            grpComprador.SuspendLayout();
            grpSeleccionBoletos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // grpComprador
            // 
            grpComprador.Controls.Add(txtCorreo);
            grpComprador.Controls.Add(txtTelefono);
            grpComprador.Controls.Add(txtDireccion);
            grpComprador.Controls.Add(txtApellidoComprador);
            grpComprador.Controls.Add(label4);
            grpComprador.Controls.Add(label3);
            grpComprador.Controls.Add(label2);
            grpComprador.Controls.Add(label1);
            grpComprador.Controls.Add(txtNombreComprador);
            grpComprador.Controls.Add(lblNombreComprador);
            grpComprador.Location = new Point(115, 30);
            grpComprador.Name = "grpComprador";
            grpComprador.Size = new Size(636, 216);
            grpComprador.TabIndex = 0;
            grpComprador.TabStop = false;
            grpComprador.Text = "Información del Comprador";
            // 
            // txtNombreComprador
            // 
            txtNombreComprador.Location = new Point(184, 25);
            txtNombreComprador.Name = "txtNombreComprador";
            txtNombreComprador.Size = new Size(421, 27);
            txtNombreComprador.TabIndex = 4;
            // 
            // lblNombreComprador
            // 
            lblNombreComprador.AutoSize = true;
            lblNombreComprador.Location = new Point(6, 25);
            lblNombreComprador.Name = "lblNombreComprador";
            lblNombreComprador.Size = new Size(67, 20);
            lblNombreComprador.TabIndex = 0;
            lblNombreComprador.Text = "Nombre:";
            // 
            // grpSeleccionBoletos
            // 
            grpSeleccionBoletos.Controls.Add(lblAsientoAsignado);
            grpSeleccionBoletos.Controls.Add(lblEtiquetaAsiento);
            grpSeleccionBoletos.Controls.Add(lblDisponibilidad);
            grpSeleccionBoletos.Controls.Add(lblEtiquetaDisponibilidad);
            grpSeleccionBoletos.Controls.Add(cmbZonas);
            grpSeleccionBoletos.Controls.Add(lblSeleccionarZona);
            grpSeleccionBoletos.Location = new Point(109, 252);
            grpSeleccionBoletos.Name = "grpSeleccionBoletos";
            grpSeleccionBoletos.Size = new Size(410, 125);
            grpSeleccionBoletos.TabIndex = 1;
            grpSeleccionBoletos.TabStop = false;
            grpSeleccionBoletos.Text = "Selección de Boletos";
            // 
            // lblAsientoAsignado
            // 
            lblAsientoAsignado.AutoSize = true;
            lblAsientoAsignado.Location = new Point(141, 78);
            lblAsientoAsignado.Name = "lblAsientoAsignado";
            lblAsientoAsignado.Size = new Size(36, 20);
            lblAsientoAsignado.TabIndex = 5;
            lblAsientoAsignado.Text = "N/A";
            // 
            // lblEtiquetaAsiento
            // 
            lblEtiquetaAsiento.AutoSize = true;
            lblEtiquetaAsiento.Location = new Point(6, 78);
            lblEtiquetaAsiento.Name = "lblEtiquetaAsiento";
            lblEtiquetaAsiento.Size = new Size(129, 20);
            lblEtiquetaAsiento.TabIndex = 4;
            lblEtiquetaAsiento.Text = "Asiento Asignado:";
            // 
            // lblDisponibilidad
            // 
            lblDisponibilidad.AutoSize = true;
            lblDisponibilidad.Location = new Point(228, 53);
            lblDisponibilidad.Name = "lblDisponibilidad";
            lblDisponibilidad.Size = new Size(71, 20);
            lblDisponibilidad.TabIndex = 3;
            lblDisponibilidad.Text = "0 boletos";
            // 
            // lblEtiquetaDisponibilidad
            // 
            lblEtiquetaDisponibilidad.AutoSize = true;
            lblEtiquetaDisponibilidad.Location = new Point(6, 53);
            lblEtiquetaDisponibilidad.Name = "lblEtiquetaDisponibilidad";
            lblEtiquetaDisponibilidad.Size = new Size(216, 20);
            lblEtiquetaDisponibilidad.TabIndex = 2;
            lblEtiquetaDisponibilidad.Text = "Boletos Disponibles en la zona:";
            // 
            // cmbZonas
            // 
            cmbZonas.FormattingEnabled = true;
            cmbZonas.Location = new Point(138, 20);
            cmbZonas.Name = "cmbZonas";
            cmbZonas.Size = new Size(151, 28);
            cmbZonas.TabIndex = 1;
            cmbZonas.SelectedIndexChanged += cmbZonas_SelectedIndexChanged;
            // 
            // lblSeleccionarZona
            // 
            lblSeleccionarZona.AutoSize = true;
            lblSeleccionarZona.Location = new Point(6, 23);
            lblSeleccionarZona.Name = "lblSeleccionarZona";
            lblSeleccionarZona.Size = new Size(126, 20);
            lblSeleccionarZona.TabIndex = 0;
            lblSeleccionarZona.Text = "Seleccionar Zona:";
            // 
            // btnComprarBoleto
            // 
            btnComprarBoleto.Location = new Point(214, 397);
            btnComprarBoleto.Name = "btnComprarBoleto";
            btnComprarBoleto.Size = new Size(94, 29);
            btnComprarBoleto.TabIndex = 2;
            btnComprarBoleto.Text = "Comprar Boleto";
            btnComprarBoleto.UseVisualStyleBackColor = true;
            btnComprarBoleto.Click += btnComprarBoleto_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(525, 315);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(125, 62);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // pnlAsientos
            // 
            pnlAsientos.Location = new Point(87, 432);
            pnlAsientos.Name = "pnlAsientos";
            pnlAsientos.Size = new Size(1584, 502);
            pnlAsientos.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 62);
            label1.Name = "label1";
            label1.Size = new Size(69, 20);
            label1.TabIndex = 5;
            label1.Text = "Apellido:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 99);
            label2.Name = "label2";
            label2.Size = new Size(75, 20);
            label2.TabIndex = 6;
            label2.Text = "Dirección:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 135);
            label3.Name = "label3";
            label3.Size = new Size(70, 20);
            label3.TabIndex = 7;
            label3.Text = "Teléfono:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 171);
            label4.Name = "label4";
            label4.Size = new Size(135, 20);
            label4.TabIndex = 8;
            label4.Text = "Correo Electrónico:";
            // 
            // txtApellidoComprador
            // 
            txtApellidoComprador.Location = new Point(184, 62);
            txtApellidoComprador.Name = "txtApellidoComprador";
            txtApellidoComprador.Size = new Size(421, 27);
            txtApellidoComprador.TabIndex = 9;
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(184, 99);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(421, 27);
            txtDireccion.TabIndex = 10;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(184, 135);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(421, 27);
            txtTelefono.TabIndex = 11;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(184, 171);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(421, 27);
            txtCorreo.TabIndex = 12;
            // 
            // BuyTicketsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1775, 990);
            Controls.Add(pnlAsientos);
            Controls.Add(pictureBox1);
            Controls.Add(btnComprarBoleto);
            Controls.Add(grpSeleccionBoletos);
            Controls.Add(grpComprador);
            FormBorderStyle = FormBorderStyle.None;
            Name = "BuyTicketsForm";
            Text = "Comprar Boletos";
            grpComprador.ResumeLayout(false);
            grpComprador.PerformLayout();
            grpSeleccionBoletos.ResumeLayout(false);
            grpSeleccionBoletos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpComprador;
        private GroupBox grpSeleccionBoletos;
        private Button btnComprarBoleto;
        private PictureBox pictureBox1;
        private TextBox txtNombreComprador;
        private Label lblNombreComprador;
        private ComboBox cmbZonas;
        private Label lblSeleccionarZona;
        private Label lblEtiquetaAsiento;
        private Label lblDisponibilidad;
        private Label lblEtiquetaDisponibilidad;
        private Label lblAsientoAsignado;
        private Panel pnlAsientos;
        private Label label2;
        private Label label1;
        private Label label3;
        private TextBox txtDireccion;
        private TextBox txtApellidoComprador;
        private Label label4;
        private TextBox txtCorreo;
        private TextBox txtTelefono;
    }
}