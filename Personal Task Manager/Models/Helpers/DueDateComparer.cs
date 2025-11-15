using Personal_Task_Manager.ViewModel;
using System.Collections;

namespace Personal_Task_Manager.Models.Helpers
{
    public class DueDateComparer : IComparer
    {
        private readonly Boolean _isAscending;
        public DueDateComparer(Boolean isAscending = true)
        {
            _isAscending = isAscending;
        }
        public int Compare(object? x, object? y)
        {
            if (x is not TaskItemViewModel taskItem1 || y is not TaskItemViewModel taskItem2)
                return 0;
            if (taskItem1.DueDate == null && taskItem2.DueDate == null)
                return 0;
            if (taskItem1.DueDate == null)
                return 1;
            if (taskItem2.DueDate == null)
                return -1;

            int result = taskItem1.DueDate > taskItem2.DueDate ? 1 : -1;

            if (taskItem1.DueDate == taskItem2.DueDate)
                result = taskItem1.Importance < taskItem2.Importance ? 1 : -1;

            return _isAscending ? result : -result;
        }
    }
}
