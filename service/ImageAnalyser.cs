using System.Drawing.Imaging;
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
        metaData.DateTaken = ifd0?.GetDescription(ExifDirectoryBase.TagDateTime);


        var location = GPS?.GetGeoLocation();
        if (GPS != null && location != null)
        {
            metaData.GPSLongitude = location.Latitude.ToString("F6");
            metaData.GPSLatitude = location.Longitude.ToString("F6");
        }
        else if (GPS != null && GPS.GetGeoLocation() == null)
        {
            metaData.GPSLatitude = "0";
            metaData.GPSLongitude = "0";
        }

        return metaData;
    }
}