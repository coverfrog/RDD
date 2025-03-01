#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Cf.Scenes
{
    [CustomEditor(typeof(SceneHandler), true)]
    public class SceneHandlerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}

#endif