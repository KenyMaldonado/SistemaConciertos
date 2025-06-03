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
            txtCorreo = new TextBox();
            txtTelefono = new TextBox();
            txtDireccion = new TextBox();
            txtApellidoComprador = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
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
            label5 = new Label();
            label6 = new Label();
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
            grpComprador.Font = new Font("Arial Rounded MT Bold", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            grpComprador.Location = new Point(76, 77);
            grpComprador.Margin = new Padding(3, 2, 3, 2);
            grpComprador.Name = "grpComprador";
            grpComprador.Padding = new Padding(3, 2, 3, 2);
            grpComprador.Size = new Size(556, 215);
            grpComprador.TabIndex = 0;
            grpComprador.TabStop = false;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(161, 185);
            txtCorreo.Margin = new Padding(3, 2, 3, 2);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(369, 25);
            txtCorreo.TabIndex = 12;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(161, 145);
            txtTelefono.Margin = new Padding(3, 2, 3, 2);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(369, 25);
            txtTelefono.TabIndex = 11;
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(161, 104);
            txtDireccion.Margin = new Padding(3, 2, 3, 2);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(369, 25);
            txtDireccion.TabIndex = 10;
            // 
            // txtApellidoComprador
            // 
            txtApellidoComprador.Location = new Point(161, 68);
            txtApellidoComprador.Margin = new Padding(3, 2, 3, 2);
            txtApellidoComprador.Name = "txtApellidoComprador";
            txtApellidoComprador.Size = new Size(369, 25);
            txtApellidoComprador.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 188);
            label4.Name = "label4";
            label4.Size = new Size(153, 17);
            label4.TabIndex = 8;
            label4.Text = "Correo Electrónico:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(83, 148);
            label3.Name = "label3";
            label3.Size = new Size(76, 17);
            label3.TabIndex = 7;
            label3.Text = "Teléfono:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(75, 107);
            label2.Name = "label2";
            label2.Size = new Size(84, 17);
            label2.TabIndex = 6;
            label2.Text = "Dirección:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(87, 71);
            label1.Name = "label1";
            label1.Size = new Size(72, 17);
            label1.TabIndex = 5;
            label1.Text = "Apellido:";
            // 
            // txtNombreComprador
            // 
            txtNombreComprador.Location = new Point(161, 30);
            txtNombreComprador.Margin = new Padding(3, 2, 3, 2);
            txtNombreComprador.Name = "txtNombreComprador";
            txtNombreComprador.Size = new Size(369, 25);
            txtNombreComprador.TabIndex = 4;
            // 
            // lblNombreComprador
            // 
            lblNombreComprador.AutoSize = true;
            lblNombreComprador.Location = new Point(88, 35);
            lblNombreComprador.Name = "lblNombreComprador";
            lblNombreComprador.Size = new Size(71, 17);
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
            grpSeleccionBoletos.Font = new Font("Arial Rounded MT Bold", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            grpSeleccionBoletos.Location = new Point(710, 88);
            grpSeleccionBoletos.Margin = new Padding(3, 2, 3, 2);
            grpSeleccionBoletos.Name = "grpSeleccionBoletos";
            grpSeleccionBoletos.Padding = new Padding(3, 2, 3, 2);
            grpSeleccionBoletos.Size = new Size(439, 159);
            grpSeleccionBoletos.TabIndex = 1;
            grpSeleccionBoletos.TabStop = false;
            // 
            // lblAsientoAsignado
            // 
            lblAsientoAsignado.AutoSize = true;
            lblAsientoAsignado.Location = new Point(273, 103);
            lblAsientoAsignado.Name = "lblAsientoAsignado";
            lblAsientoAsignado.Size = new Size(34, 17);
            lblAsientoAsignado.TabIndex = 5;
            lblAsientoAsignado.Text = "N/A";
            // 
            // lblEtiquetaAsiento
            // 
            lblEtiquetaAsiento.AutoSize = true;
            lblEtiquetaAsiento.Location = new Point(113, 103);
            lblEtiquetaAsiento.Name = "lblEtiquetaAsiento";
            lblEtiquetaAsiento.Size = new Size(140, 17);
            lblEtiquetaAsiento.TabIndex = 4;
            lblEtiquetaAsiento.Text = "Asiento Asignado:";
            // 
            // lblDisponibilidad
            // 
            lblDisponibilidad.AutoSize = true;
            lblDisponibilidad.Location = new Point(273, 68);
            lblDisponibilidad.Name = "lblDisponibilidad";
            lblDisponibilidad.Size = new Size(74, 17);
            lblDisponibilidad.TabIndex = 3;
            lblDisponibilidad.Text = "0 boletos";
            // 
            // lblEtiquetaDisponibilidad
            // 
            lblEtiquetaDisponibilidad.AutoSize = true;
            lblEtiquetaDisponibilidad.Location = new Point(19, 68);
            lblEtiquetaDisponibilidad.Name = "lblEtiquetaDisponibilidad";
            lblEtiquetaDisponibilidad.Size = new Size(234, 17);
            lblEtiquetaDisponibilidad.TabIndex = 2;
            lblEtiquetaDisponibilidad.Text = "Boletos Disponibles en la zona:";
            // 
            // cmbZonas
            // 
            cmbZonas.FormattingEnabled = true;
            cmbZonas.Location = new Point(273, 27);
            cmbZonas.Margin = new Padding(3, 2, 3, 2);
            cmbZonas.Name = "cmbZonas";
            cmbZonas.Size = new Size(133, 25);
            cmbZonas.TabIndex = 1;
            cmbZonas.SelectedIndexChanged += cmbZonas_SelectedIndexChanged;
            // 
            // lblSeleccionarZona
            // 
            lblSeleccionarZona.AutoSize = true;
            lblSeleccionarZona.Location = new Point(111, 34);
            lblSeleccionarZona.Name = "lblSeleccionarZona";
            lblSeleccionarZona.Size = new Size(142, 17);
            lblSeleccionarZona.TabIndex = 0;
            lblSeleccionarZona.Text = "Seleccionar Zona:";
            // 
            // btnComprarBoleto
            // 
            btnComprarBoleto.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnComprarBoleto.Location = new Point(850, 262);
            btnComprarBoleto.Margin = new Padding(3, 2, 3, 2);
            btnComprarBoleto.Name = "btnComprarBoleto";
            btnComprarBoleto.Size = new Size(167, 42);
            btnComprarBoleto.TabIndex = 2;
            btnComprarBoleto.Text = "Comprar Boleto";
            btnComprarBoleto.UseVisualStyleBackColor = true;
            btnComprarBoleto.Click += btnComprarBoleto_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(1263, 162);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(109, 46);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // pnlAsientos
            // 
            pnlAsientos.Location = new Point(76, 324);
            pnlAsientos.Margin = new Padding(3, 2, 3, 2);
            pnlAsientos.Name = "pnlAsientos";
            pnlAsientos.Size = new Size(1386, 376);
            pnlAsientos.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(237, 45);
            label5.Name = "label5";
            label5.Size = new Size(281, 30);
            label5.TabIndex = 5;
            label5.Text = "Información de Comprador";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(843, 45);
            label6.Name = "label6";
            label6.Size = new Size(214, 30);
            label6.TabIndex = 6;
            label6.Text = "Selección de Boletos";
            // 
            // BuyTicketsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(232, 240, 254);
            ClientSize = new Size(1553, 742);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(pnlAsientos);
            Controls.Add(pictureBox1);
            Controls.Add(btnComprarBoleto);
            Controls.Add(grpSeleccionBoletos);
            Controls.Add(grpComprador);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "BuyTicketsForm";
            Text = "Comprar Boletos";
            grpComprador.ResumeLayout(false);
            grpComprador.PerformLayout();
            grpSeleccionBoletos.ResumeLayout(false);
            grpSeleccionBoletos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private Label label5;
        private Label label6;
    }
}