using System.Text.RegularExpressions;
using System.Text;
using ItmoPhysics.Servicies.MatrixValidators.Handlers;
using ItmoPhysics.Servicies.MatrixValidators.Results;

namespace ItmoPhysics.Servicies.MatrixValidators
{
    public class MatrixStreamValidator : IMatrixStreamValidator
    {
        private readonly IMatrixValidatorHandler _validators;

        public MatrixStreamValidator(IMatrixValidatorHandlerChainFactory matrixValidatorHandlerChainFactory)
        {
            _validators = matrixValidatorHandlerChainFactory.Create();
        }

        public IMatrixValidationResult Validate(Stream fileStream)
        {
            string[][] matrix = ReadAndParseFile(fileStream);

            return _validators.Handle(matrix);
        }

        private string[][] ReadAndParseFile(Stream fileStream)
        {
            try
            {
                fileStream.Seek(0, SeekOrigin.Begin);
                using StreamReader reader = new StreamReader(fileStream, Encoding.UTF8);
                string fileContent = reader.ReadToEnd();
                string[] lines = fileContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                int rows = lines.Length;
                string[][] matrix = new string[rows][];

                for (var i = 0; i < rows; i++)
                {
                    matrix[i] = SplitValues(lines[i]);
                }

                return matrix;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file from stream: {ex.Message}");
                throw;
            }
        }

        private string[] SplitValues(string line)
        {
            string[] values = Regex.Split(line, @"\t| |\,|\;");
            values = values.Where(value => !string.IsNullOrWhiteSpace(value)).ToArray();

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].TrimEnd('\r', '\n', ' ', '\t');
            }

            return values;
        }
    }
}
