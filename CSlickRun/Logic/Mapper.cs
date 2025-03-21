namespace CSlickRun.Logic;

/// <summary>
/// Mapper class
/// </summary>
public static class Mapper
{
    /// <summary>
    /// Mapps the values of properties of a class to another
    /// </summary>
    /// <param name="source">Source class</param>
    /// <param name="destination">Target class</param>
    public static void MapClasses(object source, object destination)
    {
        var sourceProperties = source.GetType().GetProperties();
        var destinationProperties = destination.GetType().GetProperties();
        foreach (var sourceProperty in sourceProperties)
        {
            foreach (var destinationProperty in destinationProperties)
            {
                if (sourceProperty.Name == destinationProperty.Name && sourceProperty.PropertyType == destinationProperty.PropertyType)
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                }
            }
        }
    }

}