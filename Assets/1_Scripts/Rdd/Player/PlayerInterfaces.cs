using UnityEngine;

#region :: Move

public interface IPlayerMove
{
    public void OnMove(Vector2 dir, float speed);
}

public interface IPlayerMoveToDir : IPlayerMove
{
    
}

#endregion