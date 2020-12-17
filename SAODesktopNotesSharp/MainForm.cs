using SAODesktopNotesSharp.Enum;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static SAODesktopNotesSharp.Constants;

namespace SAODesktopNotesSharp {
    public partial class MainForm : Form {

        private State state = State.MAINMENU;
        private List<Note> notesList = new List<Note>();
        private Note workingNote;
        private int moveFromIndex;

        private void inputTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                DoStuff(inputTextBox.Text);
                if (state != State.EDIT_TEXT) {
                    inputTextBox.Clear();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void DoStuff(string inputText) {
            switch (state) {
                case State.MAINMENU:
                    inputText = inputText.ToUpper();
                    MainMenuActionKey action = MainMenuActionKey.UNKNOWN;
                    foreach (KeyValuePair<MainMenuActionKey, List<string>> keySet in mainMenuActionKeyDictionary) {
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
                                if (notesList.Count == 0) {
                                    Text = "No notes to delete. " + mainMenuText;
                                } else {
                                    state = State.DELETE_INDEX;
                                    Text = "Index of note to delete?";
                                }
                                break;
                            }
                        case MainMenuActionKey.E: {
                                if (notesList.Count == 0) {
                                    Text = "No notes to edit. " + mainMenuText;
                                } else {
                                    state = State.EDIT_INDEX;
                                    Text = "Index of note to edit?";
                                }
                                break;
                            }
                        case MainMenuActionKey.R: {
                                if (notesList.Count == 0) {
                                    Text = "No notes to rearrange. " + mainMenuText;
                                } else {
                                    state = State.REARRANGE_FROM_INDEX;
                                    Text = "Index of note to move?";
                                }
                                break;
                            }
                        case MainMenuActionKey.M: {
                                Hide();
                                break;
                            }
                        case MainMenuActionKey.UNKNOWN: {
                                Text = "Unknown Command. " + mainMenuText;
                                break;
                            }

                    };
                    break;
                case State.ADD_CONTENTS:
                    if (inputText == "") {
                        state = State.MAINMENU;
                        Text = "No note added. " + mainMenuText;
                    } else {
                        workingNote = new Note() { date = DateTime.Now, noteText = inputText };
                        state = State.ADD_INDEX;
                        Text = "Which index to add this note before? (Leave blank to append)";
                    }
                    break;
                case State.DELETE_INDEX:
                    try {
                        int deleteIndex = Int32.Parse(inputText);
                        workingNote = notesList[deleteIndex];
                        state = State.DELETE_CONFIRM;
                        Text = "Confirm deletion. (Y/N)";
                    } catch (Exception) {
                        state = State.MAINMENU;
                        Text = "Note index error. " + mainMenuText;
                    }
                    break;
                case State.EDIT_INDEX:
                    try {
                        int editIndex = Int32.Parse(inputText);
                        workingNote = notesList[editIndex];
                        inputTextBox.Text = workingNote.noteText;
                        inputTextBox.SelectionStart = inputTextBox.Text.Length;
                        inputTextBox.SelectionLength = 0;
                        state = State.EDIT_TEXT;
                        Text = "Editing:";
                    } catch (Exception) {
                        state = State.MAINMENU;
                        Text = "Note index error. " + mainMenuText;
                    }
                    break;
                case State.REARRANGE_FROM_INDEX:
                    try {
                        moveFromIndex = Int32.Parse(inputText);
                        workingNote = notesList[moveFromIndex];
                        notesList.Remove(workingNote);
                        Wallpaper.DrawSaveAndSetWallpaper(notesList);
                        state = State.REARRANGE_TO_INDEX;
                        Text = "Index to move note above?";
                    } catch (Exception) {
                        state = State.MAINMENU;
                        Text = "Note index error. " + mainMenuText;
                    }
                    break;
                case State.REARRANGE_TO_INDEX:
                    try {
                        int moveToIndex = Int32.Parse(inputText);
                        notesList.Insert(moveToIndex, workingNote);
                        Text = "Notes rearranged. " + mainMenuText;
                        NotesIO.SaveNotes(notesList);
                    } catch (Exception) {
                        notesList.Insert(moveFromIndex, workingNote);
                        Text = "Note index error. " + mainMenuText;
                    }
                    Wallpaper.DrawSaveAndSetWallpaper(notesList);
                    state = State.MAINMENU;
                    break;
                case State.ADD_INDEX:
                    try {
                        int insertIndex = Int32.Parse(inputText);
                        notesList.Insert(insertIndex, workingNote);
                        Text = "Note inserted. " + mainMenuText;
                    } catch (Exception) {
                        notesList.Add(workingNote);
                        Text = "Note appended. " + mainMenuText;
                    }
                    state = State.MAINMENU;
                    NotesIO.SaveNotes(notesList);
                    Wallpaper.DrawSaveAndSetWallpaper(notesList);
                    break;
                case State.DELETE_CONFIRM:
                    if (inputText.ToUpper() == "Y") {
                        notesList.Remove(workingNote);
                        Text = "Note removed. " + mainMenuText;
                        NotesIO.SaveNotes(notesList);
                        Wallpaper.DrawSaveAndSetWallpaper(notesList);
                    } else {
                        Text = "Note not removed. " + mainMenuText;
                    }
                    state = State.MAINMENU;
                    break;
                case State.EDIT_TEXT:
                    workingNote.noteText = inputText;
                    NotesIO.SaveNotes(notesList);
                    Wallpaper.DrawSaveAndSetWallpaper(notesList);
                    state = State.MAINMENU;
                    Text = "Note updated. " + mainMenuText;
                    break;
            }
        }
    }
}
