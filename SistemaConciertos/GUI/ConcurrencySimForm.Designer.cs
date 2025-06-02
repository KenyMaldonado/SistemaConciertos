namespace GUI
{
    partial class ConcurrencySimForm
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
            lblNormalQueueCount = new Label();
            lblVIPQueueCount = new Label();
            btnProcessOne = new Button();
            btnProcessAll = new Button();
            SuspendLayout();
            // 
            // lblNormalQueueCount
            // 
            lblNormalQueueCount.AutoSize = true;
            lblNormalQueueCount.Location = new Point(81, 58);
            lblNormalQueueCount.Name = "lblNormalQueueCount";
            lblNormalQueueCount.Size = new Size(108, 20);
            lblNormalQueueCount.TabIndex = 0;
            lblNormalQueueCount.Text = "Cola Normal: 0";
            // 
            // lblVIPQueueCount
            // 
            lblVIPQueueCount.AutoSize = true;
            lblVIPQueueCount.Location = new Point(81, 112);
            lblVIPQueueCount.Name = "lblVIPQueueCount";
            lblVIPQueueCount.Size = new Size(79, 20);
            lblVIPQueueCount.TabIndex = 1;
            lblVIPQueueCount.Text = "Cola VIP: 0";
            // 
            // btnProcessOne
            // 
            btnProcessOne.Location = new Point(407, 69);
            btnProcessOne.Name = "btnProcessOne";
            btnProcessOne.Size = new Size(165, 29);
            btnProcessOne.TabIndex = 2;
            btnProcessOne.Text = "Procesar Siguiente";
            btnProcessOne.UseVisualStyleBackColor = true;
            btnProcessOne.Click += btnProcessOne_Click_1;
            // 
            // btnProcessAll
            // 
            btnProcessAll.Location = new Point(407, 112);
            btnProcessAll.Name = "btnProcessAll";
            btnProcessAll.Size = new Size(165, 29);
            btnProcessAll.TabIndex = 3;
            btnProcessAll.Text = "Procesar Todas";
            btnProcessAll.UseVisualStyleBackColor = true;
            btnProcessAll.Click += btnProcessAll_Click_1;
            // 
            // ConcurrencySimForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnProcessAll);
            Controls.Add(btnProcessOne);
            Controls.Add(lblVIPQueueCount);
            Controls.Add(lblNormalQueueCount);
            Name = "ConcurrencySimForm";
            Text = "ConcurrencySimForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNormalQueueCount;
        private Label lblVIPQueueCount;
        private Button btnProcessOne;
        private Button btnProcessAll;
    }
}