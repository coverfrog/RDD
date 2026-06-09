using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

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

        // 2. ID 자동 부여 (0이거나 중복인 경우)
        if (m_idProp != null)
        {
            ulong currentId = (ulong)m_idProp.longValue;
            if (currentId == 0 || IsDuplicateID(currentId, target as SkillData))
            {
                ulong newId = GenerateUniqueID();
                m_idProp.longValue = (long)newId;
                changed = true;
                Debug.Log($"[SkillDataEditor] Assigned new unique ID {newId} to SkillData '{target.name}'");
            }
        }

        if (changed || forceSave)
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
        }
    }

    private bool IsDuplicateID(ulong id, SkillData currentAsset)
    {
        string[] guids = AssetDatabase.FindAssets("t:SkillData");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            SkillData skill = AssetDatabase.LoadAssetAtPath<SkillData>(path);
            if (skill != null && skill != currentAsset && skill.ID == id)
            {
                return true;
            }
        }
        return false;
    }

    private ulong GenerateUniqueID()
    {
        string[] guids = AssetDatabase.FindAssets("t:SkillData");
        ulong maxId = 0;
        HashSet<ulong> existingIds = new HashSet<ulong>();

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            SkillData skill = AssetDatabase.LoadAssetAtPath<SkillData>(path);
            if (skill != null && skill.ID != 0)
            {
                existingIds.Add(skill.ID);
                if (skill.ID > maxId)
                {
                    maxId = skill.ID;
                }
            }
        }

        ulong newId = maxId + 1;
        while (existingIds.Contains(newId) || newId == 0)
        {
            newId++;
        }
        return newId;
    }

    // 일괄 관리를 위한 메뉴 기능
    [MenuItem("RDD/Manage/Verify and Assign Skill Data")]
    public static void VerifyAndAssignAllSkills()
    {
        string[] guids = AssetDatabase.FindAssets("t:SkillData");
        int count = 0;

        // 1단계: 유효한 ID를 가진 스킬들의 ID를 수집
        HashSet<ulong> activeIds = new HashSet<ulong>();
        List<SkillData> skillsToAssign = new List<SkillData>();

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            SkillData skill = AssetDatabase.LoadAssetAtPath<SkillData>(path);
            if (skill == null) continue;

            if (skill.ID != 0 && !activeIds.Contains(skill.ID))
            {
                activeIds.Add(skill.ID);
            }
            else
            {
                skillsToAssign.Add(skill);
            }
        }

        // 2단계: ID가 없거나 중복인 스킬들에게 고유 ID를 순서대로 할당
        ulong currentMaxId = 0;
        foreach (ulong id in activeIds)
        {
            if (id > currentMaxId) currentMaxId = id;
        }

        ulong nextAvailableId = currentMaxId + 1;

        foreach (SkillData skill in skillsToAssign)
        {
            SerializedObject so = new SerializedObject(skill);
            SerializedProperty idProp = so.FindProperty("m_id");
            if (idProp != null)
            {
                while (activeIds.Contains(nextAvailableId) || nextAvailableId == 0)
                {
                    nextAvailableId++;
                }

                so.Update();
                idProp.longValue = (long)nextAvailableId;
                so.ApplyModifiedProperties();

                activeIds.Add(nextAvailableId);
                Debug.Log($"[SkillDataEditor] 일괄 처리: '{skill.name}' 스킬에 고유 ID {nextAvailableId} 할당");
                count++;
            }
        }

        // 3단계: 전체 스킬에 대해 levelDataList 개수(최소 1개) 및 레벨 순서 보정
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            SkillData skill = AssetDatabase.LoadAssetAtPath<SkillData>(path);
            if (skill == null) continue;

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
        Debug.Log($"[SkillDataEditor] 일괄 검사 완료. 총 {count}개의 스킬 ID를 신규 할당/보정했습니다.");
    }
}
