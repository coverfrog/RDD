using System;
using UnityEngine;

public class State<TOwner>
{
    public TOwner Owner { get; internal set; }

    public void SetOwner(TOwner owner)
    {
        Owner = owner;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }

    public virtual void OnCmdInput(InputContext inputContext) { }
}
