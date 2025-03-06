using System;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private PlayerData mData = new PlayerData();

    #region :: Unity

    private void OnEnable()
    {
        if (InputManager.Instance)
        {
            // InputManager.Instance.OnSkillSlot0 += OnSkillSlot0;
            // InputManager.Instance.OnSkillSlot1 += OnSkillSlot1;
            // InputManager.Instance.OnSkillSlot2 += OnSkillSlot2;
            // InputManager.Instance.OnSkillSlot3 += OnSkillSlot3;
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance)
        {
            // InputManager.Instance.OnSkillSlot0 -= OnSkillSlot0;
            // InputManager.Instance.OnSkillSlot1 -= OnSkillSlot1;
            // InputManager.Instance.OnSkillSlot2 -= OnSkillSlot2;
            // InputManager.Instance.OnSkillSlot3 -= OnSkillSlot3;
        }
    }

    private void Update()
    {
        if (!mData.isMine) return;
    }

    #endregion

    #region :: On Input

    private void OnSkillSlot0(bool b)
    {
        if (!mData.isMine) return;
        if (!b) return;
    }
    
    private void OnSkillSlot1(bool b)
    {
        if (!mData.isMine) return;
        if (!b) return;
    }
    
    private void OnSkillSlot2(bool b)
    {
        if (!mData.isMine) return;
        if (!b) return;
    }
    
    private void OnSkillSlot3(bool b)
    {
        if (!mData.isMine) return;
        if (!b) return;
    }

    #endregion
}
