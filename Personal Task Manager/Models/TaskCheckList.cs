using Personal_Task_Manager.ViewModel;
using Personal_Task_Manager.ViewModel.Commands;
using System.Windows.Input;

namespace Personal_Task_Manager.Models;

public class TaskCheckList : BaseViewModel
{
    private bool _isComplete;
    private string _description;

    public TaskCheckList()
    {

    }

    public TaskCheckList(String description)
    {
        Description = description;
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public bool IsComplete
    {
        get => _isComplete;
        set => SetProperty(ref _isComplete, value);
    }
}