using Holo.Drawing;
using Holo.Harvestable;
using Holo.Player;
using Overlay.NET.Common;
using Overlay.NET.Directx;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using System;
using System.Threading;

namespace Holo;

public sealed class Overlay : DirectXOverlayPlugin
{
    private readonly TickEngine _tickEngine = new();
    private const int _updateRate = 1000 / 60;

    private const string WindowName = "Albion Online Client";
    private const float MinimapBorderWidth = 2.0f;

    private int _interiorBrush;
    private int _borderBrush;
    private int _gridBrush;
    private int _localPlayerBrush;
    private int _playerBrush;

    public override void Initialize(IntPtr targetWindowHandle)
    {
        while (targetWindowHandle == IntPtr.Zero)
        {
            targetWindowHandle = GetTargetWindow();
            Thread.Sleep(10);
        }

        base.Initialize(targetWindowHandle);

        OverlayWindow = new DirectXOverlayWindow(targetWindowHandle, false);

        ImageHandler.Load(OverlayWindow.Graphics.GetDevice());

        _interiorBrush = OverlayWindow.Graphics.CreateBrush(System.Drawing.Color.FromArgb(128, System.Drawing.Color.Black));
        _borderBrush = OverlayWindow.Graphics.CreateBrush(System.Drawing.Color.Black);
        _gridBrush = OverlayWindow.Graphics.CreateBrush(System.Drawing.Color.FromArgb(128, 45, 45, 45));

        _localPlayerBrush = OverlayWindow.Graphics.CreateBrush(System.Drawing.Color.Blue);
        _playerBrush = OverlayWindow.Graphics.CreateBrush(System.Drawing.Color.Red);

        _tickEngine.PreTick += OnPreTick;
        _tickEngine.Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        if (!OverlayWindow.IsVisible)
            return;

        OverlayWindow.Update();
        InternalRender();
    }

    private void OnPreTick(object sender, EventArgs e)
    {
        var targetWindowIsActivated = IsActive();

        if (!targetWindowIsActivated)
        {
            IntPtr gameWindowHandle = GetTargetWindow();

            if (gameWindowHandle != IntPtr.Zero)
            {
                if (OverlayWindow.ParentWindow != gameWindowHandle)
                {
                    base.Initialize(gameWindowHandle);
                    OverlayWindow.UpdateParentWindow(gameWindowHandle);
                    targetWindowIsActivated = IsActive();
                }

                if (!targetWindowIsActivated)
                    targetWindowIsActivated = MainForm.CanShowOverlay();
            }
        }

        if (!targetWindowIsActivated && OverlayWindow.IsVisible)
        {
            ClearScreen();
            OverlayWindow.Hide();
        }
        else if (targetWindowIsActivated && !OverlayWindow.IsVisible)
            OverlayWindow.Show();
    }

    public override void Enable()
    {
        _tickEngine.Interval = TimeSpan.FromMicroseconds(_updateRate);
        _tickEngine.IsTicking = true;
        base.Enable();
    }

    public override void Disable()
    {
        _tickEngine.IsTicking = false;
        base.Disable();
    }

    public override void Update() => _tickEngine.Pulse();

    private void InternalRender()
    {
        float xOffset = Config.Instance.RadarParams.XOffset;
        float yOffset = Config.Instance.RadarParams.YOffset;
        float minimapSize = Config.Instance.RadarParams.Size;
        float gridWidth = minimapSize / 10;

        RawMatrix3x2 originalTransform = OverlayWindow.Graphics.GetDevice().Transform;
        Matrix3x2 translateMatrix = Matrix3x2.Translation(xOffset + minimapSize / 2, yOffset + minimapSize / 2);

        float maxAllowedCoordinate = minimapSize / 2 - MinimapBorderWidth;

        OverlayWindow.Graphics.BeginScene();
        OverlayWindow.Graphics.ClearScene();

        OverlayWindow.Graphics.DrawRectangle(xOffset, yOffset, minimapSize, minimapSize, MinimapBorderWidth, _borderBrush);
        OverlayWindow.Graphics.FillRectangle(xOffset, yOffset, minimapSize, minimapSize, _interiorBrush);

        for (float x = gridWidth; x < minimapSize; x += gridWidth)
            OverlayWindow.Graphics.DrawLine(ShiftX(x), ShiftY(0), ShiftX(x), yOffset + minimapSize - MinimapBorderWidth / 2, 1, _gridBrush);

        for (float y = gridWidth; y < minimapSize; y += gridWidth)
            OverlayWindow.Graphics.DrawLine(ShiftX(0), ShiftY(y), xOffset + minimapSize - MinimapBorderWidth / 2, ShiftY(y), 1, _gridBrush);

        OverlayWindow.Graphics.GetDevice().Transform = translateMatrix;

        // Dot in center (owr player)
        OverlayWindow.Graphics.FillCircle(1, 1, 5, _localPlayerBrush);

        float lpX = PlayerHandler.GetLocalPlayerPosX();
        float lpY = PlayerHandler.GetLocalPlayerPosY();

        // Draw Harvestable
        foreach (var pair in HarvestableHandler.Harvestables)
        {
            var h = pair.Value;

            if (!Config.Instance.CanShowHarvestable((HarvestableType)h.Type, h.Tier, h.Charges))
                continue;

            if (h.Size == 0)
                continue;

            float hX = -1 * h.PosX + lpX;
            float hY = h.PosY - lpY;

            TransformPoint(ref hX, ref hY);

            if (Math.Abs(hX) > maxAllowedCoordinate || Math.Abs(hY) > maxAllowedCoordinate)
                continue;

            string iconName = string.Empty;

            if (h.Type is >= (byte)HarvestableType.FIBER and <= (byte)HarvestableType.FIBER_GUARDIAN_DEAD)
                iconName = "fiber_" + h.Tier + "_" + h.Charges;
            else if (h.Type <= (byte)HarvestableType.WOOD_GUARDIAN_RED)
                iconName = "logs_" + h.Tier + "_" + h.Charges;
            else if (h.Type is >= (byte)HarvestableType.ROCK and <= (byte)HarvestableType.ROCK_GUARDIAN_RED)
                iconName = "rock_" + h.Tier + "_" + h.Charges;
            else if (h.Type is >= (byte)HarvestableType.HIDE and <= (byte)HarvestableType.HIDE_GUARDIAN)
                iconName = "hide_" + h.Tier + "_" + h.Charges;
            else if (h.Type is >= (byte)HarvestableType.ORE and <= (byte)HarvestableType.ORE_GUARDIAN_RED)
                iconName = "ore_" + h.Tier + "_" + h.Charges;

            if (string.IsNullOrEmpty(iconName))
                continue;

            Bitmap icon = ImageHandler.GetImage(iconName);
            if (icon == null)
                continue;

            hX -= icon.Size.Width / 2;
            hY -= icon.Size.Height / 2;

            if (Math.Abs(hX) > maxAllowedCoordinate || Math.Abs(hY) > maxAllowedCoordinate)
                continue;

            OverlayWindow.Graphics.DrawBitmap(hX, hY, icon, 1, BitmapInterpolationMode.Linear);

            //if (h.Charges > 0)
            //    g.DrawEllipse(ChargePen[h.Charges], hX - 3, hY - 3, 6, 6);
        }

        if (Config.Instance.Players.ShowPlayers)
        {
            foreach (var pair in PlayerHandler.PlayersInRange)
            {
                var p = pair.Value;

                float hX = -1 * p.PosX + lpX;
                float hY = p.PosY - lpY;

                TransformPoint(ref hX, ref hY);

                if (Math.Abs(hX) > maxAllowedCoordinate || Math.Abs(hY) > maxAllowedCoordinate)
                    continue;

                OverlayWindow.Graphics.FillEllipse(hX, hY, 5, _playerBrush);
            }
        }

        OverlayWindow.Graphics.GetDevice().Transform = originalTransform;
        OverlayWindow.Graphics.EndScene();
    }

    public override void Dispose()
    {
        OverlayWindow.Dispose();
        base.Dispose();
    }

    private void ClearScreen()
    {
        OverlayWindow.Graphics.BeginScene();
        OverlayWindow.Graphics.ClearScene();
        OverlayWindow.Graphics.EndScene();
    }

    public static IntPtr GetTargetWindow()
    {
        return Native.FindWindow(null, WindowName);
    }

    private static float ShiftX(float x) { return Config.Instance.RadarParams.XOffset + x + MinimapBorderWidth / 2; }
    private static float ShiftY(float y) { return Config.Instance.RadarParams.YOffset + y + MinimapBorderWidth / 2; }

    private static void TransformPoint(ref float x, ref float y)
    {
        const float angle = 225.0f * (float)Math.PI / 180.0f;

        float newX = x * (float)Math.Cos(angle) - y * (float)Math.Sin(angle);
        float newY = x * (float)Math.Sin(angle) + y * (float)Math.Cos(angle);

        newX *= Config.Instance.RadarParams.Scale;
        newY *= Config.Instance.RadarParams.Scale;

        x = newX;
        y = newY;
    }
}
