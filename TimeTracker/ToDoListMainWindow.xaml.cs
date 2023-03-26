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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeTracker.Classes;


namespace TimeTracker
{
    /// <summary>
    /// Logika interakcji dla pliku MainWindow.xaml
    /// </summary>
    public partial class ToDoListMainWindow : Window
    {
        /// <summary>
        /// Inicjowanie obiektu klasy WorkTasksList
        /// </summary>
        WorkTasksList wtl = new WorkTasksList();
        /// <summary>
        /// Konstruktor który ustawia DataContex na "wtl" dzięki czemu aplikacja automatycznie aktualizuje widok na podstawie danych z obiektu WorkTasksList.
        /// </summary>
        public ToDoListMainWindow()
        {
            InitializeComponent();
            DataContext = wtl;
        }
        /// <summary>
        /// Metoda, która odpowiada za działanie przycisku "Add". Sprzwdza czy treść zadania została podana, jeżeli tak to uruchamia metodę "AddItem". Na koniec odświeża okno z listą i czyści pole tekstowe.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (!txtItemDesc.Text.Trim().Equals(string.Empty))
            {
                wtl.Additem(txtItemDesc.Text.Trim());
            }

            lvToDo.Items.Refresh();
            txtItemDesc.Text = "";

        }
        /// <summary>
        /// Jest to metoda, która jest wywoływana po kliknięciu przycisku o nazwie "Done". Sprawdza ona czy jakiś element znajduje się na liście "lvToDo". Jeśli tak, to wyświetla okno z pytaniem czy użytkownik chce oznaczyć ten element jako wykonany. Jeśli użytkownik wybierze tak, właściwość "DoneDateTime" wybranego elementu (który jest typu "WorkTask") jest ustawiana na aktualną datę i czas w formacie "yyyy-MM-ddThh:mm:ss.ms". W końcu metoda wywołuje metodę "Refresh" na liście "lvToDo", aby zaktualizować wyświetlanie elementów.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Done(object sender, RoutedEventArgs e)
        {
            if (lvToDo.SelectedItem != null)
            {
                MessageBoxResult done = MessageBox.Show("This as done?", "Done?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (done == MessageBoxResult.Yes)
                {
                    (lvToDo.SelectedItem as WorkTask).DoneDateTime = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.ms");
                }
                lvToDo.Items.Refresh();
            }
        }
        /// <summary>
        /// Zdarzenie które pzrypisane jest do podwójnego kliknięcia myszką. Po podwójnym kliknięciu uruchamia metodę "Done"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvToDo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Done(sender, e);
        }
        /// <summary>
        /// metoda jest wywoływana po kliknięciu na opcję "Edit" w menu. Sprawdza ona, czy jakiś element znajduje się na liście "lvToDo". Jeśli tak, to tworzy nowe okno dialogowe (klasa "EditPopUp") z wybranym elementem (który jest typu "WorkTask") jako argument. Następnie ustawia okno dialogowe jako "Owner" tego okna i wyświetla go. Na końcu metoda wywołuje metodę "Refresh" na liście "lvToDo" aby odświeżyć widok elementów.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditMenu_Click(object sender, RoutedEventArgs e)
        {
            if (lvToDo.SelectedItem != null)
            {
                EditPopUp ep = new EditPopUp(lvToDo.SelectedItem as WorkTask);
                ep.Owner = this;
                ep.ShowDialog();
            }
            lvToDo.Items.Refresh();
        }
        /// <summary>
        /// Ta metoda jest wywoływana po kliknięciu przycisku "Delete". Sprawdza ona, czy jakiś element znajduje się na liście "lvToDo". Jeśli tak, to wyświetla okno z pytaniem czy użytkownik chce usunąć wybrany element. Jeśli użytkownik wybierze tak, wywołuje metodę "DeleteItem" na obiekcie "wtl" przekazując do niej wybrany element (który jest typu "WorkTask") jako argument. W końcu metoda wywołuje metodę "Refresh" na liście "lvToDo", aby odświeżyć widok elementów.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (lvToDo.SelectedItem != null)
            {
                MessageBoxResult del = MessageBox.Show("Delete this item?", "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (del == MessageBoxResult.Yes)
                {
                    wtl.DeleteItem(lvToDo.SelectedItem as WorkTask);
                }
                lvToDo.Items.Refresh();
            }
        }
        /// <summary>
        /// Możliwość przemieszczania okna za pomocą myszki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        /// <summary>
        /// Możliwość minimalizacji okna za pomocą przycisku.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// Możliwość maksymalizacji okna za pomocą przycisku.
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
        /// Zamknięcie okna za pomocą przycisku i dodatkowy zapis do pliku xml.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            wtl.ZapisXML();
            Close();
        }
        /// <summary>
        /// Metoda, która uruchamia się po kliknięciu klawisza "enter". Sprzwdza czy treść zadania została podana, jeżeli tak to uruchamia metodę "AddItem". Na koniec odświeża okno z listą i czyści pole tekstowe.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtItemDesc_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!txtItemDesc.Text.Trim().Equals(string.Empty))
                {
                    wtl.Additem(txtItemDesc.Text.Trim());
                }
                lvToDo.Items.Refresh();
                txtItemDesc.Text = "";
            }
        }
    }
}