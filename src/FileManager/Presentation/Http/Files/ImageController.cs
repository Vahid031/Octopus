using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Octopus.Core.Domain.ValueObjects;
using Octopus.FileManager.Core.Contract.Files.Commands;
using Octopus.Presentation.Http;
using Octopus.Presentation.Http.EnvelopModels;

namespace Octopus.FileManager.Presentation.Http.Images;


[ApiController]
[Route("api/file-manager/images")]
public class ImageController(IMediator mediator) : ControllerBase
{
    [HttpPost("")]
    [Authorize]
    [ProducesResponseType(typeof(SuccessEnvelop), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(EnvelopError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadImage([FromForm] UploadFileRequest request)
    {
        var command = new UploadFileCommand
        {
            FileDirectory = Path.Combine("images", request.Directory),
            FileStream = request.File.OpenReadStream(),
            FileType = request.File.ContentType,
            Length = request.File.Length,
            OriginalFileName = request.File.FileName,
            FileExtension = Path.GetExtension(request.File.FileName),
            UserId = HttpContext.GetUserId(),
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