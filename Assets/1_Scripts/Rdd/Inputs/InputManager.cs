using System;
using Cf.Inputs;
using Cf.Pattern;
using UnityEngine;
using UnityEngine.Serialization;

public class InputManager : Singleton<InputManager>
{
    [Header("Reference")]
    [SerializeField] private IaMoveDirection mMoveDirection;
    [SerializeField] private IaRightClick mRightClick;

    public InputData Data { get; private set; } = new InputData();

    private void OnDirectionInput(Vector3 vector3)
    {
        Data.moveDirectionVector3 = vector3;
    }

    private void OnRightClickInput(Vector2 vector2)
    {
        Data.rightClickVector2 = vector2;
    }

    protected override void Awake()
    {
        base.Awake();

        mMoveDirection = gameObject.AddComponent<IaMoveDirection>();
        mRightClick = gameObject.AddComponent<IaRightClick>();
    }

    private void Start()
    {
        mMoveDirection.OnMoveDirection += OnDirectionInput;
        mRightClick.OnMovePoint += OnRightClickInput;
    }
}
