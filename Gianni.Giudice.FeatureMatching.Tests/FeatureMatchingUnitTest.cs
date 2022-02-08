using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Gianni.Giudice.FeatureMatching.Tests;

public class FeatureMatchingUnitTest
{
    [Fact]
    public async Task ObjectShouldBeDetectedCorrectly()
    {
        var executingPath = GetExecutingPath()!;
        var imageScenesData = new List<byte[]>();
        foreach (var imagePath in Directory.EnumerateFiles(Path.Combine(executingPath, "Scenes")))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            imageScenesData.Add(imageBytes);
        }

        var objectImageData = await File.ReadAllBytesAsync(Path.Combine(executingPath, "Gianni-Giudice-object.jpg"));

        var detectObjectInScenesResults = await new ObjectDetection().DetectObjectInScenes(objectImageData, imageScenesData);

        Assert.Equal("[{\"X\":1036,\"Y\":1018},{\"X\":-2056,\"Y\":4873},{\"X\":928,\"Y\":1772},{\"X\":1078,\"Y\":1216}]",
            JsonSerializer.Serialize(detectObjectInScenesResults[0].Points));
    }

    private static string? GetExecutingPath()
    {
        var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
}