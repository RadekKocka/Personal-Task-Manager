using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Personal_Task_Manager.Services
{
    public class TaskTimerService : IDisposable
    {
        private DispatcherTimer _timer;

        public TaskTimerService(int ticks, Action action)
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(ticks)
            };
            _timer.Tick += (s, e) =>
            {
                action.Invoke();
            };
            _timer.Start();
        }

        public TaskTimerService(Action action) : this(1000, action)
        {
        }

        public void Dispose()
        {
            _timer.Stop();
            _timer.Tick -= (s, e) => { };
            _timer = null!;
        }
    }
}
