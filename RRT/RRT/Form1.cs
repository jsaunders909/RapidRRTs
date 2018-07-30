using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace RRT
{
    public partial class Form1 : Form
    {
        BackgroundWorker b;
        Bitmap bmp;
        RapidRT rrt;
        Coord from;
        Coord to;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Width = 500;
            rrt = new RapidRT(500, 500, 5,new Coord(250, 250));
            b = new BackgroundWorker();
            b.WorkerReportsProgress = true;
            b.DoWork += B_DoWork;
            b.ProgressChanged += B_ProgressChanged;
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            b.RunWorkerAsync();
        }

        private void B_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DrawLine(from, to);
            pictureBox1.Image = bmp;
        }

        private void B_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            while (true)
            {
                i++;
                rrt.RunIteration();
                from = rrt.mostRecentEdge.Item1;
                to = rrt.mostRecentEdge.Item2;
                b.ReportProgress(i);
                Thread.Sleep(30);
            }
        }

        private void DrawLine(Coord from, Coord to)
        {
            double distance = rrt.GetDistance(from, to);
            double xStep = (to.X - from.X) / distance;
            double yStep = (to.Y - from.Y) / distance;
            using (Graphics g = Graphics.FromImage(bmp))
            {
                for (int i = 0; i < distance; i++)
                {
                    int x = (int)(from.X + (i * xStep));
                    int y = (int)(from.Y + (i * yStep));
                    bmp.SetPixel(x, y, Color.Black);
                }
            }
        }
    }
}
