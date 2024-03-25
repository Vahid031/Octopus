using Microsoft.AspNetCore.Mvc;

namespace Octopus.FileManager.Presentation.Http.Files;


[ApiController]
[Route("api/partner/distribution-centers")]
[Produces("application/json")]
[Consumes("application/json")]
public class FileController : ControllerBase
{


    [HttpPost]
    public async Task<IActionResult> UploadFile([FromForm] UploadFileRequest request)
    {
        await Task.Delay(1000);

        return Ok();
    }
}


public class UploadFileRequest
{
    public IFormFile MyProperty { get; set; }
    public string Directory { get; set; }
}