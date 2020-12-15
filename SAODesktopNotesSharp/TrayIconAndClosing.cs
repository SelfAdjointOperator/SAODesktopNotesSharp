using System;
using System.Windows.Forms;

namespace SAODesktopNotesSharp {
    partial class MainForm {

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing) {
                Hide();
                e.Cancel = true;
            }
        }

        private void trayIcon_Click(object sender, EventArgs e) {
            Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e) {
            Show();
        }

    }
}
