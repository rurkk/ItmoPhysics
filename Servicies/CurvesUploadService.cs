using ItmoPhysics.Entities.MatrixInfoUpload;
using ItmoPhysics.Servicies.MatrixInfoUpload.CellsUploads;
using Microsoft.AspNetCore.Mvc;

namespace ItmoPhysics.Servicies
{
    public class CurvesUploadService : ICurvesUploadService
    {

        private readonly ICurvesUploadBuilderFactory _cellsUploadBuilderFactory;

        public CurvesUploadService(ICurvesUploadBuilderFactory cellsUploadBuilderFactory)
        {
            _cellsUploadBuilderFactory = cellsUploadBuilderFactory;
        }

        public void Upload(CellInfoUploadRequest request)
        {
            var cellsUploadBuilder = _cellsUploadBuilderFactory.Create();
            cellsUploadBuilder.WithFileIds(request.FilesIds);
            cellsUploadBuilder.WithXColumnNames(request.XColumnNames);
            cellsUploadBuilder.WithYColumnNames(request.YColumnNames);
            cellsUploadBuilder.WithCellCount(request.CellCount);
            cellsUploadBuilder.Validate();

            if (cellsUploadBuilder.IsValid)
            {
                var matrixUploadInfo = cellsUploadBuilder.Build();
                matrixUploadInfo.Upload();
            }
            else
            {
                throw new Exception("Curves names invalid");
            }
        }
    }
}

