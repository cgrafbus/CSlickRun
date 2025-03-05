using System.Windows.Input;

namespace CSlickRun.UI.ViewModels.Base;

/// <summary>
/// Eine Implementierung von <see cref="ICommand"/>, die asynchrone Operationen unterstützt.
/// </summary>
public class AsyncRelayCommand : ICommand
{
    private readonly Func<object?, bool>? _canExecute;
    private readonly Func<object?, Task> _executeAsync;
    private bool _isExecuting;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="AsyncRelayCommand"/> Klasse.
    /// </summary>
    /// <param name="executeAsync">Die asynchrone Aktion, die ausgeführt werden soll.</param>
    /// <param name="canExecute">Eine Funktion, die bestimmt, ob der Befehl ausgeführt werden kann.</param>
    /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn <paramref name="executeAsync"/> null ist.</exception>
    public AsyncRelayCommand(Func<object?, Task> executeAsync, Func<object?, bool>? canExecute = null)
    {
        _executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
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
        return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
    }

    /// <summary>
    /// Führt den Befehl asynchron aus.
    /// </summary>
    /// <param name="parameter">Das Befehlsparameter.</param>
    public async void Execute(object? parameter)
    {
        if (!CanExecute(parameter))
            return;

        _isExecuting = true;
        RaiseCanExecuteChanged();

        try
        {
            await _executeAsync(parameter);
        }
        finally
        {
            _isExecuting = false;
            RaiseCanExecuteChanged();
        }
    }

    /// <summary>
    /// Löst das <see cref="CanExecuteChanged"/>-Ereignis aus.
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}


