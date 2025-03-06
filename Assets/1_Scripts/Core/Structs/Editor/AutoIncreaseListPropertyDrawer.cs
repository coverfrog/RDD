#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Cf.Structs
{
    [CustomPropertyDrawer(typeof(AutoIncreaseList<>))]
    public class AutoIncreaseListPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty lisProperty = property.FindPropertyRelative("list");

            if (lisProperty == null) return;

            EditorGUI.PropertyField(position, lisProperty, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty listProperty = property.FindPropertyRelative("list");
            
            if (listProperty == null) return base.GetPropertyHeight(property, label);
            
            return EditorGUI.GetPropertyHeight(listProperty, true);
        }
    }
}

#endif