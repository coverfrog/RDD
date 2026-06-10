using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public bool IsLoaded { get; private set; }

    public Dictionary<ulong, SkillData> SkillInfos { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SkillInfos = new Dictionary<ulong, SkillData>();
        SkillData[] loadedSkills = Resources.LoadAll<SkillData>("Skills/Skill");
        foreach (SkillData skill in loadedSkills)
        {
            if (SkillInfos.ContainsKey(skill.ID))
            {
                Debug.LogError($"[DataManager] Duplicate Skill ID detected: {skill.ID} on asset '{skill.name}' (already registered by '{SkillInfos[skill.ID].name}'). Skipping.");
                continue;
            }
            SkillInfos.Add(skill.ID, skill);
        }

        IsLoaded = true;
    }
}
