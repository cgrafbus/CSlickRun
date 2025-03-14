namespace CSlickRun.Logic.Attributes;

public class ImageAttribute(string? imagePath) : Attribute
{
    public string? ImagePath { get; set; } = imagePath;
}