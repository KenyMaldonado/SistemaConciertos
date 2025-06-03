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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            dgvTransacciones = new DataGridView();
            btnActualizar = new Button();
            pnlDetallesBoleto = new Panel();
            txtDetalleBoleto = new TextBox();
            pbQRCode = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvTransacciones).BeginInit();
            pnlDetallesBoleto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbQRCode).BeginInit();
            SuspendLayout();
            // 
            // dgvTransacciones
            // 
            dgvTransacciones.AllowUserToAddRows = false;
            dgvTransacciones.AllowUserToDeleteRows = false;
            dgvTransacciones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvTransacciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTransacciones.BackgroundColor = Color.FromArgb(232, 240, 254);
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.Padding = new Padding(5, 0, 5, 0);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(50, 130, 184);
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvTransacciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvTransacciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.Padding = new Padding(5, 0, 5, 0);
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dgvTransacciones.DefaultCellStyle = dataGridViewCellStyle5;
            dgvTransacciones.GridColor = SystemColors.Window;
            dgvTransacciones.Location = new Point(1, 71);
            dgvTransacciones.Margin = new Padding(3, 2, 3, 2);
            dgvTransacciones.Name = "dgvTransacciones";
            dgvTransacciones.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(50, 130, 184);
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dgvTransacciones.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dgvTransacciones.RowHeadersWidth = 51;
            dgvTransacciones.Size = new Size(1139, 206);
            dgvTransacciones.TabIndex = 0;
            dgvTransacciones.SelectionChanged += dgvTransacciones_SelectionChanged;
            // 
            // btnActualizar
            // 
            btnActualizar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnActualizar.Location = new Point(762, 356);
            btnActualizar.Margin = new Padding(3, 2, 3, 2);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(195, 44);
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
            pnlDetallesBoleto.Location = new Point(181, 333);
            pnlDetallesBoleto.Margin = new Padding(3, 2, 3, 2);
            pnlDetallesBoleto.Name = "pnlDetallesBoleto";
            pnlDetallesBoleto.Size = new Size(306, 298);
            pnlDetallesBoleto.TabIndex = 2;
            // 
            // txtDetalleBoleto
            // 
            txtDetalleBoleto.Location = new Point(28, 172);
            txtDetalleBoleto.Margin = new Padding(3, 2, 3, 2);
            txtDetalleBoleto.Multiline = true;
            txtDetalleBoleto.Name = "txtDetalleBoleto";
            txtDetalleBoleto.ReadOnly = true;
            txtDetalleBoleto.ScrollBars = ScrollBars.Vertical;
            txtDetalleBoleto.Size = new Size(255, 72);
            txtDetalleBoleto.TabIndex = 1;
            // 
            // pbQRCode
            // 
            pbQRCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pbQRCode.BorderStyle = BorderStyle.FixedSingle;
            pbQRCode.Location = new Point(69, 10);
            pbQRCode.Margin = new Padding(3, 2, 3, 2);
            pbQRCode.Name = "pbQRCode";
            pbQRCode.Size = new Size(175, 150);
            pbQRCode.SizeMode = PictureBoxSizeMode.Zoom;
            pbQRCode.TabIndex = 0;
            pbQRCode.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(292, 291);
            label1.Name = "label1";
            label1.Size = new Size(108, 25);
            label1.TabIndex = 3;
            label1.Text = "Código QR";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.Location = new Point(657, 310);
            label2.Name = "label2";
            label2.Size = new Size(406, 20);
            label2.TabIndex = 4;
            label2.Text = "En caso no se actualiza la lista, presione el siguiente botón";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(497, 21);
            label3.Name = "label3";
            label3.Size = new Size(147, 30);
            label3.TabIndex = 5;
            label3.Text = "Transacciones";
            // 
            // ViewTransactionsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 240, 254);
            ClientSize = new Size(1139, 665);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pnlDetallesBoleto);
            Controls.Add(btnActualizar);
            Controls.Add(dgvTransacciones);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "ViewTransactionsForm";
            Text = "Ver Transacciones y Boletos";
            ((System.ComponentModel.ISupportInitialize)dgvTransacciones).EndInit();
            pnlDetallesBoleto.ResumeLayout(false);
            pnlDetallesBoleto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbQRCode).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvTransacciones;
        private Button btnActualizar;
        private Panel pnlDetallesBoleto;
        private TextBox txtDetalleBoleto;
        private PictureBox pbQRCode;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}