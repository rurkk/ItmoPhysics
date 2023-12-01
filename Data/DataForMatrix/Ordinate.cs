using System.ComponentModel.DataAnnotations;

namespace WebMatrixUploader.Data.DataForMatrix;

public class Ordinate
{
    [Key] public int OrdinateId { get; set; }
    public string? Name { get; set; }
}