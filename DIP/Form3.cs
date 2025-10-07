using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIP
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font.Name, 14, FontStyle.Regular);
            label1.Text = "軟體使用說明：\n\n" +
                          "簡介：\n" +
                          "　歡迎使用本軟體，本軟體提供一系列的影像處理功能，例如亮度、對比、\n" +
                          "　大小調整與濾鏡應用等等，以下將說明如何使用本軟體。\n\n" +
                          "使用步驟：\n" +
                          "　１、點擊畫面左上方的 File 來選擇要處理的圖片\n" +
                          "　２、接著點擊 IP 選單，選擇想要處理的功能\n\n" +
                          "IP選單中功能說明：\n" +
                          "　１、點擊對比或亮度後會出現一個視窗，請依照想調整的數值拉動拉桿，\n" +
                          "　　　調整後的結果會在畫面上即時顯示，最後按下「確定」輸出圖片。\n" +
                          "　２、點擊旋轉或縮放後會出現一個視窗，可以於輸入框中輸入想要旋轉的\n" +
                          "　　　角度或縮放的大小，按下「調整預覽」後，調整後的結果會在畫面上\n" +
                          "　　　即時顯示，最後按下「確定」輸出圖片。\n" +
                          "　３、直方圖等化的功能會出現一個視窗，顯示調整前後的圖片、直方圖與\n" +
                          "　　　累計分布圖，按下「確定」後即輸出圖片。\n" +
                          "　４、其餘功能皆會在按下後即輸出結果圖片。\n\n" +
                          "關於本軟體：\n" +
                          "　本軟體由數位影像處理課程林義隆老師指導。\n\n" +
                          "製作人員：\n" +
                          "　B11100013　陳柏宇\n" +
                          "　B11117048　蔡翔宇\n\n" +
                          "感謝您的使用。\n\n ";
        }
    }
}
