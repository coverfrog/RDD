using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cf/Game0/Round Data Group")]
public class RddGame0RoundDataGroup : ScriptableObject
{
    [SerializeField] private List<RddGame0RoundData> mList = new List<RddGame0RoundData>();

    public IReadOnlyList<RddGame0RoundData> List => mList;
}
