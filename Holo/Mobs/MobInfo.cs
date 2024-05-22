using Holo.Harvestable;
using System.Collections.Generic;
using System.Linq;

namespace Holo.Mobs;

public sealed class MobInfo
{
    public static readonly List<MobInfo> MobsInfo =
    [
        new(9, 1, MobType.SKINNABLE),
        new(9, 1, MobType.SKINNABLE),
        new(16, 1, MobType.SKINNABLE),
        new(17, 1, MobType.SKINNABLE),
        new(18, 1, MobType.SKINNABLE),
        new(19, 1, MobType.SKINNABLE),
        new(20, 1, MobType.SKINNABLE),
        new(21, 1, MobType.SKINNABLE),
        new(22, 1, MobType.SKINNABLE),
        new(23, 2, MobType.SKINNABLE),
        new(24, 3, MobType.SKINNABLE),
        new(25, 4, MobType.SKINNABLE),
        new(26, 5, MobType.SKINNABLE),
        new(27, 6, MobType.SKINNABLE),
        new(28, 7, MobType.SKINNABLE),
        new(29, 8, MobType.SKINNABLE),
        new(30, 1, MobType.SKINNABLE),
        new(31, 2, MobType.SKINNABLE),
        new(32, 3, MobType.SKINNABLE),
        new(34, 5, MobType.SKINNABLE),
        new(36, 1, MobType.SKINNABLE),
        new(37, 2, MobType.SKINNABLE),
        new(38, 3, MobType.SKINNABLE),
        new(41, 6, MobType.SKINNABLE),
        new(42, 7, MobType.SKINNABLE),
        new(43, 8, MobType.SKINNABLE),
        new(44, 1, MobType.SKINNABLE),
        new(45, 1, MobType.SKINNABLE),
        new(46, 6, MobType.HARVESTABLE, HarvestableMobType.ESSENCE),
        new(47, 6, MobType.HARVESTABLE, HarvestableMobType.ESSENCE),
        new(48, 3, MobType.HARVESTABLE, HarvestableMobType.SWAMP),
        new(49, 5, MobType.HARVESTABLE, HarvestableMobType.SWAMP),
        new(50, 7, MobType.HARVESTABLE, HarvestableMobType.SWAMP),
        new(51, 6, MobType.HARVESTABLE, HarvestableMobType.SWAMP),
        new(52, 3, MobType.HARVESTABLE, HarvestableMobType.STEPPE),
        new(53, 5, MobType.HARVESTABLE, HarvestableMobType.STEPPE),
        new(54, 7, MobType.HARVESTABLE, HarvestableMobType.STEPPE),
        new(55, 6, MobType.HARVESTABLE, HarvestableMobType.STEPPE),
        new(56, 3, MobType.HARVESTABLE, HarvestableMobType.MOUNTAIN),
        new(57, 3, MobType.HARVESTABLE, HarvestableMobType.MOUNTAIN),
        new(58, 5, MobType.HARVESTABLE, HarvestableMobType.MOUNTAIN),
        new(59, 5, MobType.HARVESTABLE, HarvestableMobType.MOUNTAIN),
        new(60, 7, MobType.HARVESTABLE, HarvestableMobType.MOUNTAIN),
        new(61, 6, MobType.HARVESTABLE, HarvestableMobType.MOUNTAIN),
        new(62, 3, MobType.HARVESTABLE, HarvestableMobType.FOREST),
        new(63, 3, MobType.HARVESTABLE, HarvestableMobType.FOREST),
        new(64, 5, MobType.HARVESTABLE, HarvestableMobType.FOREST),
        new(65, 5, MobType.HARVESTABLE, HarvestableMobType.FOREST),
        new(66, 7, MobType.HARVESTABLE, HarvestableMobType.FOREST),
        new(67, 6, MobType.HARVESTABLE, HarvestableMobType.FOREST),
        new(68, 3, MobType.HARVESTABLE, HarvestableMobType.HIGHLAND),
        new(69, 3, MobType.HARVESTABLE, HarvestableMobType.HIGHLAND),
        new(70, 5, MobType.HARVESTABLE, HarvestableMobType.HIGHLAND),
        new(71, 5, MobType.HARVESTABLE, HarvestableMobType.HIGHLAND),
        new(72, 7, MobType.HARVESTABLE, HarvestableMobType.HIGHLAND),
        new(73, 6, MobType.HARVESTABLE, HarvestableMobType.HIGHLAND),
        new(74, 6, MobType.HARVESTABLE, HarvestableMobType.HIGHLAND),
        new(419, 1, MobType.SKINNABLE),
        new(420, 1, MobType.SKINNABLE),
        new(75, 2, MobType.RESOURCE),
        new(76, 3, MobType.RESOURCE),
        new(77, 4, MobType.RESOURCE),
        new(78, 5, MobType.RESOURCE),
        new(79, 6, MobType.RESOURCE),
        new(80, 7, MobType.RESOURCE),
        new(81, 8, MobType.RESOURCE),
        new(82, 4, MobType.RESOURCE),
        new(83, 4, MobType.RESOURCE),
        new(84, 4, MobType.RESOURCE),
        new(85, 4, MobType.RESOURCE),
        new(86, 4, MobType.RESOURCE),
        new(87, 5, MobType.RESOURCE),
        new(88, 5, MobType.RESOURCE),
        new(89, 5, MobType.RESOURCE),
        new(90, 5, MobType.RESOURCE),
        new(91, 5, MobType.RESOURCE),
        new(92, 6, MobType.RESOURCE),
        new(93, 6, MobType.RESOURCE),
        new(94, 6, MobType.RESOURCE),
        new(95, 6, MobType.RESOURCE),
        new(96, 6, MobType.RESOURCE),
        new(97, 7, MobType.RESOURCE),
        new(98, 7, MobType.RESOURCE),
        new(99, 7, MobType.RESOURCE),
        new(100, 7, MobType.RESOURCE),
        new(101, 7, MobType.RESOURCE),
        new(102, 8, MobType.RESOURCE),
        new(103, 8, MobType.RESOURCE),
        new(104, 8, MobType.RESOURCE),
        new(105, 8, MobType.RESOURCE),
        new(106, 8, MobType.RESOURCE),
        new(107, 2, MobType.RESOURCE),
        new(108, 3, MobType.RESOURCE),
        new(109, 4, MobType.RESOURCE),
        new(110, 5, MobType.RESOURCE),
        new(111, 6, MobType.RESOURCE),
        new(112, 7, MobType.RESOURCE),
        new(113, 8, MobType.RESOURCE),
        new(114, 2, MobType.RESOURCE),
        new(115, 3, MobType.RESOURCE),
        new(116, 4, MobType.RESOURCE),
        new(117, 5, MobType.RESOURCE),
        new(118, 6, MobType.RESOURCE),
        new(119, 7, MobType.RESOURCE),
        new(120, 8, MobType.RESOURCE),
        new(121, 2, MobType.RESOURCE),
        new(122, 3, MobType.RESOURCE),
        new(123, 4, MobType.RESOURCE),
        new(124, 5, MobType.RESOURCE),
        new(125, 6, MobType.RESOURCE),
        new(126, 2, MobType.RESOURCE),
        new(127, 3, MobType.RESOURCE),
        new(128, 4, MobType.RESOURCE),
        new(129, 5, MobType.RESOURCE),
        new(130, 6, MobType.RESOURCE),
        new(131, 2, MobType.RESOURCE),
        new(132, 3, MobType.RESOURCE),
        new(133, 4, MobType.RESOURCE),
        new(134, 5, MobType.RESOURCE),
        new(135, 6, MobType.RESOURCE)
    ];

    public int ID { get; }
    public byte Tier { get; }
    public MobType MobType { get; }
    public HarvestableMobType HarvestableMobType;

    private MobInfo(int id, byte tier, MobType mobType)
    {
        ID = id;
        Tier = tier;
        MobType = mobType;
    }

    private MobInfo(int id, byte tier, MobType mobType, HarvestableMobType harvestableMobType)
    {
        ID = id;
        Tier = tier;
        MobType = mobType;
        HarvestableMobType = harvestableMobType;
    }

    public override string ToString()
    {
        return "ID: " + ID + " Tier: " + Tier + " MobType: " + MobType;
    }

    public static MobInfo GetMobInfo(int mobId)
    {
        return MobsInfo.FirstOrDefault(m => m.ID == mobId);
    }
}
