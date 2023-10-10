namespace CheckinReviewReport
{
    partial class Form1
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
            this.btnGenRpt = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblTfsProject = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGenRpt
            // 
            this.btnGenRpt.Location = new System.Drawing.Point(297, 163);
            this.btnGenRpt.Name = "btnGenRpt";
            this.btnGenRpt.Size = new System.Drawing.Size(164, 38);
            this.btnGenRpt.TabIndex = 0;
            this.btnGenRpt.Text = "Generate Report";
            this.btnGenRpt.UseVisualStyleBackColor = true;
            this.btnGenRpt.Click += new System.EventHandler(this.btnGenRpt_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(238, 102);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(281, 28);
            this.comboBox1.TabIndex = 1;
            // 
            // lblTfsProject
            // 
            this.lblTfsProject.AutoSize = true;
            this.lblTfsProject.Location = new System.Drawing.Point(124, 110);
            this.lblTfsProject.Name = "lblTfsProject";
            this.lblTfsProject.Size = new System.Drawing.Size(92, 20);
            this.lblTfsProject.TabIndex = 2;
            this.lblTfsProject.Text = "TFS Project";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblTfsProject);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnGenRpt);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenRpt;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblTfsProject;
    }
}

