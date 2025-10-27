using Personal_Task_Manager.Models;
using Personal_Task_Manager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Task_Manager.ViewModel
{
    public class TimeControlViewModel : BaseViewModel
    {
        private readonly TaskTimerService _taskTimerService;
        TaskItem _task;
        public TimeControlViewModel(TaskItem task)
        {
            _taskTimerService = new TaskTimerService(UpdateTimes);
            _task = task;
        }

        public String ElapsedTime => _task.ElapsedTimeFormatted;

        public String RemainingTime => _task.RemainingTimeFormatted;

        public String CurrentTime => DateTime.Now.ToString("HH:mm:ss");

        private void UpdateTimes()
        {
            OnPropertyChanged(nameof(CurrentTime));
            OnPropertyChanged(nameof(ElapsedTime));
            OnPropertyChanged(nameof(RemainingTime));
        }
    }
}
