using Personal_Task_Manager.Models;
using Personal_Task_Manager.Models.Enums;
using Personal_Task_Manager.ViewModel;
using System.Collections.ObjectModel;

namespace Personal_Task_Manager.DummyData
{
    public class DummySeeder
    {
        private static List<TaskItem> CreateDummyTasks()
        {
            return new List<TaskItem>
            {
                new TaskItem
                {
                    Id = 1,
                    Title = "Task 1",
                    Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Suspendisse nisl. Integer rutrum, orci vestibulum ullamcorper ultricies, lacus quam ultricies odio, vitae placerat pede sem sit amet enim. Etiam bibendum elit eget erat. Vivamus porttitor turpis ac leo. Fusce aliquam vestibulum ipsum. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Nullam faucibus mi quis velit. Praesent id justo in neque elementum ultrices. Fusce suscipit libero eget elit. Vivamus ac leo pretium faucibus. Pellentesque sapien. In rutrum. Proin mattis lacinia justo.",
                    DueDate = DateTime.Now.AddDays(2),
                    StartDate = new DateTime(2025, 4, 01, 0, 5, 36),
                    TaskElapsedTime = TimeSpan.Zero,
                    State = TaskState.InProgress,
                    Category = TaskCategory.Work,
                    Importance = TaskImportance.High,
                    SubTasks = new List<TaskCheckList>
                    {
                        new() {
                            Description = "Subtask 1",
                            IsComplete = false
                        },
                        new() {
                            Description = "Subtask 2",
                            IsComplete = true
                        }
                    }
                },
                new TaskItem
                {
                    Id = 2,
                    Title = "Task 2",
                    Description = "Description for Task 2",
                    DueDate = DateTime.Now.AddDays(5),
                    StartDate = DateTime.Now.AddSeconds(36),
                    TaskElapsedTime = new TimeSpan(0,0,30),
                    State = TaskState.Complete,
                    Category = TaskCategory.Personal,
                    Importance = TaskImportance.Medium,
                    SubTasks = []
                }
            };
        }

        public static ObservableCollection<TaskItemViewModel> GetDummyTasks()
        {
            var dummyTasks = CreateDummyTasks();
            var taskViewModels = new ObservableCollection<TaskItemViewModel>();
            foreach (var task in dummyTasks)
            {
                taskViewModels.Add(new TaskItemViewModel(task));
            }
            return taskViewModels;
        }
    }
}
