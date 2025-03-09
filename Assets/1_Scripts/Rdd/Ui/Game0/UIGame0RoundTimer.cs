using System;
using UnityEngine;
using TMPro;

public class UIGame0RoundTimer : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private TMP_Text mText;

    private void OnEnable()
    {
        RddGame0.OnRoundTimer += SetText;
    }

    private void OnDisable()
    {
        RddGame0.OnRoundTimer -= SetText;
    }

    private void SetText(float totalSec)
    {
        float min = totalSec / 60.0f;
        float sec = totalSec % 60.0f;

        mText.text = $"{min:00} : {sec:00}";
    }
}
