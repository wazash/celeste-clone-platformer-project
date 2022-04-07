using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ApplesManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI appleText;
    [SerializeField] private GameObject applePlanel;

    [SerializeField] private GameObject applesHolderGameObject;

    private int currentApples;
    private int maxApples;

    private void OnEnable()
    {
        EventsManager.OnRedAppleCollected.AddListener(AddApple);
    }
    private void OnDisable()
    {
        EventsManager.OnRedAppleCollected.RemoveListener(AddApple);
    }


    private void Start()
    {
        var apples = applesHolderGameObject.GetComponentsInChildren<Apple>();
        currentApples = 0;
        maxApples = apples.Length;

        UpdateText();
    }

    private void AddApple()
    {
        currentApples++;
        UpdateText();
    }

    private void UpdateText()
    {
        appleText.text = $"{currentApples} / {maxApples}";
    }
}
