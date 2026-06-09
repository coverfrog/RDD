using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine<TOwner>
{
    private Dictionary<string, State<TOwner>> m_stateByID;
    private List<Transition> m_transitions;

    private TOwner m_owner;

    private string m_currentStateID;


    public StateMachine(TOwner owner)
    {
        m_stateByID = new Dictionary<string, State<TOwner>>();
        m_transitions = new List<Transition>();

        m_owner = owner;
    }

    public void AddState(string id, State<TOwner> state)
    {
        _ = m_stateByID.TryAdd(id, state);
    }

    public void AddTransition(string fromID, string toID, Func<bool> condition)
    {
        m_transitions.Add(new Transition(fromID, toID, condition));
    }

    public void Run()
    {
        foreach (State<TOwner> state in m_stateByID.Values)
        {
            state.SetOwner(m_owner);
        }

        if (m_stateByID.Count > 0)
        {
            m_currentStateID = m_stateByID.Keys.ToList().First();
            m_stateByID[m_currentStateID].Enter();
        }
    }

    public void OnCmdInput(InputContext inputContext)
    {
        if (m_stateByID.TryGetValue(m_currentStateID, out var state))
        {
            state.OnCmdInput(inputContext);
        }
    }

    public void Update()
    {
        foreach (Transition transition in m_transitions)
        {
            bool isTransition = false;

            if (transition.Condition())
            {
                if (string.IsNullOrEmpty(transition.FromID))
                {
                    isTransition = true;
                }
                else
                {
                    if (m_currentStateID == transition.FromID)
                    {
                        isTransition = true;
                    }
                }
            }

            if (!isTransition) continue;

            m_stateByID[m_currentStateID].Exit();

            m_currentStateID = transition.ToID;
            m_stateByID[m_currentStateID].Enter();
        }

        if (m_stateByID.TryGetValue(m_currentStateID, out var state))
        {
            state.Update();
        }
    }

    public void FixedUpdate()
    {
        if (m_stateByID.TryGetValue(m_currentStateID, out var state))
        {
            state.FixedUpdate();
        }
    }
}
