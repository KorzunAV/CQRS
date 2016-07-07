using System;
using System.Collections.Generic;
using CQRS.Common.Errors;
using CQRS.Dtos.Commands;

namespace CQRS.Logic.Validation
{
    public class ValidationManager
    {
        private readonly Dictionary<Type, BaseValidator> _validators;

        public ValidationManager(Dictionary<Type, BaseValidator> validators)
        {
            _validators = validators;
        }

        public List<ErrorInfo> Validate<T>(T dto)
            where T : BaseCommandDto
        {
            var dtoType = typeof(T);
            if (_validators.ContainsKey(dtoType))
            {
                return _validators[dtoType].Validate(dto);
            }
            throw new ArgumentException($"Could not find validator for type={dtoType}");
        }
    }
}
