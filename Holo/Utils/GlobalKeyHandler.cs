using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Holo.Utils;

public class GlobalKeyHandler
{
    private const int WM_KEYDOWN = 0x0100;

    private static HookProc _hookProc;
    private static nint _hookId = nint.Zero;

    public delegate nint HookProc(int nCode, nint wParam, nint lParam);

    public static event KeyEventHandler KeyDown;

    public static void Start()
    {
        _hookProc = HookCallback;
        _hookId = SetHook();
    }

    public static void Stop()
    {
        UnhookWindowsHookEx(_hookId);
    }

    private static nint SetHook()
    {
        using Process curProcess = Process.GetCurrentProcess();
        using ProcessModule curModule = curProcess.MainModule;

        return SetWindowsHookEx(WH_KEYBOARD_LL, _hookProc, GetModuleHandle(curModule?.ModuleName), 0);
    }

    private static nint HookCallback(int nCode, nint wParam, nint lParam)
    {
        if (nCode >= 0 && wParam == WM_KEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            Keys key = (Keys)vkCode;

            if (wParam == WM_KEYDOWN)
                KeyDown?.Invoke(null, new KeyEventArgs(key));
        }

        return CallNextHookEx(_hookId, nCode, wParam, lParam);
    }

    #region WinAPI

    private const int WH_KEYBOARD_LL = 13;

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint SetWindowsHookEx(int idHook, HookProc lpfn, nint hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(nint hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint CallNextHookEx(nint hhk, int nCode, nint wParam, nint lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint GetModuleHandle(string lpModuleName);

    #endregion
}
