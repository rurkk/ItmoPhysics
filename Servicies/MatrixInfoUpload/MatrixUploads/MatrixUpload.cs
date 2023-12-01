using ItmoPhysics.Data;
using ItmoPhysics.Servicies.MatrixInfoUpload.MatrixUploads;
using ItmoPhysics.Servicies.MatrixInfoUpload.UserFileUploads;
using System.Text.RegularExpressions;
using System.Text;
using WebMatrixUploader.Data.DataForMatrix;

namespace ItmoPhysics.Servicies.MatrixInfoUpload
{
    public class MatrixUpload : IMatrixUpload
    {
        private readonly ApplicationContext _context;
        private readonly byte[] _matrixBytes;
        private readonly IUserFileUploadBuilderFactory _userFileUploadBuilderFactory;
        public MatrixUpload(byte[] matrix, ApplicationContext context, IUserFileUploadBuilderFactory userFileUploadBuilderFactory)
        {
            _matrixBytes = matrix;
            _context = context;
            _userFileUploadBuilderFactory = userFileUploadBuilderFactory;
        }

        public List<int> Upload()
        {
            List<byte[]> curveDatas = ParseMatrixByCurves();

            List<int> fileIds = new List<int>();

            foreach (byte[] curveData in curveDatas)
            {
                var userFileUploadBuilder = _userFileUploadBuilderFactory.Create();
                userFileUploadBuilder.WithData(curveData);
                var userFileUpload = userFileUploadBuilder.Build();
                int fileId = userFileUpload.Upload();
                fileIds.Add(fileId);
            }

            return fileIds;
        }

        private List<byte[]> ParseMatrixByCurves()
        {
            string[][] matrix;
            using (var newStream = new MemoryStream(_matrixBytes))
            {
                matrix = ReadAndParseFile(newStream);
            }
            List<byte[]> curvesBytes = new List<byte[]>();

            for (int i = 0; i < matrix[0].Length; i += 2)
            {
                List<string[]> curveColumns = new List<string[]>();

                for (int j = 0; j < matrix.Length; j++)
                {
                    string[] columns = new string[] { matrix[j][i], matrix[j][i + 1] };
                    curveColumns.Add(columns);
                }

                byte[] curveBytes = ConvertColumnsToBytes(curveColumns);
                curvesBytes.Add(curveBytes);
            }

            return curvesBytes;
        }

        private byte[] ConvertColumnsToBytes(List<string[]> columns)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(memoryStream))
                {
                    foreach (var row in columns)
                    {
                        writer.WriteLine($"{row[0]}\t{row[1]}");
                    }
                }

                return memoryStream.ToArray();
            }
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
    }
}
