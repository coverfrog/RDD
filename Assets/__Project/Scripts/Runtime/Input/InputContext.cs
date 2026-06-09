using Mirror;
using System;
using UnityEngine;

[Serializable]
public struct InputContext 
{
    [Flags]
    public enum InputFlags : ushort
    {
        None         = 0,
        ClickLeft    = 1 << 0,
        ClickedLeft  = 1 << 1,
        ClickRight   = 1 << 2,
        ClickedRight = 1 << 3,
    }

    public InputFlags Flags;
    public ushort ClickSlotsMask;
    public ushort ClickedSlotsMask;
    public Vector3 MoveGroundPoint;

    public bool IsClickLeft
    {
        get => (Flags & InputFlags.ClickLeft) != 0;
        set => SetFlag(InputFlags.ClickLeft, value);
    }
    public bool IsClickedLeft
    {
        get => (Flags & InputFlags.ClickedLeft) != 0;
        set => SetFlag(InputFlags.ClickedLeft, value);
    }
    public bool IsClickRight
    {
        get => (Flags & InputFlags.ClickRight) != 0;
        set => SetFlag(InputFlags.ClickRight, value);
    }
    public bool IsClickedRight
    {
        get => (Flags & InputFlags.ClickedRight) != 0;
        set => SetFlag(InputFlags.ClickedRight, value);
    }

    private void SetFlag(InputFlags flag, bool value)
    {
        if (value == true) 
            Flags |= flag;
        else 
            Flags &= ~flag;

        #region Note

        /*
         1. 값이 참인 경우 
         Or 연산을 통해 해당 값을 참으로 켠다.

         0  0  1  0   <- 입력 값
         0  1  0  0   <- 원래 값
         ----------
         0  1  1  0   <- 결과 값

         2. 값이 거짓인 경우
         들어온 값에 대해서 뒤집는다

         0  0  1  0   <- 입력 값
         1  1  0  1   <- 뒤집힌 입력 값

         뒤집힌 값에 대해서 And 연산을 통해 값을 걸러 낸다

         1  1  0  1   <- 뒤집힌 입력 값
         0  1  1  0   <- 원래 값
         ----------
         0  1  0  0   <- 결과 값

         */

        #endregion
    }

    public bool GetSlotClick(int idx) => (ClickSlotsMask & (1 << idx)) != 0;
    public void SetSlotClick(int idx, bool value)
    {
        if (value) 
            ClickSlotsMask |= (ushort)(1 << idx);
        else 
            ClickSlotsMask &= (ushort)~(1 << idx);

        #region Note
        /*
            SetFlag와 원리는 동일하다.
            비트 쉬프트는 1이라는 값을 왼쪽으로 'n'번 보낸다.

            idx 가 0이라면 
            0  0  0  1

            idx 가 1이라면
            0  0  1  0
       
            추가 원리는 동일하다.
         */
        #endregion
    }
    public bool GetSlotClicked(int idx) => (ClickedSlotsMask & (1 << idx)) != 0;
    public void SetSlotClicked(int idx, bool value)
    {
        if (value) 
            ClickedSlotsMask |= (ushort)(1 << idx);
        else 
            ClickedSlotsMask &= (ushort)~(1 << idx);
    }

    public void ClearOneShotInputs()
    {
        IsClickLeft = false;
        IsClickRight = false;
        ClickSlotsMask = 0;
    }
}