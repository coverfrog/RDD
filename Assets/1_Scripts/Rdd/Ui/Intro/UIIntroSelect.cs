using Cf.Ui;
using UnityEngine;
using UnityEngine.EventSystems;

public enum UIIntroSelectType
{
    GameStart,
    Settings,
    Quit,
}

public abstract class UIIntroSelect : UINode, IPointerEnterHandler, IPointerExitHandler
{
    private const float MaxLocalScale = 1.3f;
    private const float MinLocalScale = 1.0f;
    private const float EnterDuration = 0.2f;
    
    private bool _mIsEnter;
    private bool _mIsEnterComplete;
    
    private float _mEnterTimer;
    
    public abstract UIIntroSelectType GetSelectType();

    public abstract void OnInteract(UIIntroSelector introSelector);

    public void OnPointerEnter(PointerEventData eventData)
    {
        _mIsEnter = true;
        _mIsEnterComplete = false;
        _mEnterTimer = 0.0f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _mIsEnter = false;
    }

    private void OnScaleUpdate(float percent)
    {
        transform.localScale = Vector3.one * (MinLocalScale + (MaxLocalScale - MinLocalScale) * percent);
    }

    private void Update()
    {
        if (_mIsEnter)
        {
            if (_mIsEnterComplete)
            {
                return;
            }

            OnScaleUpdate(_mEnterTimer / EnterDuration);

            _mEnterTimer += Time.deltaTime;

            if (_mEnterTimer < EnterDuration)
            {
                return;
            }
            
            OnScaleUpdate(1.0f);

            _mIsEnterComplete = true;
        }

        else
        {
            OnScaleUpdate(0.0f);
        }
    }
}
