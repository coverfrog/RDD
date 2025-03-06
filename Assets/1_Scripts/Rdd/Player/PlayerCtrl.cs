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
            InputManager.Instance.OnSkillSlot += OnSkillSlot;
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance)
        {
            InputManager.Instance.OnSkillSlot -= OnSkillSlot;
        }
    }

    private void Update()
    {
        if (!mData.isMine) return;
    }

    #endregion

    #region :: On Input

    private void OnSkillSlot(int idx, bool b)
    {
        if (!mData.isMine) return;
    }

    #endregion
}
