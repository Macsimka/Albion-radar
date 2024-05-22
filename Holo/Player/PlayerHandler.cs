using System.Collections.Concurrent;

namespace Holo.Player;

public static class PlayerHandler
{
    public static readonly ConcurrentDictionary<int, Player> PlayersInRange = [];
    private static readonly Player LocalPlayer = new();
    private static string _mapID;

    public static string MapID
    {
        get => _mapID;
        set
        {
            _mapID = value;
            MainForm.SetMapID(_mapID);
        }
    }

    public static void AddPlayer(float posX, float posY, string nickname, string guild, string alliance, int id)
    {
        Player player = new(posX, posY, nickname, guild, alliance, id);
        PlayersInRange.AddOrUpdate(id, player, (_, _) => player);
    }

    public static void RemovePlayer(int id)
    {
        PlayersInRange.TryRemove(id, out _);
    }

    public static void UpdateLocalPlayerPosition(float posX, float posY)
    {
        LocalPlayer.PosX = posX;
        LocalPlayer.PosY = posY;

        MainForm.UpdatePlayerPos(posX, posY);
    }

    public static void UpdatePlayerPosition(int id, float posX, float posY)
    {
        if (PlayersInRange.TryGetValue(id, out Player player))
        {
            player.PosX = posX;
            player.PosY = posY;
        }
    }

    public static float GetLocalPlayerPosX() { return LocalPlayer.PosX; }
    public static float GetLocalPlayerPosY() { return LocalPlayer.PosY; }

    public static void Reset()
    {
        PlayersInRange.Clear();
    }
}
