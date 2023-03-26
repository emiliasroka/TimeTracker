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
using System.Xml.Linq;

namespace TimeTracker
{

    public partial class SettingsWindow : Window
    {
        /// <summary>
        /// Statyczna zmienna globalna typu int przechowująca wartość dla cyklu pomodoro w ustawieniach, dostępna dla wszystkich elementów kodu w aplikacji bez konieczności tworzenia instancji klasy. 
        /// </summary>
        public static int settingsPomodoroValue = 25;
        /// <summary>
        /// Statyczna zmienna globalna typu int przechowująca wartość dla krótkiej przerwy w ustawieniach, dostępna dla wszystkich elementów kodu w aplikacji bez konieczności tworzenia instancji klasy. 
        /// </summary>
        public static int settingsShortBreakValue = 5;
        /// <summary>
        /// Statyczna zmienna globalna typu int przechowująca wartość dla długiej przerwy w ustawieniach, dostępna dla wszystkich elementów kodu w aplikacji bez konieczności tworzenia instancji klasy
        /// </summary>
        public static int settingsLongBreakValue = 15;


        public SettingsWindow()
        {
            InitializeComponent();
            ReadXML();
        }

        /// <summary>
        /// Metoda przypisująca domyślne ustawienia aplikacji.
        /// </summary>
        public void SetDefaultSettings()
        {
            settingsPomodoroValue = 25;
            settingsShortBreakValue = 5;
            settingsLongBreakValue = 15;
            SaveXML();
        }

        /// <summary>
        /// Metoda jest wykonywana za każdym razem gdy użytkownik zmieni wartość suwaka sldPomodoro i zmienia wartość zmiennej settingsPomodoroValue na wartość obecnego położenia suwaka.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sldPomodoro_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            settingsPomodoroValue = (int)sldPomodoro.Value;
        }
        /// <summary>
        /// /// Metoda jest wykonywana za każdym razem gdy użytkownik zmieni wartość suwaka sldShortBreak i zmienia wartość zmiennej settingsShortBreakValue na wartość obecnego położenia suwaka.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sldShortBreak_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            settingsShortBreakValue = (int)sldShortBreak.Value;
        }

        /// <summary>
        /// Metoda jest wykonywana za każdym razem gdy użytkownik zmieni wartość suwaka sldLongBreak i zmienia wartość zmiennej settingsLongBreakValue na wartość obecnego położenia suwaka.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sldLongBreak_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            settingsLongBreakValue = (int)sldLongBreak.Value;
        }

        /// <summary>
        /// Metoda, która jest wywoływana przy naciśnięciu przycisku btnSaveSettings i zapisuję ustawienia do XML, po czym zamyka okno.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            SaveXML();
            this.Close();
        }

        /// <summary>
        /// Metoda zapisująca dane do pliku XML.
        /// </summary>
        public void SaveXML()
        {
            XDocument settingsXml = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("settings",
                    new XElement("pomodoro", settingsPomodoroValue),
                    new XElement("shortbreak", settingsShortBreakValue),
                    new XElement("longbreak", settingsLongBreakValue)
                )
            );
            settingsXml.Save("settings.xml");
        }
        /// <summary>
        /// Metoda odczytujaca dane z pliku XML
        /// </summary>
        public void ReadXML()
        {
            if (System.IO.File.Exists("settings.xml"))
            {
                XDocument settingsXml = XDocument.Load("settings.xml");
                settingsPomodoroValue = (int)settingsXml.Root.Element("pomodoro");
                settingsShortBreakValue = (int)settingsXml.Root.Element("shortbreak");
                settingsLongBreakValue = (int)settingsXml.Root.Element("longbreak");
                sldPomodoro.Value = settingsPomodoroValue;
                sldShortBreak.Value = settingsShortBreakValue;
                sldLongBreak.Value = settingsLongBreakValue;
            }
        }

    }
}
