using System;
using System.Collections.Generic;
using System.Linq;
using Cf.Utils;
using Cf.Inputs;
using Cf.Pattern;
using UnityEngine;
using UnityEngine.Serialization;

public class InputManager : Singleton<InputManager>
{
    [Header("Reference")]
    [SerializeField] private IaLeftClick mLeftClick;
    [SerializeField] private IaRightClick mRightClick;
    [SerializeField] private IaMousePosition mMousePosition;
    [Space]
    [SerializeField] private IaSlotGroup mSlotSkillGroup;

    [Header("View")] 
    [SerializeField] private IaData mIaData = new IaData();
    [SerializeField] private IaSetting mIaSetting = new IaSetting();

    #region :: Unity

    protected override void Awake()
    {
        base.Awake();
        
        CfUtil.Components.TryAddNewObject(this, out mLeftClick);
        CfUtil.Components.TryAddNewObject(this, out mRightClick);
        CfUtil.Components.TryAddNewObject(this, out mMousePosition);
        CfUtil.Components.TryAddNewObject(this, out mSlotSkillGroup);
        
        mSlotSkillGroup.Init("Skill", mIaSetting.SlotSkillKeyBoardList);
    }

    private void Start()
    {
        ActAdd(mLeftClick, OnLeftClickAct);
        ActAdd(mRightClick, OnRightClickAct);
        ActAdd(mMousePosition, OnMousePositionAct);

        mLeftClick.Begin();
        mRightClick.Begin();
        mMousePosition.Begin();

        mSlotSkillGroup.OnInput += OnSkillSlotAct;
        mSlotSkillGroup.Begin();
    }

    #endregion

    #region :: Act

    private static void ActAdd<T>(IaBase<T> ia, Action<T> act) where T : struct
    {
        ia.OnInput += act;
    }
    
    //
    
    public event Action<bool> OnLeftClick;
    
    private void OnLeftClickAct(bool b)
    {
        mIaData.isLeftClick = b;

        OnLeftClick?.Invoke(b);
    }

    public event Action<bool> OnRightClick;
    
    private void OnRightClickAct(bool b)
    {
        mIaData.isRightClick = b;

        OnRightClick?.Invoke(b);
    }
    
    public event Action<Vector2> OnMousePosition;

    private void OnMousePositionAct(Vector2 vector2)
    {
        mIaData.mousePoint = vector2;

        OnMousePosition?.Invoke(vector2);
    }

    public event Action<int, bool> OnSkillSlot;
    
    private void OnSkillSlotAct(int i, bool b)
    {
        mIaData.SetIsSkillSlotClickList(i, b);

        OnSkillSlot?.Invoke(i, b);
    }

    #endregion
}
