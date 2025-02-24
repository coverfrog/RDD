using UnityEngine;

public class UnitAttackToBodySlam : MonoBehaviour, IAttack
{
    public void OnAttack(PlayerInputAttackData data, UnitAttackOption option)
    {
        if (!data.isAttackMouseInput)
        {
            return;
        }
    }
}
