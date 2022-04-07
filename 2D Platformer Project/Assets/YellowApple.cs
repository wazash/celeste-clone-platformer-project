using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YellowApple : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private RoomsWhichIsActive room;

    [SerializeField] private float offset;
    [SerializeField] private float duration;

    private bool isPlayed = false;

    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    private Vector2 startedPosition;

    private void OnEnable()
    {
        EventsManager.OnYellowAppleRestart.AddListener(RestartApple);
    }
    private void OnDisable()
    {
        EventsManager.OnYellowAppleRestart.RemoveListener(RestartApple);
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        startedPosition = transform.position;
    }

    private void Update()
    {
        if(room.IsActive && player.StateMachine.CurrentState == player.DashState && !isPlayed)
        {
            RunApple();
        }
    }

    private void RunApple()
    {
        isPlayed = true;
        transform.DOMoveY(transform.position.y + offset, duration).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            DisableApple();
        });
    }

    private void DisableApple()
    {
        spriteRenderer.enabled = false;
        circleCollider.enabled = false;
    }

    private void RestartApple()
    {
        //transform.position = startedPosition;

        spriteRenderer.enabled = true;
        circleCollider.enabled = true;

        isPlayed = false;
    }
}
