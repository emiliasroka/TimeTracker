using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimeTracker.Classes;

namespace TimeTracker
{
    /// <summary>
    /// Klasa typu public partial, która reprezentuje główne okno aplikacji Pomodoro. Zawiera kilka zmiennych typu int, które przechowują chociażby długość czasów dla różnych etapów pracy(pomodoro, 
    /// krótka przerwa, długa przerwa) z obiektu "SettingsWindow", zmienną "timerSound" przechowującą odwołanie do obiektu służącego do odtwarzania dźwięków, a "countdown" przechowuje odwołanie do 
    /// obiektu reprezentującego odliczanie czasu. Zawiera metody służące do przełączania się między czasomierzami oraz do używania GUI.
    /// </summary>
    public partial class PomodoroMainWindow : Window
    {
        /// <summary>
        /// Zmienna typu int, która przechowuje wartość właściwości "settingsPomodoroValue" z obiektu "SettingsWindow".Wartość reprezentuje długość czasu, w minutach, dla okresu "pomodoro".
        /// </summary>
        int pomodoroMinutes = SettingsWindow.settingsPomodoroValue;
        /// <summary>
        /// Zmienna typu int, która przechowuje wartość właściwości "settingsShortBreakValue" z obiektu "SettingsWindow".Wartość reprezentuje długość czasu, w minutach, dla okresu "shortTimeBreak".
        /// </summary>
        int shortBreakMinutes = SettingsWindow.settingsShortBreakValue;
        /// <summary>
        /// Zmienna typu int, która przechowuje wartość właściwości "settingsPomodoroValue" z obiektu "settingsLongBreakValue".Wartość reprezentuje długość czasu, w minutach, dla okresu "longTimeBreak".
        /// </summary>
        int longBreakMinutes = SettingsWindow.settingsLongBreakValue;
        /// <summary>
        /// Zmienna typu "SoundPlayer", która przechowuje odwołanie do obiektu służącego do odtwarzania dźwięków.
        /// </summary>
        SoundPlayer timerSound;
        /// <summary>
        /// Zmienna typu Countdown, która przechowuje odwołanie do obiektu reprezentującego odliczanie czasu.
        /// </summary>
        Countdown countdown;

        /// <summary>
        /// Zmienna typu int, która przechowuje unikalny identyfikator dla timera.
        /// </summary>
        int timerId = 1;
        /// <summary>
        /// Zmienna typu int przypisująca licznikowi wartość 0.
        /// </summary>
        int counter = 0;
        /// <summary>
        /// Zmienna typu int, przechowująca wartość, która reprezentuje liczbę zakończonych cykli pomodoro. 
        /// </summary>
        int pomodoroCounter = 0;
        /// <summary>
        /// Zmienna typu int, przechowująca wartość, która reprezentuje liczbę zakończonych krótkich przerw. 
        /// </summary>
        int shortBreakCounter = 0;
        /// <summary>
        /// Zmienna typu int, przechowująca wartość, która reprezentuje liczbę zakończonych długich przerw. 
        /// </summary>
        int longBreakCounter = 0;
        /// <summary>
        /// Prywatna zmienna typu SettingsWindow, która przechowuje odwołanie do nowej instancji klasy SettingsWindow.
        /// </summary>
        private SettingsWindow settingsWindow = new SettingsWindow();
        /// <summary>
        /// 
        /// </summary>
        public PomodoroMainWindow()
        {
            InitializeComponent();
            this.countdown = new Countdown();
            this.countdown.TickHappend += Countdown_TickHappend;
            timerSound = new SoundPlayer("alm_RingTone.wav");

            settingsWindow.SetDefaultSettings();
            settingsWindow.ReadXML();

            SetTimer(pomodoroMinutes);

            UpdateTimerOnScreen();
        }

        /// <summary>
        /// Metoda która jest uruchamiana po kliknięciu przycisku o nazwie btnPomodoro. W metodzie wywołuje się cykl czasomierza pomodoro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPomodoro_Click(object sender, EventArgs e)
        {
            Pomodoro();
        }
        /// <summary>
        /// Metoda która jest uruchamiana po kliknięciu przycisku o nazwie btnShortBreak. W metodzie wywołuje się czasomierz z krótką przerwą.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShortBreak_Click(object sender, EventArgs e)
        {
            ShortBreak();
        }
        /// <summary>
        /// Metoda która jest uruchamiana po kliknięciu przycisku o nazwie btnLongBreak. W metodzie wywołuje się czasomierz z długą przerwą.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLongBreak_Click(object sender, EventArgs e)
        {
            LongBreak();
        }
        /// <summary>
        ///  Metoda, która jest uruchamiana po kliknięciu przycisku o nazwie btnStartStop. Metoda jest odpowiedzialna za rozpoczęcie lub 
        ///  zatrzymanie odliczania czasu przez aplikację, po naciśnięciu przycisku START/STOP.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartStop_Click(object sender, EventArgs e)
        {

            if (btnStartStop.Content.ToString() == "START")
            {
                this.countdown.StartCountdown();
                SetStartStopBtnAsStop();
            }
            else if (btnStartStop.Content.ToString() == "STOP")
            {
                this.countdown.StopCountdown();
                SetStartStopBtnAsStart();
            }
        }
        /// <summary>
        /// Metoda, która jest uruchamiana po kliknięciu przycisku o nazwie btnSkip, pomija aktualny cykl czasomierza.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSkip_Click(object sender, RoutedEventArgs e)
        {
            this.countdown.StartCountdown();
            SkipTimer();
            UpdateTimerOnScreen();
        }
        /// <summary>
        /// Metoda, która jest uruchamiana po kliknięciu przycisku o nazwie btnReset, resetuję bierzący czasomierz.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            switch (timerId)
            {
                case 1:
                    Pomodoro();
                    break;
                case 2:
                    ShortBreak();
                    break;
                case 3:
                    LongBreak();
                    break;
            }
        }
        /// <summary>
        /// Metoda, która jest uruchamiana po kliknięciu przycisku o nazwie btnSettings. Po uruchomieniu tworzona jest nowa instancja okna o nazwie 
        /// "SettingsWindow" i przypisywana do zmiennej "settingsWindow". Następnie do zdarzenia "Closed" tego okna jest dodawany delegat "settingsWindow_Closed", 
        /// który będzie wykonywany po zamknięciu okna. Na końcu okno jest wyświetlane na ekranie poprzez wywołanie metody "Show()".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Closed += settingsWindow_Closed;
            settingsWindow.Show();
        }

        /// <summary>
        /// Metoda, która zapisuję ustawienia po zamknięciu okna settingsWindow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingsWindow_Closed(object sender, EventArgs e)
        {
            SetSettings();
        }
        /// <summary>
        /// Metoda, która po zamknięciu głównego okna zapisuję ustawienia na domyślne. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PomodoroMainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            settingsWindow.SetDefaultSettings();
            settingsWindow.SaveXML();
        }
        /// <summary>
        /// Metoda, któa po naciśnięciu przycisku STOP zamienia jego kolor i nazwę na START.
        /// </summary>
        private void SetStartStopBtnAsStart()
        {
            btnStartStop.Content = "START";
            btnStartStop.Background = new SolidColorBrush(Color.FromRgb(37, 116, 77));
        }
        /// <summary>
        /// /Metoda, któa po naciśnięciu przycisku START zamienia jego kolor i nazwę na STOP.
        /// </summary>
        private void SetStartStopBtnAsStop()
        {
            btnStartStop.Content = "STOP";
            btnStartStop.Background = new SolidColorBrush(Color.FromRgb(55, 174, 115));
        }

        /// <summary>
        /// Metoda typu void o nazwie, która jest uruchamiana, gdy wystąpi zdarzenie tick na obiekcie countdown. Aktualizuje ona licznik wyświetlany 
        /// na ekranie przez wywołanie metody UpdateTimerOnScreen. Następnie sprawdza wartość zmiennej timerId i odpowiada za odpowiednie działanie algorytmu pomodoro (po trzech krótkich przerwach następuje długa przerwa).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Countdown_TickHappend(object sender, EventArgs e)
        {
            UpdateTimerOnScreen();
            if (timerId == 1)
            {
                if (this.countdown.IsTimeUp == true && counter < 4)
                {
                    pomodoroCounter++;
                    counter++;
                    timerSound.Play();
                    BackgroundColorAnimation();
                    Counters();
                    MessageBox.Show("Time to take a short break!", "Timer");
                    ShortBreak();

                }
                else if (this.countdown.IsTimeUp == true && counter >= 4)
                {
                    pomodoroCounter++;
                    timerSound.Play();
                    BackgroundColorAnimation();
                    Counters();
                    MessageBox.Show("Time to take a long break!", "Timer");
                    LongBreak();
                }
            }
            if (timerId == 2)
            {
                if (this.countdown.IsTimeUp == true)
                {
                    shortBreakCounter++;
                    counter++;
                    timerSound.Play();
                    BackgroundColorAnimation();
                    Counters();
                    MessageBox.Show("Your break finished! Time to focus again.", "Timer");
                    Pomodoro();
                }
            }
            if (timerId == 3)
            {
                if (this.countdown.IsTimeUp == true)
                {
                    longBreakCounter++;
                    counter = 0;
                    timerSound.Play();
                    BackgroundColorAnimation();
                    Counters();
                    MessageBox.Show("Your break finished! Time to focus again.", "Timer");
                    Pomodoro();
                }
            }

        }
        /// <summary>
        /// Metoda służąca do aktualizowania licznika wyświetlanego na ekranie
        /// </summary>
        private void UpdateTimerOnScreen()
        {
            tblMinutes.Text = this.countdown.MinutesLeft.ToString("00");
            tblSeconds.Text = this.countdown.SecondsLeft.ToString("00");
        }
        /// <summary>
        /// Metoda rozpoczynająca odliczanie czasomierza.
        /// </summary>
        /// <param name="minutesToCountdown"></param>
        private void StartCountdown(int minutesToCountdown)
        {
            if (btnStartStop.Content.ToString() == "STOP")
            {
                this.countdown.SetTime(minutesToCountdown);
                UpdateTimerOnScreen();
                this.countdown.StartCountdown();
            }
        }
        /// <summary>
        /// Metoda ustawiająca czas czasomierza.
        /// </summary>
        /// <param name="minutes"></param>
        private void SetTimer(int minutes)
        {
            this.countdown.StopCountdown();
            SetStartStopBtnAsStart();
            this.countdown.SetTime(minutes);
            UpdateTimerOnScreen();
            if (btnStartStop.Content.ToString() == "STOP")
                StartCountdown(minutes);
        }
        /// <summary>
        /// Metoda służąca do ustawienia ustawień. 
        /// </summary>
        private void SetSettings()
        {
            pomodoroMinutes = SettingsWindow.settingsPomodoroValue;
            shortBreakMinutes = SettingsWindow.settingsShortBreakValue;
            longBreakMinutes = SettingsWindow.settingsLongBreakValue;
            SetTimer(pomodoroMinutes);
        }

        /// <summary>
        /// Metoda pomijająca wybrany czasomierz.
        /// </summary>
        private void SkipTimer()
        {
            this.countdown.Skip();
        }
        /// <summary>
        /// Metoda ustawiająca czasomierz na cykl Pomodoro.
        /// </summary>
        private void Pomodoro()
        {
            SetTimer(pomodoroMinutes);
            timerId = 1;
        }
        /// <summary>
        /// Metoda ustawiająca czasomierz na krótką przerwę.
        /// </summary>
        private void ShortBreak()
        {
            SetTimer(shortBreakMinutes);
            timerId = 2;
        }
        /// <summary>
        /// Metoda ustawiająca czasomierz na długą przerwę.
        /// </summary>
        private void LongBreak()
        {
            SetTimer(longBreakMinutes);
            timerId = 3;
        }
        /// <summary>
        /// Metoda służąca do aktualizacji liczników wyświetlanych na ekranie.
        /// </summary>
        private void Counters()
        {
            tblPomodoroCounter.Text = "POMODOROS: " + pomodoroCounter.ToString();
            tblShortBreakCounter.Text = "SHORT BREAKS: " + shortBreakCounter.ToString();
            tblLongBreakCounter.Text = "LONG BREAKS: " + longBreakCounter.ToString();
        }
        /// <summary>
        /// Metoda do animacji koloru tła.
        /// </summary>
        private void BackgroundColorAnimation()
        {
            ColorAnimation ca1 = new ColorAnimation(Color.FromRgb(6, 196, 174), new Duration(TimeSpan.FromSeconds(2)));
            ColorAnimation ca2 = new ColorAnimation(Color.FromRgb(127, 206, 127), new Duration(TimeSpan.FromSeconds(2)));
            MainStackPanel.Background = new SolidColorBrush(Color.FromRgb(127, 206, 127));
            MainStackPanel.Background.BeginAnimation(SolidColorBrush.ColorProperty, ca1);
            MainStackPanel.Background = new SolidColorBrush(Color.FromRgb(6, 196, 174));
            MainStackPanel.Background.BeginAnimation(SolidColorBrush.ColorProperty, ca2);
        }

    }
}
