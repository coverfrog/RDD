using System;
using UnityEngine;

public class RddUser : MonoBehaviour
{
    public event Action<RddUserData> OnDataChange;

    public RddUserData Data
    {
        get => _mData;
        set
        {
            _mData = value;
            
            OnDataChange?.Invoke(_mData);
        }
    }

    private RddUserData _mData = new RddUserData();

    private void Start()
    {
        OnDataChange?.Invoke(_mData);
    }
}
