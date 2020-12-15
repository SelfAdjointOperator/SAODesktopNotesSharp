using System;

namespace SAODesktopNotesSharp {
    [Serializable()]
    public class Note {
        [System.Xml.Serialization.XmlElement("Date")]
        public DateTime date;

        [System.Xml.Serialization.XmlElement("NoteText")]
        public string noteText;
    }
}
