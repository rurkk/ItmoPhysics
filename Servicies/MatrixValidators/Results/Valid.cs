namespace ItmoPhysics.Servicies.MatrixValidators.Results
{
    public class Valid : MatrixValidationResultBase
    {
        public Valid() : base(true, "valid matrix") {}
        public Valid(string message) : base(true, message) { }
    }
}
