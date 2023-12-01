using System.ComponentModel.DataAnnotations;
using ItmoPhysics.Servicies.MatrixValidators.Results;

namespace ItmoPhysics.Servicies.MatrixValidators.Handlers
{
    public interface IMatrixValidatorHandler
    {
        IMatrixValidatorHandler SetNext(IMatrixValidatorHandler handler);
        IMatrixValidationResult Handle(string[][] matrix);
    }
}
