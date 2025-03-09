using System;
using UnityEngine;

public class SceneGame : SceneCtrl
{
    public override SceneName GetSceneName => SceneName.Game0;
    
    // ::
    
    [SerializeField] private UIGame0RoundTimer mRoundTimer;
}
