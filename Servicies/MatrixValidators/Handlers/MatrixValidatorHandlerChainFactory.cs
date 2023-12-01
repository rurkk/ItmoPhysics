namespace ItmoPhysics.Servicies.MatrixValidators.Handlers
{
    public class MatrixValidatorHandlerChainFactory : IMatrixValidatorHandlerChainFactory
    {
        public IMatrixValidatorHandler Create()
        {
            var firstHandler = new RectangularMatrixValidatorHandler();
            firstHandler
                .SetNext(new NumericTypeMatrixValidatorHandler())
                .SetNext(new UniqueValuesMatrixValidatorHandler());
            return firstHandler;
        }
    }
}
