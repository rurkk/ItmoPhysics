using ItmoPhysics.Data;
using WebMatrixUploader.Data.DataForMatrix;

namespace ItmoPhysics.Servicies.MatrixInfoUpload.UserFileUploads
{
    public class UserFileUpload : IUserFileUpload
    {
        private readonly ApplicationContext _context;
        private readonly byte[] _data;

        public UserFileUpload(byte[] data, ApplicationContext context)
        {
            _context = context;
            _data = data;
        }

        public int Upload()
        {
            var filePath = GetFilePath();
            var userId = "1";//User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            using FileStream fileStream = File.Create(filePath);
            using (var newStream = new MemoryStream(_data))
            {
                newStream.CopyTo(fileStream);
            }

            var fileRecord = new UserFile
            {
                FilePath = filePath,
                UserId = userId
            };
            _context.UsersFiles.Add(fileRecord);
            _context.SaveChanges();

            return fileRecord.FileId;
        }

        private string GetFilePath()
        {
            var fileName = $"{Guid.NewGuid()}";
            var userId = "1";//User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //if (userId is null) throw new ArgumentNullException(nameof(userId));
            var userFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", userId);
            if (!Directory.Exists(userFolderPath)) Directory.CreateDirectory(userFolderPath);
            // New filename generator
            var filePath = Path.Combine(userFolderPath, fileName);
            var fileExtension = Path.GetExtension(fileName).ToLower();
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var count = 1;
            while (File.Exists(filePath))
            {
                fileName = $"{fileNameWithoutExtension}({count}){fileExtension}";
                filePath = Path.Combine(userFolderPath, fileName);
                count++;
            }

            return filePath;
        }
    }
}
