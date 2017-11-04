namespace Chat
{
    partial class Form2
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
            this.messageBox = new System.Windows.Forms.TextBox();
            this.sendMessage = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.messageViewer = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Header = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(12, 546);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(723, 22);
            this.messageBox.TabIndex = 0;
            // 
            // sendMessage
            // 
            this.sendMessage.Location = new System.Drawing.Point(741, 545);
            this.sendMessage.Name = "sendMessage";
            this.sendMessage.Size = new System.Drawing.Size(75, 23);
            this.sendMessage.TabIndex = 1;
            this.sendMessage.Text = "Send";
            this.sendMessage.UseVisualStyleBackColor = true;
            this.sendMessage.Click += new System.EventHandler(this.sendMessage_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(822, 545);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Attach";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // messageViewer
            // 
            this.messageViewer.Location = new System.Drawing.Point(12, 46);
            this.messageViewer.Name = "messageViewer";
            this.messageViewer.ReadOnly = true;
            this.messageViewer.Size = new System.Drawing.Size(885, 493);
            this.messageViewer.TabIndex = 3;
            this.messageViewer.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(817, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 38);
            this.button1.TabIndex = 4;
            this.button1.Text = "Settings";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Header
            // 
            this.Header.AutoSize = true;
            this.Header.Location = new System.Drawing.Point(12, 13);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(46, 17);
            this.Header.TabIndex = 5;
            this.Header.Text = "label1";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 580);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.messageViewer);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.sendMessage);
            this.Controls.Add(this.messageBox);
            this.Name = "Form2";
            this.Text = "Chat";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.Button sendMessage;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox messageViewer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label Header;
    }
}