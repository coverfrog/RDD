#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class SoHandlerCustomEditor<T> : Editor where T : Object
{
    protected abstract bool IsSoHeaderSpace { get; }
    protected abstract bool IsSoHeaderView { get; }
    protected abstract bool IsSoClearButton { get; }
    protected abstract bool IsSoFindButton { get; }
    
    protected abstract string SoHeaderName { get; }
    protected abstract string SoPropertyName { get; }
    
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (IsSoHeaderSpace) HeaderSpace();
        if (IsSoHeaderView) HeaderView();
        if (IsSoClearButton) SoClearButton();
        if (IsSoFindButton) SoFindButton();
    }

    #region :: Header

    private void HeaderSpace()
    {
        GUILayout.Space(10);
    }

    private void HeaderView()
    {
        GUILayout.Label(SoHeaderName);
    }

    #endregion

    #region :: So

    private bool SoTryGetProperty(out SerializedObject o, out SerializedProperty property)
    {
        o = new SerializedObject(target);
        property = o.FindProperty(SoPropertyName);

        return property != null;
    }

    private void SoApplyProperty(SerializedObject o)
    {
        o.ApplyModifiedProperties();
    }

    private void SoClearButton()
    {
        // 버튼
        if (!GUILayout.Button("So Clear")) return;
        
        // 찾기
        if (!SoTryGetProperty(out SerializedObject o, out SerializedProperty property)) return;

        // 값 적용
        property.ClearArray();
        
        // 적용
        SoApplyProperty(o);
    }

    private void SoFindButton()
    {
        // 버튼
        if (!GUILayout.Button("So Find")) return;
        
        // 찾기
        string typeName = typeof(T).Name;
        string[] guids = AssetDatabase.FindAssets($"t:{typeName}");
        
        List<T> assetList = new List<T>(guids.Length);
        
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            
            if (!asset) continue;
            
            assetList.Add(asset);
        }
        
        // 개수 점검
        if (assetList.Count == 0) return;

        // 찾기
        if (!SoTryGetProperty(out SerializedObject o, out SerializedProperty property)) return;

        // 값 적용
        property.ClearArray();

        for (int i = 0; i < assetList.Count; i++)
        {
            property.InsertArrayElementAtIndex(i);
            property.GetArrayElementAtIndex(i).objectReferenceValue = assetList[i];
        }

        SoApplyProperty(o);
    }

    #endregion
}

#endif