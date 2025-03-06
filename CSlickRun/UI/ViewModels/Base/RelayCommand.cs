using System.Windows.Input;

namespace CSlickRun.UI.ViewModels.Base;

/// <summary>
/// Eine Implementierung von <see cref="ICommand"/>, die synchrone Operationen unterstützt.
/// </summary>
public class RelayCommand : ICommand
{
    private readonly Func<object?, bool>? _canExecute;
    private readonly Action<object?> _execute;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="RelayCommand"/> Klasse.
    /// </summary>
    /// <param name="execute">Die Aktion, die ausgeführt werden soll.</param>
    /// <param name="canExecute">Eine Funktion, die bestimmt, ob der Befehl ausgeführt werden kann.</param>
    /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn <paramref name="execute"/> null ist.</exception>
    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    /// <summary>
    /// Wird ausgelöst, wenn sich die Ausführbarkeit des Befehls ändert.
    /// </summary>
    public event EventHandler? CanExecuteChanged;

    /// <summary>
    /// Bestimmt, ob der Befehl ausgeführt werden kann.
    /// </summary>
    /// <param name="parameter">Das Befehlsparameter.</param>
    /// <returns>True, wenn der Befehl ausgeführt werden kann; andernfalls False.</returns>
    public bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke(parameter) ?? true;
    }

    /// <summary>
    /// Führt den Befehl aus.
    /// </summary>
    /// <param name="parameter">Das Befehlsparameter.</param>
    public void Execute(object? parameter)
    {
        _execute(parameter);
    }

    /// <summary>
    /// Löst das <see cref="CanExecuteChanged"/>-Ereignis aus.
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}