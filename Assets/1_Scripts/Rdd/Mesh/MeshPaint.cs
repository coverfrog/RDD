using System;
using Cf.Cams;
using UnityEngine;

public class MeshPaint : MonoBehaviour
{
    private void Start()
    {
        _ = InputManager.Instance;
        InputManager.Instance.OnLeftClick += InstanceOnOnLeftClick;
    }

    private void InstanceOnOnLeftClick(bool obj)
    {
        if (!obj) return;

        if (!CamUtil.ToRay(Input.mousePosition, 1 << 0, out var resultCount, out var hits)) return;

        var hit = CamUtil.GetNearHit(resultCount, hits);
      
        if (!hit.collider) return;
        
        Debug.Log(hit.collider.gameObject.name);
    }
}
