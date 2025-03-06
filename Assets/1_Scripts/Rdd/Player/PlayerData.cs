using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public bool isMine = true;
    public string nickName = "Debug";

    [SerializeField] private List<PlayerSkillSlot> skillSlotList;
}

[Serializable]
public class PlayerSkillSlot
{
    
}
