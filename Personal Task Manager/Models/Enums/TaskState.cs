namespace Personal_Task_Manager.Models.Enums
{
    [Flags]
    public enum TaskState
    {
        /// <summary>
        /// Task that is ongoing.
        /// </summary>
        InProgress = 1,
        /// <summary>
        /// Completed task.
        /// </summary>
        Complete = 1 << 1,
        /// <summary>
        /// Task not yet started.
        /// </summary>
        NotStarted = 1 << 2,
        /// <summary>
        /// Task that is overdue.
        /// </summary>
        Late = 1 << 3,
        /// <summary>
        /// Archived task.
        /// </summary>
        Archived = 1 << 4,
        /// <summary>
        /// Deleted task
        /// </summary>
        Deleted = 1 << 5,

        All = InProgress | Complete | NotStarted | Late | Archived | Deleted
    }
}
