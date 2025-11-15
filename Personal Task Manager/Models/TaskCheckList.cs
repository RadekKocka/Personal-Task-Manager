using Personal_Task_Manager.ViewModel;

namespace Personal_Task_Manager.Models
{
    public class TaskCheckList : BaseViewModel 
    {
        private bool isComplete;

        public string Description { get; set; }
        public bool IsComplete
        {
            get => isComplete;
            set
            {
                SetProperty(ref isComplete, value);
            }
        }
    }
}
