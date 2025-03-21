using CSlickRun.Logic.Attributes;

namespace CSlickRun.Logic.Helpers;

/// <summary>
/// Provides extension methods for enums.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the image path from the attribute of an enum value.
    /// </summary>
    /// <param name="e">The enum value.</param>
    /// <returns>The image path from the attribute, or the enum value as a string if no attribute is found.</returns>
    public static string? GetImagePathFromAttribute(this Enum? e)
    {
        var attribute = e.GetAttribute<ImageAttribute>();
        return attribute == null ? e.ToString() : attribute.ImagePath;
    }

    /// <summary>
    /// Gets the specified attribute from an enum value.
    /// </summary>
    /// <typeparam name="T">The type of the attribute to search for.</typeparam>
    /// <param name="value">The enum value.</param>
    /// <returns>The attribute of the specified type, or <c>null</c> if no attribute is found.</returns>
    public static T? GetAttribute<T>(this Enum? value)
        where T : Attribute
    {
        var type = value?.GetType();
        var memberInfo = type?.GetMember(value?.ToString() ?? string.Empty);
        var attributes = memberInfo?[0].GetCustomAttributes(typeof(T), false);
        return attributes is { Length: > 0 } ? (T)attributes[0] : null;
    }
}