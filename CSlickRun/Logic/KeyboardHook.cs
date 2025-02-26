using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace CSlickRun.Logic;

public class KeyboardHook
{
    private const int HOTKEY_ID = 9000; // Unique ID for the hotkey

    private IntPtr _windowHandle;

    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    public event Action HotkeyPressed;

    public void RegisterHotkey(Window window, List<string> keys)
    {
        if (keys.Count == 0)
        {
            return;
        }

        List<uint> keyCodes = new();
        foreach (var key in keys)
        {
            if (VirtualKeyCodes.KeyCodes.TryGetValue(key, out var code))
            {
                keyCodes.Add(code);
            }
        }

        var charKey = keyCodes.Last(); 
        keyCodes.RemoveAt(keys.Count - 1);
        var modifiers = keyCodes.Aggregate<uint, uint>(0, (current, code) => current | code);
        var helper = new WindowInteropHelper(window);
        _windowHandle = helper.Handle;
        RegisterHotKey(_windowHandle, HOTKEY_ID, modifiers, charKey);
        ComponentDispatcher.ThreadFilterMessage += OnHotkeyPressed;
    }

    public void UnregisterHotkey()
    {
        UnregisterHotKey(_windowHandle, HOTKEY_ID);
        ComponentDispatcher.ThreadFilterMessage -= OnHotkeyPressed;
    }

    private void OnHotkeyPressed(ref MSG msg, ref bool handled)
    {
        if (msg.message == 0x0312 && msg.wParam.ToInt32() == HOTKEY_ID)
        {
            HotkeyPressed?.Invoke();
            handled = true;
        }
    }
}