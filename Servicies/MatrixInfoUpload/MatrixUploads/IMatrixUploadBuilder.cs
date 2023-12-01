using ItmoPhysics.Servicies.MatrixInfoUpload.MatrixUploads;
using ItmoPhysics.Servicies.MatrixValidators.Results;

namespace ItmoPhysics.Servicies.MatrixInfoUpload
{
    public interface IMatrixUploadBuilder
    {
        IMatrixUpload Build();
        void WithMatrix(byte[] matrix);
        IMatrixValidationResult ValidationResult { get; set; }
    }
}
