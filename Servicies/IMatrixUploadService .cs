using ItmoPhysics.Entities.MatrixInfoUpload;
using Microsoft.AspNetCore.Mvc;

namespace ItmoPhysics.Servicies
{
    public interface IMatrixUploadService
    {
        void Upload(FileUploadRequest request);
        public List<int> FilesIds { get; set; }
    }
}
