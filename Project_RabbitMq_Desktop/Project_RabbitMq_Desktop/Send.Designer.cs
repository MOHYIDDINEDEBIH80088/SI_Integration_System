namespace Project_RabbitMq_Desktop
{
    partial class Send
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
            label1 = new Label();
            txtDate = new TextBox();
            txtTime = new TextBox();
            label2 = new Label();
            txtProductionTime = new TextBox();
            label3 = new Label();
            txtPartCode = new TextBox();
            label4 = new Label();
            txtTestResult = new TextBox();
            label5 = new Label();
            btnGenerate = new Button();
            btnSave = new Button();
            btnSend = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(149, 69);
            label1.Name = "label1";
            label1.Size = new Size(37, 15);
            label1.TabIndex = 0;
            label1.Text = "Date :";
            // 
            // txtDate
            // 
            txtDate.Location = new Point(219, 66);
            txtDate.Name = "txtDate";
            txtDate.Size = new Size(298, 23);
            txtDate.TabIndex = 1;
            // 
            // txtTime
            // 
            txtTime.Location = new Point(219, 116);
            txtTime.Name = "txtTime";
            txtTime.Size = new Size(298, 23);
            txtTime.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(149, 119);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 2;
            label2.Text = "Time :";
            // 
            // txtProductionTime
            // 
            txtProductionTime.Location = new Point(218, 218);
            txtProductionTime.Name = "txtProductionTime";
            txtProductionTime.Size = new Size(298, 23);
            txtProductionTime.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(148, 221);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 6;
            label3.Text = "Prod Time :";
            // 
            // txtPartCode
            // 
            txtPartCode.Location = new Point(218, 168);
            txtPartCode.Name = "txtPartCode";
            txtPartCode.Size = new Size(298, 23);
            txtPartCode.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(148, 171);
            label4.Name = "label4";
            label4.Size = new Size(65, 15);
            label4.TabIndex = 4;
            label4.Text = "Part Code :";
            // 
            // txtTestResult
            // 
            txtTestResult.Location = new Point(218, 267);
            txtTestResult.Name = "txtTestResult";
            txtTestResult.Size = new Size(298, 23);
            txtTestResult.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(148, 270);
            label5.Name = "label5";
            label5.Size = new Size(68, 15);
            label5.TabIndex = 8;
            label5.Text = "Test Result :";
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(149, 331);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(97, 40);
            btnGenerate.TabIndex = 10;
            btnGenerate.Text = "Generate Data";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(297, 331);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(83, 40);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save Data";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(427, 331);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(89, 40);
            btnSend.TabIndex = 12;
            btnSend.Text = "Send Data";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // Send
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(673, 383);
            Controls.Add(btnSend);
            Controls.Add(btnSave);
            Controls.Add(btnGenerate);
            Controls.Add(txtTestResult);
            Controls.Add(label5);
            Controls.Add(txtProductionTime);
            Controls.Add(label3);
            Controls.Add(txtPartCode);
            Controls.Add(label4);
            Controls.Add(txtTime);
            Controls.Add(label2);
            Controls.Add(txtDate);
            Controls.Add(label1);
            Name = "Send";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtDate;
        private TextBox txtTime;
        private Label label2;
        private TextBox txtProductionTime;
        private Label label3;
        private TextBox txtPartCode;
        private Label label4;
        private TextBox txtTestResult;
        private Label label5;
        private Button btnGenerate;
        private Button btnSave;
        private Button btnSend;
    }
}
