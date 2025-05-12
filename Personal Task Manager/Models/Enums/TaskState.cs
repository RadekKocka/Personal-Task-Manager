namespace Personal_Task_Manager.Models.Enums
{
    public enum TaskState
    {
        /// <summary>
        /// Task that is ongoing.
        /// </summary>
        InProgress,
        /// <summary>
        /// Completed task.
        /// </summary>
        Complete,
        /// <summary>
        /// Task not yet started.
        /// </summary>
        NotStarted,
        /// <summary>
        /// Task that is overdue.
        /// </summary>
        Late,
        /// <summary>
        /// Archived task.
        /// </summary>
        Archived,
        /// <summary>
        /// Deleted task
        /// </summary>
        Deleted
    }
}
