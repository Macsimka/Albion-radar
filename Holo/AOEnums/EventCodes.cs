namespace Holo.AOEnums;

public enum EventCodes : ushort
{
    Leave = 1,
    JoinFinished = 2,
    Move = 3,
    ActiveSpellEffectsUpdate = 10,
    InventoryPutItem = 25,
    InventoryDeleteItem = 26,
    NewCharacter = 27,
    NewSimpleHarvestableObjectList = 37,
    NewHarvestableObject = 38,
    HarvestableChangeState = 44,
    MobChangeState = 45,
    HarvestFinished = 59,
    UpdateMoney = 78,
    NewMob = 119,
    ClusterInfoUpdate = 137,
}
