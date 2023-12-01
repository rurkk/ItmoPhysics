namespace ItmoPhysics.Servicies.MatrixValidators
{
    public class RectangularMatrixValidator : IMatrixValidator
    {
        public bool Validate(string[][] matrix)
        {
            return matrix.Any() && matrix.All(row => row.Length == matrix[0].Length);
        }
    }
}
