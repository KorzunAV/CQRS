namespace CQRS.Logic
{
    /// <summary>
    /// Command/Query execution result
    /// </summary>
    public class ExecutionResult<T> : ExecutionResult
    {
        /// <summary>
        /// Execution result data
        /// </summary>
        public T Data { get; set; }
    }

    public class ExecutionResult
    {
    }
}