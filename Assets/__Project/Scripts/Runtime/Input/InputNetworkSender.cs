using Mirror;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
public class InputNetworkSender : NetworkBehaviour, IInputObserver
{
    #region : Ctrl

    internal PlayerCtrl Ctrl
    {
        get
        {
            if (m_ctrl == null) m_ctrl = GetComponent<PlayerCtrl>();
            return m_ctrl;
        }
    }

    internal PlayerCtrl m_ctrl;

    #endregion

    private bool m_isAuth;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        if (InputManager.Instance)
            InputManager.Instance.AddObserver(this);

        m_isAuth = true;
    }

    public override void OnStopLocalPlayer()
    {
        base.OnStopLocalPlayer();

        if (InputManager.Instance)
            InputManager.Instance.RemoveObserver(this);
    }

    public void OnInput(InputContext inputContext)
    {
        if (m_isAuth == false)
            return;

        Ctrl.CurrentInputContext = inputContext; // 로컬 (즉각 반영 위함)
        CmdOnInput(inputContext);                // 서버 
    }

    [Command]
    private void CmdOnInput(InputContext inputContext)
    {
        Ctrl.CurrentInputContext = inputContext;
    }
}
