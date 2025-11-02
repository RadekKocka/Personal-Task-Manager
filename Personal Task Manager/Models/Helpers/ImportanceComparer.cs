using System.Collections;

namespace Personal_Task_Manager.Models.Helpers
{
    internal class ImportanceComparer : IComparer
    {
        private readonly Boolean _isAscending;
        public ImportanceComparer(Boolean isAscending = true)
        {
            _isAscending = isAscending;
        }
        public int Compare(object? x, object? y)
        {
            var taskImportanceX = x as TaskItem;
            var taskImportanceY = y as TaskItem;
            if (taskImportanceX == null || taskImportanceY == null)
                return 0;

            int result = taskImportanceX.Importance < taskImportanceY.Importance ? 1 : -1;

            if (taskImportanceX.Importance == taskImportanceY.Importance)
                return taskImportanceX.DueDate > taskImportanceY.DueDate ? 1 : -1;

            return _isAscending ? -result : result;
        }
    }
}
