using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DIP
{
    public partial class DIPSample : Form
    {
        public string OriginalImagePath { get; private set; }
        public DIPSample()
        {
            InitializeComponent();
        }

        [DllImport("dip_proc.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void encode(int *f0,int w,int h,int *g0);
        
        Bitmap NpBitmap;
        int[] f;
        int[] g;
        int w, h;
        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("123");
        }
        private void DIPSample_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
			this.stStripLabel.Text = "";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oFileDlg.CheckFileExists = true;
            oFileDlg.CheckPathExists = true;
            oFileDlg.Title = "Open File - DIP Sample";
            oFileDlg.ValidateNames = true;
            oFileDlg.Filter = "bmp files (*.bmp)|*.bmp";
            oFileDlg.FileName = "";

            if (oFileDlg.ShowDialog() == DialogResult.OK)
            {
                OriginalImagePath = oFileDlg.FileName;
                MSForm childForm = new MSForm();
                childForm.MdiParent = this;
                childForm.pf1 = stStripLabel;
                NpBitmap = bmp_read(oFileDlg);
                childForm.pBitmap = NpBitmap;
                w = NpBitmap.Width;
                h = NpBitmap.Height;
                childForm.Show();
            }
        }

        private Bitmap bmp_read(OpenFileDialog oFileDlg)
        {
            Bitmap pBitmap;
            string fileloc = oFileDlg.FileName;
            pBitmap = new Bitmap(fileloc);
            w = pBitmap.Width;
            h = pBitmap.Height;
            return pBitmap;
        }

        private int[] bmp2array(Bitmap myBitmap)
        {
            int[] ImgData = new int[myBitmap.Width * myBitmap.Height];
            BitmapData byteArray = myBitmap.LockBits(new Rectangle(0, 0, myBitmap.Width, myBitmap.Height),
                                          ImageLockMode.ReadWrite,
                                          myBitmap.PixelFormat);
            int ByteOfSkip = byteArray.Stride - byteArray.Width * (int)(byteArray.Stride / myBitmap.Width);
            unsafe
            {
                byte* imgPtr = (byte*)(byteArray.Scan0);
                for (int y = 0; y < byteArray.Height; y++)
                {
                    for (int x = 0; x < byteArray.Width; x++)
                    {
                        ImgData[x + byteArray.Height * y] = (int)*(imgPtr);
                        //ImgData[x, y] = (int)*(imgPtr + 1);
                        //ImgData[x, y] = (int)*(imgPtr + 2);
                        imgPtr += (int)(byteArray.Stride / myBitmap.Width);
                    }
                    imgPtr += ByteOfSkip;
                }
            }
            myBitmap.UnlockBits(byteArray);
            return ImgData;
        }

        private static Bitmap array2bmp(int[] ImgData)
        {
            int Width = (int)Math.Sqrt(ImgData.GetLength(0));
            int Height = (int)Math.Sqrt(ImgData.GetLength(0));
            Bitmap myBitmap = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            BitmapData byteArray = myBitmap.LockBits(new Rectangle(0, 0, Width, Height),
                                           ImageLockMode.WriteOnly,
                                           PixelFormat.Format24bppRgb);
            //Padding bytes的長度
            int ByteOfSkip = byteArray.Stride - myBitmap.Width * 3;
            unsafe
            {                                   // 指標取出影像資料
                byte* imgPtr = (byte*)byteArray.Scan0;
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        *imgPtr = (byte)ImgData[x + Height * y];       //B
                        *(imgPtr + 1) = (byte)ImgData[x + Height * y]; //G 
                        *(imgPtr + 2) = (byte)ImgData[x + Height * y]; //R  
                        imgPtr += 3;
                    }
                    imgPtr += ByteOfSkip; // 跳過Padding bytes
                }
            }
            myBitmap.UnlockBits(byteArray);
            return myBitmap;
        }

        private void rGBtoGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int []f;
            int []g;
            foreach (MSForm cF in MdiChildren)
			   {
					if (cF.Focused)
					{
					    f = bmp2array(cF.pBitmap);
			            g=new int[w*h];
                        unsafe
                        {
                            fixed (int* f0 = f) fixed (int* g0 = g)
                            {
                                // encode(f0, w, h, g0);
                                for (int i = 0; i < w*h; i++)
                                {
                                    g0[i] = 255 - f0[i];
                                }

                            }
                        } 
                        NpBitmap = array2bmp(g);
				        break;
				    }
			   }
			MSForm childForm = new MSForm();
	        childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
			childForm.pBitmap = NpBitmap; 
			childForm.Show();
        }

        private void rotateToolStripMenuItem_Click(object sender, EventArgs e) // 對比調整
        {
            Form1 childForm1 = new Form1();
            childForm1.LoadImages(OriginalImagePath, OriginalImagePath);
            childForm1.Set_F_number(0);
            childForm1.Contrast_Init();
            int contrast = 0;
            if (childForm1.ShowDialog() == DialogResult.OK)
            {
                contrast = childForm1.val;
                Console.WriteLine(contrast);
            }
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[w * h];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            for (int i = 0; i < w * h; i++)
                            {
                                g0[i] = (f0[i] - 127) * contrast + 127;
                                g0[i] = g0[i] < 0 ? 0 : (g0[i] > 255 ? 255 : g0[i]);
                            }
                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 亮度ToolStripMenuItem_Click(object sender, EventArgs e) // 亮度調整
        {
            Form1 childForm1 = new Form1();
            childForm1.LoadImages(OriginalImagePath, OriginalImagePath);
            childForm1.Set_F_number(1);
            childForm1.Bright_Init();
            int bright = 0;
            if(childForm1.ShowDialog() == DialogResult.OK)
            {
                bright = childForm1.val;
                Console.WriteLine(bright);
            }


            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[w * h];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            for (int i = 0; i < w * h; i++)
                            {
                                g0[i] = f0[i] + bright;
                                g0[i] = g0[i] < 0 ? 0 : (g0[i] > 255 ? 255 : g0[i]);
                            }

                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 上下翻轉ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[w * h];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            int ii = 0, jj = 0, j;
                            for (int i = 0; i < w * h; i++)
                            {
                                ii = i / w;
                                jj = i % w;
                                
                                j = (h - ii -1) * w + jj;

                                g0[j] = f0[i];
                                
                            }

                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 左右翻轉ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[w * h];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            int ii = 0, jj = 0, j;
                            for (int i = 0; i < w * h; i++)
                            {
                                ii = i / w;
                                jj = i % w;

                                j = ii * h + (w - jj - 1);

                                g0[j] = f0[i];

                            }

                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[w * h];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            int ii = 0, jj = 0, j;
                            for (int i = 0; i < w * h; i++)
                            {
                                ii = i / w;
                                jj = i % w;

                                j = (w - jj - 1) * h + ii;

                                g0[j] = f0[i];

                            }

                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 度ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[w * h];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            int ii = 0, jj = 0, j;
                            for (int i = 0; i < w * h; i++)
                            {
                                ii = i / w;
                                jj = i % w;

                                j = jj * h + (h - ii - 1);

                                g0[j] = f0[i];

                            }

                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 內插ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 childForm1 = new Form1();
            childForm1.LoadImages(OriginalImagePath, OriginalImagePath);
            childForm1.Set_F_number(3);
            childForm1.Scalling_Init();
            double scale = 0;
            if (childForm1.ShowDialog() == DialogResult.OK)
            {
                scale = childForm1.scale;
                Console.WriteLine(scale);
            }

            int[] f;
            int[] g;

            double ry, rx, cx, cy, alpha, beta, a, b, c;

            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    int w = cF.pBitmap.Width;
                    int h = cF.pBitmap.Height;
                    int nw = (int)(w * scale);
                    int nh = (int)(h * scale);

                    // Convert 1D array to 2D array
                    int[,] f2D = new int[w, h];
                    for (int i = 0; i < w; i++)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            f2D[i, j] = f[i * h + j];
                        }
                    }

                    // Scaling
                    int[,] g2D = new int[nw, nh];
                    for (int i = 0; i < nw; i++)
                    {
                        for (int j = 0; j < nh; j++)
                        {
                            rx = i + 1;
                            cx = j + 1;
                            ry = (w - 1.0) / (nw - 1) * rx + (1.0 - (w - 1.0) / (nw - 1));
                            cy = (h - 1.0) / (nh - 1) * cx + (1.0 - (h - 1.0) / (nh - 1));

                            alpha = ry - (int)ry;
                            beta = cy - (int)cy;

                            // Ensure indices are within bounds
                            int ry1 = Math.Min((int)ry, w - 1);
                            int ry2 = Math.Min(ry1 + 1, w - 1);
                            int cy1 = Math.Min((int)cy, h - 1);
                            int cy2 = Math.Min(cy1 + 1, h - 1);

                            a = (1 - alpha) * f2D[ry1, cy1] + alpha * f2D[ry2, cy1];
                            b = (1 - alpha) * f2D[ry1, cy2] + alpha * f2D[ry2, cy2];
                            c = beta * b + (1 - beta) * a;
                            g2D[i, j] = (int)(c + 0.5);
                        }
                    }

                    // Convert 2D array back to 1D array
                    g = new int[nw * nh];
                    for (int i = 0; i < nw; i++)
                    {
                        for (int j = 0; j < nh; j++)
                        {
                            g[i * nh + j] = g2D[i, j];
                        }
                    }

                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 鄰近ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 childForm1 = new Form1();
            childForm1.LoadImages(OriginalImagePath, OriginalImagePath);
            childForm1.Set_F_number(2);
            childForm1.Scalling_Init();
            double scale = 0;
            if (childForm1.ShowDialog() == DialogResult.OK)
            {
                scale = childForm1.scale;
                Console.WriteLine(scale);
            }

            int[] f;
            int[] g;

            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    int w = cF.pBitmap.Width;
                    int h = cF.pBitmap.Height;
                    int nw = (int)(w * scale);
                    int nh = (int)(h * scale);

                    // Convert 1D array to 2D array
                    int[,] f2D = new int[w, h];
                    for (int i = 0; i < w; i++)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            f2D[i, j] = f[i * h + j];
                        }
                    }

                    // Scaling using Nearest Neighbor Interpolation
                    int[,] g2D = new int[nw, nh];
                    for (int i = 0; i < nw; i++)
                    {
                        for (int j = 0; j < nh; j++)
                        {
                            int srcX = (int)(i / scale);
                            int srcY = (int)(j / scale);
                            g2D[i, j] = f2D[srcX, srcY];
                        }
                    }

                    // Convert 2D array back to 1D array
                    g = new int[nw * nh];
                    for (int i = 0; i < nw; i++)
                    {
                        for (int j = 0; j < nh; j++)
                        {
                            g[i * nh + j] = g2D[i, j];
                        }
                    }

                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 任意角度的旋轉ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 childForm1 = new Form1();
            childForm1.LoadImages(OriginalImagePath, OriginalImagePath);
            childForm1.Set_F_number(4);
            childForm1.Scalling_Init();
            double angle = 0;
            if (childForm1.ShowDialog() == DialogResult.OK)
            {
                angle = childForm1.scale;
                Console.WriteLine(angle);
            }

            int[] f;
            int[] g;
            double theta = angle * Math.PI / 180; // 角度轉換為弧度

            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    int w = cF.pBitmap.Width;
                    int h = cF.pBitmap.Height;
                    int nw = (int)(Math.Abs(w * Math.Cos(theta)) + Math.Abs(h * Math.Sin(theta)));
                    int nh = (int)(Math.Abs(h * Math.Cos(theta)) + Math.Abs(w * Math.Sin(theta)));

                    // Convert 1D array to 2D array
                    int[,] f2D = new int[w, h];
                    for (int i = 0; i < w; i++)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            f2D[i, j] = f[i * h + j];
                        }
                    }

                    int[,] g2D = new int[nw, nh];
                    int cx = w / 2; // x-coordinate of the center
                    int cy = h / 2; // y-coordinate of the center
                    int ncx = nw / 2; // new center x
                    int ncy = nh / 2; // new center y

                    for (int i = 0; i < nw; i++)
                    {
                        for (int j = 0; j < nh; j++)
                        {
                            int x = i - ncx;
                            int y = j - ncy;
                            int newX = (int)(x * Math.Cos(theta) - y * Math.Sin(theta)) + cx;
                            int newY = (int)(x * Math.Sin(theta) + y * Math.Cos(theta)) + cy;

                            if (newX >= 0 && newX < w && newY >= 0 && newY < h)
                            {
                                g2D[i, j] = f2D[newX, newY];
                            }
                            else
                            {
                                g2D[i, j] = 255; // 白色
                            }
                        }
                    }

                    // Convert 2D array back to 1D array
                    g = new int[nw * nh];
                    for (int i = 0; i < nw; i++)
                    {
                        for (int j = 0; j < nh; j++)
                        {
                            g[i * nh + j] = g2D[i, j];
                        }
                    }

                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 直方圖等化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 childForm1 = new Form2();
            childForm1.LoadImages(OriginalImagePath, OriginalImagePath);
            double scale = 0;
            if (childForm1.ShowDialog() == DialogResult.OK)
            {
            }

            int[] f;
            int[] g;
            int[] count;
            double[] toChange;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[w * h];
                    count = new int[256];
                    toChange = new double[256];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            for (int i = 0; i < w * h; i++)
                            {
                                count[f0[i]]++; // 統計出現次數
                            }
                            for (int i = 0; i < 256; i++)
                            {
                                toChange[i] = ((double)count[i] / (w * h)) * 255.0; // 統計出現機率，再乘上最大值
                            }
                            for (int i = 1; i < 256; i++)
                            {
                                toChange[i] += toChange[i - 1]; // 累加
                            }
                            for(int i = 0; i < 256; i++)
                            {
                                toChange[i] = Math.Round(toChange[i]); // 四捨五入
                            }
                            for (int i = 0; i < w * h; i++)
                            {
                                g[i] = (int)toChange[f0[i]];
                            }
                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 濾波器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void sobel濾波器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[f.Length];
                    int w = cF.pBitmap.Width;
                    int h = cF.pBitmap.Height;

                    double[,] Gx = new double[3, 3] {{ -1, 0, 1 },
                                                     { -2, 0, 2 },
                                                     { -1, 0, 1 }};

                    double[,] Gy = new double[3, 3] {{-1, -2, -1},
                                                     { 0, 0, 0 },
                                                     { 1, 2, 1 }};

                    double[,] f2D = new double[w, h];
                    for (int i = 0; i < w; i++)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            f2D[i, j] = f[i * h + j];
                        }
                    }

                    double[,] g2D = new double[w, h];
                    for (int i = 1; i < w - 1; i++)
                    {
                        for (int j = 1; j < h - 1; j++)
                        {
                            double gx = 0;
                            double gy = 0;
                            for (int a = -1; a <= 1; a++)
                            {
                                for (int b = -1; b <= 1; b++)
                                {
                                    gx += f2D[i + a, j + b] * Gx[1 + a, 1 + b];
                                    gy += f2D[i + a, j + b] * Gy[1 + a, 1 + b];
                                }
                            }
                            g2D[i, j] = Math.Sqrt(gx * gx + gy * gy);
                            g2D[i, j] = g2D[i, j] < 0 ? 0 : (g2D[i, j] > 255 ? 255 : g2D[i, j]);
                        }
                    }


                    for (int i = 0; i < w; i++)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            g[i * h + j] = Convert.ToInt32(g2D[i, j]);
                        }
                    }

                    NpBitmap = array2bmp(g);
                    break;
                }
            }

            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void prewitt濾波器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[f.Length];
                    int w = cF.pBitmap.Width;
                    int h = cF.pBitmap.Height;

                    double[,] Gx = new double[3, 3] {
                { -1, 0, 1 },
                { -1, 0, 1 },
                { -1, 0, 1 }
            };

                    double[,] Gy = new double[3, 3] {
                { -1, -1, -1 },
                { 0, 0, 0 },
                { 1, 1, 1 }
            };

                    double[,] f2D = new double[w, h];
                    for (int i = 0; i < w; i++)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            f2D[i, j] = f[i * h + j];
                        }
                    }

                    double[,] g2D = new double[w, h];
                    for (int i = 1; i < w - 1; i++)
                    {
                        for (int j = 1; j < h - 1; j++)
                        {
                            double gx = 0;
                            double gy = 0;
                            for (int a = -1; a <= 1; a++)
                            {
                                for (int b = -1; b <= 1; b++)
                                {
                                    gx += f2D[i + a, j + b] * Gx[1 + a, 1 + b];
                                    gy += f2D[i + a, j + b] * Gy[1 + a, 1 + b];
                                }
                            }
                            g2D[i, j] = Math.Sqrt(gx * gx + gy * gy);
                            g2D[i, j] = g2D[i, j] < 0 ? 0 : (g2D[i, j] > 255 ? 255 : g2D[i, j]);
                        }
                    }

                    for (int i = 0; i < w; i++)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            g[i * h + j] = Convert.ToInt32(g2D[i, j]);
                        }
                    }

                    NpBitmap = array2bmp(g);
                    break;
                }
            }

            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 平均濾波器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            double[][] m = new double[3][];
            m[0] = new double[] { 1.0 / 9.0, 1.0 / 9.0, 1.0 / 9.0 };
            m[1] = new double[] { 1.0 / 9.0, 1.0 / 9.0, 1.0 / 9.0 };
            m[2] = new double[] { 1.0 / 9.0, 1.0 / 9.0, 1.0 / 9.0 };
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[w * h];
                    unsafe
                    {
                        // Convert 1D array to 2D array
                        double[,] f2D = new double[w, h];
                        for (int i = 0; i < w; i++)
                        {
                            for (int j = 0; j < h; j++)
                            {
                                f2D[i, j] = f[i * h + j];
                            }
                        }

                        double[,] g2D = new double[w, h];
                        for (int i = 1; i < w - 1; i++)
                        {
                            for (int j = 1; j < h - 1; j++)
                            {
                                for (int a = -1; a <= 1; a++)
                                {
                                    for (int b = -1; b <= 1; b++)
                                    {
                                        g2D[i, j] += f2D[i + a, j + b] * m[1 + a][1 + b];
                                    }
                                }
                            }
                        }


                        // Convert 2D array back to 1D array
                        g = new int[w * h];
                        for (int i = 0; i < w; i++)
                        {
                            for (int j = 0; j < h; j++)
                            {
                                g[i * h + j] = Convert.ToInt32(g2D[i, j]);
                            }
                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 高斯濾波器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;

            // Define Gaussian kernel
            double[][] m = new double[3][];
            m[0] = new double[] { 1.0 / 16, 2.0 / 16, 1.0 / 16 };
            m[1] = new double[] { 2.0 / 16, 4.0 / 16, 2.0 / 16 };
            m[2] = new double[] { 1.0 / 16, 2.0 / 16, 1.0 / 16 };

            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    int w = cF.pBitmap.Width;
                    int h = cF.pBitmap.Height;
                    g = new int[w * h];

                    // Convert 1D array to 2D array
                    double[,] f2D = new double[w, h];
                    for (int i = 0; i < w; i++)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            f2D[i, j] = f[i * h + j];
                        }
                    }

                    double[,] g2D = new double[w, h];
                    for (int i = 1; i < w - 1; i++)
                    {
                        for (int j = 1; j < h - 1; j++)
                        {
                            double sum = 0.0;
                            for (int a = -1; a <= 1; a++)
                            {
                                for (int b = -1; b <= 1; b++)
                                {
                                    sum += f2D[i + a, j + b] * m[1 + a][1 + b];
                                }
                            }
                            g2D[i, j] = sum;
                        }
                    }

                    // Convert 2D array back to 1D array
                    for (int i = 0; i < w; i++)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            g[i * h + j] = Convert.ToInt32(g2D[i, j]);
                        }
                    }

                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 影像銳化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;

            // Define Laplacian sharpening kernel
            double[][] laplacianKernel = new double[3][];
            laplacianKernel[0] = new double[] { 1.0/9.0, 1.0 / 9.0, 1.0 / 9.0 };
            laplacianKernel[1] = new double[] { 1.0 / 9.0, 1.0 / 9.0, 1.0 / 9.0 };
            laplacianKernel[2] = new double[] { 1.0 / 9.0, 1.0 / 9.0, 1.0 / 9.0 };

            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    int w = cF.pBitmap.Width;
                    int h = cF.pBitmap.Height;
                    g = new int[w * h];

                    // Convert 1D array to 2D array
                    double[,] f2D = new double[w, h];
                    for (int i = 0; i < w; i++)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            f2D[i, j] = f[i * h + j];
                        }
                    }

                    double[,] g2D = new double[w, h];
                    for (int i = 1; i < h - 1; i++)
                    {
                        for (int j = 1; j < w - 1; j++)
                        {
                            double sum = 0.0;
                            for (int a = -1; a <= 1; a++)
                            {
                                for (int b = -1; b <= 1; b++)
                                {
                                    sum += f2D[i + a, j + b] * laplacianKernel[1 + a][1 + b];
                                    sum = sum < 0 ? 0 : (sum > 255 ? 255 : sum);
                                }
                            }
                            g2D[i, j] = sum;
                        }
                    }

                    for (int i = 1; i < h - 1; i++)
                    {
                        for (int j = 1; j < w - 1; j++)
                        {
                            f2D[i, j] = 2 * f2D[i, j] - g2D[i, j];
                            f2D[i,j] = f2D[i, j] < 0 ? 0 : (f2D[i, j] > 255 ? 255 : f2D[i, j]);
                        }
                    }


                    // Convert 2D array back to 1D array
                    for (int i = 0; i < w; i++)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            f[i * h + j] = Convert.ToInt32(f2D[i, j]);
                        }
                    }

                    NpBitmap = array2bmp(f);
                    break;
                }
            }

            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void otuss分割ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            double opt_p = 0;

            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    int w = cF.pBitmap.Width;
                    int h = cF.pBitmap.Height;
                    int[] hist = new int[w * h];
                    double[] p = new double[w * h];
                    g = new int[w * h];

                    // Convert 1D array to 2D array
                    int[,] f2D = new int[w, h];
                    for (int i = 0; i < w; i++)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            f2D[i, j] = f[i * h + j];
                        }
                    }

                    for (int i = 0; i < h; i++)
                    {
                        for (int j = 0; j < w; j++)
                        {
                            hist[f2D[i, j]]++;
                        }
                    }

                    int sum_hist = 0;
                    for (int i = 0; i < w * h; i++) {
                        sum_hist += hist[i];
                    }

                    for (int i = 0; i < w * h; i++)
                    {
                        p[i] = (double)hist[i] / sum_hist;
                    }

                    double mu1, mu2, w1, w2;
                    double opt_sigmaw = 10000000;
                    double sigmaw, sigma1, sigma2;

                    for (int cut_p = 1; cut_p < 256; cut_p++)
                    {
                        w1 = 0;
                        sigma1 = 0;
                        mu1 = 0;
                        for (int i = 0; i <= cut_p; i++)
                            w1 += p[i];
                        for (int i = 0; i <= cut_p; i++)
                            mu1 += i * p[i] / w1;
                        
                        for (int i = 0; i <= cut_p; i++)
                            sigma1 += (i - mu1) * (i - mu1) * p[i] / w1;

                        w2 = 0;
                        sigma2 = 0;
                        mu2 = 0;
                        for (int i = cut_p + 1; i < 256; i++)
                            w2 += p[i];
                        for (int i = cut_p + 1; i < 256; i++)
                            mu2 += i * p[i];
                        mu2 /= w2;
                        for (int i = cut_p + 1; i < 256; i++)
                            sigma2 += (i - mu2) * (i - mu2) * p[i] / w2;

                        sigmaw = mu1 * sigma1 + mu2 * sigma2;
                        if (sigmaw < opt_sigmaw)
                        {
                            opt_sigmaw = sigmaw;
                            opt_p = cut_p;
                        }
                    }


                    int[,] g2D = new int[w, h];
                    

                    for (int i = 0; i < h; i++)
                    {
                        for (int j = 0; j < w; j++)
                        {
                            if (f2D[i, j] > opt_p)
                                g2D[i, j] = 255;
                            else
                                g2D[i, j] = 0;
                        }
                    }

                    // Convert 2D array back to 1D array
                    for (int i = 0; i < w; i++)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            g[i * h + j] = Convert.ToInt32(g2D[i, j]);
                        }
                    }

                    NpBitmap = array2bmp(g);
                    break;
                }
            }

            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Text = opt_p.ToString();
            childForm.Show();
        }

        private void 說明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 childForm1 = new Form3();
            if (childForm1.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}
