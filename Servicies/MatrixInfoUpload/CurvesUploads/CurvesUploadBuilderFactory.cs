using ItmoPhysics.Data;
using ItmoPhysics.Servicies.MatrixValidators;

namespace ItmoPhysics.Servicies.MatrixInfoUpload.CellsUploads
{
    public class CurvesUploadBuilderFactory : ICurvesUploadBuilderFactory
    {
        private readonly ApplicationContext _context;
        public CurvesUploadBuilderFactory(ApplicationContext context)
        {
            _context = context;
        }
        public ICurvesUploadBuilder Create()
        {
            return new CurvesUploadBuilder(_context);
        }
    }
}
