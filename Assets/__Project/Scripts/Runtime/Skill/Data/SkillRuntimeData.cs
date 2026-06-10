using System;
using UnityEngine;

[Serializable]
public struct SkillRuntimeData
{
    public ulong ID;
    public int Level;
    public double CooldownEndTime;
}
