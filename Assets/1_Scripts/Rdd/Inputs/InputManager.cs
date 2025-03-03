using System;
using Cf.Inputs;
using Cf.Pattern;
using UnityEngine;
using UnityEngine.Serialization;

public class InputManager : Singleton<InputManager>
{
    [Header("Reference")]
    [SerializeField] private InputActionDirection mDirection;

    public InputData Data { get; private set; }

    private void OnDirectionInput(Vector3 vector3)
    {
        Data.directionVector3 = vector3;
    }

    protected override void Awake()
    {
        base.Awake();

        mDirection = gameObject.AddComponent<InputActionDirection>();
    }

    private void Start()
    {
        mDirection.OnMove += OnDirectionInput;
    }
}
