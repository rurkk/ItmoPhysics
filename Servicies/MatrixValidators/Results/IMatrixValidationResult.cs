namespace ItmoPhysics.Servicies.MatrixValidators.Results
{
    public interface IMatrixValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}
