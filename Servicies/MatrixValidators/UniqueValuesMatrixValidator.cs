namespace ItmoPhysics.Servicies.MatrixValidators
{
    public class UniqueValuesMatrixValidator : IMatrixValidator
    {
        public bool Validate(string[][] matrix)
        {
            if (!matrix.Any()) return false;

            var firstNumber = matrix[0][0];

            var flag = true;

            foreach (var values in matrix)
            {
                foreach (var value in values)
                {
                    if (firstNumber != value) flag = false;
                }
            }

            return !flag;
        }
    }
}
