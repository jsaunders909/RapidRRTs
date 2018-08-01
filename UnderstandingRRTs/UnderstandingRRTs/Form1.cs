using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnderstandingRRTs
{
    public partial class Form1 : Form
    {
        BackgroundWorker b;
        Bitmap bmp;
        Coord from;
        Coord to;
        RRT rrt = new RRT(500, 500, new Coord(250, 250,0));
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(500, 500);
            b = new BackgroundWorker();
            b.DoWork += new DoWorkEventHandler(b_DoWork);
            b.ProgressChanged += new ProgressChangedEventHandler(b_ProgressChanged);
            b.WorkerReportsProgress = true;
            b.RunWorkerAsync();
            SetMap();
        }

        private void b_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DrawLine(from, to);
            pictureBox1.Image = bmp; 
        }

        public void b_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            while (i < 10000)
            {
                i++;
                rrt.RunIteration();
                from = rrt.mostRecentEdge.Item1;
                to = rrt.mostRecentEdge.Item2;
                b.ReportProgress(i);
                Thread.Sleep(10);
            }
        }

        public void DrawLine(Coord from, Coord to)
        {
            if ((0 < from.x && from.x < 500 && 0 < from.y && from.y < 500) && (0 < to.x && to.x < 500 && 0 < to.y && to.y < 500))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    double xStep = (double)(to.x - from.x) / 100;
                    double yStep = (double)(to.y - from.y) / 100;
                    int x = 0;
                    int y = 0;
                    for (int i = 0; i < 100; i++)
                    {
                        x = from.x + (int)(xStep * i);
                        y = from.y + (int)(yStep * i);
                        g.DrawEllipse(new Pen(Color.Black), x, y, 1f, 1f);
                    }
                }
            }
        }

        private void SetMap()
        {
            bool[,] obsticles = rrt.obsitcleMap;
            for (int i =0; i < bmp.Width; i++)
            {
                for (int j =0; j < bmp.Height; j++)
                {
                    if (obsticles[i, j]) { bmp.SetPixel(i, j, Color.Red); }
                }
            }
        }

    }
}
