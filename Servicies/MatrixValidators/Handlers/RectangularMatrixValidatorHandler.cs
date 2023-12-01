using ItmoPhysics.Servicies.MatrixValidators.Results;

namespace ItmoPhysics.Servicies.MatrixValidators.Handlers
{
    public class RectangularMatrixValidatorHandler : BaseMatrixValidatorHandler
    {
        public override IMatrixValidationResult Handle(string[][] matrix)
        {
            var validator = new RectangularMatrixValidator();
            bool result = validator.Validate(matrix);

            if (result)
            {
                return base.Handle(matrix);
            }

            return new Invalid("Matrix must be rectangular");
        }
    }
}
