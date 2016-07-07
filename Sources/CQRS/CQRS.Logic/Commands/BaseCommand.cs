using System;

namespace CQRS.Logic.Commands
{
    /// <summary>
    /// Base command 
    /// </summary>
    public abstract class BaseCommand
    {
        /// <summary>
        /// Command identifier
        /// </summary>
        public abstract Guid CommandId { get; }
    }
}