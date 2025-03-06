#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Cf.Editors
{
    public static partial class CfEditorUtil
    {
        public static class Gui
        {
            public static void ShowScript(ScriptableObject target)
            {
                GUI.enabled = false;
                EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject(target), typeof(MonoScript), false);
                GUI.enabled = true;
            }
        }
    }
}

#endif