using ItmoPhysics.Entities.MatrixInfoUpload;
using ItmoPhysics.Servicies;
using ItmoPhysics.Servicies.MatrixInfoUpload;
using ItmoPhysics.Servicies.MatrixInfoUpload.CellsUploads;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ItmoPhysics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadMatrixController : ControllerBase
    {
        private readonly IMatrixUploadService _matrixUploadService;
        private readonly ICurvesUploadService _curvesUploadService;

        public UploadMatrixController(IMatrixUploadService matrixUploadService, ICurvesUploadService curvesUploadService)
        {
            _matrixUploadService = matrixUploadService;
            _curvesUploadService = curvesUploadService;
        }

        [HttpPost("validate-file")]
        public IActionResult ValidateFile([FromForm] FileUploadRequest request)
        {
            try
            {
                _matrixUploadService.Upload(request);
                return Ok(new { CellCount = _matrixUploadService.FilesIds.Count, FilesIds = _matrixUploadService.FilesIds, Message = "File is valid" });
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost("process-data")]
        public IActionResult ProcessData([FromForm] CellInfoUploadRequest request)
        {
            try
            {
                _curvesUploadService.Upload(request);
                return Ok("Success");
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
