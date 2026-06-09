using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineGroup<TOwner>
{
    private Dictionary<int, StateMachine<TOwner>> m_smByLayer;

    private TOwner m_owner;

    public StateMachineGroup(TOwner owner)
    {
        m_owner = owner;
        m_smByLayer = new Dictionary<int, StateMachine<TOwner>>();
    }

    public void AddState(int layer, string id, State<TOwner> state)
    {
        if (!m_smByLayer.ContainsKey(layer))
            m_smByLayer.Add(layer, new StateMachine<TOwner>(m_owner));

        m_smByLayer[layer].AddState(id, state);
    }

    public void AddTransition(int layer, string fromID, string toID, Func<bool> condition)
    {
        if (!m_smByLayer.ContainsKey(layer))
            m_smByLayer.Add(layer, new StateMachine<TOwner>(m_owner));

        m_smByLayer[layer].AddTransition(fromID, toID, condition);
    }

    public void Run()
    {
        foreach (StateMachine<TOwner> sm in m_smByLayer.Values)
        {
            sm.Run();
        }
    }

    public void Update()
    {
        foreach (StateMachine<TOwner> sm in m_smByLayer.Values)
        {
            sm.Update();
        }
    }

    public void FixedUpdate()
    {
        foreach (StateMachine<TOwner> sm in m_smByLayer.Values)
        {
            sm.FixedUpdate();
        }
    }

    public void OnCmdInput(InputContext inputContext)
    {
        foreach (StateMachine<TOwner> sm in m_smByLayer.Values)
        {
            sm.OnCmdInput(inputContext);
        }
    }
}
