using System;
using Cf.Inputs;
using Cf.Pattern;
using UnityEngine;
using UnityEngine.Serialization;

public class InputManager : Singleton<InputManager>
{
    [Header("Reference")]
    [SerializeField] private IaMoveDirection mMoveDirection;
    [SerializeField] private IaMovePoint mMovePoint;

    public InputData Data { get; private set; } = new InputData();

    private void OnDirectionInput(Vector3 vector3)
    {
        Data.directionVector3 = vector3;
    }

    protected override void Awake()
    {
        base.Awake();

        mMoveDirection = gameObject.AddComponent<IaMoveDirection>();
        mMovePoint = gameObject.AddComponent<IaMovePoint>();
    }

    private void Start()
    {
        mMoveDirection.OnMoveDirection += OnDirectionInput;
    }
}
