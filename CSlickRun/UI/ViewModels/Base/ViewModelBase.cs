using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CSlickRun.UI.ViewModels.Base;

/// <summary>
/// Basisklasse für ViewModels, die die INotifyPropertyChanged-Schnittstelle implementiert.
/// </summary>
public class ViewModelBase : INotifyPropertyChanged
{
    /// <summary>
    /// Ereignis, das ausgelöst wird, wenn sich eine Eigenschaft ändert.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Benachrichtigt Listener, dass sich eine Eigenschaft geändert hat.
    /// </summary>
    /// <param name="propertyName">Der Name der geänderten Eigenschaft.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Setzt den Wert eines Feldes und benachrichtigt Listener über die Änderung.
    /// </summary>
    /// <typeparam name="T">Der Typ des Feldes.</typeparam>
    /// <param name="field">Eine Referenz auf das Feld.</param>
    /// <param name="value">Der neue Wert des Feldes.</param>
    /// <param name="propertyName">Der Name der geänderten Eigenschaft.</param>
    /// <returns>True, wenn der Wert geändert wurde; andernfalls False.</returns>
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}