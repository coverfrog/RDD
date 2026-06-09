using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set;  }

    [SerializeField] private InputContext m_context = new();

    private readonly List<IInputObserver> m_observers = new();


    private CInputs m_cInputs;


    public void AddObserver(IInputObserver inputObserver)
    {
        if (m_observers.Contains(inputObserver) == false)
            m_observers.Add(inputObserver);
    }

    public void RemoveObserver(IInputObserver inputObserver)
    {
        if (m_observers.Contains(inputObserver) == true) 
            m_observers.Remove(inputObserver);
    }

    private void Nottify()
    {
        for (int i = m_observers.Count - 1; i >= 0; i--)
        {
            IInputObserver observer = m_observers[i];

            if (observer != null)
            {
                observer.OnInput(m_context);
            }
            else
            {
                m_observers.RemoveAt(i);
            }
        }
    }

    private void Setup()
    {
        m_cInputs = new();
        m_context = new InputContext();

        Setup_ClickLeft();
        Setup_ClickRight();

        Setup_Slot(m_cInputs.Player.Slot0, 0);
        Setup_Slot(m_cInputs.Player.Slot1, 1);
        Setup_Slot(m_cInputs.Player.Slot2, 2);
        Setup_Slot(m_cInputs.Player.Slot3, 3);

        m_cInputs.Enable();
    }

    private void Setup_ClickLeft()
    {
        InputAction act = m_cInputs.Player.ClickLeft;

        act.performed += ctx =>
        {
            m_context.IsClickLeft = true;
            m_context.IsClickedLeft = true;
        };

        act.canceled += ctx =>
        {
            m_context.IsClickedLeft = false;
        };
    }

    private void Setup_ClickRight()
    {
        InputAction act = m_cInputs.Player.ClickRight;

        act.performed += ctx =>
        {
            m_context.IsClickRight = true;
            m_context.IsClickedRight = true;

            if (UtilWorld.TryGetMouseGroundPoint(out m_context.MoveGroundPoint)) { }
        };

        act.canceled += ctx =>
        {
            m_context.IsClickedRight = false;
        };
    }

    private void Setup_Slot(InputAction ia, int idx)
    {
        ia.performed += ctx =>
        {
            m_context.SetSlotClick(idx, true);
            m_context.SetSlotClicked(idx, true);
        };
        ia.canceled += ctx =>
        {
            m_context.SetSlotClicked(idx, false);
        };
    }

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
        Setup();
    }

    private void Update()
    {
        Nottify();
    }

    private void LateUpdate()
    {
        m_context.ClearOneShotInputs();
    }

    private void OnDestroy()
    {
        if (m_cInputs != null)
        {
            m_cInputs.Disable();
            m_cInputs.Dispose();
        }
    }
}
