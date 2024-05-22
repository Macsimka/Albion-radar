using System.Drawing;

namespace Holo.Drawing;

public static class DrawingHandler
{
    private static void DrawMobs(Graphics g)
    {
        /*float lpX = PlayerHandler.GetLocalPlayerPosX();
        float lpY = PlayerHandler.GetLocalPlayerPosY();

        foreach (var pair in MobsHandler.Mobs)
        {
            Mob m = pair.Value;

            float hX = -1 * m.PosX + lpX;
            float hY = m.PosY - lpY;

                //DrawingUtils.RotatePoint(ref hX, ref hY);

            byte mobTier = 5;
            MobType mobType = MobType.OTHER;

            if (m.MobInfo != null)
            {
                mobTier = m.MobInfo.Tier;
                mobType = m.MobInfo.MobType;

                if (!Settings.IsInMobs(mobType))
                    continue;

                if (!Settings.IsInTiers(mobTier, m.EnchantmentLevel))
                    continue;

                switch (m.MobInfo.HarvestableMobType)
                {
                    case HarvestableMobType.ESSENCE:
                    //idk?
                    case HarvestableMobType.SWAMP:
                        if (!Settings.IsInHarvestable(HarvestableType.FIBER))
                            continue;
                        break;
                    case HarvestableMobType.STEPPE:
                        if (!Settings.IsInHarvestable(HarvestableType.HIDE))
                            continue;
                        break;
                    case HarvestableMobType.MOUNTAIN:
                        if (!Settings.IsInHarvestable(HarvestableType.ORE))
                            continue;
                        break;
                    case HarvestableMobType.FOREST:
                        if (!Settings.IsInHarvestable(HarvestableType.WOOD))
                            continue;
                        break;
                    case HarvestableMobType.HIGHLAND:
                        if (!Settings.IsInHarvestable(HarvestableType.ROCK))
                            continue;
                        break;
                }

                if (Settings.IsInTiers(mobTier, m.EnchantmentLevel))
                {
                    g.FillEllipse(HarvestBrushes[mobTier], hX - 2.5f, hY - 2.5f, 5f, 5f);
                    g.TranslateTransform(hX, hY);
                    g.RotateTransform(135f);
                    g.DrawString(m.getMapStringInfo(), Font, FontPerColor[mobTier], -2.5f, -2.5f);
                    g.RotateTransform(-135f);
                    g.TranslateTransform(-hX, -hY);
                }
            }
            else
            {
                if (Settings.IsInMobs(MobType.OTHER))
                    g.FillEllipse(Brushes.Black, hX - 1, hY - 1, 2f, 2f);

                continue;
            }

            if (m.EnchantmentLevel > 0)
                g.DrawEllipse(ChargePen[m.EnchantmentLevel], hX - 3, hY - 3, 6, 6);
        }*/

    }
}
