using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIP
{
    public partial class Form2 : Form
    {
        Bitmap originalImage;
        int w, h;
        public Form2()
        {
            InitializeComponent();
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
            pictureBox2.Image = IP(originalImage);
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

            //draw histogram before IP
            int[] f = bmp2array(originalImage);
            int[] count = new int[256];
            // 統計出現次數
            for (int i = 0; i < w * h; i++)
            {
                count[f[i]]++; 
            }
            int max = count.Max();
            Bitmap hist = new Bitmap(256, max);
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < count[i]; j++)
                {
                    hist.SetPixel(i, max - j - 1, Color.Black);
                }
            }

            // 計算累計值
            // 假設count是一個包含每個灰階值出現次數的數組
            int[] cumulativeCount = new int[256];
            cumulativeCount[0] = count[0];
            for (int i = 1; i < 256; i++)
            {
                cumulativeCount[i] = cumulativeCount[i - 1] + count[i];
            }

            // 將累計數據線性映射到直方圖的高度
            int maxCumulativeCount = cumulativeCount.Max();
            for (int i = 0; i < 256; i++)
            {
                cumulativeCount[i] = (cumulativeCount[i] * hist.Height) / maxCumulativeCount;
            }

            // 在直方圖上繪製紅色累計線
            using (Graphics graphics = Graphics.FromImage(hist))
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Point[] points = new Point[256];
                for (int i = 0; i < 256; i++)
                {
                    // 注意Y坐標需要反轉，因為圖像的坐標系原點在左上角
                    points[i] = new Point(i, hist.Height - cumulativeCount[i]);
                }
                graphics.DrawLines(new Pen(Color.Red, 2), points); // 使用紅色筆畫線，筆寬為2
            }
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.Height = 256;
            pictureBox3.Width = 256;
            pictureBox3.Image = hist;

            // draw histogram after IP
            int[] g = bmp2array(IP(originalImage));
            int[] count2 = new int[256];
            // 統計出現次數
            for (int i = 0; i < w * h; i++)
            {
                count2[g[i]]++; 
            }
            int max2 = count2.Max();
            Bitmap hist2 = new Bitmap(256, max2);
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < count2[i]; j++)
                {
                    hist2.SetPixel(i, max2 - j - 1, Color.Black);
                }
            }
            // 計算累計值
            // 假設count是一個包含每個灰階值出現次數的數組
            cumulativeCount = new int[256];
            cumulativeCount[0] = count2[0];
            for (int i = 1; i < 256; i++)
            {
                cumulativeCount[i] = cumulativeCount[i - 1] + count2[i];
            }

            // 將累計數據線性映射到直方圖的高度
            maxCumulativeCount = cumulativeCount.Max();
            for (int i = 0; i < 256; i++)
            {
                cumulativeCount[i] = (cumulativeCount[i] * hist2.Height) / maxCumulativeCount;
            }

            // 在直方圖上繪製紅色累計線
            using (Graphics graphics = Graphics.FromImage(hist2))
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Point[] points = new Point[256];
                for (int i = 0; i < 256; i++)
                {
                    // 注意Y坐標需要反轉，因為圖像的坐標系原點在左上角
                    points[i] = new Point(i, hist2.Height - cumulativeCount[i]);
                }
                graphics.DrawLines(new Pen(Color.Red, 2), points); // 使用紅色筆畫線，筆寬為2
            }
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.Height = 256;
            pictureBox4.Width = 256;
            pictureBox4.Image = hist2;
            
        }

        private Bitmap IP(Bitmap original)
        {
            Bitmap NpBitmap;
            int[] f;
            int[] g;
            int[] count;
            double[] toChange;
            f = bmp2array(original);
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
                    for (int i = 0; i < 256; i++)
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
            return NpBitmap;
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

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
