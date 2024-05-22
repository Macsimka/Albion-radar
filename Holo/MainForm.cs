using Holo.Networking;
using Holo.Utils;
using SharpPcap;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Holo;

public partial class MainForm : Form
{
    private static readonly object LogFileLock = new();

    private readonly PacketHandler _packetHandler = new();
    private readonly DarkModeCS DM = null;
    private static MainForm _mainForm;
    private static SettingForm _settingForm;

    public MainForm()
    {
        System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";

        Thread.CurrentThread.CurrentCulture = customCulture;

        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
        {
            string logMessage = $"==================[{DateTime.Now}]==================" + Environment.NewLine +
                                args.ExceptionObject + Environment.NewLine +
                                "=========================================================";

            WriteLogToFile(logMessage);
        };

        InitializeComponent();
        _mainForm = this;

        DM = new DarkModeCS(this);

        CreateListener();

        new Thread(() =>
        {
            Overlay overlay = new();
            overlay.Initialize(IntPtr.Zero);
            overlay.Enable();

            while (true)
            {
                overlay.Update();
                Thread.Sleep(4); // prevent burn CPU/GPU
            }

            // ReSharper disable once FunctionNeverReturns
        }).Start();

        GlobalKeyHandler.Start();
        AppDomain.CurrentDomain.ProcessExit += (_, _) => GlobalKeyHandler.Stop();

        GlobalKeyHandler.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Home)
                ToggleSettingsWindow();
        };
    }

    private void CreateListener()
    {
        CaptureDeviceList allDevices = CaptureDeviceList.Instance;

        if (allDevices.Count == 0)
        {
            Log("No interfaces found! Make sure WinPcap is installed.");
            return;
        }

        foreach (var device in allDevices)
        {
            device.OnPacketArrival += _packetHandler.HandlePacket;
            device.Open(DeviceModes.Promiscuous, 5);
            device.Filter = "udp and (dst port 5056 or src port 5056)";
            device.StartCapture();
        }
    }

    public static void Log(string message)
    {
        void write()
        {
            _mainForm.rbLog.AppendText(message + Environment.NewLine);
            _mainForm.rbLog.ScrollToCaret();
        }

        if (_mainForm.rbLog.InvokeRequired)
            _mainForm.rbLog.Invoke(write);
        else
            write();
    }

    private static void WriteLogToFile(string message)
    {
        lock (LogFileLock)
        {
            using StreamWriter sw = File.AppendText("logs.txt");
            sw.WriteLine(message);
        }
    }

    public static void UpdatePlayerPos(float x, float y)
    {
        void update()
        {
            _mainForm.lPlayerPos.Text = $@"{x} {y}";
        }

        if (_mainForm.lPlayerPos.InvokeRequired)
            _mainForm.lPlayerPos.Invoke(update);
        else
            update();
    }

    public static void SetMapID(string mapID)
    {
        void update()
        {
            _mainForm.lMapID.Text = mapID;
            _mainForm.lMapID.ForeColor = Color.Green;
        }

        if (_mainForm.lMapID.InvokeRequired)
            _mainForm.lMapID.Invoke(update);
        else
            update();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        Environment.Exit(Environment.ExitCode);
    }

    public static bool CanShowOverlay()
    {
        return _settingForm is { Visible: true };
    }

    private void bSettings_Click(object sender, EventArgs e)
    {
        ToggleSettingsWindow();
    }

    private void ToggleSettingsWindow()
    {
        if (_settingForm == null)
        {
            _settingForm = new SettingForm();
            _settingForm.Show();

            _settingForm.Closing += (_, _) => { _settingForm = null; };
        }
        else
        {
            _settingForm.Close();
            _settingForm = null;
        }
    }
}
