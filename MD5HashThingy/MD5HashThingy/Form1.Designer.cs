namespace MD5HashThingy
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
            this.filePath = new System.Windows.Forms.TextBox();
            this.hashButton = new System.Windows.Forms.Button();
            this.hashOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // filePath
            // 
            this.filePath.Location = new System.Drawing.Point(73, 31);
            this.filePath.Name = "filePath";
            this.filePath.Size = new System.Drawing.Size(100, 20);
            this.filePath.TabIndex = 1;
            // 
            // hashButton
            // 
            this.hashButton.Location = new System.Drawing.Point(195, 27);
            this.hashButton.Name = "hashButton";
            this.hashButton.Size = new System.Drawing.Size(75, 23);
            this.hashButton.TabIndex = 2;
            this.hashButton.Text = "HASH";
            this.hashButton.UseVisualStyleBackColor = true;
            this.hashButton.Click += new System.EventHandler(this.hashButton_Click);
            // 
            // hashOutput
            // 
            this.hashOutput.Location = new System.Drawing.Point(82, 123);
            this.hashOutput.Name = "hashOutput";
            this.hashOutput.Size = new System.Drawing.Size(100, 20);
            this.hashOutput.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.hashOutput);
            this.Controls.Add(this.hashButton);
            this.Controls.Add(this.filePath);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.Button hashButton;
        private System.Windows.Forms.TextBox hashOutput;
    }
}

