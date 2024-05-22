namespace Holo.Player;

public sealed class Player(float posX, float posY, string nickname, string guild, string alliance, int id)
{
    public int ID { get; } = id;
    public float PosX { get; set; } = posX;
    public float PosY { get; set; } = posY;
    public string Nickname { get; } = nickname;
    public string Guild { get; } = guild;
    public string Alliance { get; } = alliance;

    public Player() : this(0, 0, "", "", "", 0) { }

    public override string ToString()
    {
        return Nickname + "(" + ID + "):" + Guild + " " + Alliance + " [" + PosX + " " + PosY + "]";
    }
}
