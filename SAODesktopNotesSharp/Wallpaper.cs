using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static SAODesktopNotesSharp.Constants;

namespace SAODesktopNotesSharp {
    static class Wallpaper {

        private static string wallpaperPath = System.IO.Path.GetFullPath(wallpaperFilename);
        private static int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        private static int screenHeight = Screen.PrimaryScreen.Bounds.Height;

        private static int horizontalMargin = 10;

        private static void DrawAndSaveWallpaper(List<Note> notesList) {
            Bitmap bmp = new Bitmap(screenWidth, screenHeight);
            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            Font fnt = new Font("Courier New", 14);
            //Font fnt = new Font(FontFamily.GenericMonospace, 14);

            g.DrawString("here we are now again", fnt, Brushes.White, 500, 500);
            using (Bitmap saoLogo = Properties.Resources.saopng_144) {
                g.DrawImage(saoLogo, Convert.ToInt32((screenWidth - saoLogo.Width) / 2), 10);
            }

            foreach (Note note in notesList) {
                RectangleF rectangleF = new RectangleF(horizontalMargin, 400, screenWidth - horizontalMargin, 420);
                g.DrawString(note.date.ToString() + " : " + note.noteText, fnt, Brushes.Fuchsia, rectangleF);
            }

            g.Flush();
            bmp.Save(wallpaperPath, System.Drawing.Imaging.ImageFormat.Png);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private static void SetWallpaper() {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            key.SetValue(@"WallpaperStyle", 6.ToString());
            key.SetValue(@"TileWallpaper", 0.ToString());
            SystemParametersInfo(20, 0, wallpaperPath, 0x01 | 0x02);
        }

        public static void DrawSaveAndSetWallpaper(List<Note> notesList) {
            DrawAndSaveWallpaper(notesList);
            SetWallpaper();
        }

    }
}
