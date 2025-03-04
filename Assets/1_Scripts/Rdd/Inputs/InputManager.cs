using System;
using Cf.Inputs;
using Cf.Pattern;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [Header("Reference")]
    [SerializeField] private IaLeftClick mLeftClick;
    [SerializeField] private IaRightClick mRightClick;
    [SerializeField] private IaMousePosition mMousePosition;

    [Header("View")] 
    [SerializeField] private InputData mData;
    
    #region :: Unity

    protected override void Awake()
    {
        base.Awake();

        if (!TryGetComponent(out mLeftClick))
        {
            mLeftClick = gameObject.AddComponent<IaLeftClick>();
        }
        
        if (!TryGetComponent(out mRightClick))
        {
            mRightClick = gameObject.AddComponent<IaRightClick>();
        }

        if (!TryGetComponent(out mMousePosition))
        {
            mMousePosition = gameObject.AddComponent<IaMousePosition>();
        }
    }

    private void Start()
    {
        mLeftClick.OnInput += OnLeftClickAct;
        mRightClick.OnInput += OnRightClickAct;
        mMousePosition.OnInput += OnMousePositionAct;
    }

    #endregion

    #region :: Act

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
    #endregion
}
