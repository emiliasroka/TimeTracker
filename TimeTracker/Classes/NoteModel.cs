using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TimeTracker.Classes
{
    /// <summary>
    /// Klasa NoteModel, która implementuje interfejs ICloneable. 
    /// Implementuje metodę Clone()
    /// </summary>
    /// 
    [Serializable]
    public class NoteModel : ICloneable, IEquatable<NoteModel>
    {

        /// <summary>
        /// Publiczne pole title przechowujące tytuł notatki.
        /// </summary>
        public string title;
        /// <summary>
        /// Publiczne pole note przechowujace treść notatki.
        /// </summary>
        public string note;

        /// <summary>
        /// Publiczna właściwość typu string, która umożliwia dostęp do prywatnego pola title.
        /// </summary>
        [XmlElement("Title")]
        public string Title { get => title; set => title = value; }
        /// <summary>
        /// Publiczna właściwość typu string, która umożliwia dostęp do prywatnego pola note.
        /// </summary>
        [XmlElement("Note")]
        public string Note { get => note; set => note = value; }

        /// <summary>
        /// Konstruktor nieparametryczny klasy NoteModel
        /// </summary>
        public NoteModel()
        {

        }
        /// <summary>
        /// Konstruktor parametryczny klasy NoteModel, przyjmujący dwa parametry
        /// </summary>
        /// <param name="title"></param>
        /// <param name="note"></param>
        public NoteModel(string title, string note) : this()
        {
            this.Title = title;
            this.Note = note;
        }

        /// <summary>
        /// Metoda interfejsu ICloneable, która pozwala na tworzenie kopii obiektu. Jest nadpisana przez implementację MemberwiseClone(), 
        /// która tworzy składnikową kopię obiektu
        /// </summary>
        /// <returns></returns>
        public object Clone() => this.MemberwiseClone();

        /// <summary>
        /// Implementacja metody Equals dla klasy NoteModel. 
        /// Metoda porównuje właściwość Title dwóch obiektów NoteModel, "other" i "this" i zwraca wartość Boolean wskazującą, czy są one równe.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(NoteModel other)
        {
            if (other.Title == this.Title)
                return true;
            return false;
        }

        /// <summary>
        /// Implementacja metody CompareTo dla klasy NoteModel. 
        /// Metoda porównuje właściwość Title dwóch obiektów NoteModel, "other" i "this" i zwraca liczbę całkowitą wskazującą ich względną kolejność.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(NoteModel other)
        {
            if (object.ReferenceEquals(other, null))
                return 1;
            int wynik = Title.CompareTo(other.Title);
            if (wynik == 0)
                return 0;
            return 1;
        }
    }

}
