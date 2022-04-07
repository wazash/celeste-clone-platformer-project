using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        Destroy(gameObject);
        EventsManager.OnRedAppleCollected.Invoke();
    }
}
