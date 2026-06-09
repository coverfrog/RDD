using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public bool IsLoaded { get; private set; }

    public Dictionary<ulong, SkillData> SkillInfos { get; private set; } = new Dictionary<ulong, SkillData>();

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
        SkillInfos.Clear();
        SkillInfos.Add(1, Resources.Load<SkillData>("Skills/dash"));
        SkillInfos.Add(2, Resources.Load<SkillData>("Skills/fireball"));
        SkillInfos.Add(3, Resources.Load<SkillData>("Skills/sacrifice"));
        SkillInfos.Add(4, Resources.Load<SkillData>("Skills/teleport"));

        IsLoaded = true;
    }
}
