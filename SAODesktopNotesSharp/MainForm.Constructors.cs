using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static SAODesktopNotesSharp.NotesIO;
using static SAODesktopNotesSharp.Constants;

namespace SAODesktopNotesSharp {
    partial class MainForm {
        public MainForm() {
            InitializeComponent();
            Width = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 0.7);
            Location = new Point(Convert.ToInt32((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2), Screen.PrimaryScreen.WorkingArea.Height - this.Height);

            try {
                notesList = LoadNotes(notesFilename);
            } catch (FileNotFoundException) {
                notesList = new List<Note>();
                SaveNotes(notesFilename, notesList);
            }

            Wallpaper.DrawSaveAndSetWallpaper(notesList);

        }
    }
}
