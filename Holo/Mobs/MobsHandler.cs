using System.Collections.Concurrent;

namespace Holo.Mobs;

public static class MobsHandler
{
    public static readonly ConcurrentDictionary<int, Mob> Mobs = [];

    public static void AddMob(int id, int typeId, float posX, float posY, int health)
    {
        Mob m = new Mob(id, typeId, posX, posY, health, 0);
        Mobs.AddOrUpdate(id, m, (_, _) => m);
    }

    public static void RemoveMob(int id)
    {
        Mobs.TryRemove(id, out _);
    }

    internal static void UpdateMobEnchantmentLevel(int mobId, byte enchantmentLevel)
    {
        if (Mobs.TryGetValue(mobId, out Mob mob))
            mob.EnchantmentLevel = enchantmentLevel;
    }

    public static void Reset()
    {
        Mobs.Clear();
    }
}
