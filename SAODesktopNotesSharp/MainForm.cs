using SAODesktopNotesSharp.Enum;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace SAODesktopNotesSharp {
    public partial class MainForm : Form {

        private Dictionary<MainMenuActionKey, List<string>> mainMenuActionKeyMap = new Dictionary<MainMenuActionKey, List<string>>() {
            { MainMenuActionKey.Q, new List<string>(){"Q", "QUIT"} },
            { MainMenuActionKey.A, new List<string>(){"A", "ADD" } },
            { MainMenuActionKey.D, new List<string>(){"D", "DEL", "DELETE" } },
            { MainMenuActionKey.E, new List<string>(){"E", "EDIT" } },
            { MainMenuActionKey.R, new List<string>(){"R", "REARRANGE" } }
        };

        private State state = State.MAINMENU;
        private List<Note> notesList = new List<Note>();

        private void inputTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                DoStuff(inputTextBox.Text.ToUpper());
                inputTextBox.Clear();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void DoStuff(string inputText) {
            if (state == State.MAINMENU) {
                MainMenuActionKey action = MainMenuActionKey.UNKNOWN;
                foreach (KeyValuePair<MainMenuActionKey, List<string>> keySet in mainMenuActionKeyMap) {
                    if (keySet.Value.Contains(inputText)) {
                        action = keySet.Key;
                        break;
                    }
                }
                switch (action) {
                    case MainMenuActionKey.Q: {
                            Text = "Quitting...";
                            Application.Exit();
                            break;
                        }
                    case MainMenuActionKey.A: {
                            state = State.ADD_CONTENTS;
                            Text = "New note contents?";
                            break;
                        }
                    case MainMenuActionKey.D: {
                            state = State.DELETE_INDEX;
                            Text = "Index of note to delete?";
                            break;
                        }
                    case MainMenuActionKey.E: {
                            state = State.EDIT_INDEX;
                            Text = "Index of note to edit?";
                            break; }
                    case MainMenuActionKey.R: {
                            state = State.REARRANGE_INDEX_FROM;
                            Text = "Index of note to move?";
                            break; }
                    case MainMenuActionKey.UNKNOWN: {
                            Text = "Unknown Command";
                            break;
                        }

                };
            }


        }



    }
}
