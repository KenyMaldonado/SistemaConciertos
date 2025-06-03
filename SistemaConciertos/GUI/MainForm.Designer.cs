namespace GUI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            menuBoletos = new ToolStripMenuItem();
            menuItemComprarBoleto = new ToolStripMenuItem();
            menuItemVerTransacciones = new ToolStripMenuItem();
            menuItemSimulacion = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            menuItemSalir = new ToolStripMenuItem();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            picLogo = new PictureBox();
            pnlContenedor = new Panel();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlContenedor.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuBoletos });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(1091, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuBoletos
            // 
            menuBoletos.DropDownItems.AddRange(new ToolStripItem[] { menuItemComprarBoleto, menuItemVerTransacciones, menuItemSimulacion, toolStripSeparator1, menuItemSalir });
            menuBoletos.Name = "menuBoletos";
            menuBoletos.Size = new Size(58, 20);
            menuBoletos.Text = "&Boletos";
            // 
            // menuItemComprarBoleto
            // 
            menuItemComprarBoleto.Name = "menuItemComprarBoleto";
            menuItemComprarBoleto.Size = new Size(167, 22);
            menuItemComprarBoleto.Text = "&Comprar Boleto";
            menuItemComprarBoleto.Click += menuItemComprarBoleto_Click;
            // 
            // menuItemVerTransacciones
            // 
            menuItemVerTransacciones.Name = "menuItemVerTransacciones";
            menuItemVerTransacciones.Size = new Size(167, 22);
            menuItemVerTransacciones.Text = "&Ver Transacciones";
            menuItemVerTransacciones.Click += menuItemVerTransacciones_Click;
            // 
            // menuItemSimulacion
            // 
            menuItemSimulacion.Name = "menuItemSimulacion";
            menuItemSimulacion.Size = new Size(167, 22);
            menuItemSimulacion.Text = "&Simulación";
            menuItemSimulacion.Click += menuItemSimulacion_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(164, 6);
            // 
            // menuItemSalir
            // 
            menuItemSalir.Name = "menuItemSalir";
            menuItemSalir.Size = new Size(167, 22);
            menuItemSalir.Text = "&Salir";
            menuItemSalir.Click += menuItemSalir_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(206, 214);
            label1.Name = "label1";
            label1.Size = new Size(642, 28);
            label1.TabIndex = 0;
            label1.Text = "¡Bienvenido a TicketExpress! - Tu entrada a la música\"";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Arial Rounded MT Bold", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(137, 267);
            label2.Name = "label2";
            label2.Size = new Size(807, 22);
            label2.TabIndex = 1;
            label2.Text = "Compra tus entradas para tus conciertos y eventos favoritos de manera rápida y segura";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(266, 320);
            label3.Name = "label3";
            label3.Size = new Size(574, 18);
            label3.TabIndex = 2;
            label3.Text = "Explora nuestra selección de espectáculos y reserva tus boletos online";
            // 
            // picLogo
            // 
            picLogo.Image = Properties.Resources.LogoTicket1;
            picLogo.Location = new Point(292, 74);
            picLogo.Margin = new Padding(3, 2, 3, 2);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(514, 103);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 3;
            picLogo.TabStop = false;
            // 
            // pnlContenedor
            // 
            pnlContenedor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlContenedor.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlContenedor.Controls.Add(picLogo);
            pnlContenedor.Controls.Add(label3);
            pnlContenedor.Controls.Add(label2);
            pnlContenedor.Controls.Add(label1);
            pnlContenedor.Location = new Point(0, 23);
            pnlContenedor.Margin = new Padding(3, 2, 3, 2);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Size = new Size(1091, 465);
            pnlContenedor.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            BackColor = Color.FromArgb(232, 240, 254);
            ClientSize = new Size(1091, 492);
            Controls.Add(menuStrip1);
            Controls.Add(pnlContenedor);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainForm";
            Text = "TicketExpress";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlContenedor.ResumeLayout(false);
            pnlContenedor.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuBoletos;
        private ToolStripMenuItem menuItemComprarBoleto;
        private ToolStripMenuItem menuItemVerTransacciones;
        private ToolStripMenuItem menuItemSimulacion;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem menuItemSalir;
        private Label label1;
        private Label label2;
        private Label label3;
        private PictureBox picLogo;
        private Panel pnlContenedor;
    }
}
