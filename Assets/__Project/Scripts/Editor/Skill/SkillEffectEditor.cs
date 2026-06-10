using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillEffect))]
public class SkillEffectEditor : Editor
{
    private SerializedProperty m_isProjectileProp;
    private SerializedProperty m_projectilePrefabProp;
    private SerializedProperty m_projectileSpeedProp;

    private SerializedProperty m_isDashProp;
    private SerializedProperty m_DashSpeedProp;

    private SerializedProperty m_isSurroundingEffectProp;
    private SerializedProperty m_surroundingEffectPrefabProp;

    private SerializedProperty m_isHitEffectProp;
    private SerializedProperty m_hitEffectPrefabProp;

    private void OnEnable()
    {
        m_isProjectileProp = serializedObject.FindProperty("m_isProjectile");
        m_projectilePrefabProp = serializedObject.FindProperty("m_projectilePrefab");
        m_projectileSpeedProp = serializedObject.FindProperty("m_projectileSpeed");

        m_isDashProp = serializedObject.FindProperty("m_isDash");
        m_DashSpeedProp = serializedObject.FindProperty("m_dashSpeed");

        m_isSurroundingEffectProp = serializedObject.FindProperty("m_isSurroundingEffect");
        m_surroundingEffectPrefabProp = serializedObject.FindProperty("m_surroundingEffectPrefab");

        m_isHitEffectProp = serializedObject.FindProperty("m_isHitEffect");
        m_hitEffectPrefabProp = serializedObject.FindProperty("m_hitEffectPrefab");
    }

    public override void OnInspectorGUI()
    {
        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject((ScriptableObject)target), typeof(MonoScript), false);
        GUI.enabled = true;

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

        // 2. Projectile Settings
        EditorGUILayout.PropertyField(m_isDashProp, new GUIContent("Is Dash"));
        if (m_isDashProp.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(m_DashSpeedProp, new GUIContent("Dash Speed"));
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space();

        // 3. Surrounding Settings
        EditorGUILayout.PropertyField(m_isSurroundingEffectProp, new GUIContent("Is Surrounding Effect"));
        if (m_isSurroundingEffectProp.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(m_surroundingEffectPrefabProp, new GUIContent("Surrounding Effect Prefab"));
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space();

        // 4. Hit Settings
        EditorGUILayout.PropertyField(m_isHitEffectProp, new GUIContent("Is Hit Effect"));
        if (m_isHitEffectProp.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(m_hitEffectPrefabProp, new GUIContent("Hit Effect Prefab"));
            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
