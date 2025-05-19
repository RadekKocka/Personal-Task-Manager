using Personal_Task_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Task_Manager.ViewModel
{
    public class TaskCheckListViewModel : BaseViewModel
    {
        private TaskCheckList _taskCheckList;

        public TaskCheckListViewModel(TaskCheckList taskCheckList)
        {
            _taskCheckList = taskCheckList;
        }
        public string Description
        {
            get => _taskCheckList.Description;
            set
            {
                _taskCheckList.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public bool IsComplete
        {
            get => _taskCheckList.IsComplete;
            set
            {
                _taskCheckList.IsComplete = value;
                OnPropertyChanged(nameof(IsComplete));
            }
        }
    }
}
