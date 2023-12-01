using ItmoPhysics.Servicies.MatrixValidators.Results;

namespace ItmoPhysics.Servicies.MatrixValidators
{
    public interface IMatrixStreamValidator
    {
        IMatrixValidationResult Validate(Stream fileStream);
    }
}
