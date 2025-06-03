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
            label1 = new Label();
            SuspendLayout();
            // 
            // lblNormalQueueCount
            // 
            lblNormalQueueCount.AutoSize = true;
            lblNormalQueueCount.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblNormalQueueCount.Location = new Point(71, 44);
            lblNormalQueueCount.Name = "lblNormalQueueCount";
            lblNormalQueueCount.Size = new Size(119, 21);
            lblNormalQueueCount.TabIndex = 0;
            lblNormalQueueCount.Text = "Cola Normal: 0";
            // 
            // lblVIPQueueCount
            // 
            lblVIPQueueCount.AutoSize = true;
            lblVIPQueueCount.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblVIPQueueCount.Location = new Point(101, 83);
            lblVIPQueueCount.Name = "lblVIPQueueCount";
            lblVIPQueueCount.Size = new Size(89, 21);
            lblVIPQueueCount.TabIndex = 1;
            lblVIPQueueCount.Text = "Cola VIP: 0";
            // 
            // btnProcessOne
            // 
            btnProcessOne.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnProcessOne.Location = new Point(329, 29);
            btnProcessOne.Margin = new Padding(3, 2, 3, 2);
            btnProcessOne.Name = "btnProcessOne";
            btnProcessOne.Size = new Size(218, 36);
            btnProcessOne.TabIndex = 2;
            btnProcessOne.Text = "Procesar Siguiente";
            btnProcessOne.UseVisualStyleBackColor = true;
            btnProcessOne.Click += btnProcessOne_Click_1;
            // 
            // btnProcessAll
            // 
            btnProcessAll.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnProcessAll.Location = new Point(329, 95);
            btnProcessAll.Margin = new Padding(3, 2, 3, 2);
            btnProcessAll.Name = "btnProcessAll";
            btnProcessAll.Size = new Size(218, 36);
            btnProcessAll.TabIndex = 3;
            btnProcessAll.Text = "Procesar Todas";
            btnProcessAll.UseVisualStyleBackColor = true;
            btnProcessAll.Click += btnProcessAll_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(434, 67);
            label1.Name = "label1";
            label1.Size = new Size(20, 21);
            label1.TabIndex = 4;
            label1.Text = "o";
            // 
            // ConcurrencySimForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 240, 254);
            ClientSize = new Size(619, 173);
            Controls.Add(label1);
            Controls.Add(btnProcessAll);
            Controls.Add(btnProcessOne);
            Controls.Add(lblVIPQueueCount);
            Controls.Add(lblNormalQueueCount);
            Margin = new Padding(3, 2, 3, 2);
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
        private Label label1;
    }
}