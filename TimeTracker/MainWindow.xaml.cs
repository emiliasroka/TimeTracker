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

namespace TimeTracker
{
    /// <summary>
    /// Klasa typu public partial dotycząca głównego okna aplikacji. Zawiera listę z cytatami oraz ma przypisane metody otwierania kolejnych okien przy naciśnięciu guzików.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowRandomMotivationalQuote();
        }

        private readonly List<string> quotes = new List<string>
        {
            "Learning is a process, not a product. - John Dewey",
            "Learning is like a puzzle. Each piece is important, but only when all of them are put together does it form a complete picture. - Robert A. Heinlein",
            "Learning is like a journey. You have to start with the first step and then go on. - Confucius",
            "Learning is like a marathon, not a sprint. Don't try to learn everything at once, but gradually and systematically. - Brian Tracy",
            "Learning is like jumping on a trampoline. The more energy you put in, the higher you will soar. - Steven Kotler",
            "Learning is like opening a door. The more you know, the more there is to gain. - Neil deGrasse Tyson",
            "Learning is like a chain. Each individual link is important, but only when they are connected together does it create something durable. - Leonardo da Vinci",
            "Learning is like a rock garden. The more layers you discover, the deeper your understanding. - Ruth A. Newton",
            "Learning is like climbing. You need a goal, a plan, and determination to reach the summit. - Edmund Hillary",
            "Learning is like a wave. You have to ride it, not fight it, to reach your goal. - Bruce Lee"
        };
        /// <summary>
        /// Konstruktor, który inicjalizuje składniki interfejsu użytkownika (komponenty) i wywołuje metodę ShowRandomMotivationalQuote, która wyświetla losowy cytat motywacyjny z listy quotes.
        /// </summary>
        private void ShowRandomMotivationalQuote()
        {
            var random = new Random();
            var randomQuote = quotes[random.Next(quotes.Count)];
            tblQuotes.Text = randomQuote;
        }

        /// <summary>
        /// Metoda obsługiwana przez przycisk btnToDoList_Click, która otwiera okno z ToDoListą.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToDoList_Click(object sender, RoutedEventArgs e)
        {
            ToDoListMainWindow toDoLisitMainWindow = new ToDoListMainWindow();
            toDoLisitMainWindow.Show();
        }
        /// <summary>
        /// Metoda obsługiwana przez przycisk btnPomodoroTimer_Click, która otwiera okno z czasomierzem pomodoro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPomodoroTimer_Click(object sender, RoutedEventArgs e)
        {
            PomodoroMainWindow pomodoroMainWindow = new PomodoroMainWindow();
            pomodoroMainWindow.Show();
        }

        /// <summary>
        /// Metoda obsługiwana przez przycisk btnNotebook_Click, która otwiera okno z notatnikiem.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNotebook_Click(object sender, RoutedEventArgs e)
        {
            NotepadMainWindow notepadMainWindow = new NotepadMainWindow();
            notepadMainWindow.Show();
        }
    }
}
