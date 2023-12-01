using ItmoPhysics.Data;
using ItmoPhysics.Servicies.MatrixInfoUpload.UserFileUploads;
using ItmoPhysics.Servicies.MatrixValidators;

namespace ItmoPhysics.Servicies.MatrixInfoUpload
{
    public class MatrixUploadBuilderFactory : IMatrixUploadBuilderFactory
    {
        private readonly IMatrixStreamValidator _matrixValidator;
        private readonly ApplicationContext _context;
        private IUserFileUploadBuilderFactory _userFileUploadBuilderFactory;
        public MatrixUploadBuilderFactory(IMatrixStreamValidator matrixValidator, ApplicationContext context, IUserFileUploadBuilderFactory userFileUploadBuilderFactory)
        {
            _matrixValidator = matrixValidator;
            _context = context;
            _userFileUploadBuilderFactory = userFileUploadBuilderFactory;
        }

        public IMatrixUploadBuilder Create()
        {
            return new MatrixUploadBuilder(_matrixValidator, _userFileUploadBuilderFactory, _context);
        }
    }
}
