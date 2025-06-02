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
            pnlContenedor = new Panel();
            menuStrip1 = new MenuStrip();
            menuBoletos = new ToolStripMenuItem();
            menuItemComprarBoleto = new ToolStripMenuItem();
            menuItemVerTransacciones = new ToolStripMenuItem();
            menuItemSimulacion = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            menuItemSalir = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlContenedor
            // 
            pnlContenedor.Location = new Point(0, 31);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Size = new Size(1764, 924);
            pnlContenedor.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuBoletos });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1764, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuBoletos
            // 
            menuBoletos.DropDownItems.AddRange(new ToolStripItem[] { menuItemComprarBoleto, menuItemVerTransacciones, menuItemSimulacion, toolStripSeparator1, menuItemSalir });
            menuBoletos.Name = "menuBoletos";
            menuBoletos.Size = new Size(73, 24);
            menuBoletos.Text = "&Boletos";
            // 
            // menuItemComprarBoleto
            // 
            menuItemComprarBoleto.Name = "menuItemComprarBoleto";
            menuItemComprarBoleto.Size = new Size(208, 26);
            menuItemComprarBoleto.Text = "&Comprar Boleto";
            menuItemComprarBoleto.Click += menuItemComprarBoleto_Click;
            // 
            // menuItemVerTransacciones
            // 
            menuItemVerTransacciones.Name = "menuItemVerTransacciones";
            menuItemVerTransacciones.Size = new Size(208, 26);
            menuItemVerTransacciones.Text = "&Ver Transacciones";
            menuItemVerTransacciones.Click += menuItemVerTransacciones_Click;
            // 
            // menuItemSimulacion
            // 
            menuItemSimulacion.Name = "menuItemSimulacion";
            menuItemSimulacion.Size = new Size(208, 26);
            menuItemSimulacion.Text = "&Simulación";
            menuItemSimulacion.Click += menuItemSimulacion_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(205, 6);
            // 
            // menuItemSalir
            // 
            menuItemSalir.Name = "menuItemSalir";
            menuItemSalir.Size = new Size(208, 26);
            menuItemSalir.Text = "&Salir";
            menuItemSalir.Click += menuItemSalir_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1764, 960);
            Controls.Add(menuStrip1);
            Controls.Add(pnlContenedor);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlContenedor;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuBoletos;
        private ToolStripMenuItem menuItemComprarBoleto;
        private ToolStripMenuItem menuItemVerTransacciones;
        private ToolStripMenuItem menuItemSimulacion;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem menuItemSalir;
    }
}
