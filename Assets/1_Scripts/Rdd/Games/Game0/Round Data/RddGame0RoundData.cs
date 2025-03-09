using UnityEngine;

[CreateAssetMenu(menuName = "Cf/Game0/Round Data")]
public class RddGame0RoundData : ScriptableObject
{
    [SerializeField] private float mDuration = 15.0f;

    public float Duration => mDuration;
}
