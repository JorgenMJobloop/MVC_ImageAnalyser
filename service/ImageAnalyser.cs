using System.Drawing;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.Jpeg;

public class ImageAnalyser
{
    public ImageMetadata Analyse(string? filePath)
    {
        var fileInfo = new FileInfo(filePath);
        using var image = Image.FromFile(filePath);

        var metaData = new ImageMetadata
        {
            FileName = fileInfo.Name,
            FileSizeInBytes = fileInfo.Length,
            Width = image.Width,
            Height = image.Height,
            Format = image.RawFormat.ToString()
        };

        var directories = ImageMetadataReader.ReadMetadata(filePath);

        var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
        var ifd0 = directories.OfType<ExifIfd0Directory>().FirstOrDefault();
        var GPS = directories.OfType<GpsDirectory>().FirstOrDefault();

        metaData.CameraMake = ifd0?.GetDescription(ExifDirectoryBase.TagMake);
        metaData.CameraModel = ifd0?.GetDescription(ExifDirectoryBase.TagModel);
        var dateTime = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTime);

        if (string.IsNullOrWhiteSpace(dateTime))
        {
            throw new Exception("An error occured!");
        }

        var parseDateTime = DateTime.Parse(dateTime);
        metaData.DateTaken = parseDateTime.ToUniversalTime();


        if (GPS != null)
        {
            metaData.GPSLongitude = GPS.GetGeoLocation().Longitude.ToString();
            metaData.GPSLatitude = GPS.GetGeoLocation().Latitude.ToString();
        }

        return metaData;
    }
}