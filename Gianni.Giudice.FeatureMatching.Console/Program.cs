using System.Text.Json;

namespace Gianni.Giudice.FeatureMatching.Console;

class Program
{
    public static async Task Main(string[] args)
    {
        var imageScenesData = new List<byte[]>();
        foreach (var imagePath in Directory.EnumerateFiles(args[1]))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            imageScenesData.Add(imageBytes);
        }
        
        var objectImageData = await File.ReadAllBytesAsync(args[0]);
        var detectObjectInScenesResults = await new ObjectDetection().DetectObjectInScenes(objectImageData, imageScenesData);
        foreach (var objectDetectionResult in detectObjectInScenesResults)
        {
            System.Console.WriteLine($"Points: {JsonSerializer.Serialize(objectDetectionResult.Points)}");
        }
    }
}