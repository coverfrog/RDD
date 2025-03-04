using System;
using Cf.Cams;
using UnityEngine;

public class UnitMoveToPoint : MonoBehaviour
{
    private void OnEnable()
    {
        InputManager.Instance.OnRightClick += OnRightClick;
        InputManager.Instance.OnRightClickValue += OnRightClickValue;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnRightClick -= OnRightClick;
        InputManager.Instance.OnRightClickValue -= OnRightClickValue;
    }

    private void OnRightClick(bool b)
    {
        
    }
    
    private void OnRightClickValue(Vector2 vector2)
    {
        
    }
}
