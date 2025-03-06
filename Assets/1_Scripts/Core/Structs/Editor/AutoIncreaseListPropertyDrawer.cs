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
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();

            var listField = new PropertyField(property.FindPropertyRelative("list"));

            container.Add(listField);
            
            return container;
        }
    }
}

#endif