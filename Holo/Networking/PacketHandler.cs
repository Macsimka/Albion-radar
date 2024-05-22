using Holo.AOEnums;
using Holo.Harvestable;
using Holo.Mobs;
using Holo.Player;
using PacketDotNet;
using PhotonPackageParser;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Holo.Networking;

public sealed class PacketHandler : PhotonParser
{
    public void HandlePacket(object _, PacketCapture e)
    {
        UdpPacket packet = Packet.ParsePacket(e.GetPacket().LinkLayerType, e.GetPacket().Data).Extract<UdpPacket>();

        try
        {
            ReceivePacket(packet.PayloadData);
        }
        catch (Exception)
        {
            //MainForm.Log(ex.ToString());
        }
    }

    protected override void OnEvent(byte code, Dictionary<byte, object> parameters)
    {
        if (code == 3)
        {
            parameters.Add(252, (short)EventCodes.Move);
            byte[] bytes = (byte[])parameters[1];

            parameters.Add(4, BitConverter.ToSingle(bytes, 9));
            parameters.Add(5, BitConverter.ToSingle(bytes, 13));
        }

        short codeID = parameters.TryGetValue(252, out var codeObj) ? (short)codeObj : (short)0;

        if (codeID == 0)
            return;

        EventCodes eventCode = (EventCodes)codeID;

        switch (eventCode)
        {
            case EventCodes.Leave:
                HandleLeave(parameters);
                break;
            case EventCodes.Move:
                HandlePlayerMovement(parameters);
                break;
            case EventCodes.NewCharacter:
                HandleNewPlayerEvent(parameters);
                break;
            case EventCodes.NewSimpleHarvestableObjectList:
                HandleNewSimpleHarvestableObjectList(parameters);
                break;
            case EventCodes.NewHarvestableObject:
                HandleNewHarvestableObject(parameters);
                break;
            case EventCodes.HarvestableChangeState:
                HandleHarvestableChangeState(parameters);
                break;
            case EventCodes.HarvestFinished:
                //HandleHarvestFinished(parameters);
                break;
            case EventCodes.MobChangeState:
                HandleMobChangeState(parameters);
                break;
            case EventCodes.NewMob:
                HandleNewMob(parameters);
                break;
            case EventCodes.JoinFinished:
                HandleJoinFinished();
                break;
        }
    }

    protected override void OnRequest(byte operationCode, Dictionary<byte, object> parameters)
    {
        OperationCodes code = parameters.TryGetValue(253, out var codeObj) ? (OperationCodes)Convert.ToInt16(codeObj) : 0;

        switch (code)
        {
            case OperationCodes.Move:
                HandleLocalPlayerMovement(parameters);
                break;
        }
    }

    protected override void OnResponse(byte OperationCode, short ReturnCode, string DebugMessage, Dictionary<byte, object> parameters)
    {
        OperationCodes code = parameters.TryGetValue(253, out var codeObj) ? (OperationCodes)Convert.ToInt16(codeObj) : 0;

        switch (code)
        {
            case OperationCodes.Join:
                HandleJoinOperation(parameters);
                break;
        }
    }

    private static void HandleMobChangeState(Dictionary<byte, object> parameters)
    {
        try
        {
            int mobId = Convert.ToInt32(parameters[0]);
            byte enchantmentLevel = Convert.ToByte(parameters[1]);

            MobsHandler.UpdateMobEnchantmentLevel(mobId, enchantmentLevel);
        }
        catch (Exception e)
        {
            Console.WriteLine($@"HandleMobChangeState: {e}");
        }
    }

    private static void HandleJoinFinished()
    {
        try
        {
            HarvestableHandler.Reset();
            MobsHandler.Reset();
            PlayerHandler.Reset();
        }
        catch (Exception e)
        {
            MainForm.Log($"HandleJoinFinished: {e}");
        }
    }

    private static void HandleNewMob(Dictionary<byte, object> parameters)
    {
        try
        {
            /*
                Rhino Data (4258 HP)
                Key = 0, Value = 12694, Type= System.Int16 // long Object Id ao5
                Key = 1, Value = 41, Type= System.Byte  // short Type Id ao6
                Key = 2, Value = 255, Type= System.Byte // Flagging status ao7
                                Blue = 0,
                                Highland = 1,
                                Forest = 2,
                                Steppe = 3,
                                Mountain = 4,
                                Swamp = 5,
                                Red = byte.MaxValue
                Key = 6, Value = , Type= System.String // apb?
                Key = 7, Value = System.Single[], Type= System.Single[] // Pos // arg apc
                Key = 8, Value = System.Single[], Type= System.Single[] // Pos Target // arg apd
                Key = 9, Value = 26835839, Type= System.Int32 // GameTimeStamp ape
                Key = 10, Value = 171.1836, Type= System.Single // apf (float)
                Key = 11, Value = 2, Type= System.Single // apg (float)
                Key = 13, Value = 4258, Type= System.Single // Health api (float)
                Key = 14, Value = 4258, Type= System.Single // apj (float)
                Key = 16, Value = 26665619, Type= System.Int32 // GameTimeStamp app
                Key = 17, Value = 245, Type= System.Single // float apm
                Key = 18, Value = 245, Type= System.Single // float apn
                Key = 19, Value = 7, Type= System.Single // float apo
                Key = 20, Value = 26835811, Type= System.Int32 // GameTimeStamp app
                Key = 252, Value = 106, Type= System.Int16
            */

            int id = Convert.ToInt32(parameters[0]);
            int typeId = Convert.ToInt32(parameters[1]);

            float[] loc = (float[])parameters[7];
            float posX = loc[0];
            float posY = loc[1];

            // Console.WriteLine("Loc Locs: " + loc.Length);
            //DateTime timeA = new DateTime(long.Parse(parameters[9].ToString()));
            //DateTime timeB = new DateTime(long.Parse(parameters[16].ToString()));
            //DateTime timeC = new DateTime(long.Parse(parameters[20].ToString()));

            int health = parameters.TryGetValue(13, out object healthObj) ? Convert.ToInt32(healthObj) : 0;
            //int rarity = Convert.ToInt32(parameters[19]);

            MobsHandler.AddMob(id, typeId, posX, posY, health);
        }
        catch (Exception e)
        {
            MainForm.Log($"HandleNewMob: {e}");
        }
    }

    private static void HandleNewSimpleHarvestableObjectList(Dictionary<byte, object> parameters)
    {
        List<int> a0 = [];

        if (parameters[0].GetType() == typeof(byte[]))
        {
            byte[] typeListByte = (byte[])parameters[0]; //list of types
            foreach (byte b in typeListByte)
                a0.Add(b);
        }
        else if (parameters[0].GetType() == typeof(short[]))
        {
            short[] typeListByte = (short[])parameters[0]; //list of types
            foreach (short b in typeListByte)
                a0.Add(b);
        }
        else
        {
            MainForm.Log("onNewSimpleHarvestableObjectList type error: " + parameters[0].GetType());
            return;
        }

        try
        {
            /*
            Key = 0, Value = System.Int16[] //id
            Key = 1, Value = System.Byte[] // type WOOD etc
            Key = 2, Value = System.Byte[] // tier
            Key = 3, Value = System.Single[] //location
            Key = 4, Value = System.Byte[] // size
            Key = 252, Value = 29
             */
            byte[] a1 = (byte[])parameters[1]; //list of types
            byte[] a2 = (byte[])parameters[2]; //list of tiers
            float[] a3 = (float[])parameters[3]; //list of positions X1, Y1, X2, Y2 ...
            byte[] a4 = (byte[])parameters[4]; //size

            for (int i = 0; i < a0.Count; ++i)
            {
                int id = a0[i];
                byte type = a1[i];
                byte tier = a2[i];
                float posX = a3[i * 2];
                float posY = a3[i * 2 + 1];
                byte count = a4[i];

                HarvestableHandler.AddHarvestable(id, type, tier, posX, posY, 0, count);
            }

        }
        catch (Exception e)
        {
            MainForm.Log($"HandleNewSimpleHarvestableObjectList: {e}");
        }
    }

    private static void HandleNewHarvestableObject(Dictionary<byte, object> parameters)
    {
        try
        {
            int id = Convert.ToInt32(parameters[0]);
            byte type = Convert.ToByte(parameters[5]);
            byte tier = Convert.ToByte(parameters[7]);
            float[] loc = (float[])parameters[8];

            float posX = loc[0];
            float posY = loc[1];

            byte size = parameters.TryGetValue(10, out var sizeObj) ? Convert.ToByte(sizeObj) : (byte)0;
            byte charges = parameters.TryGetValue(11, out var chargesObj) ? Convert.ToByte(chargesObj) : (byte)0;

            HarvestableHandler.AddHarvestable(id, type, tier, posX, posY, charges, size);
        }
        catch (Exception e)
        {
            MainForm.Log($"HandleNewHarvestableObject: {e}");
        }
    }

    private static void HandleHarvestFinished(Dictionary<byte, object> parameters)
    {
        try
        {
            int harvestableId = Convert.ToInt32(parameters[3]);
            int count = Convert.ToInt32(parameters[5]);

            HarvestableHandler.UpdateHarvestable(harvestableId, (byte)count);
        }
        catch (Exception e)
        {
            MainForm.Log($"HandleHarvestFinished: {e}");
        }
    }

    private static void HandleHarvestableChangeState(Dictionary<byte, object> parameters)
    {
        try
        {
            int id = Convert.ToInt32(parameters[0]);
            byte size = parameters.TryGetValue(1, out object sizeObj) ? Convert.ToByte(sizeObj) : (byte)0;

            HarvestableHandler.UpdateHarvestable(id, size);
        }
        catch (Exception e)
        {
            MainForm.Log($"HandleHarvestableChangeState: {e}");
        }
    }

    private static void HandleLeave(Dictionary<byte, object> parameters)
    {
        try
        {
            int id = Convert.ToInt32(parameters[0]);
            PlayerHandler.RemovePlayer(id);
        }
        catch (Exception e)
        {
            MainForm.Log($"HandleLeave {e}");
        }
    }

    private static void HandleLocalPlayerMovement(Dictionary<byte, object> parameters)
    {
        float[] location = (float[])parameters[1];
        float posX = Convert.ToSingle(location[0]);
        float posY = Convert.ToSingle(location[1]);

        PlayerHandler.UpdateLocalPlayerPosition(posX, posY);
    }

    private static void HandlePlayerMovement(Dictionary<byte, object> parameters)
    {
        try
        {
            int id = Convert.ToInt32(parameters[0]);
            float posX = Convert.ToSingle(parameters[4]);
            float posY = Convert.ToSingle(parameters[5]);

            PlayerHandler.UpdatePlayerPosition(id, posX, posY);
        }
        catch (Exception e)
        {
            MainForm.Log($"HandlePlayerMovement: {e}");
        }
    }

    private static void HandleNewPlayerEvent(Dictionary<byte, object> parameters)
    {
        try
        {
            if (Config.Instance.Players.PlaySound)
                new Thread(() => Console.Beep(1000, 500)).Start();

            int id = Convert.ToInt32(parameters[0]);
            string nick = Convert.ToString(parameters[1]);
            string guild = parameters.TryGetValue(8, out var guildObj) ? Convert.ToString(guildObj) : string.Empty;
            string alliance = Convert.ToString(parameters[49]);

            float[] a13 = (float[])parameters[14];

            PlayerHandler.AddPlayer(a13[0], a13[1], nick, guild, alliance, id);
        }
        catch (Exception e)
        {
            MainForm.Log($"HandleNewPlayerEvent: {e}");
        }
    }

    private static void HandleJoinOperation(Dictionary<byte, object> parameters)
    {
        string mapStr = parameters.TryGetValue(8, out var mapObj) ? Convert.ToString(mapObj) : string.Empty;

        if (string.IsNullOrEmpty(mapStr))
        {
            MainForm.Log($"Can't parse mapID: {mapStr}");
            return;
        }

        PlayerHandler.MapID = mapStr;
    }
}
