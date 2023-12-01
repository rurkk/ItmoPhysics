using System.ComponentModel.DataAnnotations;

namespace WebMatrixUploader.Data.DataForMatrix;

public class Abscissa
{
    [Key] public int AbscissaId { get; set; }
    public string? Name { get; set; }
}