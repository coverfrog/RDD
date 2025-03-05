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
    [SerializeField] private IaSlot0 mSlot0;
    [SerializeField] private IaSlot1 mSlot1;
    [SerializeField] private IaSlot2 mSlot2;
    [SerializeField] private IaSlot3 mSlot3;

    [Header("View")] 
    [SerializeField] private IaData mIaData = new IaData();
    [SerializeField] private IaSetting mIaSetting = new IaSetting();

    #region :: Get

    public IaSetting GetIaSetting() => mIaSetting;

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
        
        mLeftClick.Begin();
        mRightClick.Begin();
        mMousePosition.Begin();
        
        mSlot0.UpdateBindKey(mIaSetting);
        mSlot1.UpdateBindKey(mIaSetting);
        mSlot2.UpdateBindKey(mIaSetting);
        mSlot3.UpdateBindKey(mIaSetting);
        
        mSlot0.Begin();
        mSlot1.Begin();
        mSlot2.Begin();
        mSlot3.Begin();
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

    public event Action<bool> OnSlot0;

    private void OnSlot0Act(bool b)
    {
        mIaData.isSlot0Click = b;
        
        OnSlot0?.Invoke(b);
    }
    
    public event Action<bool> OnSlot1;

    private void OnSlot1Act(bool b)
    {
        mIaData.isSlot1Click = b;
        
        OnSlot1?.Invoke(b);
    }
    
    public event Action<bool> OnSlot2;

    private void OnSlot2Act(bool b)
    {
        mIaData.isSlot2Click = b;
        
        OnSlot2?.Invoke(b);
    }
    
    public event Action<bool> OnSlot3;

    private void OnSlot3Act(bool b)
    {
        mIaData.isSlot3Click = b;
        
        OnSlot3?.Invoke(b);
    }

    #endregion
}
