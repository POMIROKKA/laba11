using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace лаба_11
{
    public partial class Form1 : Form
    {
        int pbh, pbw;
        public Form1()
        {
            InitializeComponent();
        }

        private Boolean FillListBox(string aPath)
        {
            DirectoryInfo di = new DirectoryInfo(aPath); //информация о каталоге
            FileInfo[] fi = di.GetFiles("*.jpg");// массив информации о файлах
            listBox1.Items.Clear(); //очистка ранее полученный список файлов
            foreach (FileInfo fc in fi) //добавляем в listBox1 имена файлов, содержащихся в каталоге aPath
            {
                listBox1.Items.Add(fc.Name);
            }
            label1.Text = aPath;
            if (fi.Length == 0) return false;
            else
            {
                listBox1.SelectedIndex = 0; // выбираем первый файл из списка
                return true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.Description = "Выберите папку";
            fb.ShowNewFolderButton = false;
            if (fb.ShowDialog() == DialogResult.OK)
                if (!FillListBox(fb.SelectedPath + "\\"))
                    pictureBox1.Image = null;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            double mh, mw;
            pictureBox1.Image = new Bitmap(label1.Text + listBox1.SelectedItem.ToString());
            if ((pictureBox1.Image.Width > pbw) || (pictureBox1.Image.Height > pbh))
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                mh = (double)pbh / (double)pictureBox1.Image.Height;
                mw = (double)pbw / (double)pictureBox1.Image.Width;
                if (mh < mw)
                {
                    pictureBox1.Width = Convert.ToInt16(pictureBox1.Image.Width * mh);
                    pictureBox1.Height = pbh;
                }
                else
                {
                    pictureBox1.Width = pbw;
                    pictureBox1.Height = Convert.ToInt16(pictureBox1.Image.Height * mw);
                }
            }
            else
                if (pictureBox1.SizeMode == PictureBoxSizeMode.StretchImage)
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pbh = pictureBox1.Height;
            pbw = pictureBox1.Width;
            listBox1.Sorted = true;
            FillListBox(Application.StartupPath + "\\");
        }
    }
}
