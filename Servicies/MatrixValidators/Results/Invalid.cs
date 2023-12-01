namespace ItmoPhysics.Servicies.MatrixValidators.Results
{
    public class Invalid : MatrixValidationResultBase
    {
        public Invalid() : base(false, "Invalid matrix") { }
        public Invalid(string message) : base(false, message) { }
    }
}
