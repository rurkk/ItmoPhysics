using ItmoPhysics.Servicies.MatrixInfoUpload.CurvesUploads;

namespace ItmoPhysics.Servicies.MatrixInfoUpload.CellsUploads
{
    public interface ICurvesUploadBuilder
    {
        ICurvesUpload Build();
        void WithFileIds(List<int> matrixId);
        void WithCellCount(int cellCount);
        void WithXColumnNames(IEnumerable<string> xColumnNames);
        void WithYColumnNames(IEnumerable<string> yColumnNames);
        void Validate();
        bool IsValid { get; set; }
    }
}
