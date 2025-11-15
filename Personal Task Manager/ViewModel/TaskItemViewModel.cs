using Personal_Task_Manager.Models;
using Personal_Task_Manager.Models.Enums;
using Personal_Task_Manager.ViewModel.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Personal_Task_Manager.ViewModel
{
    public class TaskItemViewModel : BaseViewModel
    {
        #region Fields
        private readonly TaskItem _model;
        #endregion

        #region Constructor
        public TaskItemViewModel(TaskItem taskItem)
        {
            _model = taskItem ?? throw new ArgumentNullException(nameof(taskItem));
            OnPropertyChanged(nameof(SubTasks));
            AddSubTaskCommand = new RelayCommand(_ => AddSubTask());
        }
        #endregion

        #region Properties
        public TaskItem Model => _model;

        public string Title
        {
            get => _model.Title;
            set
            {
                if (_model.Title == value) return;
                _model.Title = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _model.Description;
            set
            {
                if (_model.Description == value) return;
                _model.Description = value;
                OnPropertyChanged();
            }
        }

        public DateTime? DueDate
        {
            get => _model.DueDate;
            set
            {
                if (_model.DueDate == value) return;
                _model.DueDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(RemainingTimeFormatted));
            }
        }

        public DateTime StartDate
        {
            get => _model.StartDate;
            set
            {
                if (_model.StartDate == value) return;
                _model.StartDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ElapsedTimeFormatted));
            }
        }

        public DateTime EndDate
        {
            get => _model.EndDate;
            set
            {
                if (_model.EndDate == value) return;
                _model.EndDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ElapsedTimeFormatted));
            }
        }

        public bool IsComplete => _model.IsComplete;

        public TaskState State
        {
            get => _model.State;
            set
            {
                if (_model.State == value) return;
                _model.State = value;
                OnPropertyChanged();
            }
        }

        public TaskCategory Category
        {
            get => _model.Category;
            set
            {
                if (_model.Category == value) return;
                _model.Category = value;
                OnPropertyChanged();
            }
        }

        public TaskImportance Importance
        {
            get => _model.Importance;
            set
            {
                if (_model.Importance == value) return;
                _model.Importance = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TaskCheckList> SubTasks => new(_model.SubTasks);

        public string ElapsedTimeFormatted => FormatTimeSpan(GetTaskElapsedTime());
        public string RemainingTimeFormatted => FormatTimeSpan(GetRemainingTime());

        #endregion

        #region Commands
        public ICommand AddSubTaskCommand { get; }

        #endregion
        #region Methods
        private static string FormatTimeSpan(TimeSpan ts)
        {
            ts = ts.Duration();

            int days = ts.Days;
            int hours = ts.Hours;
            int minutes = ts.Minutes;
            int seconds = ts.Seconds;

            if (days > 0)
            {
                return $"{days}d {hours:D2}:{minutes:D2}:{seconds:D2}";
            }

            int totalHours = (int)ts.TotalHours;
            return $"{totalHours:D2}:{minutes:D2}:{seconds:D2}";
        }

        public TimeSpan GetTaskElapsedTime()
        {
            if (IsComplete)
            {
                return EndDate - StartDate;
            }
            else
            {
                if (StartDate > DateTime.Now)
                    return TimeSpan.Zero;

                return DateTime.Now - StartDate;
            }
        }

        public TimeSpan GetRemainingTime()
        {
            if (DueDate.HasValue)
            {
                if (DueDate.Value < DateTime.Now)
                    return TimeSpan.Zero;
                return DueDate.Value - DateTime.Now;
            }
            return TimeSpan.Zero;
        }

        public void CompleteTask()
        {
            State = TaskState.Complete;
            EndDate = DateTime.Now;
        }

        public void CompleteSubTasks()
        {
            foreach (var subTask in SubTasks)
            {
                subTask.IsComplete = true;
            }
        }

        private void AddSubTask()
        {
            //TODO : Implement a dialog to get subtask details from the user
            var addTaskWindow = new Views.AddSubTaskWindow(this);
            if (addTaskWindow.ShowDialog() == true)
            {
                _model.SubTasks.Add(addTaskWindow.SubTask);
            }
            OnPropertyChanged(nameof(SubTasks));
        }
        #endregion

    }
}
