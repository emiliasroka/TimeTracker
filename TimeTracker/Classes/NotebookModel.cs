using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TimeTracker.Classes;

namespace TimeTracker.Classes
{
    /// <summary>
    /// Klasa NotebookModel, która implementuje interfejsy ICloneable i INotebookModelm, która zawiera metody AddNote() oraz RemoveNote()oraz dziedziczy po klasie NoteModel. 
    /// Implementuje metody takie jak Clone(), DeepClone(), ReadXml(), SaveXml()
    /// </summary>
    [Serializable]

    public class NotebookModel : NoteModel, INotebookModel, ICloneable
    {

        /// <summary>
        /// Tworzy publiczną listę obiektów typu NoteModel. Ta lista jest używana do przechowywania notatek w aplikacji. 
        /// </summary>

        public List<NoteModel> notes = new List<NoteModel>();

        /// <summary>
        /// Tworzy publiczne właściwości Notes, pozwalają na uzyskanie dostępu do listy notatek z klasy NotebookModel.
        /// </summary>
        public List<NoteModel> Notes { get => notes; set => notes = value; }

        /// <summary>
        /// Konstruktor klasy NotebookModel. Jest wywoływany automatycznie podczas tworzenia nowego obiektu klasy NotebookModel. Jest on nadpisaniem konstruktora klasy bazowej, którym jest NoteModel.
        /// </summary>
        public NotebookModel() : base()
        {
            notes = new List<NoteModel>();
        }

        /// <summary>
        /// Metoda, która dodaje notatkę do listy
        /// </summary>
        /// <param name="note"></param>
        public void AddNote(NoteModel note)
        {
            notes.Add(note);
        }

        /// <summary>
        /// Metoda, która usuwa notatkę z listy
        /// </summary>
        /// <param name="note"></param>
        public void RemoveNote(NoteModel note)
        {
            if (this.notes.Contains(note))
                this.notes.Remove(note);

        }

        /// <summary>
        /// Metoda tworzy kopię płytką listy notatek
        /// </summary>
        /// <returns>Kopia płytka listy</returns>
        public object Clone() => this.MemberwiseClone();

        /// <summary>
        /// Metoda tworzy kopię głęboką listy notatek
        /// </summary>
        /// <returns>Kopia głęboka listy notatek</returns>
        public object DeepClone()
        {
            NotebookModel clonenotes = new NotebookModel();
            clonenotes = (NotebookModel)this.MemberwiseClone();
            clonenotes.notes = new List<NoteModel>();
            foreach (NoteModel note in this.notes)
                clonenotes.notes.Add((NoteModel)note.Clone());
            return clonenotes;
        }

        public static List<NoteModel> ReadXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<NoteModel>));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return (List<NoteModel>)serializer.Deserialize(fileStream);
            }
        }

        public static void SaveXml(List<NoteModel> notes, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<NoteModel>));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fileStream, notes);
            }
        }
    }
}