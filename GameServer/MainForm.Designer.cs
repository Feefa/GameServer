namespace GameServer
{
    partial class MainForm
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
            this.StartStopPanel = new System.Windows.Forms.Panel();
            this.StartStopButton = new System.Windows.Forms.Button();
            this.StartStopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartStopPanel
            // 
            this.StartStopPanel.Controls.Add(this.StartStopButton);
            this.StartStopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.StartStopPanel.Location = new System.Drawing.Point(0, 0);
            this.StartStopPanel.Name = "StartStopPanel";
            this.StartStopPanel.Size = new System.Drawing.Size(284, 24);
            this.StartStopPanel.TabIndex = 0;
            // 
            // StartStopButton
            // 
            this.StartStopButton.Location = new System.Drawing.Point(0, 0);
            this.StartStopButton.Name = "StartStopButton";
            this.StartStopButton.Size = new System.Drawing.Size(144, 23);
            this.StartStopButton.TabIndex = 0;
            this.StartStopButton.Text = "Start";
            this.StartStopButton.UseVisualStyleBackColor = true;
            this.StartStopButton.Click += new System.EventHandler(this.StartStopButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.StartStopPanel);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.StartStopPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel StartStopPanel;
        private System.Windows.Forms.Button StartStopButton;
    }
}

