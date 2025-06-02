namespace GUI
{
    partial class ViewTransactionsForm
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
            dgvTransacciones = new DataGridView();
            btnActualizar = new Button();
            pnlDetallesBoleto = new Panel();
            txtDetalleBoleto = new TextBox();
            pbQRCode = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dgvTransacciones).BeginInit();
            pnlDetallesBoleto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbQRCode).BeginInit();
            SuspendLayout();
            // 
            // dgvTransacciones
            // 
            dgvTransacciones.AllowUserToAddRows = false;
            dgvTransacciones.AllowUserToDeleteRows = false;
            dgvTransacciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTransacciones.Dock = DockStyle.Top;
            dgvTransacciones.Location = new Point(0, 0);
            dgvTransacciones.Name = "dgvTransacciones";
            dgvTransacciones.ReadOnly = true;
            dgvTransacciones.RowHeadersWidth = 51;
            dgvTransacciones.Size = new Size(1302, 218);
            dgvTransacciones.TabIndex = 0;
            dgvTransacciones.SelectionChanged += dgvTransacciones_SelectionChanged;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(51, 317);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(144, 29);
            btnActualizar.TabIndex = 1;
            btnActualizar.Text = "Actualizar Lista";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // pnlDetallesBoleto
            // 
            pnlDetallesBoleto.BorderStyle = BorderStyle.FixedSingle;
            pnlDetallesBoleto.Controls.Add(txtDetalleBoleto);
            pnlDetallesBoleto.Controls.Add(pbQRCode);
            pnlDetallesBoleto.Location = new Point(712, 249);
            pnlDetallesBoleto.Name = "pnlDetallesBoleto";
            pnlDetallesBoleto.Size = new Size(350, 344);
            pnlDetallesBoleto.TabIndex = 2;
            // 
            // txtDetalleBoleto
            // 
            txtDetalleBoleto.Location = new Point(32, 230);
            txtDetalleBoleto.Multiline = true;
            txtDetalleBoleto.Name = "txtDetalleBoleto";
            txtDetalleBoleto.ReadOnly = true;
            txtDetalleBoleto.ScrollBars = ScrollBars.Vertical;
            txtDetalleBoleto.Size = new Size(291, 95);
            txtDetalleBoleto.TabIndex = 1;
            // 
            // pbQRCode
            // 
            pbQRCode.BorderStyle = BorderStyle.FixedSingle;
            pbQRCode.Location = new Point(79, 14);
            pbQRCode.Name = "pbQRCode";
            pbQRCode.Size = new Size(200, 200);
            pbQRCode.SizeMode = PictureBoxSizeMode.Zoom;
            pbQRCode.TabIndex = 0;
            pbQRCode.TabStop = false;
            // 
            // ViewTransactionsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1302, 708);
            Controls.Add(pnlDetallesBoleto);
            Controls.Add(btnActualizar);
            Controls.Add(dgvTransacciones);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ViewTransactionsForm";
            Text = "Ver Transacciones y Boletos";
            ((System.ComponentModel.ISupportInitialize)dgvTransacciones).EndInit();
            pnlDetallesBoleto.ResumeLayout(false);
            pnlDetallesBoleto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbQRCode).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvTransacciones;
        private Button btnActualizar;
        private Panel pnlDetallesBoleto;
        private TextBox txtDetalleBoleto;
        private PictureBox pbQRCode;
    }
}