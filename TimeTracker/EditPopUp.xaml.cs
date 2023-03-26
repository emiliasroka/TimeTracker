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
    /// Logika interakcji dla pliku EditPopup.xaml
    /// </summary>
    public partial class EditPopUp : Window
    {
        /// <summary>
        /// Prywatna zmienna typu string.
        /// </summary>
        private string tempdesc;
        /// <summary>
        /// Konstruktor który ustawia DataContex na "item" dzięki czemu aplikacja automatycznie aktualizuje widok na podstawie danych z obiektu WorkTask.
        /// </summary>
        /// <param name="item"></param>
        public EditPopUp(WorkTask item)
        {
            InitializeComponent();
            DataContext = item;
            tempdesc = item.Description;
        }
        /// <summary>
        /// Ta metoda jest wywoływana, gdy okno jest zamykane. Sprawdza ona, czy zmienna DialogResult jest null lub false.
        /// Jeśli jest false, to ustawia właściwość Description obiektu WorkTask, który jest DataContext okna na wartość zmiennej o nazwie tempdesc.
        /// Zmienna tempdesc jest przyjmowana jako tymczasowy opis, który został ewentualnie zmodyfikowany przez użytkownika, ale został odrzucony przez użytkownika, gdy zamknął okno bez zapisywania zmian.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool result = DialogResult ?? false;
            if (!result)
                (DataContext as WorkTask).Description = tempdesc;
        }
        /// <summary>
        /// Ta metoda jest wywoływana po kliknięciu przycisku "OK". Ustawia zmienną DialogResult na true i następnie zamyka bieżące okno.
        /// Zmienna DialogResult jest używana do oznaczania wyniku okna dialogowego i często służy do określenia, czy użytkownik nacisnął przycisk OK czy Anuluj.
        /// W tym przypadku przycisk OK ustawia DialogResult na true, co oznacza, że użytkownik zaakceptował lub potwierdził zmiany lub dane wprowadzone w oknie dialogowym.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
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
        /// Ta metoda jest wywoływana po kliknięciu klawisza "Enter". Ustawia zmienną DialogResult na true i następnie zamyka bieżące okno.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DialogResult = true;
                Close();
            }

        }
    }
}
