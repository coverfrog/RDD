using UnityEngine;

#region :: Move

public interface IPlayerMoveDir : IMove
{
    public void OnMove(Vector2 dir, float speed);
}

#endregion