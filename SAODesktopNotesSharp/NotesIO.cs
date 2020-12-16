using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using static SAODesktopNotesSharp.Constants;

namespace SAODesktopNotesSharp {
    class NotesIO {
        public static List<Note> LoadNotes() {
            List<Note> notesList = new List<Note>();

            using (XmlReader xmlReader = XmlReader.Create(notesFilename, new XmlReaderSettings() { })) {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Note>));
                notesList = (List<Note>)xmlSerializer.Deserialize(xmlReader);
            }

            return notesList;
        }

        public static void SaveNotes(List<Note> notesList) {
            using (XmlWriter xmlWriter = XmlWriter.Create(notesFilename, new XmlWriterSettings() { Indent = true })) {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Note>));
                xmlSerializer.Serialize(xmlWriter, notesList);
            }
        }
    }
}
