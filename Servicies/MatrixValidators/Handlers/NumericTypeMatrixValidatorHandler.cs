using ItmoPhysics.Servicies.MatrixValidators.Results;

namespace ItmoPhysics.Servicies.MatrixValidators.Handlers
{
    public class NumericTypeMatrixValidatorHandler : BaseMatrixValidatorHandler
    {
        public override IMatrixValidationResult Handle(string[][] matrix)
        {
            var validator = new NumericTypeMatrixValidator();
            bool result = validator.Validate(matrix);

            if (result) {
                return base.Handle(matrix);
            }

            return new Invalid("All elements of matrix must be numerics");
        }
    }
}
