using System;
using System.Collections.Generic;
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
    /// Deklaracja klasy "NoteWindow", która jest pochodną klasy "Window" z biblioteki systemowej "System.Windows". 
    /// </summary>
    /// 
    [Serializable]
    public partial class NoteWindow : Window
    {
        /// <summary>
        /// Konstruktor wywołujący metodę InitializeComponent(), która inicjalizuje komponenty interfejsu użytkownika
        /// </summary>
        public NoteWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Obiekt jest instancją klasy NoteModel, która jest używana do przechowywania informacji o notatce, takiej jak tytuł i treść notatki
        /// </summary>
        /// 
        NoteModel note = new NoteModel();

        /// <summary>
        /// Obiekt jest instancją klasy NotebookModel, która jest używana do przechowywania informacji o liście notatek.
        /// </summary>
        NotebookModel notes = new NotebookModel();

        /// <summary>
        /// Konstruktor parametryczny, przyjmuje jeden parametr, który jest obiektem typu NoteModel
        /// </summary>
        /// <param name="n"></param>
        public NoteWindow(NoteModel n) : this()
        {
            note = n;
            txtTitle.Text = n.Title;
            txtNote.Text = n.Note;
        }

        /// <summary>
        /// Metoda obsługiwana przez przycisk Save, która zapisuje wartości TextBox w oknie NoteWindow i dodaje je do pól notatki. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            {
                try
                {
                    if (txtNote.Text != "" && txtTitle.Text != "")
                    {
                        if (txtTitle.Text != "" || txtNote.Text != "")
                        {
                            note.Title = txtTitle.Text;
                            note.Note = txtNote.Text;
                            DialogResult = true;
                        }
                        else
                        {
                            DialogResult = false;
                        }
                    }
                    else
                    {
                        throw new NoValueException("All fields must be completed!");
                    }
                }
                catch (NoValueException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Metoda minimalizuje okno NotepadMainWindow, obsługiwana przez przycisk.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Metoda zamyka okno NotepadMainWindow, obsługiwana przez przycisk.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Metoda maksymalizuje okno NotepadMainWindow, obsługiwana przez przycisk.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Metoda obsługuje naciśnięcie kursora nad obszarem Border
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }


    }
}
