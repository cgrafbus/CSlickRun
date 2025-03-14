using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace CSlickRun.Logic;

/// <summary>
///     Klasse zur Verwaltung von globalen Hotkeys.
/// </summary>
public class KeyboardHook
{
    private const int HOTKEY_ID = 9000;
    private const uint MOD_ALT = 0x0001;
    private const uint MOD_CONTROL = 0x0002;
    private const uint MOD_SHIFT = 0x0004;
    private const uint MOD_WIN = 0x0008;

    private bool _hotkeyRegistered;
    private IntPtr _windowHandle;

    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    /// <summary>
    ///     Ereignis, das ausgelöst wird, wenn der Hotkey gedrückt wird.
    /// </summary>
    public event Action HotkeyPressed;

    /// <summary>
    ///     Registriert einen globalen Hotkey.
    /// </summary>
    /// <param name="window">Das Fenster, das den Hotkey registriert.</param>
    /// <param name="keys">Die Liste der Tasten, die den Hotkey bilden.</param>
    /// <exception cref="InvalidOperationException">Wird ausgelöst, wenn das Fensterhandle ungültig ist.</exception>
    /// <exception cref="Win32Exception">Wird ausgelöst, wenn die Registrierung des Hotkeys fehlschlägt.</exception>
    public void RegisterHotkey(Window window, List<string> keys)
    {
        if (keys.Count == 0) return;

        List<uint> keyCodes = new();
        foreach (var key in keys)
            if (VirtualKeyCodes.KeyCodes.TryGetValue(key, out var code))
                keyCodes.Add(code);

        var charKey = keyCodes.Last();
        keyCodes.RemoveAt(keyCodes.Count - 1);

        uint modifiers = 0;

        foreach (var code in keyCodes)
            switch (code)
            {
                case 0xA4:
                case 0xA5:
                    modifiers |= MOD_ALT; // Left/Right ALT
                    break;
                case 0xA2:
                case 0xA3:
                    modifiers |= MOD_CONTROL; // Left/Right CTRL
                    break;
                case 0xA0:
                case 0xA1:
                    modifiers |= MOD_SHIFT; // Left/Right SHIFT
                    break;
                case 0x5B:
                case 0x5C:
                    modifiers |= MOD_WIN; // Left/Right WIN
                    break;
            }

        var helper = new WindowInteropHelper(window);
        _windowHandle = helper.Handle;

        if (_windowHandle == IntPtr.Zero) throw new InvalidOperationException("Window handle is invalid.");

        var success = RegisterHotKey(_windowHandle, HOTKEY_ID, modifiers, charKey);
        if (!success) throw new Win32Exception(Marshal.GetLastWin32Error());

        _hotkeyRegistered = true;
        ComponentDispatcher.ThreadFilterMessage += OnHotkeyPressed;
    }

    public bool IsHotkeyRegistered()
    {
        return _hotkeyRegistered;
    }

    /// <summary>
    ///     Hebt die Registrierung des globalen Hotkeys auf.
    /// </summary>
    public void UnregisterHotkey()
    {
        UnregisterHotKey(_windowHandle, HOTKEY_ID);
        ComponentDispatcher.ThreadFilterMessage -= OnHotkeyPressed;
        _hotkeyRegistered = false;
    }

    /// <summary>
    ///     Event-Handler, der aufgerufen wird, wenn der Hotkey gedrückt wird.
    /// </summary>
    /// <param name="msg">Die Nachricht.</param>
    /// <param name="handled">Gibt an, ob die Nachricht behandelt wurde.</param>
    private void OnHotkeyPressed(ref MSG msg, ref bool handled)
    {
        if (msg.message == 0x0312 && msg.wParam.ToInt32() == HOTKEY_ID)
        {
            HotkeyPressed?.Invoke();
            handled = true;
        }
    }
}