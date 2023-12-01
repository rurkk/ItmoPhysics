namespace ItmoPhysics.Servicies.MatrixValidators
{
    public class NumericTypeMatrixValidator : IMatrixValidator
    {
        public bool Validate(string[][] matrix)
        {
            return matrix.Any() && matrix.All(row => row.All(IsNumeric));
        }

        private bool IsNumeric(string value)
        {
            return double.TryParse(value, out _);
        }
    }
}
