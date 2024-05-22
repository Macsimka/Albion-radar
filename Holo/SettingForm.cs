using Holo.Utils;
using System;
using System.Windows.Forms;

namespace Holo;

public partial class SettingForm : Form
{
    private bool CanChangeSettings;
    private DarkModeCS DM;

    public SettingForm()
    {
        InitializeComponent();
        DM = new DarkModeCS(this);
    }

    private void SettingForm_Load(object sender, EventArgs e)
    {
        var config = Config.Instance;

        nRadarX.Value = (decimal)config.RadarParams.XOffset;
        nRadarY.Value = (decimal)config.RadarParams.YOffset;
        nRadarSize.Value = (decimal)config.RadarParams.Size;
        nRadarScale.Value = (decimal)config.RadarParams.Scale;

        cbShowPlayers.Checked = config.Players.ShowPlayers;
        cbPlaySound.Checked = config.Players.PlaySound;

        // Wood
        for (int i = 0; i < config.Wood.Tier.Length; ++i)
        {
            var cbs = Controls.Find($"cbWoodT{i + 1}", true);

            if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                throw new();

            ((CheckBox)cbs[0]).Checked = config.Wood.Tier[i].Enabled;

            // Wood has enchants starting from tier 4
            if (i < 3)
                continue;

            for (int x = 0; x < config.Wood.Tier[i].Enchants.Length; ++x)
            {
                cbs = Controls.Find($"cbWoodT{i + 1}E{x + 1}", true);

                if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                    throw new();

                ((CheckBox)cbs[0]).Checked = config.Wood.Tier[i].Enchants[x];
                ((CheckBox)cbs[0]).Enabled = config.Wood.Tier[i].Enabled;
            }
        }

        // Stone
        for (int i = 0; i < config.Stone.Tier.Length; ++i)
        {
            var cbs = Controls.Find($"cbStoneT{i + 1}", true);

            if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                throw new();

            ((CheckBox)cbs[0]).Checked = config.Stone.Tier[i].Enabled;

            // Stone has enchants starting from tier 4
            if (i < 3)
                continue;

            for (int x = 0; x < config.Stone.Tier[i].Enchants.Length; ++x)
            {
                cbs = Controls.Find($"cbStoneT{i + 1}E{x + 1}", true);

                if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                    throw new();

                ((CheckBox)cbs[0]).Checked = config.Stone.Tier[i].Enchants[x];
                ((CheckBox)cbs[0]).Enabled = config.Stone.Tier[i].Enabled;
            }
        }

        // Hide
        for (int i = 0; i < config.Hide.Tier.Length; ++i)
        {
            var cbs = Controls.Find($"cbHideT{i + 1}", true);

            if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                throw new();

            ((CheckBox)cbs[0]).Checked = config.Hide.Tier[i].Enabled;

            // Hide has enchants starting from tier 4
            if (i < 3)
                continue;

            for (int x = 0; x < config.Hide.Tier[i].Enchants.Length; ++x)
            {
                cbs = Controls.Find($"cbHideT{i + 1}E{x + 1}", true);

                if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                    throw new();

                ((CheckBox)cbs[0]).Checked = config.Hide.Tier[i].Enchants[x];
                ((CheckBox)cbs[0]).Enabled = config.Hide.Tier[i].Enabled;
            }
        }

        // Ore
        for (int i = 1; i < config.Ore.Tier.Length; ++i)
        {
            var cbs = Controls.Find($"cbOreT{i + 1}", true);

            if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                throw new();

            ((CheckBox)cbs[0]).Checked = config.Ore.Tier[i].Enabled;

            // Ore has enchants starting from tier 4
            if (i < 3)
                continue;

            for (int x = 0; x < config.Ore.Tier[i].Enchants.Length; ++x)
            {
                cbs = Controls.Find($"cbOreT{i + 1}E{x + 1}", true);

                if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                    throw new();

                ((CheckBox)cbs[0]).Checked = config.Ore.Tier[i].Enchants[x];
                ((CheckBox)cbs[0]).Enabled = config.Ore.Tier[i].Enabled;
            }
        }

        // Fiber
        for (int i = 1; i < config.Fiber.Tier.Length; ++i)
        {
            var cbs = Controls.Find($"cbFiberT{i + 1}", true);

            if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                throw new();

            ((CheckBox)cbs[0]).Checked = config.Fiber.Tier[i].Enabled;

            // Fiber has enchants starting from tier 4
            if (i < 3)
                continue;

            for (int x = 0; x < config.Fiber.Tier[i].Enchants.Length; ++x)
            {
                cbs = Controls.Find($"cbFiberT{i + 1}E{x + 1}", true);

                if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                    throw new();

                ((CheckBox)cbs[0]).Checked = config.Fiber.Tier[i].Enchants[x];
                ((CheckBox)cbs[0]).Enabled = config.Fiber.Tier[i].Enabled;
            }
        }

        CanChangeSettings = true;
    }

    private void PropertyChanged(object sender, EventArgs e)
    {
        UpdateSettings();
    }

    private void UpdateSettings()
    {
        if (!CanChangeSettings)
            return;

        var config = Config.Instance;

        config.RadarParams.XOffset = (float)nRadarX.Value;
        config.RadarParams.YOffset = (float)nRadarY.Value;
        config.RadarParams.Size = (float)nRadarSize.Value;
        config.RadarParams.Scale = (float)nRadarScale.Value;

        config.Players.ShowPlayers = cbShowPlayers.Checked;
        config.Players.PlaySound = cbPlaySound.Checked;

        // Wood
        for (int i = 0; i < config.Wood.Tier.Length; ++i)
        {
            var cbs = Controls.Find($"cbWoodT{i + 1}", true);

            if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                throw new();

            config.Wood.Tier[i].Enabled = ((CheckBox)cbs[0]).Checked;

            // Wood has enchants starting from tier 4
            if (i < 3)
                continue;

            for (int x = 0; x < config.Wood.Tier[i].Enchants.Length; ++x)
            {
                cbs = Controls.Find($"cbWoodT{i + 1}E{x + 1}", true);

                if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                    throw new();

                config.Wood.Tier[i].Enchants[x] = ((CheckBox)cbs[0]).Checked;
                ((CheckBox)cbs[0]).Enabled = config.Wood.Tier[i].Enabled;
            }
        }

        // Stone
        for (int i = 0; i < config.Stone.Tier.Length; ++i)
        {
            var cbs = Controls.Find($"cbStoneT{i + 1}", true);

            if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                throw new();

            config.Stone.Tier[i].Enabled = ((CheckBox)cbs[0]).Checked;

            // Stone has enchants starting from tier 4
            if (i < 3)
                continue;

            for (int x = 0; x < config.Stone.Tier[i].Enchants.Length; ++x)
            {
                cbs = Controls.Find($"cbStoneT{i + 1}E{x + 1}", true);

                if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                    throw new();

                config.Stone.Tier[i].Enchants[x] = ((CheckBox)cbs[0]).Checked;
                ((CheckBox)cbs[0]).Enabled = config.Stone.Tier[i].Enabled;
            }
        }

        // Hide
        for (int i = 0; i < config.Hide.Tier.Length; ++i)
        {
            var cbs = Controls.Find($"cbHideT{i + 1}", true);

            if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                throw new();

            config.Hide.Tier[i].Enabled = ((CheckBox)cbs[0]).Checked;

            // Hide has enchants starting from tier 4
            if (i < 3)
                continue;

            for (int x = 0; x < config.Hide.Tier[i].Enchants.Length; ++x)
            {
                cbs = Controls.Find($"cbHideT{i + 1}E{x + 1}", true);

                if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                    throw new();

                config.Hide.Tier[i].Enchants[x] = ((CheckBox)cbs[0]).Checked;
                ((CheckBox)cbs[0]).Enabled = config.Hide.Tier[i].Enabled;
            }
        }

        // Ore
        for (int i = 1; i < config.Ore.Tier.Length; ++i)
        {
            var cbs = Controls.Find($"cbOreT{i + 1}", true);

            if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                throw new();

            config.Ore.Tier[i].Enabled = ((CheckBox)cbs[0]).Checked;

            // Ore has enchants starting from tier 4
            if (i < 3)
                continue;

            for (int x = 0; x < config.Ore.Tier[i].Enchants.Length; ++x)
            {
                cbs = Controls.Find($"cbOreT{i + 1}E{x + 1}", true);

                if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                    throw new();

                config.Ore.Tier[i].Enchants[x] = ((CheckBox)cbs[0]).Checked;
                ((CheckBox)cbs[0]).Enabled = config.Ore.Tier[i].Enabled;
            }
        }

        // Fiber
        for (int i = 1; i < config.Fiber.Tier.Length; ++i)
        {
            var cbs = Controls.Find($"cbFiberT{i + 1}", true);

            if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                throw new();

            config.Fiber.Tier[i].Enabled = ((CheckBox)cbs[0]).Checked;

            // Fiber has enchants starting from tier 4
            if (i < 3)
                continue;

            for (int x = 0; x < config.Fiber.Tier[i].Enchants.Length; ++x)
            {
                cbs = Controls.Find($"cbFiberT{i + 1}E{x + 1}", true);

                if (cbs.Length != 1 || cbs[0].GetType() != typeof(CheckBox))
                    throw new();

                config.Fiber.Tier[i].Enchants[x] = ((CheckBox)cbs[0]).Checked;
                ((CheckBox)cbs[0]).Enabled = config.Fiber.Tier[i].Enabled;
            }
        }

        Config.Save();
    }
}
