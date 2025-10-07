using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIP
{
    public partial class Form1 : Form
    {
        Bitmap originalImage;
        public int val;
        public double scale;
        int w, h;
        int function_number = 200;
        
        public Form1()
        {
            InitializeComponent();
        }

        public void Set_F_number(int number)
        {
            function_number = number;
        }

        public void LoadImages(string originalImagePath, string adjustedImagePath)
        {
            // 載入原圖
            originalImage = new Bitmap(originalImagePath);
            pictureBox1.Image = originalImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Size = new Size(pictureBox1.Width + this.Size.Width, pictureBox1.Height + this.Size.Height);
            w = pictureBox1.Width;
            h = pictureBox1.Height;
            pictureBox2.Image = originalImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private Bitmap IP_contrast(Bitmap original)
        {
            Bitmap NpBitmap;
            int[] f;
            int[] g;
            f = bmp2array(original);
            g = new int[w * h];
            unsafe
            {
                fixed (int* f0 = f) fixed (int* g0 = g)
                {
                    for (int i = 0; i < w * h; i++)
                    {
                        g0[i] = (f0[i] - 127) * trackBar1.Value + 127;
                        g0[i] = g0[i] < 0 ? 0 : (g0[i] > 255 ? 255 : g0[i]);
                    }
                }
            }
            NpBitmap = array2bmp(g);
            return NpBitmap;
        }

        private Bitmap IP_bright(Bitmap original)
        {
            Bitmap NpBitmap;
            int[] f;
            int[] g;
            f = bmp2array(original);
            g = new int[w * h];
            unsafe
            {
                fixed (int* f0 = f) fixed (int* g0 = g)
                {
                    for (int i = 0; i < w * h; i++)
                    {
                        g0[i] = f0[i] + trackBar1.Value;
                        g0[i] = g0[i] < 0 ? 0 : (g0[i] > 255 ? 255 : g0[i]);
                    }
                }
            }
            NpBitmap = array2bmp(g);
            return NpBitmap;
        }

        private Bitmap IP_Neighbor(Bitmap original) // 鄰近
        {
            Bitmap NpBitmap;
            int[] f;
            int[] g;
            f = bmp2array(original);
            int w = original.Width;
            int h = original.Height;
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
            return NpBitmap;
        }

        private Bitmap IP_Interpolation(Bitmap original) // 內插
        {
            Bitmap NpBitmap;
            int[] f;
            int[] g;
            double ry, rx, cx, cy, alpha, beta, a, b, c;
            f = bmp2array(original);
            int w = original.Width;
            int h = original.Height;
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
            return NpBitmap;
        }

        private Bitmap IP_rotate(Bitmap original)
        {
            Bitmap NpBitmap;
            int[] f;
            int[] g;
            f = bmp2array(original);
            int w = original.Width;
            int h = original.Height;
            double theta = scale * Math.PI / 180; // 角度轉換為弧度
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
            return NpBitmap;
        }

        public void Bright_Init()
        {
            trackBar1.Visible = true;
            textBox1.Visible = false;
            label1.Visible = true;
            button2.Visible = false;
            trackBar1.Maximum = 255;
            trackBar1.Minimum = -255;
        }
        public void Contrast_Init()
        {
            trackBar1.Visible = true;
            textBox1.Visible = false;
            label1.Visible = true;
            button2.Visible = false;
            trackBar1.Maximum = 127;
            trackBar1.Minimum = 0;
        }

        public void Scalling_Init()
        {
            trackBar1.Visible = false;
            textBox1.Visible = true;
            label1.Visible = false;
            button2.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            val = trackBar1.Value;
            label1.Text = val.ToString();
            Bitmap adjustedImage;
            switch (function_number) // 切換功能
            {
                case 0: // 對比調整
                    adjustedImage = IP_contrast(originalImage);
                    pictureBox2.Image = adjustedImage;
                    pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                    break;
                case 1: // 亮度調整
                    adjustedImage = IP_bright(originalImage);
                    pictureBox2.Image = adjustedImage;
                    pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                    break;
                default:
                    break;
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.Text == string.Empty) return; // 如果 TextBox 是空的，則不進行任何操作
            if (!double.TryParse(textBox.Text, out _)) // 如果輸入的內容不能轉換為 double
            {
                textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1); // 移除最後一個字符
                textBox.SelectionStart = textBox.Text.Length; // 將光標移到最後
            }
            scale = Convert.ToDouble(textBox1.Text);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (((e.KeyChar < '0') || (e.KeyChar > '9')) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) // 調整預覽按鈕
        {
            Bitmap adjustedImage;
            switch (function_number) // 切換功能
            {
                case 2:
                    adjustedImage = IP_Neighbor(originalImage);
                    pictureBox2.Image = adjustedImage;
                    pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                    break;
                case 3: // 縮放調整 (內插)
                    adjustedImage = IP_Interpolation(originalImage);
                    pictureBox2.Image = adjustedImage;
                    pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                    break;
                case 4: // 任意角度旋轉
                    adjustedImage = IP_rotate(originalImage);
                    pictureBox2.Image = adjustedImage;
                    pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                    break;
                default:
                    break;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

    }
}
