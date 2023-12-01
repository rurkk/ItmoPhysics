using ItmoPhysics.Servicies.MatrixValidators.Results;

namespace ItmoPhysics.Servicies.MatrixValidators.Handlers
{
    public class UniqueValuesMatrixValidatorHandler : BaseMatrixValidatorHandler
    {
        public override IMatrixValidationResult Handle(string[][] matrix)
        {
            var validator = new UniqueValuesMatrixValidator();
            bool result = validator.Validate(matrix);

            if (result)
            {
                return base.Handle(matrix);
            }

            return new Invalid("Matrix values could not be the same numeric");
        }
    }
}
