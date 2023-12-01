namespace ItmoPhysics.Entities.MatrixInfoUpload
{
    public class CellInfoUploadRequest
    {
        public required List<int> FilesIds { get; set; }
        public required int CellCount { get; set; }
        public required List<string> XColumnNames { get; set; }
        public required List<string> YColumnNames { get; set; }
    }
}
