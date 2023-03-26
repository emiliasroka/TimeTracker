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
    /// Klasa która tworzy listę zadań. Klasa implementuje interfejs INotifyPropertyChanged.
    /// </summary>
    [Serializable]
    public class WorkTasksList : INotifyPropertyChanged
    {
        /// <summary>
        /// Zmienna typu XDocument
        /// </summary>
        XDocument toDoList;
        /// <summary>
        /// Lista złożona z zadań z klasy WorkTask.
        /// </summary>
        List<WorkTask> taskList;
        /// <summary>
        /// Zmienna typu bool, która zwraca albo prawda albo fałsz
        /// </summary>
        bool showDoneItems;
        /// <summary>
        /// Publiczną właściwość, która zwraca listę obiektów WorkTask, w zależności od wartości pola „showDoneItems”. Jeśli „showDoneItems” ma wartość true, zwraca całą listę zadań, w przeciwnym razie zwraca tylko zadania, które nie zostały zakończone.
        /// </summary>
        public List<WorkTask> TaskList
        {
            get
            {
                if (showDoneItems)
                {
                    return taskList;
                }
                else
                {
                    return (from i in taskList
                            where i.DoneDateTime == ""
                            select i).ToList();
                }
            }
        }
        /// <summary>
        /// Właściwość publiczna, która pobiera i ustawia wartość prywatnego pola „showDoneItems” oraz wywołuje zdarzenie PropertyChanged, gdy wartość zostanie zmieniona.
        /// </summary>
        public bool ShowDoneItems
        {
            get { return showDoneItems; }
            set
            {
                showDoneItems = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ShowDoneItems"));
                    PropertyChanged(this, new PropertyChangedEventArgs("TaskList"));
                }
            }
        }
        /// <summary>
        /// Konstruktor nieparametryczny, który sprawdza, czy plik xml istnieje, ładuje go do pola „toDoList” i tworzy z niego listę obiektów WorkTask. Jeśli plik nie istnieje, tworzy nowy XDocument i pustą listę obiektów WorkTask.
        /// </summary>
        public WorkTasksList()
        {
            if (System.IO.File.Exists("tasks.xml"))
            {
                toDoList = XDocument.Load("tasks.xml");
                taskList = (from e in toDoList.Root.Elements()
                            select new WorkTask(e)).ToList();
            }
            else
            {
                toDoList = new XDocument();
                toDoList.Add(new XElement("todolist"));
                taskList = new List<WorkTask>();
            }
        }
        /// <summary>
        /// Metoda która przyjmuje ciąg znaków jako parametr i dodaje go do pól taskList i „toDoList”, wywołuje zdarzenie PropertyChanged, gdy zmienia się taskList.
        /// </summary>
        /// <param name="description"></param>
        public void Additem(string description)
        {
            XElement item = new XElement("task");
            XElement createAt = new XElement("createdatetime");
            createAt.Value = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.ms");
            item.Add(createAt);
            XElement desc = new XElement("description");
            desc.Value = description;
            item.Add(desc);

            toDoList.Root.Add(item);
            taskList.Add(new WorkTask(item));

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("TaskList"));
            }
        }
        /// <summary>
        /// Metoda DeleteItem przyjmuje obiekt WorkTask jako parametr, usuwa go z listy, wywołuje zdarzenie PropertyChanged, gdy zmienia się lista.
        /// </summary>
        /// <param name="item"></param>
        public void DeleteItem(WorkTask item)
        {
            taskList.Remove(item);
            item.Task.Remove();
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("TaskList"));
            }
        }
        /// <summary>
        /// Metoda zapisująca liste zadań do pliku xml.
        /// </summary>
        public void ZapisXML()
        {
            toDoList.Save("tasks.xml");
        }
        /// <summary>
        /// Zdarzenie które służy do powiadamiania o zmianie wartości właściwości i umożliwia odpowiednią aktualizację interfejsu użytkownika.
        /// </summary>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}