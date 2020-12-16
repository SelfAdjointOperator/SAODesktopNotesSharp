using SAODesktopNotesSharp.Enum;
using System.Collections.Generic;

namespace SAODesktopNotesSharp {
    public partial class MainForm {
        private Dictionary<MainMenuActionKey, List<string>> mainMenuActionKeyDictionary = new Dictionary<MainMenuActionKey, List<string>>() {
            { MainMenuActionKey.Q, new List<string>(){ "Q", "QUIT" } },
            { MainMenuActionKey.A, new List<string>(){ "A", "ADD" } },
            { MainMenuActionKey.D, new List<string>(){ "D", "DEL", "DELETE" } },
            { MainMenuActionKey.E, new List<string>(){ "E", "EDIT" } },
            { MainMenuActionKey.R, new List<string>(){ "R", "REARRANGE" } },
            { MainMenuActionKey.M, new List<string>(){ "M", "MINIMISE", "MINIMIZE" } }
        };
    }
}
