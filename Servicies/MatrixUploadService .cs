using ItmoPhysics.Entities.MatrixInfoUpload;
using ItmoPhysics.Servicies.MatrixInfoUpload;
using Microsoft.AspNetCore.Mvc;

namespace ItmoPhysics.Servicies
{
    public class MatrixUploadService : IMatrixUploadService
    {
        private readonly IMatrixUploadBuilderFactory _matrixUploadBuilderFactory;

        public MatrixUploadService(IMatrixUploadBuilderFactory matrixUploadBuilderFactory)
        {
            _matrixUploadBuilderFactory = matrixUploadBuilderFactory;
        }

        public List<int>? FilesIds { get; set; }

        public void Upload(FileUploadRequest request)
        {
            byte[] matrixBytes;

            using (var matrixStream = new MemoryStream())
            {
                request.File.CopyTo(matrixStream);
                matrixStream.Seek(0, SeekOrigin.Begin);
                matrixBytes = matrixStream.ToArray();
            }

            var matrixUploadBuilder = _matrixUploadBuilderFactory.Create();
            matrixUploadBuilder.WithMatrix(matrixBytes);

            var validationResult = matrixUploadBuilder.ValidationResult;

            if (!validationResult.IsValid)
            {
                throw new Exception("File validation failed. Details: " + validationResult.Message);
            }

            var matrixUpload = matrixUploadBuilder.Build();
            FilesIds = matrixUpload.Upload();
        }
    }
}
