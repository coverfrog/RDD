using System;
using Cf.Components;
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
    [SerializeField] private IaSkillSlot0 mSkillSlot0;
    [SerializeField] private IaSkillSlot1 mSkillSlot1;
    [SerializeField] private IaSkillSlot2 mSkillSlot2;
    [SerializeField] private IaSkillSlot3 mSkillSlot3;
    [Space] 
    [SerializeField] private IaSlotGroup mSlotGroup;

    [Header("View")] 
    [SerializeField] private IaData mIaData = new IaData();
    [SerializeField] private IaSetting mIaSetting = new IaSetting();

    #region :: Unity

    protected override void Awake()
    {
        base.Awake();

        ComponentsUtil.TryAddComponent(this, out mLeftClick);
        ComponentsUtil.TryAddComponent(this, out mRightClick);
        ComponentsUtil.TryAddComponent(this, out mMousePosition);
        
        ComponentsUtil.TryAddComponent(this, out mSkillSlot0);
        ComponentsUtil.TryAddComponent(this, out mSkillSlot1);
        ComponentsUtil.TryAddComponent(this, out mSkillSlot2);
        ComponentsUtil.TryAddComponent(this, out mSkillSlot3);
        
        ComponentsUtil.TryAddComponent(this, out mSlotGroup);
    }

    private void Start()
    {
        ActAdd(mLeftClick    , OnLeftClickAct);
        ActAdd(mRightClick   , OnRightClickAct);
        ActAdd(mMousePosition, OnMousePositionAct);
        
        ActAdd(mSkillSlot0        , OnSlot0Act);
        ActAdd(mSkillSlot1        , OnSlot1Act);
        ActAdd(mSkillSlot2        , OnSlot2Act);
        ActAdd(mSkillSlot3        , OnSlot3Act);
        
        mLeftClick.Begin();
        mRightClick.Begin();
        mMousePosition.Begin();
        
        mSkillSlot0.UpdateBindKey(mIaSetting);
        mSkillSlot1.UpdateBindKey(mIaSetting);
        mSkillSlot2.UpdateBindKey(mIaSetting);
        mSkillSlot3.UpdateBindKey(mIaSetting);
        
        mSkillSlot0.Begin();
        mSkillSlot1.Begin();
        mSkillSlot2.Begin();
        mSkillSlot3.Begin();
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

    public event Action<bool> OnSkillSlot0;

    private void OnSlot0Act(bool b)
    {
        mIaData.isSkillSlot0Click = b;
        
        OnSkillSlot0?.Invoke(b);
    }
    
    public event Action<bool> OnSkillSlot1;

    private void OnSlot1Act(bool b)
    {
        mIaData.isSkillSlot1Click = b;
        
        OnSkillSlot1?.Invoke(b);
    }
    
    public event Action<bool> OnSkillSlot2;

    private void OnSlot2Act(bool b)
    {
        mIaData.isSkillSlot2Click = b;
        
        OnSkillSlot2?.Invoke(b);
    }
    
    public event Action<bool> OnSkillSlot3;

    private void OnSlot3Act(bool b)
    {
        mIaData.isSkillSlot3Click = b;
        
        OnSkillSlot3?.Invoke(b);
    }

    #endregion
}
