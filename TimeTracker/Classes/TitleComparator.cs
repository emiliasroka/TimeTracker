using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimeTracker.Classes
{
    /// <summary>
    /// Klasa implementuje interfejs IComparer dla klasy NoteModel. 
    /// Metoda Compare porównuje właściwość Title dwóch obiektów NoteModel (x i y) i zwraca liczbę całkowitą wskazującą ich względną kolejność w operacji sortowania
    /// </summary>
    public class TitleComparator : IComparer<NoteModel>
    {
        public int Compare(NoteModel x, NoteModel y)
        {
            return string.Compare(x.Title, y.Title);
        }
    }
}