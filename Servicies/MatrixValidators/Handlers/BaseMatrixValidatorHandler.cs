using ItmoPhysics.Servicies.MatrixValidators.Results;

namespace ItmoPhysics.Servicies.MatrixValidators.Handlers
{
    public class BaseMatrixValidatorHandler : IMatrixValidatorHandler
    {
        private IMatrixValidatorHandler? _nextHandler;

        public IMatrixValidatorHandler SetNext(IMatrixValidatorHandler handler)
        {
            _nextHandler = handler ?? throw new ArgumentNullException(nameof(handler));
            return _nextHandler;
        }

        public virtual IMatrixValidationResult Handle(string[][] matrix)
        {
            if (_nextHandler is null) return new Valid("Matrix validated successfully");
            return _nextHandler.Handle(matrix);
        }
    }
}
