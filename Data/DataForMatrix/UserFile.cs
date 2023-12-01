using System.ComponentModel.DataAnnotations;

namespace WebMatrixUploader.Data.DataForMatrix;

public class UserFile
{
    [Key]
    public int FileId { get; set; }
    public string? FilePath { get; set; }
    public string? UserId { get; set; }
}