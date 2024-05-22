namespace Holo.Harvestable;

public sealed class Harvestable(int id, byte type, byte tier, float posX, float posY, byte charges, byte size)
{
    public byte Type { get; } = type;
    public byte Tier { get; } = tier;
    public float PosX { get; } = posX;
    public float PosY { get; } = posY;
    public byte Charges { get; } = charges;
    public byte Size { get; set; } = size;

    public override string ToString()
    {
        return "ID: " + id + " type:" + (HarvestableType)Type + " tier: " + Tier + " Size: " + Size + " posX:" + PosX + " posY: " + PosY + " charges: " + Charges;
    }
}
