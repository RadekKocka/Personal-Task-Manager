using Personal_Task_Manager.ViewModel;

namespace Personal_Task_Manager.Models
{
    public class TaskCheckList : BaseViewModel, IObservable<TaskItem>, IDisposable
    {
        private bool isComplete;
        private IObserver<TaskItem>? _observer;

        public string Description { get; set; }
        public bool IsComplete
        {
            get => isComplete;
            set
            {
                if (SetProperty(ref isComplete, value))
                {
                    NotifySubsribers();
                }
            }
        }

        private void NotifySubsribers()
        {
            if (_observer == null)
                return;
            _observer.OnNext(null);  
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IDisposable Subscribe(IObserver<TaskItem> observer)
        {
            _observer = observer ?? throw new ArgumentNullException(nameof(observer));
            return this;
        }
    }
}
