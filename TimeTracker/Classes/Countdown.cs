using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace TimeTracker.Classes
{
    /// <summary>
    /// Publiczna klasa implementująca interfejs "ICountdown". Posiada  pola intervalSeconds, timeLeft i timer klasy DispatcherTimer. 
    /// Ma publiczne zdarzenie o nazwie TickHappend i dwie właściwości o nazwie MinutesLeft i SecondsLeft, obie zwracają wartość timeLeft.
    /// Klasa posiada konstruktor, który inicjalizuje obiekt Timer i przypisuje metodę Timer_Tick jako obsługę zdarzenia dla zdarzenia tick.
    /// Posiada metody takie jak Timer_Tick(), StartCountdown(), StopCountdown(), Skip(), SetTime(), IsTimeUp();
    /// </summary>
    public class Countdown : ICountdown
    {
        /// <summary>
        /// Stała przechowująca liczbę 60, jako liczbę sekund w ciągu minuty.
        /// </summary>
        private const int secondsPerMinute = 60;
        /// <summary>
        /// Obiekt TimeSpan, który reprezentuje interwał czasowy w postaci liczby godzin, minut i sekund. W tym przypadku, jest to interwał równy 
        /// 1 sekundzie (0 godzin, 0 minut, 1 sekunda). Ta zmienna jest używana do określenia częstotliwości wywoływania zdarzenia Timer_Tick.
        /// </summary>
        private TimeSpan intervalSeconds = new TimeSpan(0, 0, 1);
        /// <summary>
        /// Zmienna przechowująca czas, którzy pozostał do końca odliczania czasomierza.
        /// </summary>
        private int timeLeft;
        /// <summary>
        /// Obiekt klasy DispatcherTimer, który pozwala na wykonywanie określonych czynności co określony interwał czasowy, 
        /// który jest ustawiony przy pomocy zmiennej intervalSeconds
        /// </summary>
        private DispatcherTimer timer;

        /// <summary>
        /// Zmienna obliczająca ile minut pozostało do końca odliczania czasomierza.
        /// </summary>
        public int MinutesLeft => timeLeft / secondsPerMinute;
        /// <summary>
        /// Zmienna obliczająca ile sekund pozostało do końca odliczania czasomierza.
        /// </summary>
        public int SecondsLeft => timeLeft % secondsPerMinute;


        /// <summary>
        /// EventHandler jest klasą delegatów w C#, która pozwala na subskrybowanie i wywoływanie zdarzeń. Event "TickHappend" jest wywoływany w metodzie Timer_Tick gdy licznik czasu 
        /// pozostałego zostaje zmniejszony o 1. Ten event przesyła informację do innych elementów aplikacji, że zdarzenie Timer_Tick zaszło.
        /// </summary>
        public event EventHandler TickHappend;


        public Countdown()
        {
            this.timer = new DispatcherTimer();
            this.timer.Interval = intervalSeconds;
            this.timer.Tick += Timer_Tick;

        }
        /// <summary>
        /// Metoda jest obsługą zdarzenia dla obiektu DispatcherTimer o nazwie "timer". Metoda ta jest wywoływana co określony interwał 
        /// czasu przez obiekt timer, zwiększa licznik czasu pozostałego o 1 (timeLeft--) i wywołuje zdarzenie "TickHappend" z argumentami "this".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            this.timeLeft--;
            TickHappend?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Właściwość pozwalająca na sprawdzenie czy czas upłynął.
        /// </summary>
        public bool IsTimeUp => this.timeLeft == 0;

        /// <summary>
        /// Metoda ustawiająca czas czasomierza.
        /// </summary>
        /// <param name="minutesToSet"></param>
        public void SetTime(int minutesToSet)
        {
            this.timeLeft = minutesToSet * secondsPerMinute;
        }
        /// <summary>
        /// Metoda pomijająca czas odliczania czasomierza. Właściwie ustawia go na 1 sekundę przed końcem.
        /// </summary>
        /// <returns></returns>
        public int Skip()
        {
            return this.timeLeft = 1;
        }
        /// <summary>
        /// Metoda startująca czasomierz.
        /// </summary>
        public void StartCountdown()
        {
            this.timer.Start();
        }
        /// <summary>
        /// Metoda zatrzymująca czasomierz.
        /// </summary>
        public void StopCountdown()
        {
            this.timer.Stop();
        }
    }
}
