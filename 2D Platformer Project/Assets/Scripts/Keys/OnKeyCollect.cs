using Assets.Scripts.StateMachine.PlayerStateMachine;
using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.Events;

public class OnKeyCollect : MonoBehaviour
{
    public bool willCameraMoveAfterPick;
    public UnityEvent OnKeyCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            OnKeyCollected?.Invoke();

            if (willCameraMoveAfterPick)
            {
                collision.transform.position = transform.position;
            }
        }
    }
}
