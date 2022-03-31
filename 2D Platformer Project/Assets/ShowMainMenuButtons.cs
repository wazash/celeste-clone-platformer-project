using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMainMenuButtons : MonoBehaviour
{
    [SerializeField] private float endPositionX;

    [SerializeField] private FloatReference fadeCoverDuration;
    [SerializeField] private FloatReference fadeCoverDelay;
    [SerializeField] private FloatReference duration;
    private float delay;

    private Button[] buttons;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
    }

    void Start()
    {
        delay = fadeCoverDelay.Value + fadeCoverDuration.Value;

        buttons[0].GetComponent<RectTransform>().DOAnchorPosX(endPositionX, duration.Value).SetEase(Ease.OutBack).SetDelay(delay).OnComplete(() =>
        {
            buttons[1].GetComponent<RectTransform>().DOAnchorPosX(endPositionX, duration.Value).SetEase(Ease.OutBack).OnComplete(() =>
            {
                buttons[2].GetComponent<RectTransform>().DOAnchorPosX(endPositionX, duration.Value).SetEase(Ease.OutBack);
            });
        });
    }
}
