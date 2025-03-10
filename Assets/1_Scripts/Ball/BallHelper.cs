using System;
using UnityEngine;
using UnityEngine.Serialization;

public static class BallOptionConst
{
    public const double Mass    = 0.0459;               // 질량  
    public const double Area    = 0.001432;             // 표면적
    public const double Density = 1.225;                // 공기 밀도
    public const double Cd      = 0.22;                 // 항력 계수
    public const double Magnus  = 300.0;                // 회전 계수
}

[Serializable]
public class BallOption
{
    public double mass    = BallOptionConst.Mass;       // 질량  
    public double area    = BallOptionConst.Area;       // 표면적
    public double density = BallOptionConst.Density;    // 공기 밀도
    public double cd      = BallOptionConst.Cd;         // 항력 계수
    public double magnus  = BallOptionConst.Magnus;     // 회전 계수
}

[Serializable]
public class BallEnv
{
    public Vector3 windDir;
}

[Serializable]
public class BallData
{
    public Vector3 pos;
    public GeoType geoType;
    [Space]
    public float velocityLinear;
    public float velocityAngular;
}

public class BallHelper : MonoBehaviour
{
    [SerializeField] private BallOption mOption;
    [SerializeField] private BallEnv mEnv;
    [SerializeField] private BallData mData;

    private Ray _mRay;
    private RaycastHit[] _mHitResult;

    public void OnSwing(SwingInput swingInput)
    {
        
    }
}
