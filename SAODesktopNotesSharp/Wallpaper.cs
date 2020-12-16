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
        private static int noteLeftMargin = horizontalMargin + 335;

        private static Brush textGoldBrush = new SolidBrush(textGold);
        private static Font fnt = new Font("Courier New", 12);
        private static int lineHeight = 22;
        private static double charWidth = 12.5;

        private static int noteWidth = screenWidth - (noteLeftMargin + horizontalMargin);
        private static int messageCharsPerLine = Convert.ToInt32(noteWidth / charWidth);

        private static IEnumerable<string> splitString(string stringToSplit, int splitSize) {
            if (stringToSplit == "") {
                yield return " "; // for notes with no content
            } else {
                for (int i = 0; i < stringToSplit.Length; i += splitSize)
                    yield return stringToSplit.Substring(i, Math.Min(splitSize, stringToSplit.Length - i));
            }
        }

        private static void DrawAndSaveWallpaper(List<Note> notesList) {
            Bitmap bmp = new Bitmap(screenWidth, screenHeight);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(backgroundWhite);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using (Bitmap saoLogo = Properties.Resources.saopng_144) {
                g.DrawImage(saoLogo, Convert.ToInt32((screenWidth - saoLogo.Width) / 2), 10);
            }

            int noteIndex = 0;
            int currentVerticalPosition = 160;
            foreach (Note note in notesList) {
                g.DrawString(noteIndex.ToString().PadLeft(2, '0') + " : " + note.date.ToString() + " : ", fnt, textGoldBrush, horizontalMargin, currentVerticalPosition);
                foreach (string splitStringLine in splitString(note.noteText, messageCharsPerLine)) {
                    RectangleF rectangleF = new RectangleF(noteLeftMargin, currentVerticalPosition, noteWidth, lineHeight);
                    g.DrawString(splitStringLine, fnt, textGoldBrush, rectangleF);
                    currentVerticalPosition += lineHeight;
                }
                noteIndex++;
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
