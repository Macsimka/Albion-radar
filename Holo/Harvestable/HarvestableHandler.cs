using System.Collections.Concurrent;

namespace Holo.Harvestable;

public static class HarvestableHandler
{
    public static readonly ConcurrentDictionary<int, Harvestable> Harvestables = [];

    public static void AddHarvestable(int id, byte type, byte tier, float posX, float posY, byte charges, byte size)
    {
        Harvestable h = new(id, type, tier, posX, posY, charges, size);
        Harvestables.AddOrUpdate(id, h, (_, _) => h);
    }

    public static void RemoveHarvestable(int id)
    {
        Harvestables.TryRemove(id, out _);
    }

    public static void UpdateHarvestable(int id, byte count)
    {
        if (Harvestables.TryGetValue(id, out Harvestable harvestable))
            harvestable.Size = count;
    }

    public static void Reset()
    {
        Harvestables.Clear();
    }
}
