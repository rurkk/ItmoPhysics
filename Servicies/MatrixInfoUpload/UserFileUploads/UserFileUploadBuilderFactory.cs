using ItmoPhysics.Data;
using ItmoPhysics.Servicies.MatrixInfoUpload.CellsUploads;

namespace ItmoPhysics.Servicies.MatrixInfoUpload.UserFileUploads
{
    public class UserFileUploadBuilderFactory : IUserFileUploadBuilderFactory
    {
        private readonly ApplicationContext _context;
        public UserFileUploadBuilderFactory(ApplicationContext context)
        {
            _context = context;
        }
        public IUserFileUploadBuilder Create()
        {
            return new UserFileUploadBuilder(_context);
        }
    }
}
