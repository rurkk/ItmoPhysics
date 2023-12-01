using ItmoPhysics.Entities.MatrixInfoUpload;
using Microsoft.AspNetCore.Mvc;

namespace ItmoPhysics.Servicies
{
    public interface ICurvesUploadService
    {
        void Upload(CellInfoUploadRequest request);
    }
}
