using System.Timers;
using System.Windows;
using Timer = System.Timers.Timer;

namespace Personal_Task_Manager.Services
{
    public class TaskTimerService : IDisposable
    {
        private readonly Timer _timer;
        private readonly Action _action;

        public TaskTimerService(int intervalMs, Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            _timer = new Timer(intervalMs) { AutoReset = true };
            _timer.Elapsed += Timer_Elapsed;

            _timer.Start();
        }

        public TaskTimerService(Action action) : this(1000, action)
        {
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            var dispatcher = Application.Current?.Dispatcher;
            if (dispatcher is not null)
                dispatcher.BeginInvoke(_action);
            else
                _action();
        }

        public void Dispose()
        {
            _timer.Stop();
            _timer.Elapsed -= Timer_Elapsed;
            _timer.Dispose();
        }
    }
}
