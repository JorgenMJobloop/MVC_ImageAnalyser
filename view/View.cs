public class View
{
    public void ShowImageInfo(ImageMetadata metaData)
    {
        Console.WriteLine("Image file information:");
        Console.WriteLine($"Filename: {metaData.FileName}");
        Console.WriteLine($"Size: {metaData.FileSizeInBytes / 1024.0:F2} KB");
        Console.WriteLine($"Resolution: {metaData.Width} x {metaData.Height}");
        Console.WriteLine($"Format: {metaData.Format}");

        Console.WriteLine("\nImage EXIF-metadata:");
        Console.WriteLine($"Camera Make: {metaData.CameraMake}");
        Console.WriteLine($"Camera Model: {metaData.CameraModel}");
        Console.WriteLine($"Date taken: {metaData.DateTaken}");
        Console.WriteLine($"GPS: {metaData.GPSLatitude}, {metaData.GPSLongitude}");
    }
}