public class ImageController
{
    private readonly ImageAnalyser imageAnalyser = new ImageAnalyser();
    private readonly View view = new View();

    public void AnalyseAndDisplayImageInformation(string? filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"The file: {filePath} does not exist!");
            return;
        }

        var metaData = imageAnalyser.Analyse(filePath);
        view.ShowImageInfo(metaData);
    }
}