using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuLayoutsManager : MonoBehaviour
{
    [SerializeField] private Vector3 offPosition;
    [SerializeField] private Vector3 onPosition;

    private RectTransform rectTransform;

    [SerializeField] private float duration;

    private static int clicksCounter;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        SetButtonsInactive();
    }

    public void ShowLayout()
    {
        SetButtonsActive();
        if(clicksCounter < 1)
        {
            rectTransform.DOAnchorPos(onPosition, duration).SetEase(Ease.OutBack);   
        }
        else
        {
            rectTransform.DOAnchorPos(onPosition, duration).SetDelay(duration).SetEase(Ease.OutBack);
        }

        clicksCounter++;
    }

    public void HideLayout()
    {
        SetButtonsInactive();
        rectTransform.DOAnchorPos(offPosition, duration).SetEase(Ease.InBack);   
    }

    private void SetButtonsInactive()
    {
        Button[] buttons = GetComponentsInChildren<Button>();

        foreach (var button in buttons)
        {
            button.enabled = false;
        }
    }

    private void SetButtonsActive()
    {
        Button[] buttons = GetComponentsInChildren<Button>();

        foreach (var button in buttons)
        {
            button.enabled = true;
        }
    }
}