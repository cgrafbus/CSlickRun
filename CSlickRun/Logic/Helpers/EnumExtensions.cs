using CSlickRun.Logic.Attributes;

namespace CSlickRun.Logic.Helpers;

public static class EnumExtensions
{
    /// <summary>
    ///     Ermittelt das Attribut eines Enum-Wertes
    /// </summary>
    /// <typeparam name="T">Typ des zu suchenden Attributes</typeparam>
    /// <param name="value">Enum-Wert</param>
    /// <returns>Liefert das Attribut zurück oder falls keins von spezifizierten Typ gefunden wird <c>null</c>.</returns>
    public static string? GetImagePathFromAttribute(this Enum? e)
    {
        var attribute = e.GetAttribute<ImageAttribute>();
        return attribute == null ? e.ToString() : attribute.ImagePath;
    }

    /// <summary>
    ///     Ermittelt das Attribut eines Enum-Wertes
    /// </summary>
    /// <typeparam name="T">Typ des zu suchenden Attributes</typeparam>
    /// <param name="value">Enum-Wert</param>
    /// <returns>Liefert das Attribut zurück oder falls keins von spezifizierten Typ gefunden wird <c>null</c>.</returns>
    public static T? GetAttribute<T>(this Enum? value)
        where T : Attribute
    {
        var type = value.GetType();
        var memberInfo = type.GetMember(value.ToString());
        var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
        return attributes.Length > 0 ? (T)attributes[0] : null;
    }
}