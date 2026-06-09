using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(SkillData))]
public class SkillDataEditor : Editor
{
    private SerializedProperty m_idProp;
    private SerializedProperty m_levelDataListProp;

    private void OnEnable()
    {
        m_idProp = serializedObject.FindProperty("m_id");
        m_levelDataListProp = serializedObject.FindProperty("m_levelDataList");

        // 활성화될 때 ID 자동 부여 및 Level 검증을 자동으로 수행합니다.
        ValidateAndFixData();
    }

    public override void OnInspectorGUI()
    {
        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject((ScriptableObject)target), typeof(MonoScript), false);
        GUI.enabled = true;

        serializedObject.Update();

        // 에셋의 ID를 표시 (ReadOnly이므로 비활성화된 것처럼 표시하거나 기본 Draw)
        EditorGUILayout.PropertyField(m_idProp);
        
        // Casting Mode
        SerializedProperty castingModeProp = serializedObject.FindProperty("m_castingMode");
        if (castingModeProp != null)
        {
            EditorGUILayout.PropertyField(castingModeProp);
        }

        // LevelDataList 그리기
        if (m_levelDataListProp != null)
        {
            // 요소가 비어 있으면 안 됨 (최소 1개 확보)
            if (m_levelDataListProp.arraySize < 1)
            {
                m_levelDataListProp.arraySize = 1;
            }

            // 각 요소의 Level 값 자동 부여
            for (int i = 0; i < m_levelDataListProp.arraySize; i++)
            {
                SerializedProperty element = m_levelDataListProp.GetArrayElementAtIndex(i);
                SerializedProperty levelProp = element.FindPropertyRelative("Level");
                if (levelProp != null && levelProp.intValue != i + 1)
                {
                    levelProp.intValue = i + 1;
                }
            }

            // Element X 대신 "Level X" 형식으로 커스터마이징 렌더링
            m_levelDataListProp.isExpanded = EditorGUILayout.Foldout(m_levelDataListProp.isExpanded, new GUIContent("Level Data List"), true);
            if (m_levelDataListProp.isExpanded)
            {
                EditorGUI.indentLevel++;
                
                int size = EditorGUILayout.IntField("Size", m_levelDataListProp.arraySize);
                if (size < 1) size = 1;
                m_levelDataListProp.arraySize = size;

                for (int i = 0; i < m_levelDataListProp.arraySize; i++)
                {
                    SerializedProperty element = m_levelDataListProp.GetArrayElementAtIndex(i);
                    // Element i 대신 "Level i + 1" 로 라벨링하여 렌더링
                    EditorGUILayout.PropertyField(element, new GUIContent($"Level {i + 1}"), true);
                }
                
                EditorGUI.indentLevel--;
            }
        }

        serializedObject.ApplyModifiedProperties();

        // 수동 검증 및 ID 할당 버튼 제공
        if (GUILayout.Button("Verify and Assign ID"))
        {
            ValidateAndFixData(true);
        }
    }

    private void ValidateAndFixData(bool forceSave = false)
    {
        serializedObject.Update();

        bool changed = false;

        // 1. LevelDataList 요소 최소 1개 유지 및 Level 자동 부여
        if (m_levelDataListProp != null)
        {
            if (m_levelDataListProp.arraySize < 1)
            {
                m_levelDataListProp.arraySize = 1;
                changed = true;
            }

            for (int i = 0; i < m_levelDataListProp.arraySize; i++)
            {
                SerializedProperty element = m_levelDataListProp.GetArrayElementAtIndex(i);
                SerializedProperty levelProp = element.FindPropertyRelative("Level");
                if (levelProp != null && levelProp.intValue != i + 1)
                {
                    levelProp.intValue = i + 1;
                    changed = true;
                }
            }
        }

        // 2. ID 자동 부여: 이름 순 정렬 기준으로 전체 재계산 후 자신의 순번을 ID로 할당
        if (m_idProp != null)
        {
            ulong assignedId = GetNameSortedId(target as SkillData);
            if ((ulong)m_idProp.longValue != assignedId)
            {
                m_idProp.longValue = (long)assignedId;
                changed = true;
                Debug.Log($"[SkillDataEditor] '{target.name}' → ID {assignedId} 할당 (이름 순 정렬 기준)");
            }
        }

        if (changed || forceSave)
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
        }
    }

    /// <summary>
    /// 프로젝트 내 전체 SkillData를 에셋 이름 기준 오름차순으로 정렬한 뒤,
    /// 현재 에셋(self)의 순번(1-based)을 반환합니다.
    /// </summary>
    private static ulong GetNameSortedId(SkillData self)
    {
        List<SkillData> all = LoadAllSkillDataSortedByName();
        int index = all.IndexOf(self);
        return index >= 0 ? (ulong)(index + 1) : 1;
    }

    /// <summary>
    /// 프로젝트 내 전체 SkillData 에셋을 이름 오름차순으로 정렬하여 반환합니다.
    /// </summary>
    private static List<SkillData> LoadAllSkillDataSortedByName()
    {
        string[] guids = AssetDatabase.FindAssets("t:SkillData");
        List<SkillData> list = new List<SkillData>(guids.Length);

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            SkillData skill = AssetDatabase.LoadAssetAtPath<SkillData>(path);
            if (skill != null)
            {
                list.Add(skill);
            }
        }

        // 에셋 이름 기준 오름차순 정렬 (대소문자 무시)
        list.Sort((a, b) => string.Compare(a.name, b.name, System.StringComparison.OrdinalIgnoreCase));
        return list;
    }

    // 일괄 관리를 위한 메뉴 기능
    [MenuItem("RDD/Manage/Verify and Assign Skill Data")]
    public static void VerifyAndAssignAllSkills()
    {
        // 이름 순 정렬된 전체 목록
        List<SkillData> sorted = LoadAllSkillDataSortedByName();
        int count = 0;

        // 1단계: 이름 순 순번(1-based)으로 ID 일괄 재할당
        for (int i = 0; i < sorted.Count; i++)
        {
            SkillData skill = sorted[i];
            ulong expectedId = (ulong)(i + 1);

            SerializedObject so = new SerializedObject(skill);
            SerializedProperty idProp = so.FindProperty("m_id");
            if (idProp != null)
            {
                so.Update();
                if ((ulong)idProp.longValue != expectedId)
                {
                    idProp.longValue = (long)expectedId;
                    so.ApplyModifiedProperties();
                    EditorUtility.SetDirty(skill);
                    Debug.Log($"[SkillDataEditor] 일괄 처리: '{skill.name}' → ID {expectedId} 할당 (이름 순 {i + 1}번째)");
                    count++;
                }
            }
        }

        // 2단계: 전체 스킬에 대해 levelDataList 개수(최소 1개) 및 레벨 순서 보정
        foreach (SkillData skill in sorted)
        {
            SerializedObject so = new SerializedObject(skill);
            SerializedProperty listProp = so.FindProperty("m_levelDataList");
            bool changed = false;

            if (listProp != null)
            {
                so.Update();
                if (listProp.arraySize < 1)
                {
                    listProp.arraySize = 1;
                    changed = true;
                }

                for (int i = 0; i < listProp.arraySize; i++)
                {
                    SerializedProperty element = listProp.GetArrayElementAtIndex(i);
                    SerializedProperty levelProp = element.FindPropertyRelative("Level");
                    if (levelProp != null && levelProp.intValue != i + 1)
                    {
                        levelProp.intValue = i + 1;
                        changed = true;
                    }
                }

                if (changed)
                {
                    so.ApplyModifiedProperties();
                    EditorUtility.SetDirty(skill);
                    Debug.Log($"[SkillDataEditor] 일괄 처리: '{skill.name}' 스킬의 Level 데이터를 보정했습니다.");
                }
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"[SkillDataEditor] 일괄 검사 완료. 총 {count}개의 스킬 ID를 이름 순 기준으로 재할당했습니다.");
    }
}
