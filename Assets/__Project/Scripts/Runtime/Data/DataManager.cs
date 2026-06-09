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
        SkillInfos = Resources.LoadAll<SkillData>("Skills/Skill").ToDictionary(x => x.ID, x => x);

        IsLoaded = true;
    }
}
