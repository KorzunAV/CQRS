using CQRS.Common.Errors;
using CQRS.Dtos.Commands;
using CQRS.Logic.Validation;

namespace CQRS.Logic.Blos
{
    /// <summary>
    /// Base business logic
    /// </summary>
    public abstract class BaseBlo
    {
        protected ValidationManager ValidationManager { get; set; }

        protected BaseBlo(ValidationManager validationManager)
        {
            ValidationManager = validationManager;
        }
        
        /// <summary>
        /// Validate entity dto
        /// </summary>
        /// <param name="entityDto">entity Dto</param>
        /// <returns></returns>
        protected void Validate(BaseCommandDto entityDto)
        {
            var errors = ValidationManager.Validate(entityDto);

            if (errors.Count != 0)
                throw new ValidationException(errors);
        }

        public abstract void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator);
    }
}
