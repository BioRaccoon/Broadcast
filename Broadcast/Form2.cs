using Gst.Video;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Broadcast
{
    public partial class Form2 : Form
    {
        private Broadcaster parentForm;
        public Form2(Broadcaster parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            colorPicBox.BackColor = parentForm.BackColor;
            titleTextBox.Text = parentForm.broadcasterLabel.Text;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void selectColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.Color = parentForm.BackColor;
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                colorPicBox.BackColor = MyDialog.Color;
            }
                
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            parentForm.BackColor = colorPicBox.BackColor;
            if (titleTextBox.Text.Length != 0) 
            {
                parentForm.broadcasterLabel.Text = titleTextBox.Text;
            }
            try
            {
                parentForm.BackgroundImage = Image.FromFile(backgroundImagePathTextBox.Text);
            }
            catch { }
            try
            {
                parentForm.logoIconPicBox.Image = Image.FromFile(logoPathTextBox.Text);
            }
            catch { }

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void imageSelectButton_Click(object sender, EventArgs e)
        {
            string filePath;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Image files (*.jpg)|*.jpg|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                try
                {
                    File.Copy(filePath, Path.GetFileName(filePath), true);
                }
                catch { }
                backgroundImagePathTextBox.Text = filePath;

            }
        }

        private void logoFileDialogSelectButton_Click(object sender, EventArgs e)
        {
            string filePath;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Image files (*.jpg)|*.jpg|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                try
                {
                    File.Copy(filePath, Path.GetFileName(filePath), true);
                }
                catch { }
                logoPathTextBox.Text = filePath;
            }
        }
    }
}
