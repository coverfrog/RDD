using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class SwingInput
{
    public bool isPutting;
    [Space]
    public float ballAngleHorizontal;
    public float ballAngleVertical;
    public float ballSpeed;
    [Space] 
    public float camAngle0;
    public float camAngle1;
}

public class SwingHelper : MonoBehaviour
{
    [SerializeField] private BallHelper mBallHelper;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Swing(new SwingInput()
            {
                isPutting = false,
                
                ballAngleHorizontal = 0,
                ballAngleVertical = 1,
                ballSpeed = 2.0f,
            });
        }
    }

    private void Swing(SwingInput swingInput)
    {
        mBallHelper.OnSwing(swingInput);
    }
}
