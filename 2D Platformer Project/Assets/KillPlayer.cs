using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KillPlayer : MonoBehaviour
{
    public UnityEvent OnTouchKillable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnTouchKillable.Invoke();
        }
    }
}
