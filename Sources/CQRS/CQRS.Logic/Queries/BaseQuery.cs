using System;

namespace CQRS.Logic.Queries
{
    /// <summary>
    /// Command could only read data from DB 
    /// </summary>
    public abstract class BaseQuery
    {
        /// <summary>
        /// Query identifier
        /// </summary>
        public abstract Guid QueryId { get; }
    }
}