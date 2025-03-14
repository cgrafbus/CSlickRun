using CSlickRun.Logic.Attributes;

namespace CSlickRun.UI.Controls;

public enum ItemStatus
{
    [Image(null)] None,
    [Image("new.png")] New,
    [Image("modified.png")] Modified,
    [Image("deleted.png")] Deleted
}