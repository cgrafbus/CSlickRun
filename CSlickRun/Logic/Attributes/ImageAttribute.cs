namespace CSlickRun.Logic.Attributes;

/// <summary>
/// Attribute that should contain an Image-Path / Name
/// </summary>
/// <param name="imagePath">Image Path / Name</param>
public class ImageAttribute(string? imagePath) : Attribute
{
    /// <summary>
    /// Image Path / Name
    /// </summary>
    public string? ImagePath { get; set; } = imagePath;
}