using System;
using System.Collections.ObjectModel;
using Personal_Task_Manager.Models;
using Personal_Task_Manager.Models.Enums;
using Personal_Task_Manager.ViewModel;

namespace Personal_Task_Manager.ViewModel
{
    public class TaskItemViewModel : BaseViewModel
    {
        private readonly TaskItem _taskItem;
        public ObservableCollection<TaskCheckListViewModel> SubTasks { get; }

        public TaskItem Model => _taskItem;

        public TaskItemViewModel(TaskItem taskItem)
        {
            _taskItem = taskItem;

            SubTasks = new ObservableCollection<TaskCheckListViewModel>();
            foreach (var subTask in _taskItem.SubTasks)
            {
                SubTasks.Add(new TaskCheckListViewModel(subTask));
            }
        }

        public int Id => _taskItem.Id;

        public string Title
        {
            get => _taskItem.Title;
            set
            {
                if (_taskItem.Title != value)
                {
                    _taskItem.Title = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? Description
        {
            get => _taskItem.Description;
            set
            {
                if (_taskItem.Description != value)
                {
                    _taskItem.Description = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DueDate
        {
            get => _taskItem.DueDate;
            set
            {
                if (_taskItem.DueDate != value)
                {
                    _taskItem.DueDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime StartDate
        {
            get => _taskItem.StartDate;
            set
            {
                if (_taskItem.StartDate != value)
                {
                    _taskItem.StartDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime EndDate
        {
            get => _taskItem.EndDate;
            set
            {
                if (_taskItem.EndDate != value)
                {
                    _taskItem.EndDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Deleted
        {
            get => _taskItem.Deleted;
            set
            {
                if (_taskItem.Deleted != value)
                {
                    _taskItem.Deleted = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Archived
        {
            get => _taskItem.Archived;
            set
            {
                if (_taskItem.Archived != value)
                {
                    _taskItem.Archived = value;
                    OnPropertyChanged();
                }
            }
        }

        public TimeSpan TaskElapsedTime
        {
            get => _taskItem.TaskElapsedTime;
            set
            {
                if (_taskItem.TaskElapsedTime != value)
                {
                    _taskItem.TaskElapsedTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public TaskState State
        {
            get => _taskItem.State;
            set
            {
                if (_taskItem.State != value)
                {
                    _taskItem.State = value;
                    OnPropertyChanged();
                }
            }
        }

        public TaskCategory Category
        {
            get => _taskItem.Category;
            set
            {
                if (_taskItem.Category != value)
                {
                    _taskItem.Category = value;
                    OnPropertyChanged();
                }
            }
        }

        public TaskImportance Importance
        {
            get => _taskItem.Importance;
            set
            {
                if (_taskItem.Importance != value)
                {
                    _taskItem.Importance = value;
                    OnPropertyChanged();
                }
            }
        }

        public void CompleteTask()
        {
            _taskItem.CompleteTask();
            foreach (var subTask in SubTasks)
            {
                subTask.IsComplete = true;
            }
        }

        public TimeSpan GetElapsedTime()
        {
            return _taskItem.GetElapsedTime();
        }
    }
}
