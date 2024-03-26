using MediatR;
using Microsoft.AspNetCore.Mvc;
using Octopus.Core.Domain.ValueObjects;
using Octopus.FileManager.Core.Contract.Files.Commands;

namespace Octopus.FileManager.Presentation.Http.Images;


[ApiController]
[Route("api/file-manager/images")]
public class ImageController(IMediator mediator) : ControllerBase
{
    [HttpPost("")]
    public async Task<IActionResult> UploadImage([FromForm] UploadFileRequest request)
    {
        var command = new UploadFileCommand
        {
            FileDirectory = Path.Combine("Images", request.Directory),
            FileStream = request.File.OpenReadStream(),
            FileType = request.File.ContentType,
            Length = request.File.Length,
            OriginalFileName = request.File.FileName,
            FileExtension = Path.GetExtension(request.File.FileName),
            UserId = UserId.New(),
        };

        var result = await mediator.Send(command);

        return Created(result.AbsolutePath, result);
    }
}


public class UploadFileRequest
{
    public IFormFile File { get; set; }
    public string Directory { get; set; }
}