using ItmoPhysics.Servicies.MatrixInfoUpload.MatrixUploads;

namespace ItmoPhysics.Servicies.MatrixInfoUpload
{
    public interface IMatrixUploadBuilderFactory
    {
        IMatrixUploadBuilder Create();
    }
}
