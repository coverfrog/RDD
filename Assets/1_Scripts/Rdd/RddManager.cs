using System;
using Cf.Docs;
using Cf.Pattern;
using UnityEngine;
using UnityEngine.Serialization;

public class RddManager : Singleton<RddManager>
{
    [Header("Reference")]
    [SerializeField] private RddUser mRddUser;
    [SerializeField] private RddUserInput mRddUserInput;
    [SerializeField] private RddGame0 mRddGame0;

    [Header("Ctrl")] 
    [SerializeField] private SceneCtrl mSceneCtrl;
    
    [Header("Data")] 
    [SerializeField] private RddUserData mRddUserData;
    [SerializeField] private RddUserInputData mRddUserInputData;
    [SerializeField] private RddGame0Data mRddGame0Data;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init()
    {
        _ = Instance;
    }

    #region :: Unity

    protected override void Awake()
    {
        base.Awake();

        gameObject.name = "Rdd Manager";
        
        mRddUser = new GameObject("Rdd User").AddComponent<RddUser>();
        mRddUserInput = new GameObject("Rdd User Input").AddComponent<RddUserInput>();
        mRddGame0 = new GameObject("Rdd Game 0").AddComponent<RddGame0>();
        
        mRddUser.transform.SetParent(transform);
        mRddUserInput.transform.SetParent(transform);
        
        mRddUser.OnDataChange += data => mRddUserData = data;
        mRddUserInput.OnDataChange += data => mRddUserInputData = data;
        mRddGame0.OnDataChange += data => mRddGame0Data = data;
    }

    private void Start()
    {
        if (!mSceneCtrl)
        {
            Debug.Assert(true, "[Rdd Mgr] Not Found Scene Ctrl");
        }

        switch (mSceneCtrl.GetSceneName)
        {
            case SceneName.Intro:
                break;
            case SceneName.Game0:
                mRddGame0.RoundDebugStart(0);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    #endregion

    #region :: Ctrl

    public void SetCtrlScene(SceneCtrl sceneCtrl) => mSceneCtrl = sceneCtrl;

    #endregion
}
