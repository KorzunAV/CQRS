using System;

namespace CQRS.Dtos
{
    public interface IDbExecutionManager
    {
        TReult TryExecute<TReult>(Func<TReult> expression);
    }
}