using Holo.Harvestable;
using Holo.Utils;
using System;
using System.IO;

namespace Holo;

public sealed class Config
{
    public Radar RadarParams { get; set; } = new();
    public PlayerSettings Players { get; set; } = new();
    public HarvestableSettings Wood { get; set; } = new();
    public HarvestableSettings Stone { get; set; } = new();
    public HarvestableSettings Hide { get; set; } = new();
    public HarvestableSettings Ore { get; set; } = new();
    public HarvestableSettings Fiber { get; set; } = new();

    static Config()
    {
        try
        {
            if (File.Exists(FileName))
                Instance = Serializer.DeserializeFromFile<Config>(FileName);
        }
        catch (Exception)
        {
            // ignored
        }
        finally
        {
            if (Instance == null)
                Default();

            Save();
        }
    }

    public const string FileName = "config.json";
    public static Config Instance;

    public class Radar
    {
        public float XOffset { get; set; } = 10;
        public float YOffset { get; set; } = 160;
        public float Size { get; set; } = 200;
        public float Scale { get; set; } = 4.0f;
    }

    public class HarvestableSettings
    {
        public class TierSettings
        {
            public bool Enabled { get; set; } = true;
            public bool[] Enchants { get; set; } = [true, true, true, true];
        }

        public HarvestableSettings()
        {
            for (int i = 0; i < Tier.Length; ++i)
                Tier[i] = new TierSettings();
        }

        public TierSettings[] Tier { get; set; } = new TierSettings[8];
    }

    public class PlayerSettings
    {
        public bool ShowPlayers { get; set; } = true;
        public bool PlaySound { get; set; } = false;
    }

    private static void Default()
    {
        Instance = new Config();
        Save();
    }

    public static void Save()
    {
        Serializer.Serialize(Instance, FileName);
    }

    public bool CanShowHarvestable(HarvestableType type, byte tier, byte enchant)
    {
        HarvestableSettings settings;

        switch (type)
        {
            case >= HarvestableType.FIBER and <= HarvestableType.FIBER_GUARDIAN_DEAD:
                settings = Fiber;
                break;
            case <= HarvestableType.WOOD_GUARDIAN_RED:
                settings = Wood;
                break;
            case >= HarvestableType.ROCK and <= HarvestableType.ROCK_GUARDIAN_RED:
                settings = Stone;
                break;
            case >= HarvestableType.HIDE and <= HarvestableType.HIDE_GUARDIAN:
                settings = Hide;
                break;
            case >= HarvestableType.ORE and <= HarvestableType.ORE_GUARDIAN_RED:
                settings = Ore;
                break;
            default:
                return false;
        }

        if (!settings.Tier[tier - 1].Enabled)
            return false;

        if (enchant > 0 && !settings.Tier[tier - 1].Enchants[enchant - 1])
            return false;

        return true;
    }
}
