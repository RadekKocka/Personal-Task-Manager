using Personal_Task_Manager.Models;
using Personal_Task_Manager.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Personal_Task_Manager.ViewModel
{
    public class AddSubTaskItemViewModel : BaseViewModel
    {
        #region Fields
        private string _taskDescription;
        #endregion
        #region Constructor
        public AddSubTaskItemViewModel(Func<Boolean, TaskCheckList> isCanceledAction)
        {
            // create and store the new subtask before invoking the close delegate so the Window can retrieve it
            SaveSubTaskCommand = new RelayCommand(_ =>
            {
                CreatedSubTask = CreateSubTask();
                isCanceledAction?.Invoke(false);
            }, _ => !String.IsNullOrEmpty(TaskDescription));

            CancelCommand = new RelayCommand(_ => isCanceledAction?.Invoke(true));
        }

        #region Properties
        public String TaskDescription
        {
            get => _taskDescription;
            set => SetProperty(ref _taskDescription, value);
        }

        // Expose the created subtask so the Window can read it after the VM triggers the close delegate
        public TaskCheckList CreatedSubTask { get; private set; }
        #endregion

        #region Commands
        public ICommand SaveSubTaskCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        private TaskCheckList CreateSubTask()
        {
            var newSubTask = new TaskCheckList
            {
                Description = TaskDescription,
                IsComplete = false
            };
            return newSubTask;
        }
        #endregion

    }
}
