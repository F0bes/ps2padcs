namespace ps2padgui
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
            this.lbllh = new System.Windows.Forms.Label();
            this.lbllv = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // lbllh
            // 
            this.lbllh.AutoSize = true;
            this.lbllh.Location = new System.Drawing.Point(65, 33);
            this.lbllh.Name = "lbllh";
            this.lbllh.Size = new System.Drawing.Size(35, 13);
            this.lbllh.TabIndex = 0;
            this.lbllh.Text = "label1";
            // 
            // lbllv
            // 
            this.lbllv.AutoSize = true;
            this.lbllv.Location = new System.Drawing.Point(65, 57);
            this.lbllv.Name = "lbllv";
            this.lbllv.Size = new System.Drawing.Size(35, 13);
            this.lbllv.TabIndex = 1;
            this.lbllv.Text = "label1";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(209, 100);
            this.Controls.Add(this.lbllv);
            this.Controls.Add(this.lbllh);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbllh;
        private System.Windows.Forms.Label lbllv;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

