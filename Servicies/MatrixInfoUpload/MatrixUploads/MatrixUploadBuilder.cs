using Azure.Core;
using ItmoPhysics.Servicies.MatrixValidators;
using ItmoPhysics.Servicies.MatrixValidators.Results;
using System.Text.RegularExpressions;
using System.Text;
using ItmoPhysics.Data;
using ItmoPhysics.Servicies.MatrixInfoUpload.MatrixUploads;
using ItmoPhysics.Servicies.MatrixInfoUpload.UserFileUploads;

namespace ItmoPhysics.Servicies.MatrixInfoUpload
{
    public class MatrixUploadBuilder : IMatrixUploadBuilder
    {
        private byte[]? _matrixBytes;
        private readonly IMatrixStreamValidator _matrixValidator;
        private readonly IUserFileUploadBuilderFactory _userFileUploadBuilderFactory;
        private readonly ApplicationContext _context;
        public MatrixUploadBuilder(IMatrixStreamValidator matrixValidator, IUserFileUploadBuilderFactory userFileUploadBuilderFactory, ApplicationContext context)
        {
            _matrixValidator = matrixValidator;
            ValidationResult = new NullMatrixValidationResult();
            _context = context;
            _userFileUploadBuilderFactory = userFileUploadBuilderFactory;
        }

        public IMatrixValidationResult ValidationResult { get; set; }


        public IMatrixUpload Build()
        {
            if (_matrixBytes is null) throw new InvalidOperationException();
            if (!ValidationResult.IsValid) throw new InvalidOperationException();

            return new MatrixUpload(_matrixBytes, _context, _userFileUploadBuilderFactory);
        }

        public void WithMatrix(byte[] matrixBytes)
        {
            using (var newStream = new MemoryStream(matrixBytes))
            {
                ValidationResult = _matrixValidator.Validate(newStream);
            }

            if (ValidationResult.IsValid)
            {
                string[][] matrix;
                using (var newStream = new MemoryStream(matrixBytes))
                {
                    matrix = ReadAndParseFile(newStream);
                }
                if (matrix[0].Length == 1)
                {
                    matrix = AddIndexColumn(matrix);
                    matrixBytes = UpdateMatrix(matrix);
                }
            }

            _matrixBytes = matrixBytes;
        }

        private string[][] ReadAndParseFile(Stream fileStream)
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

        private string[][] AddIndexColumn(string[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;

            for (int i = 0; i < rows; i++)
            {
                Array.Resize(ref matrix[i], cols + 1);
                matrix[i][cols] = matrix[i][0];
                matrix[i][0] = (i + 1).ToString();
            }

            return matrix;
        }

        private byte[] UpdateMatrix(string[][] matrix)
        {
            using (MemoryStream updatedMatrixStream = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(updatedMatrixStream, Encoding.UTF8))
            {
                foreach (var row in matrix)
                {
                    writer.WriteLine(string.Join(' ', row));
                }
                writer.Flush();
                updatedMatrixStream.Position = 0;

                return updatedMatrixStream.ToArray();
            }
        }
    }
}
