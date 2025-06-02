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
            grpComprador.SuspendLayout();
            grpSeleccionBoletos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // grpComprador
            // 
            grpComprador.Controls.Add(txtNombreComprador);
            grpComprador.Controls.Add(lblNombreComprador);
            grpComprador.Location = new Point(75, 47);
            grpComprador.Name = "grpComprador";
            grpComprador.Size = new Size(416, 125);
            grpComprador.TabIndex = 0;
            grpComprador.TabStop = false;
            grpComprador.Text = "Información del Comprador";
            // 
            // txtNombreComprador
            // 
            txtNombreComprador.Location = new Point(184, 20);
            txtNombreComprador.Name = "txtNombreComprador";
            txtNombreComprador.Size = new Size(189, 27);
            txtNombreComprador.TabIndex = 4;
            // 
            // lblNombreComprador
            // 
            lblNombreComprador.AutoSize = true;
            lblNombreComprador.Location = new Point(6, 23);
            lblNombreComprador.Name = "lblNombreComprador";
            lblNombreComprador.Size = new Size(172, 20);
            lblNombreComprador.TabIndex = 0;
            lblNombreComprador.Text = "Nombre del Comprador:";
            // 
            // grpSeleccionBoletos
            // 
            grpSeleccionBoletos.Controls.Add(lblAsientoAsignado);
            grpSeleccionBoletos.Controls.Add(lblEtiquetaAsiento);
            grpSeleccionBoletos.Controls.Add(lblDisponibilidad);
            grpSeleccionBoletos.Controls.Add(lblEtiquetaDisponibilidad);
            grpSeleccionBoletos.Controls.Add(cmbZonas);
            grpSeleccionBoletos.Controls.Add(lblSeleccionarZona);
            grpSeleccionBoletos.Location = new Point(81, 214);
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
            btnComprarBoleto.Location = new Point(545, 107);
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
    }
}