using Personal_Task_Manager.Models;
using Personal_Task_Manager.ViewModel.Commands;
using System.Windows.Input;

namespace Personal_Task_Manager.ViewModel;

public class AddSubTaskItemViewModel : BaseViewModel
{
    #region Fields
    private string _taskDescription;
    #endregion

    #region Constructor
    public AddSubTaskItemViewModel()
    {
        SaveSubTaskCommand = new RelayCommand(_ => Close(true), _ => CanSaveSubTask());
        CancelCommand = new RelayCommand(_ => Close(false));
    }

    public AddSubTaskItemViewModel(TaskCheckList checkListItem) : this()
    {
        SaveSubTaskCommand = new RelayCommand(_ => Close(true), _ => CanSaveSubTask());
        CancelCommand = new RelayCommand(_ => Close(false));
    }
    #endregion

    #region Properties
    public String TaskDescription
    {
        get => _taskDescription;
        set => SetProperty(ref _taskDescription, value);
    }

    public event Action<Boolean> CloseEventHandler;
    #endregion

    #region Commands
    public ICommand SaveSubTaskCommand { get; }
    public ICommand CancelCommand { get; }
    #endregion

    #region Methods
    private bool CanSaveSubTask()
    {
        return !String.IsNullOrEmpty(TaskDescription);
    }

    private void Close(Boolean dialogResult)
    {
        CloseEventHandler?.Invoke(dialogResult);
    }
    #endregion
}
