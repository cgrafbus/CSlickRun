using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace CSlickRun.Logic;

/// <summary>
/// Class for managing global hotkeys.
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
    /// Event that is triggered when the hotkey is pressed.
    /// </summary>
    public event Action? HotkeyPressed;

    /// <summary>
    /// Registers a global hotkey.
    /// </summary>
    /// <param name="window">The window that registers the hotkey.</param>
    /// <param name="keys">The list of keys that form the hotkey.</param>
    /// <exception cref="InvalidOperationException">Thrown if the window handle is invalid.</exception>
    /// <exception cref="Win32Exception">Thrown if the registration of the hotkey fails.</exception>
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

        if (_windowHandle == IntPtr.Zero)
        {
            throw new InvalidOperationException("Window handle is invalid.");
        }

        var success = RegisterHotKey(_windowHandle, HOTKEY_ID, modifiers, charKey);
        if (!success)
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        _hotkeyRegistered = true;
        ComponentDispatcher.ThreadFilterMessage += OnHotkeyPressed;
    }

    /// <summary>
    /// Checks if the hotkey is registered.
    /// </summary>
    /// <returns>True if the hotkey is registered, otherwise false.</returns>
    public bool IsHotkeyRegistered()
    {
        return _hotkeyRegistered;
    }

    /// <summary>
    /// Unregisters the global hotkey.
    /// </summary>
    public void UnregisterHotkey()
    {
        UnregisterHotKey(_windowHandle, HOTKEY_ID);
        ComponentDispatcher.ThreadFilterMessage -= OnHotkeyPressed;
        _hotkeyRegistered = false;
    }

    /// <summary>
    /// Event handler that is called when the hotkey is pressed.
    /// </summary>
    /// <param name="msg">The message.</param>
    /// <param name="handled">Indicates whether the message was handled.</param>
    private void OnHotkeyPressed(ref MSG msg, ref bool handled)
    {
        if (msg.message == 0x0312 && msg.wParam.ToInt32() == HOTKEY_ID)
        {
            HotkeyPressed?.Invoke();
            handled = true;
        }
    }
}
