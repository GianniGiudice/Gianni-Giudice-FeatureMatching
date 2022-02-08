using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Gianni.Giudice.FeatureMatching.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FeatureMatchingController : ControllerBase
{
    private readonly ObjectDetection _objectDetection;
    
    public FeatureMatchingController(ObjectDetection objectDetection)
    {
        _objectDetection = objectDetection;
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> OnPostUploadAsync(List<IFormFile>
        files)
    {
        if(files.Count != 2)
            return BadRequest();
        using var objectSourceStream = files[0].OpenReadStream();
        using var objectMemoryStream = new MemoryStream();
        objectSourceStream.CopyTo(objectMemoryStream);
        var imageObjectData = objectMemoryStream.ToArray();
        using var sceneSourceStream = files[1].OpenReadStream();
        using var sceneMemoryStream = new MemoryStream();
        sceneSourceStream.CopyTo(sceneMemoryStream);
        var imageSceneData = sceneMemoryStream.ToArray();
        var resultList = await new ObjectDetection().DetectObjectInScenes(imageObjectData, new List<byte[]>{imageSceneData});
        var imageData = resultList.FirstOrDefault()!;
        return File(imageData.ImageData!, "image/png");
    }
    
    private static string? GetExecutingPath()
    {
        var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
}