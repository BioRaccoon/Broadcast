namespace Broadcast
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.colorPicBox = new System.Windows.Forms.PictureBox();
            this.selectColorButton = new System.Windows.Forms.Button();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.backgroundImagePathTextBox = new System.Windows.Forms.TextBox();
            this.logoPathTextBox = new System.Windows.Forms.TextBox();
            this.imageSelectButton = new System.Windows.Forms.Button();
            this.logoFileDialogSelectButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.colorPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Background Color :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Background Image: ";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Title Text: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Logo Image: ";
            // 
            // colorPicBox
            // 
            this.colorPicBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorPicBox.Location = new System.Drawing.Point(156, 29);
            this.colorPicBox.Name = "colorPicBox";
            this.colorPicBox.Size = new System.Drawing.Size(160, 23);
            this.colorPicBox.TabIndex = 4;
            this.colorPicBox.TabStop = false;
            // 
            // selectColorButton
            // 
            this.selectColorButton.Location = new System.Drawing.Point(322, 29);
            this.selectColorButton.Name = "selectColorButton";
            this.selectColorButton.Size = new System.Drawing.Size(51, 23);
            this.selectColorButton.TabIndex = 5;
            this.selectColorButton.Text = "Select";
            this.selectColorButton.UseVisualStyleBackColor = true;
            this.selectColorButton.Click += new System.EventHandler(this.selectColorButton_Click);
            // 
            // titleTextBox
            // 
            this.titleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.titleTextBox.Location = new System.Drawing.Point(156, 108);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(217, 20);
            this.titleTextBox.TabIndex = 6;
            // 
            // backgroundImagePathTextBox
            // 
            this.backgroundImagePathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.backgroundImagePathTextBox.Location = new System.Drawing.Point(156, 73);
            this.backgroundImagePathTextBox.Name = "backgroundImagePathTextBox";
            this.backgroundImagePathTextBox.ReadOnly = true;
            this.backgroundImagePathTextBox.Size = new System.Drawing.Size(217, 20);
            this.backgroundImagePathTextBox.TabIndex = 7;
            // 
            // logoPathTextBox
            // 
            this.logoPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logoPathTextBox.Location = new System.Drawing.Point(156, 145);
            this.logoPathTextBox.Name = "logoPathTextBox";
            this.logoPathTextBox.ReadOnly = true;
            this.logoPathTextBox.Size = new System.Drawing.Size(217, 20);
            this.logoPathTextBox.TabIndex = 8;
            // 
            // imageSelectButton
            // 
            this.imageSelectButton.Location = new System.Drawing.Point(379, 71);
            this.imageSelectButton.Name = "imageSelectButton";
            this.imageSelectButton.Size = new System.Drawing.Size(51, 23);
            this.imageSelectButton.TabIndex = 9;
            this.imageSelectButton.Text = "Select";
            this.imageSelectButton.UseVisualStyleBackColor = true;
            this.imageSelectButton.Click += new System.EventHandler(this.imageSelectButton_Click);
            // 
            // logoFileDialogSelectButton
            // 
            this.logoFileDialogSelectButton.Location = new System.Drawing.Point(379, 142);
            this.logoFileDialogSelectButton.Name = "logoFileDialogSelectButton";
            this.logoFileDialogSelectButton.Size = new System.Drawing.Size(51, 23);
            this.logoFileDialogSelectButton.TabIndex = 10;
            this.logoFileDialogSelectButton.Text = "Select";
            this.logoFileDialogSelectButton.UseVisualStyleBackColor = true;
            this.logoFileDialogSelectButton.Click += new System.EventHandler(this.logoFileDialogSelectButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(355, 184);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(274, 184);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 12;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 219);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.logoFileDialogSelectButton);
            this.Controls.Add(this.imageSelectButton);
            this.Controls.Add(this.logoPathTextBox);
            this.Controls.Add(this.backgroundImagePathTextBox);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.selectColorButton);
            this.Controls.Add(this.colorPicBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Customization Menu";
            ((System.ComponentModel.ISupportInitialize)(this.colorPicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox colorPicBox;
        private System.Windows.Forms.Button selectColorButton;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.TextBox backgroundImagePathTextBox;
        private System.Windows.Forms.TextBox logoPathTextBox;
        private System.Windows.Forms.Button imageSelectButton;
        private System.Windows.Forms.Button logoFileDialogSelectButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button applyButton;
    }
}