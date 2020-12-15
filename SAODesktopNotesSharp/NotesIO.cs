using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SAODesktopNotesSharp {
    class NotesIO {
        public static List<Note> LoadNotes(string filename) {
            List<Note> notesList = new List<Note>();

            using (StreamReader streamReader = new StreamReader(filename))
            using (XmlReader xmlReader = XmlReader.Create(streamReader, new XmlReaderSettings() { })) {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Note>));
                notesList = (List<Note>)xmlSerializer.Deserialize(xmlReader);
            }

            return notesList;
        }

        public static void SaveNotes(string filename , List<Note> notesList) {
            using (StreamWriter streamWriter = new StreamWriter(filename))
            using (XmlWriter xmlWriter = XmlWriter.Create(streamWriter, new XmlWriterSettings() { Indent = true })) {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Note>));
                xmlSerializer.Serialize(xmlWriter, notesList);
            }
        }
    }
}
