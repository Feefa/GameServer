﻿namespace GameServer
{
    partial class WebServerLoggingProvider
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
            this.MessagesTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // MessagesTextBox
            // 
            this.MessagesTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessagesTextBox.Location = new System.Drawing.Point(0, 0);
            this.MessagesTextBox.Multiline = true;
            this.MessagesTextBox.Name = "MessagesTextBox";
            this.MessagesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MessagesTextBox.Size = new System.Drawing.Size(800, 555);
            this.MessagesTextBox.TabIndex = 0;
            // 
            // WebServerLoggingProvider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 555);
            this.Controls.Add(this.MessagesTextBox);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "WebServerLoggingProvider";
            this.Text = "Web Server Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WebServerLoggingProvider_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MessagesTextBox;
    }
}