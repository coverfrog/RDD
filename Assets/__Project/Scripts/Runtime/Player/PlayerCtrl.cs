using Mirror;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputNetworkSender))]
[RequireComponent(typeof(StatCtrl))]
public class PlayerCtrl : NetworkBehaviour
{
    public InputContext CurrentInputContext { get; set; }

    #region : Rigidbody

    public Rigidbody Rb3d
    {
        get
        {
            if (m_rb3d == null) m_rb3d = GetComponent<Rigidbody>();
            return m_rb3d;
        }
    }

    internal Rigidbody m_rb3d;

    #endregion

    #region : StatCtrl


    public StatCtrl StatCtrl
    {
        get
        {
            if (m_statCtrl == null) m_statCtrl = GetComponent<StatCtrl>();
            return m_statCtrl;
        }
    }

    internal StatCtrl m_statCtrl;

    #endregion

    #region : StateMachineGroup

    public StateMachineGroup<PlayerCtrl> SmGroup
    {
        get
        {
            if (m_smGroup == null)
            {
                m_smGroup = new StateMachineGroup<PlayerCtrl>(this);

                // Layer0: Idle <-> Move
                m_smGroup.AddState(0, "Idle", new PlayerIdleState());
                m_smGroup.AddState(0, "Move", new PlayerMoveState());

                m_smGroup.AddTransition(0, "Idle", "Move", () =>
                    CurrentInputContext.IsClickRight);

                m_smGroup.AddTransition(0, "Move", "Idle", () =>
                {
                    Vector3 currentXZ = new Vector3(transform.position.x, 0, transform.position.z);
                    Vector3 targetXZ = new Vector3(CurrentInputContext.MoveGroundPoint.x, 0, CurrentInputContext.MoveGroundPoint.z);
                    return Vector3.Distance(currentXZ, targetXZ) < 0.2f;
                });

                m_smGroup.Run();
            }

            return m_smGroup;
        }
    }

    internal StateMachineGroup<PlayerCtrl> m_smGroup;

    #endregion

    private void Update()
    {
        SmGroup.Update();
    }

    private void FixedUpdate()
    {
        SmGroup.FixedUpdate();
    }

    private void LateUpdate()
    {
        InputContext context = CurrentInputContext;
        context.ClearOneShotInputs();

        CurrentInputContext = context;
    }
}
