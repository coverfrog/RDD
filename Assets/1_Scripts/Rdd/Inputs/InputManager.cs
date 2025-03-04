using System;
using Cf.Inputs;
using Cf.Pattern;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [Header("Reference")]
    [SerializeField] private IaRightClick mRightClick;

    [Header("View")] 
    [SerializeField] private InputData mData;
    
    #region :: Unity

    protected override void Awake()
    {
        base.Awake();

        if (!TryGetComponent(out mRightClick)) mRightClick = gameObject.AddComponent<IaRightClick>();
    }

    private void Start()
    {
        mRightClick.OnClick += OnRightClickInput;
        mRightClick.OnValue += OnRightClickValueInput;
    }

    #endregion

    #region :: Act

    public event Action<bool> OnRightClick;
    public event Action<Vector2> OnRightClickValue;
    
    private void OnRightClickInput(bool b)
    {
        mData.isRightClick = b;

        OnRightClick?.Invoke(b);
        
    }

    private void OnRightClickValueInput(Vector2 vector2)
    {
        mData.rightClickVector2 = vector2;

        OnRightClickValue?.Invoke(vector2);
    }
    #endregion
}
