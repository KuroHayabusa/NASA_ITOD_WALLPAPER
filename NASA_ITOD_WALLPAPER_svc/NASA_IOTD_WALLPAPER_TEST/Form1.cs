﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.ServiceModel.Syndication;
using System.Xml;
using System.IO;
using System.Runtime.InteropServices;




namespace NASA_IOTD_WALLPAPER_TEST
{

    public partial class Form1 : Form
    {
        private string TEST_URL = "D:\\Users\\Josh\\Code\\Projects\\NASA_IOTD_WALLPAPER\\NASA_ITOD_WALLPAPER_svc\\TestFiles\\lg_image_of_the_day.rss";
        private string NASA_RSS_URL = "https://www.nasa.gov/rss/dyn/lg_image_of_the_day.rss";

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);


        public Form1()
        {
            InitializeComponent();
        }


        private void SetWallpaper(string wallpaperFilePath)
        {

            if (Path.GetExtension(wallpaperFilePath) != ".bmp" )
            {
                //if our source image is not a bitmap, load our file, convert it, then save it out
                Image wallpaper = Image.FromFile(wallpaperFilePath);
                Bitmap wallpaperBMP = new Bitmap(wallpaper);
                //save the file, note that the input variable is converted here to have a .bmp extension
                wallpaperBMP.Save(Path.ChangeExtension(wallpaperFilePath, ".bmp"));
            }

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, wallpaperFilePath,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }

        private void CacheImageButton_Click(object sender, EventArgs e)
        {
            RSSImageCacher cacher = new RSSImageCacher("NASA_IOTD");

            string filepath = cacher.CacheFirstImageFromNASARSS(TEST_URL);
            if (filepath.Length != 0)
            {
                SetWallpaper(filepath);
            }
            
        }


    }
}
