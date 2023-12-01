namespace ItmoPhysics.Servicies.MatrixValidators.Results
{
    public abstract class MatrixValidationResultBase : IMatrixValidationResult
    {
        public MatrixValidationResultBase(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }

        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}
