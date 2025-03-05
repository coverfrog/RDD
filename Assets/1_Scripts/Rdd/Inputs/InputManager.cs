using System;
using Cf.Components;
using Cf.Inputs;
using Cf.Pattern;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [Header("Reference")]
    [SerializeField] private IaLeftClick mLeftClick;
    [SerializeField] private IaRightClick mRightClick;
    [SerializeField] private IaMousePosition mMousePosition;
    [Space]
    [SerializeField] private IaSlot0 mSlot0;
    [SerializeField] private IaSlot1 mSlot1;
    [SerializeField] private IaSlot2 mSlot2;
    [SerializeField] private IaSlot3 mSlot3;

    [Header("View")] 
    [SerializeField] private InputData mData;
    [SerializeField] private InputSetting mSetting;

    #region :: Get

    public InputSetting GetSetting() => mSetting;

    #endregion
    
    #region :: Unity

    protected override void Awake()
    {
        base.Awake();

        ComponentsUtil.TryAddComponent(this, out mLeftClick);
        ComponentsUtil.TryAddComponent(this, out mRightClick);
        ComponentsUtil.TryAddComponent(this, out mMousePosition);
        
        ComponentsUtil.TryAddComponent(this, out mSlot0);
        ComponentsUtil.TryAddComponent(this, out mSlot1);
        ComponentsUtil.TryAddComponent(this, out mSlot2);
        ComponentsUtil.TryAddComponent(this, out mSlot3);
    }

    private void Start()
    {
        ActAdd(mLeftClick    , OnLeftClickAct);
        ActAdd(mRightClick   , OnRightClickAct);
        ActAdd(mMousePosition, OnMousePositionAct);
        
        ActAdd(mSlot0        , OnSlot0Act);
        ActAdd(mSlot1        , OnSlot1Act);
        ActAdd(mSlot2        , OnSlot2Act);
        ActAdd(mSlot3        , OnSlot3Act);
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
        mData.isLeftClick = b;

        OnLeftClick?.Invoke(b);
    }

    public event Action<bool> OnRightClick;
    
    private void OnRightClickAct(bool b)
    {
        mData.isRightClick = b;

        OnRightClick?.Invoke(b);
    }
    
    public event Action<Vector2> OnMousePosition;

    private void OnMousePositionAct(Vector2 vector2)
    {
        mData.mousePoint = vector2;

        OnMousePosition?.Invoke(vector2);
    }

    public event Action<bool> OnSlot0;

    private void OnSlot0Act(bool b)
    {
        mData.isSlot0Click = b;
        
        OnSlot0?.Invoke(b);
    }
    
    public event Action<bool> OnSlot1;

    private void OnSlot1Act(bool b)
    {
        mData.isSlot1Click = b;
        
        OnSlot1?.Invoke(b);
    }
    
    public event Action<bool> OnSlot2;

    private void OnSlot2Act(bool b)
    {
        mData.isSlot2Click = b;
        
        OnSlot2?.Invoke(b);
    }
    
    public event Action<bool> OnSlot3;

    private void OnSlot3Act(bool b)
    {
        mData.isSlot3Click = b;
        
        OnSlot3?.Invoke(b);
    }

    #endregion
}
