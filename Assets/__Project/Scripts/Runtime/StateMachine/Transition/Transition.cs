using System;
using UnityEngine;

[Serializable]
public class Transition
{
    public string FromID { get; private set; }

    public string ToID { get; private set; }

    public Func<bool> Condition { get; private set; }

    public Transition(string fromID, string toID, Func<bool> condition)
    {
        FromID = fromID;
        ToID = toID;
        Condition = condition;
    }
}
