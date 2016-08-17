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

namespace ImgBrowseCopy
{
    public partial class Form1 : Form
    {
        DirectoryInfo SourceDir, DestinationDir;
        Stack<FileInfo> ImagesLeft, LastImages = new Stack<FileInfo>();
        FileInfo CurrentFile;
        int MinWidth, MinHeight, NImages;
        int CurImgN = 0;

        public Form1(DirectoryInfo sourceDir, DirectoryInfo destinationDir, int minWidth, int minHeight)
        {
            SourceDir = sourceDir;
            DestinationDir = destinationDir;
            MinWidth = minWidth;
            MinHeight = minHeight;

            ImagesLeft = new Stack<FileInfo>(sourceDir.GetFiles());
            NImages = ImagesLeft.Count;

            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CurrentFile.CopyTo(Path.Combine(DestinationDir.FullName, CurrentFile.Name));
                NextImage();
            }
            else if (e.KeyCode == Keys.Right)
            {
                NextImage();
            }

            else if (e.KeyCode == Keys.Left)
            {
                if (LastImages.Any()) ImagesLeft.Push(LastImages.Pop());
                if (LastImages.Any()) ImagesLeft.Push(LastImages.Pop());
                NextImage();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NextImage();
        }

        void NextImage()
        {
            while (true)
            {
                if (!ImagesLeft.Any())
                {
                    Console.WriteLine("All done");
                    Close();
                    break;
                }

                CurrentFile = ImagesLeft.Pop();
                CurImgN++;
                Image img;
                try
                {
                    img = Image.FromFile(CurrentFile.FullName);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    continue;
                }

                this.Text = string.Format("ImgBrowseCopy - '{0}' - {1}/{2} - {3:0.##}%", CurrentFile.Name, CurImgN, NImages, CurImgN * 1.0 / NImages * 100);

                if (img.Width < MinWidth || img.Height < MinHeight) continue;

                LastImages.Push(CurrentFile);
                var old = pictureBox1.Image;
                pictureBox1.Image = img;
                if(old != null) old.Dispose();
                break;
            }
        }
    }
}
