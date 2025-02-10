#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(RoundHandler))]
public class RoundSoHandlerCustomEditor : SoHandlerCustomEditor<RoundSoData>
{
    protected override bool IsHeaderSpace => true;
    protected override bool IsHeaderView => true;
    protected override bool IsSoClearButton => true;
    protected override bool IsSoFindButton => true;
    
    protected override string HeaderName => "Act";
    protected override string SoPropertyName => "mRoundSoDataList";
}

#endif