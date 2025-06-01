using Personal_Task_Manager.Models;
using Personal_Task_Manager.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Task_Manager.Services
{
    public class TaskItemBuilder
    {
        private readonly TaskItem _taskItem;

        private TaskItemBuilder()
        {
            _taskItem = new TaskItem();
        }

        public static TaskItemBuilder Create() => new();

        public TaskItemBuilder WithTitle(string title)
        {
            _taskItem.Title = title;
            return this;
        }
        public TaskItemBuilder WithDescription(string description)
        {
            _taskItem.Description = description;
            return this;
        }
        public TaskItemBuilder WithDueDate(DateTime? dueDate)
        {
            _taskItem.DueDate = dueDate;
            return this;
        }
        public TaskItemBuilder WithState(TaskState state)
        {
            _taskItem.State = state;
            return this;
        }
        public TaskItemBuilder WithCategory(TaskCategory category)
        {
            _taskItem.Category = category;
            return this;
        }
        public TaskItemBuilder WithImportance(TaskImportance importance)
        {
            _taskItem.Importance = importance;
            return this;
        }
        public TaskItemBuilder WithSubTasks(List<TaskCheckList> subTasks)
        {
            _taskItem.SubTasks = subTasks;
            foreach (var subTask in _taskItem.SubTasks)
            {
                subTask.Subscribe(_taskItem);
            }
            return this;
        }

        public TaskItemBuilder SetSubTask(Action<TaskChecklistBuilder> action)
        {
            var subTaskBuilder = TaskChecklistBuilder.Create();
            action(subTaskBuilder);
            _taskItem.SubTasks.Add(subTaskBuilder.Build());
            return this;
        }

        public TaskItem Build()
        {
            _taskItem.StartDate = DateTime.Now;
            if (string.IsNullOrEmpty(_taskItem.Title))
                throw new InvalidOperationException("Title cannot be null or empty.");
            return _taskItem;
        }
    }
}
