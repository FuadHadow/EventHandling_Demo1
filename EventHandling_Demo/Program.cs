using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHandling_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer myTimer = new Timer();
            TimerSubscriber subscriber = new TimerSubscriber();
            subscriber.Subscribe(myTimer);

            myTimer.StartTimer(1000, 5);  // Ticks every second, 5 times
        }
    }

    public class Timer
    {
        public event TimerEventHandler TimerTick;

        public void StartTimer(int interval, int ticks)
        {
            int tickCount = 0;
            while (tickCount < ticks)
            {
                System.Threading.Thread.Sleep(interval);
                OnTimerTick(new TimerEventArgs { TickCount = ++tickCount });
            }
        }

        protected virtual void OnTimerTick(TimerEventArgs e)
        {
            TimerTick?.Invoke(this, e);
        }
    }

    public delegate void TimerEventHandler(object sender, TimerEventArgs e);

    public class TimerEventArgs : EventArgs
    {
        public int TickCount { get; set; }
    }

    public class TimerSubscriber
    {
        public void Subscribe(Timer timer)
        {
            timer.TimerTick += HandleTimerTick;
        }

        private void HandleTimerTick(object sender, TimerEventArgs e)
        {
            Console.WriteLine($"Tick {e.TickCount} received.");
        }
    }
}
