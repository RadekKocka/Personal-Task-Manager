using Personal_Task_Manager.Models;
using Personal_Task_Manager.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Task_Manager.DummyData
{
    public class DummySeeder
    {
        public static ObservableCollection<TaskItem> GetDummyTasks()
        {
            return new ObservableCollection<TaskItem>
            {
                new TaskItem
                {
                    Id = 1,
                    Title = "Task 1",
                    Description = "Description for Task 1",
                    DueDate = DateTime.Now.AddDays(2),
                    StartDate = DateTime.Now,
                    IsComplete = false,
                    Timer = TimeSpan.FromHours(1),
                    State = TaskState.InProgress,
                    Category = TaskCategory.Work,
                    Importance = TaskImportance.High,
                    SubTasks = []
                },
                new TaskItem
                {
                    Id = 2,
                    Title = "Task 2",
                    Description = "Description for Task 2",
                    DueDate = DateTime.Now.AddDays(5),
                    StartDate = DateTime.Now,
                    IsComplete = true,
                    Timer = TimeSpan.FromHours(2),
                    State = TaskState.Complete,
                    Category = TaskCategory.Personal,
                    Importance = TaskImportance.Medium,
                    SubTasks = []
                }
            };
        }
    }
}
