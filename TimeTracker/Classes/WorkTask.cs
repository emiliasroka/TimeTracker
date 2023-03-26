using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TimeTracker.Classes
{
    /// <summary>
    /// Klasa która tworzy zadania i umożliwia zmienianie właściwości zadania, takie jak czas utworzenia i zakończenia, opis i status. Klasa implementuje interfejs INotifyPropertyChanged
    /// </summary>
    [Serializable]
    public class WorkTask : INotifyPropertyChanged
    {
        /// <summary>
        /// Prywatne pole typu XElement, które jest klasą z przestrzeni nazw System.Xml.Linq
        /// </summary>
        private XElement task;
        /// <summary>
        /// Właściwość publiczna która zwraca wartość zmiennej "task".
        /// </summary>
        public XElement Task { get { return task; } }
        /// <summary>
        /// Konstruktor parametryczny, który pobiera element XElement i przypisuje go do pola zadania.
        /// </summary>
        /// <param name="task"></param>
        public WorkTask(XElement task)
        {
            this.task = task;
        }
        /// <summary>
        /// Właściwość publiczna która pobiera i ustawia datę stworzenia zadania z XElement.
        /// </summary>
        public string CreatedDateTime
        {
            get { return task.Element("createdatetime").Value; }
            set
            {
                task.Element("createdatetime").Value = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CreatedDateTime"));
                }
            }
        }
        /// <summary>
        /// Właściwość publiczna która pobiera i ustawia treść zadania z XElement.
        /// </summary>
        public string Description
        {
            get { return task.Element("description").Value; }
            set
            {
                task.Element("description").Value = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Description"));
                }
            }
        }
        /// <summary>
        /// Właściwość publiczna która pobiera i ustawia datę wykonania zadania z XElement.
        /// </summary>
        public string DoneDateTime
        {
            get { return task.Element("donedatetime") == null ? "" : task.Element("donedatetime").Value; }
            set
            {
                if (task.Element("donedatetime") == null)
                {
                    task.Add(new XElement("donedatetime"));
                }

                task.Element("donedatetime").Value = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DoneDateTime"));
                }
            }
        }
        /// <summary>
        /// Właściwość publiczna która zwraca wykonane zadania z XElement.
        /// </summary>
        public bool IsDone { get { return (task.Element("donedatetime") != null); } }
        /// <summary>
        /// Metoda zwraca łańcuchową reprezentację obiektu WorkTask, jeśli zadanie zostanie wykonane, będzie zawierało datę i godzinę wykonania, jeśli nie, zwróci tylko opis zadania.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result;
            if (DoneDateTime != "")
            {
                DateTime doneAt = DateTime.Parse(DoneDateTime);
                result = String.Format("{0} - Done at: {1:yyyy-MM-dd hh:mm}", Description, doneAt);
            }
            else
            {
                result = Description;
            }
            return result;
        }
        /// <summary>
        ///  Zdarzenie które służy do powiadamiania o zmianie wartości właściwości i umożliwia odpowiednią aktualizację interfejsu użytkownika.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
