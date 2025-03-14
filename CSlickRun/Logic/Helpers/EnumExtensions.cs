#nullable disable
using System.Reflection;
using CSlickRun.Logic.Attributes;

namespace CSlickRun.Logic.Helpers;

public static class EnumExtensions
{
    public static string GetImageAttribute(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = (ImageAttribute)field?.GetCustomAttribute(typeof(ImageAttribute));
        return attribute?.ImagePath ?? value.ToString();
    }
}