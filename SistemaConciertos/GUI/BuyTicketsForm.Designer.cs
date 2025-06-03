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
            numCantidadBoletos = new NumericUpDown();
            label7 = new Label();
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
            ((System.ComponentModel.ISupportInitialize)numCantidadBoletos).BeginInit();
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
            grpComprador.Location = new Point(87, 103);
            grpComprador.Name = "grpComprador";
            grpComprador.Size = new Size(635, 287);
            grpComprador.TabIndex = 0;
            grpComprador.TabStop = false;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(184, 247);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(421, 29);
            txtCorreo.TabIndex = 12;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(184, 193);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(421, 29);
            txtTelefono.TabIndex = 11;
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(184, 139);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(421, 29);
            txtDireccion.TabIndex = 10;
            // 
            // txtApellidoComprador
            // 
            txtApellidoComprador.Location = new Point(184, 91);
            txtApellidoComprador.Name = "txtApellidoComprador";
            txtApellidoComprador.Size = new Size(421, 29);
            txtApellidoComprador.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 251);
            label4.Name = "label4";
            label4.Size = new Size(188, 22);
            label4.TabIndex = 8;
            label4.Text = "Correo Electrónico:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(95, 197);
            label3.Name = "label3";
            label3.Size = new Size(94, 22);
            label3.TabIndex = 7;
            label3.Text = "Teléfono:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(86, 143);
            label2.Name = "label2";
            label2.Size = new Size(103, 22);
            label2.TabIndex = 6;
            label2.Text = "Dirección:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(99, 95);
            label1.Name = "label1";
            label1.Size = new Size(91, 22);
            label1.TabIndex = 5;
            label1.Text = "Apellido:";
            // 
            // txtNombreComprador
            // 
            txtNombreComprador.Location = new Point(184, 40);
            txtNombreComprador.Name = "txtNombreComprador";
            txtNombreComprador.Size = new Size(421, 29);
            txtNombreComprador.TabIndex = 4;
            // 
            // lblNombreComprador
            // 
            lblNombreComprador.AutoSize = true;
            lblNombreComprador.Location = new Point(101, 47);
            lblNombreComprador.Name = "lblNombreComprador";
            lblNombreComprador.Size = new Size(89, 22);
            lblNombreComprador.TabIndex = 0;
            lblNombreComprador.Text = "Nombre:";
            // 
            // grpSeleccionBoletos
            // 
            grpSeleccionBoletos.Controls.Add(numCantidadBoletos);
            grpSeleccionBoletos.Controls.Add(label7);
            grpSeleccionBoletos.Controls.Add(lblAsientoAsignado);
            grpSeleccionBoletos.Controls.Add(lblEtiquetaAsiento);
            grpSeleccionBoletos.Controls.Add(lblDisponibilidad);
            grpSeleccionBoletos.Controls.Add(lblEtiquetaDisponibilidad);
            grpSeleccionBoletos.Controls.Add(cmbZonas);
            grpSeleccionBoletos.Controls.Add(lblSeleccionarZona);
            grpSeleccionBoletos.Font = new Font("Arial Rounded MT Bold", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            grpSeleccionBoletos.Location = new Point(811, 117);
            grpSeleccionBoletos.Name = "grpSeleccionBoletos";
            grpSeleccionBoletos.Size = new Size(502, 226);
            grpSeleccionBoletos.TabIndex = 1;
            grpSeleccionBoletos.TabStop = false;
            // 
            // numCantidadBoletos
            // 
            numCantidadBoletos.Location = new Point(294, 59);
            numCantidadBoletos.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numCantidadBoletos.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCantidadBoletos.Name = "numCantidadBoletos";
            numCantidadBoletos.Size = new Size(150, 29);
            numCantidadBoletos.TabIndex = 7;
            numCantidadBoletos.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(87, 66);
            label7.Name = "label7";
            label7.Size = new Size(201, 22);
            label7.TabIndex = 6;
            label7.Text = "Cantidad de Boletos:";
            // 
            // lblAsientoAsignado
            // 
            lblAsientoAsignado.AutoSize = true;
            lblAsientoAsignado.Location = new Point(307, 186);
            lblAsientoAsignado.Name = "lblAsientoAsignado";
            lblAsientoAsignado.Size = new Size(43, 22);
            lblAsientoAsignado.TabIndex = 5;
            lblAsientoAsignado.Text = "N/A";
            // 
            // lblEtiquetaAsiento
            // 
            lblEtiquetaAsiento.AutoSize = true;
            lblEtiquetaAsiento.Location = new Point(125, 186);
            lblEtiquetaAsiento.Name = "lblEtiquetaAsiento";
            lblEtiquetaAsiento.Size = new Size(176, 22);
            lblEtiquetaAsiento.TabIndex = 4;
            lblEtiquetaAsiento.Text = "Asiento Asignado:";
            // 
            // lblDisponibilidad
            // 
            lblDisponibilidad.AutoSize = true;
            lblDisponibilidad.Location = new Point(312, 152);
            lblDisponibilidad.Name = "lblDisponibilidad";
            lblDisponibilidad.Size = new Size(93, 22);
            lblDisponibilidad.TabIndex = 3;
            lblDisponibilidad.Text = "0 boletos";
            // 
            // lblEtiquetaDisponibilidad
            // 
            lblEtiquetaDisponibilidad.AutoSize = true;
            lblEtiquetaDisponibilidad.Location = new Point(9, 152);
            lblEtiquetaDisponibilidad.Name = "lblEtiquetaDisponibilidad";
            lblEtiquetaDisponibilidad.Size = new Size(292, 22);
            lblEtiquetaDisponibilidad.TabIndex = 2;
            lblEtiquetaDisponibilidad.Text = "Boletos Disponibles en la zona:";
            // 
            // cmbZonas
            // 
            cmbZonas.FormattingEnabled = true;
            cmbZonas.Location = new Point(307, 112);
            cmbZonas.Name = "cmbZonas";
            cmbZonas.Size = new Size(151, 30);
            cmbZonas.TabIndex = 1;
            cmbZonas.SelectedIndexChanged += cmbZonas_SelectedIndexChanged;
            // 
            // lblSeleccionarZona
            // 
            lblSeleccionarZona.AutoSize = true;
            lblSeleccionarZona.Location = new Point(125, 115);
            lblSeleccionarZona.Name = "lblSeleccionarZona";
            lblSeleccionarZona.Size = new Size(174, 22);
            lblSeleccionarZona.TabIndex = 0;
            lblSeleccionarZona.Text = "Seleccionar Zona:";
            // 
            // btnComprarBoleto
            // 
            btnComprarBoleto.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnComprarBoleto.Location = new Point(971, 349);
            btnComprarBoleto.Name = "btnComprarBoleto";
            btnComprarBoleto.Size = new Size(191, 56);
            btnComprarBoleto.TabIndex = 2;
            btnComprarBoleto.Text = "Comprar Boleto";
            btnComprarBoleto.UseVisualStyleBackColor = true;
            btnComprarBoleto.Click += btnComprarBoleto_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(1443, 216);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(125, 61);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // pnlAsientos
            // 
            pnlAsientos.Location = new Point(87, 432);
            pnlAsientos.Name = "pnlAsientos";
            pnlAsientos.Size = new Size(1584, 501);
            pnlAsientos.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(271, 60);
            label5.Name = "label5";
            label5.Size = new Size(367, 37);
            label5.TabIndex = 5;
            label5.Text = "Información de Comprador";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(963, 60);
            label6.Name = "label6";
            label6.Size = new Size(280, 37);
            label6.TabIndex = 6;
            label6.Text = "Selección de Boletos";
            // 
            // BuyTicketsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(232, 240, 254);
            ClientSize = new Size(1775, 989);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(pnlAsientos);
            Controls.Add(pictureBox1);
            Controls.Add(btnComprarBoleto);
            Controls.Add(grpSeleccionBoletos);
            Controls.Add(grpComprador);
            FormBorderStyle = FormBorderStyle.None;
            Name = "BuyTicketsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Comprar Boletos";
            grpComprador.ResumeLayout(false);
            grpComprador.PerformLayout();
            grpSeleccionBoletos.ResumeLayout(false);
            grpSeleccionBoletos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCantidadBoletos).EndInit();
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
        private NumericUpDown numCantidadBoletos;
        private Label label7;
    }
}