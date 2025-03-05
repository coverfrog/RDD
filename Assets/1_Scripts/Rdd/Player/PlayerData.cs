using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public bool isMine = true;
    public string nickName = "Debug";
    [Space] 
    public ulong skillSlot0Index = 1;
    public ulong skillSlot1Index = 2;
    public ulong skillSlot2Index = 3;
    public ulong skillSlot3Index = 4;
}
