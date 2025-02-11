#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoundHandler))]
public sealed class RoundSoHandlerCustomEditor : SoHandlerCustomEditor<RoundSoData>
{
    protected override bool IsSoHeaderSpace => true;
    protected override bool IsSoHeaderView => true;
    protected override bool IsSoClearButton => true;
    protected override bool IsSoFindButton => true;
    
    protected override string SoHeaderName => "So Act";
    protected override string SoPropertyName => "mRoundSoDataList";

    
    private const bool IsRoundHeaderSpace = true;
    private const bool IsRoundHeaderView = true;
    private const bool IsRoundDataToExcel = true;
    
    private const string RoundHeaderName = "Round Act";
    
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (IsRoundHeaderSpace) HeaderSpace();
        if (IsRoundHeaderView) HeaderView();
        if (IsRoundDataToExcel) RoundDataToExcel();
    }

    
    #region Header

    private void HeaderSpace()
    {
        GUILayout.Space(10);
    }
    
    private void HeaderView()
    {
        GUILayout.Label(RoundHeaderName);
    }

    #endregion

    
    #region :: Custom Act

    private void RoundDataToExcel()
    {
        EditorGUILayout.BeginHorizontal();

        try
        {
            
            if(!GUILayout.Button("...")) return;
        }
        finally
        {
            EditorGUILayout.EndHorizontal();
        }
    }

    #endregion
}

#endif