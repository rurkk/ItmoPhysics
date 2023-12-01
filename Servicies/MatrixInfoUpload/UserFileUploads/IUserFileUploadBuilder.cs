using ItmoPhysics.Servicies.MatrixValidators.Results;

namespace ItmoPhysics.Servicies.MatrixInfoUpload.UserFileUploads
{
    public interface IUserFileUploadBuilder
    {
        IUserFileUpload Build();
        void WithData(byte[] data);
    }
}
