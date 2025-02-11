#if UNITY_EDITOR
using System;
using System.IO;
using UnityEngine;
using UnityEditor;

public class MapSceneTemplateWindow : EditorWindow
{
    [MenuItem("Cf/Scene/Template Window")]
    public static void Init()
    {
        _ = GetWindow<MapSceneTemplateWindow>();
    }

    private string _mSelectPath;
    private string _mFileName;

    private void OnEnable()
    {
        _mSelectPath = "Assets/0_Scenes";
        _mFileName = $"Scene_";
    }

    private void OnGUI()
    {
        SelectFolder();
        SelectFileName();
    }

    private void SelectFolder()
    {
        GUIStyle labelStyle = new GUIStyle(GUI.skin.textField);
        
        EditorGUILayout.BeginHorizontal();

        try
        {
            if (GUILayout.Button("Folder", GUILayout.Width(100)))
            {
                string selectPath = EditorUtility.OpenFolderPanel("Folder Select", "Assets", "");

                if (string.IsNullOrEmpty(selectPath))
                {
                    return;
                }

                if (!selectPath.Contains(Application.dataPath))
                {
                    return;
                }

                string assetPath = selectPath[(Application.dataPath.Length - "Assets".Length)..];

                _mSelectPath = assetPath;
            }
            
            _mSelectPath = EditorGUILayout.TextField(_mSelectPath, labelStyle);
        }
        finally
        {
            EditorGUILayout.EndHorizontal();
        }
    }

    private void SelectFileName()
    {
        EditorGUILayout.BeginHorizontal();

        try
        {
            EditorGUILayout.LabelField("", GUILayout.Width(100));

            _mFileName = EditorGUILayout.TextField(_mFileName);

        }
        finally
        {
            EditorGUILayout.EndHorizontal();
        }
    }
}

#endif