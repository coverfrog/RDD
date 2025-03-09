using System;
using UnityEngine;

public class RddGame0Floor : MonoBehaviour
{
    [Header("Corner")]
    [SerializeField] [Min(0.2f)] private float mCornerOffset = 0.2f;
    [SerializeField] [Min(0.1f)] private float mCornerSize = 0.2f;
    [SerializeField] private Color mCornerColor = new Color(1, 1, 1, 1);
    
    private Vector3[] GetCorners()
    {
        Vector3 center = transform.position;

        return new []
        {
            center + new Vector3(-1, 0, -1) * mCornerOffset,
            center + new Vector3(-1, 0, +1) * mCornerOffset,
            center + new Vector3(+1, 0, +1) * mCornerOffset,
            center + new Vector3(+1, 0, -1) * mCornerOffset,
        };
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = mCornerColor;
        
        foreach (var corner in GetCorners())
        {
            Gizmos.DrawCube(corner, Vector3.one * mCornerSize);
        }
    }
}
