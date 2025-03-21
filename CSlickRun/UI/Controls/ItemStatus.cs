using CSlickRun.Logic.Attributes;

namespace CSlickRun.UI.Controls;

/// <summary>
/// Enum for Itemstatus
/// </summary>
public enum ItemStatus
{
    /// <summary>
    /// None
    /// </summary>
    [Image(null)] None,
    /// <summary>
    /// New
    /// </summary>
    [Image("new.png")] New,
    /// <summary>
    /// Modified
    /// </summary>
    [Image("modified.png")] Modified,
    /// <summary>
    /// Deleted
    /// </summary>
    [Image("deleted.png")] Deleted
}