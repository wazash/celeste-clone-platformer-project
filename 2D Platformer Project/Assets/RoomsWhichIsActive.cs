using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsWhichIsActive : MonoBehaviour
{
    public bool IsActive { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsActive = false;
        }
    }
}
