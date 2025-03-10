using System;
using UnityEngine;
using UnityEngine.Serialization;

public static class BallEnvConst
{
    public const double Mass    = 0.0459;               // 질량  
    public const double Area    = 0.001432;             // 표면적
    public const double Density = 1.225;                // 공기 밀도
    public const double Cd      = 0.22;                 // 항력 계수
    public const double Magnus  = 300.0;                // 회전 계수
}

[Serializable]
public class BallEnv
{
    public double mass    = BallEnvConst.Mass;          // 질량  
    public double area    = BallEnvConst.Area;          // 표면적
    public double density = BallEnvConst.Density;       // 공기 밀도
    public double cd      = BallEnvConst.Cd;            // 항력 계수
    public double magnus  = BallEnvConst.Magnus;        // 회전 계수
    [Space]
    public double windX;
    public double windZ;
    public double radius;
    public double rx;
    public double ry;
    public double rz;
}

[Serializable]
public class BallData
{
    // public double[] q;
    // public double[] deltaQ;
    // public double ds;
    // public double qScale;
    // public double[] dq;
}

public class BallHelper : MonoBehaviour
{
    [SerializeField] private BallEnv mEnv;

    public void Compute0()
    {
        
    }
}
