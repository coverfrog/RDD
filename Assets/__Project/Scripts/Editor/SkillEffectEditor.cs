using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillEffect))]
public class SkillEffectEditor : Editor
{
    private SerializedProperty m_isProjectileProp;
    private SerializedProperty m_projectilePrefabProp;
    private SerializedProperty m_projectileSpeedProp;

    private SerializedProperty m_hasSurroundingEffectProp;
    private SerializedProperty m_surroundingEffectPrefabProp;

    private SerializedProperty m_hasHitEffectProp;
    private SerializedProperty m_hitEffectPrefabProp;

    private void OnEnable()
    {
        m_isProjectileProp = serializedObject.FindProperty("m_isProjectile");
        m_projectilePrefabProp = serializedObject.FindProperty("m_projectilePrefab");
        m_projectileSpeedProp = serializedObject.FindProperty("m_projectileSpeed");

        m_hasSurroundingEffectProp = serializedObject.FindProperty("m_hasSurroundingEffect");
        m_surroundingEffectPrefabProp = serializedObject.FindProperty("m_surroundingEffectPrefab");

        m_hasHitEffectProp = serializedObject.FindProperty("m_hasHitEffect");
        m_hitEffectPrefabProp = serializedObject.FindProperty("m_hitEffectPrefab");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Skill Effect Configurations", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // 1. Projectile Settings
        EditorGUILayout.PropertyField(m_isProjectileProp, new GUIContent("Is Projectile"));
        if (m_isProjectileProp.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(m_projectilePrefabProp, new GUIContent("Projectile Prefab"));
            EditorGUILayout.PropertyField(m_projectileSpeedProp, new GUIContent("Projectile Speed"));
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space();

        // 2. Surrounding Settings
        EditorGUILayout.PropertyField(m_hasSurroundingEffectProp, new GUIContent("Has Surrounding Effect"));
        if (m_hasSurroundingEffectProp.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(m_surroundingEffectPrefabProp, new GUIContent("Surrounding Effect Prefab"));
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space();

        // 3. Hit Settings
        EditorGUILayout.PropertyField(m_hasHitEffectProp, new GUIContent("Has Hit Effect"));
        if (m_hasHitEffectProp.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(m_hitEffectPrefabProp, new GUIContent("Hit Effect Prefab"));
            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
