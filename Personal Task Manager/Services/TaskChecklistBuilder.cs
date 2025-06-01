using Personal_Task_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Task_Manager.Services
{
    public class TaskChecklistBuilder
    {
        private readonly TaskCheckList _taskCheckList;

        private TaskChecklistBuilder()
        {
            _taskCheckList = new TaskCheckList();
        }

        public static TaskChecklistBuilder Create() => new();

        public TaskChecklistBuilder WithDescription(string description)
        {
            _taskCheckList.Description = description;
            return this;
        }

        public TaskCheckList Build()
        {
            if (string.IsNullOrWhiteSpace(_taskCheckList.Description))
            {
                throw new ArgumentException("Description cannot be null or empty.");
            }
            return _taskCheckList;
        }
    }
}
