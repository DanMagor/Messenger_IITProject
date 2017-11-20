namespace Chat
{
    partial class Form3
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
            this.repetition3 = new System.Windows.Forms.RadioButton();
            this.repetition5 = new System.Windows.Forms.RadioButton();
            this.hamming = new System.Windows.Forms.RadioButton();
            this.encodingGroup = new System.Windows.Forms.GroupBox();
            this.convolution = new System.Windows.Forms.RadioButton();
            this.compressionBox = new System.Windows.Forms.GroupBox();
            this.lz78 = new System.Windows.Forms.RadioButton();
            this.rle = new System.Windows.Forms.RadioButton();
            this.huffman = new System.Windows.Forms.RadioButton();
            this.saveSettings = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.encodingGroup.SuspendLayout();
            this.compressionBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // repetition3
            // 
            this.repetition3.AutoSize = true;
            this.repetition3.Location = new System.Drawing.Point(39, 40);
            this.repetition3.Name = "repetition3";
            this.repetition3.Size = new System.Drawing.Size(105, 21);
            this.repetition3.TabIndex = 0;
            this.repetition3.TabStop = true;
            this.repetition3.Text = "Repetition 3";
            this.repetition3.UseVisualStyleBackColor = true;
            // 
            // repetition5
            // 
            this.repetition5.AutoSize = true;
            this.repetition5.Location = new System.Drawing.Point(39, 67);
            this.repetition5.Name = "repetition5";
            this.repetition5.Size = new System.Drawing.Size(105, 21);
            this.repetition5.TabIndex = 1;
            this.repetition5.TabStop = true;
            this.repetition5.Text = "Repetition 5";
            this.repetition5.UseVisualStyleBackColor = true;
            // 
            // hamming
            // 
            this.hamming.AutoSize = true;
            this.hamming.Location = new System.Drawing.Point(39, 94);
            this.hamming.Name = "hamming";
            this.hamming.Size = new System.Drawing.Size(88, 21);
            this.hamming.TabIndex = 2;
            this.hamming.TabStop = true;
            this.hamming.Text = "Hamming";
            this.hamming.UseVisualStyleBackColor = true;
            // 
            // encodingGroup
            // 
            this.encodingGroup.Controls.Add(this.convolution);
            this.encodingGroup.Controls.Add(this.repetition3);
            this.encodingGroup.Controls.Add(this.hamming);
            this.encodingGroup.Controls.Add(this.repetition5);
            this.encodingGroup.Location = new System.Drawing.Point(12, 45);
            this.encodingGroup.Name = "encodingGroup";
            this.encodingGroup.Size = new System.Drawing.Size(200, 170);
            this.encodingGroup.TabIndex = 3;
            this.encodingGroup.TabStop = false;
            this.encodingGroup.Text = "Encoding";
            // 
            // convolution
            // 
            this.convolution.AutoSize = true;
            this.convolution.Location = new System.Drawing.Point(39, 122);
            this.convolution.Name = "convolution";
            this.convolution.Size = new System.Drawing.Size(103, 21);
            this.convolution.TabIndex = 3;
            this.convolution.TabStop = true;
            this.convolution.Text = "Convolution";
            this.convolution.UseVisualStyleBackColor = true;
            // 
            // compressionBox
            // 
            this.compressionBox.Controls.Add(this.lz78);
            this.compressionBox.Controls.Add(this.rle);
            this.compressionBox.Controls.Add(this.huffman);
            this.compressionBox.Location = new System.Drawing.Point(289, 45);
            this.compressionBox.Name = "compressionBox";
            this.compressionBox.Size = new System.Drawing.Size(200, 170);
            this.compressionBox.TabIndex = 4;
            this.compressionBox.TabStop = false;
            this.compressionBox.Text = "Compression";
            // 
            // lz78
            // 
            this.lz78.AutoSize = true;
            this.lz78.Location = new System.Drawing.Point(46, 95);
            this.lz78.Name = "lz78";
            this.lz78.Size = new System.Drawing.Size(67, 21);
            this.lz78.TabIndex = 2;
            this.lz78.TabStop = true;
            this.lz78.Text = "LZ-78";
            this.lz78.UseVisualStyleBackColor = true;
            // 
            // rle
            // 
            this.rle.AutoSize = true;
            this.rle.Location = new System.Drawing.Point(46, 67);
            this.rle.Name = "rle";
            this.rle.Size = new System.Drawing.Size(56, 21);
            this.rle.TabIndex = 1;
            this.rle.TabStop = true;
            this.rle.Text = "RLE";
            this.rle.UseVisualStyleBackColor = true;
            // 
            // huffman
            // 
            this.huffman.AutoSize = true;
            this.huffman.Location = new System.Drawing.Point(46, 40);
            this.huffman.Name = "huffman";
            this.huffman.Size = new System.Drawing.Size(82, 21);
            this.huffman.TabIndex = 0;
            this.huffman.TabStop = true;
            this.huffman.Text = "Huffman";
            this.huffman.UseVisualStyleBackColor = true;
            // 
            // saveSettings
            // 
            this.saveSettings.Location = new System.Drawing.Point(13, 271);
            this.saveSettings.Name = "saveSettings";
            this.saveSettings.Size = new System.Drawing.Size(199, 50);
            this.saveSettings.TabIndex = 5;
            this.saveSettings.Text = "Save";
            this.saveSettings.UseVisualStyleBackColor = true;
            this.saveSettings.Click += new System.EventHandler(this.button1_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(303, 271);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(186, 50);
            this.cancel.TabIndex = 6;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 358);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.saveSettings);
            this.Controls.Add(this.compressionBox);
            this.Controls.Add(this.encodingGroup);
            this.Name = "Form3";
            this.Text = "Settings";
            this.encodingGroup.ResumeLayout(false);
            this.encodingGroup.PerformLayout();
            this.compressionBox.ResumeLayout(false);
            this.compressionBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton repetition3;
        private System.Windows.Forms.RadioButton repetition5;
        private System.Windows.Forms.RadioButton hamming;
        private System.Windows.Forms.GroupBox encodingGroup;
        private System.Windows.Forms.RadioButton convolution;
        private System.Windows.Forms.GroupBox compressionBox;
        private System.Windows.Forms.RadioButton lz78;
        private System.Windows.Forms.RadioButton rle;
        private System.Windows.Forms.RadioButton huffman;
        private System.Windows.Forms.Button saveSettings;
        private System.Windows.Forms.Button cancel;
    }
}