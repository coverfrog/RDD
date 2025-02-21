using UnityEngine;

public class PlayerAttackToBodySlam : MonoBehaviour, IAttack
{
    private bool _mIsAttacking;
    
    public void OnAttack(PlayerInputAttackData data)
    {
        if (!data.isAttackMouseInput)
        {
            return;
        }
        
        Debug.Log("ATTACK");
    }
}
