namespace CSlickRun.Logic.Attributes;

public class ImageAttribute : Attribute
{
    public ImageAttribute(string? ImagePath)
    {
        ImagePath = ImagePath;
    }

    public string? ImagePath { get; set; }
}