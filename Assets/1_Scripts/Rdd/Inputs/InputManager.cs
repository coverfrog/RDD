using System;
using Cf.Inputs;
using Cf.Pattern;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [Header("Reference")]
    [SerializeField] private InputActionMove mMove;

    public event Action<Vector3> OnMove;

    private void OnMoveInput(Vector3 vector3)
    {
        OnMove?.Invoke(vector3);
    }

    protected override void Awake()
    {
        base.Awake();

        mMove = gameObject.AddComponent<InputActionMove>();
    }

    private void Start()
    {
        mMove.OnMove += OnMoveInput;
    }
}
