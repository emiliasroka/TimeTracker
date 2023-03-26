using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimeTracker.Classes;

namespace TimeTracker
{
    /// <summary>
    /// Klasa głównego okna aplikacji, która jest dziedzicząca po klasie Window z biblioteki WPF. 
    /// Zawiera ona elementy interfejsu użytkownika, takie jak przyciski, pola tekstowe i listy, które służą do tworzenia, edycji i przeglądania notatek. 
    /// Implementuje metody obsługujące interakcje użytkownika, takie jak przyciski ButtonMinimize_Click(), ButtonMaximized_Click(), CloseButton_Click(), btnAdd_Click(), btnEdit_Click, btnDelete_Click().
    /// </summary>
    /// 

    [Serializable]

    public partial class NotepadMainWindow : Window
    {
        /// <summary>
        /// Obiekt jest instancją klasy NoteModel, która jest używana do przechowywania informacji o notatce, takiej jak tytuł i treść notatki
        /// </summary>
        NoteModel note = new NoteModel();

        /// <summary>
        /// Obiekt jest instancją klasy NotebookModel, która jest używana do przechowywania informacji o liście notatek.
        /// </summary>
        NotebookModel notes = new NotebookModel();

        /// <summary>
        /// Konstruktor wywołujący metodę InitializeComponent(), która inicjalizuje komponenty interfejsu użytkownika
        /// </summary>
        public NotepadMainWindow()
        {
            InitializeComponent();
            notes.Notes = NotebookModel.ReadXml("listOfNotes");
            lstNotes.ItemsSource = notes.Notes;
        }


        /// <summary>
        /// Metoda obsługuje naciśnięcie kursora nad obszarem Border
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //Możliwość przemieszczania okna za pomocą myszki 
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        /// <summary>
        /// Metoda minimalzuje okno NotepadMainWindow, obsługiwana przez przycisk.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //Możliwość minimalizacji okna za pomocą przycisku.
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// Metoda maksymalizuje okno NotepadMainWindow, obsługiwana przez przycisk.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //Możliwość maksymalizacji okna za pomocą przycisku.
        private void ButtonMaximized_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }
        /// <summary>
        /// Metoda zamyka okno NotepadMainWindow, obsługiwana przez przycisk.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //Zamknięcie okna za pomocą przycisku.
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            NotebookModel.SaveXml(notes.Notes, "listOfNotes");
            Close();
        }

        /// <summary>
        /// Metoda obsługiwana przez przycisk btnAdd_Click, która otwiera okno NoteWindow i dodaje notatkę do listy w kolejności alfabetycznej
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NoteModel n = new NoteModel();
            NoteWindow window = new NoteWindow(n);
            bool? result = window.ShowDialog();
            if (result == true)
            {
                notes.AddNote(n);
                lstNotes.ItemsSource = new ObservableCollection<NoteModel>(notes.Notes);
                lstNotes.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Title", System.ComponentModel.ListSortDirection.Ascending));
            }

        }

        /// <summary>
        /// Metoda obsługiwana przez przycisk btnEdit_Click, która otwiera okno NoteWindow jeżeli element ListView został wybrany
        /// oraz zmienia wartości title i note dla wybranego elementu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            if (lstNotes.SelectedIndex > -1)
            {

                NoteModel selectedItem = (NoteModel)lstNotes.SelectedItem;
                NoteWindow window = new NoteWindow(selectedItem);
                bool? result = window.ShowDialog();

                if (result == true)
                {

                    txtbTitle.Text = selectedItem.Title;
                    txtbNote.Text = selectedItem.Note;
                    window.txtTitle.Text = txtbTitle.Text;
                    window.txtNote.Text = txtbNote.Text;


                    if (selectedItem != null)
                    {
                        selectedItem.Title = txtbTitle.Text;
                        selectedItem.Note = txtbNote.Text;
                        lstNotes.Items.Refresh();
                    }

                }

            }
        }

        /// <summary>
        /// Metoda obsługiwana przez przycisk btnDelete_Click, która usuwa wybraną z ListView notatkę.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            NoteModel selectedItem = (NoteModel)lstNotes.SelectedItem;
            if (lstNotes.SelectedIndex > -1)
            {
                notes.RemoveNote(((NoteModel)lstNotes.SelectedItem));
                lstNotes.ItemsSource = new ObservableCollection<NoteModel>(notes.Notes);
            }
            selectedItem.Title = txtbTitle.Text;
            selectedItem.Note = txtbNote.Text;
            lstNotes.Items.Refresh();
        }

        /// <summary>
        /// Metoda zapisuje utworzone obiekty w pliku XML przy zamknięciu okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            NotebookModel.SaveXml(notes.Notes, "listOfNotes");
        }

        /// <summary>
        /// Metoda wczytuje notatki z pliku XML po załadowaniu okna aplikacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<NoteModel> listNotes = new List<NoteModel>();
            listNotes = NotebookModel.ReadXml("listOfNotes");
            lstNotes.ItemsSource = listNotes;
            lstNotes.Items.Refresh();
        }

        private void lstNotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstNotes.SelectedIndex > -1)
            {
                NoteModel selectedItem = (NoteModel)lstNotes.SelectedItem;
                txtbTitle.Text = selectedItem.Title;
                txtbNote.Text = selectedItem.Note;
            }
        }

        private void btnSaveXML_Click(object sender, RoutedEventArgs e)
        {
            NotebookModel.SaveXml(notes.Notes, "listOfNotes");
        }


    }
}
