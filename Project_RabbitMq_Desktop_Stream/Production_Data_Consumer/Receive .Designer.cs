namespace Production_Data_Consumer
{
    partial class Form1
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
            listBoxMessages = new ListBox();
            lblTotalPartsProduced = new Label();
            lblTotalPartsOK = new Label();
            lblAverageProductionTime = new Label();
            lblTotalPartsWithoutDefects = new Label();
            SuspendLayout();
            // 
            // listBoxMessages
            // 
            listBoxMessages.FormattingEnabled = true;
            listBoxMessages.ItemHeight = 15;
            listBoxMessages.Location = new Point(12, 12);
            listBoxMessages.Name = "listBoxMessages";
            listBoxMessages.Size = new Size(644, 244);
            listBoxMessages.TabIndex = 0;
            // 
            // lblTotalPartsProduced
            // 
            lblTotalPartsProduced.AutoSize = true;
            lblTotalPartsProduced.Font = new Font("Segoe UI", 12F);
            lblTotalPartsProduced.Location = new Point(15, 275);
            lblTotalPartsProduced.Name = "lblTotalPartsProduced";
            lblTotalPartsProduced.Size = new Size(170, 21);
            lblTotalPartsProduced.TabIndex = 1;
            lblTotalPartsProduced.Text = "Total Parts Produced : 0";
            // 
            // lblTotalPartsOK
            // 
            lblTotalPartsOK.AutoSize = true;
            lblTotalPartsOK.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotalPartsOK.Location = new Point(292, 275);
            lblTotalPartsOK.Name = "lblTotalPartsOK";
            lblTotalPartsOK.Size = new Size(125, 21);
            lblTotalPartsOK.TabIndex = 2;
            lblTotalPartsOK.Text = "Total Parts OK : 0";
            // 
            // lblAverageProductionTime
            // 
            lblAverageProductionTime.AutoSize = true;
            lblAverageProductionTime.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAverageProductionTime.Location = new Point(292, 318);
            lblAverageProductionTime.Name = "lblAverageProductionTime";
            lblAverageProductionTime.Size = new Size(265, 21);
            lblAverageProductionTime.TabIndex = 4;
            lblAverageProductionTime.Text = "Average Production Time : 0 seconds";
            // 
            // lblTotalPartsWithoutDefects
            // 
            lblTotalPartsWithoutDefects.AutoSize = true;
            lblTotalPartsWithoutDefects.Font = new Font("Segoe UI", 12F);
            lblTotalPartsWithoutDefects.Location = new Point(15, 318);
            lblTotalPartsWithoutDefects.Name = "lblTotalPartsWithoutDefects";
            lblTotalPartsWithoutDefects.Size = new Size(215, 21);
            lblTotalPartsWithoutDefects.TabIndex = 3;
            lblTotalPartsWithoutDefects.Text = "Total Parts Without Defects : 0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(672, 392);
            Controls.Add(lblAverageProductionTime);
            Controls.Add(lblTotalPartsWithoutDefects);
            Controls.Add(lblTotalPartsOK);
            Controls.Add(lblTotalPartsProduced);
            Controls.Add(listBoxMessages);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBoxMessages;
        private Label lblTotalPartsProduced;
        private Label lblTotalPartsOK;
        private Label lblAverageProductionTime;
        private Label lblTotalPartsWithoutDefects;
    }
}
