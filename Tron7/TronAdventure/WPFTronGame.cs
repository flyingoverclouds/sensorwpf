using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;
using TronGame;

namespace TronAdventure
{
    public class WPFTronGame : BaseTronGame
    {
        public WPFTronGame(int playerCount, int colCount, int rowCount, int speed)
            : base(playerCount, colCount, rowCount, speed)
        {
        }

        DispatcherTimer _timer;

        protected override void CreateTimer(int ms)
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, ms);
            _timer.Tick += new EventHandler(TickTimer);
        }

        protected override void StartTimer()
        {
            _timer.Start();
        }

        protected override void StopTimer()
        {
            _timer.Stop();
        }


        /// <summary>
        /// Speed (as ms intervale between two move)
        /// 
        /// <remarks>Change by Nicolas CLERC for Sensor support</remarks>
        /// </summary>
        public int Speed
        {
            set
            {
                _timer.Interval = new TimeSpan(0, 0, 0, 0, value);
            }

            get
            {
                return Convert.ToInt32(_timer.Interval.TotalMilliseconds);
            }
        }

    }
}
