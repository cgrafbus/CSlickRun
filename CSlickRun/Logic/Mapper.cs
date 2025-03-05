namespace CSlickRun.Logic;

public static class Mapper
{
    /// <summary>
    /// Mappt die Werte der Properties einer Klasse zu einer anderen
    /// </summary>
    /// <param name="source">Ursprungsklasse</param>
    /// <param name="destination">Zielklasse</param>
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