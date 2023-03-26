using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Classes
{
    /// <summary>
    /// Interfejs, który zawiera deklaracje metod, właściwości i zdarzenia, które bedzie implementować klasa odpowiedzialna za odliczanie czasu.
    /// </summary>
    interface ICountdown
    {
        /// <summary>
        /// Właściwość MinutesLeft pozwala na obranie informacji o pozostałym czasie odliczania w minutach.
        /// </summary>
        int MinutesLeft { get; }
        /// <summary>
        /// Właściwość SecondsLeft pozwala na obranie informacji o pozostałym czasie odliczania w sekundach.
        /// </summary>
        int SecondsLeft { get; }
        /// <summary>
        /// Właściwość IsTimeUp pozwala na sprawdzenie czy czas upłynął.
        /// </summary>
        bool IsTimeUp { get; }
        /// <summary>
        /// Metoda pozwalająca na ustawienie czasu czasomierza
        /// </summary>
        /// <param name="minutesToSet"></param>
        void SetTime(int minutesToSet);
        /// <summary>
        /// Metoda rozpoczynająca odliczanie czasomierza
        /// </summary>
        void StartCountdown();
        /// <summary>
        /// Metoda zatrzymująca odliczanie czasomierza
        /// </summary>
        void StopCountdown();
        /// <summary>
        /// Event pozwala na subskrybowanie zdarzenia, które jest wywoływane co określony interwał czasu.
        /// </summary>
        event EventHandler TickHappend;
    }
}