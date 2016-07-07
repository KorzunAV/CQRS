using System;
using System.Collections.Generic;
using CQRS.Common.Errors;
using CQRS.Dtos.Commands;

namespace CQRS.Logic.Validation
{
    public abstract class BaseValidator
    {
        /// <summary>
        /// Validates the specified activity dto.
        /// </summary>
        /// <param name="activityDto">The activity dto.</param>
        /// <returns>List of errors</returns>
        public abstract List<ErrorInfo> Validate(BaseCommandDto activityDto);

        public abstract IEnumerable<Type> GetSupportedTypes();
    }
}