using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamerasManager : MonoBehaviour
{
    private void OnEnable()
    {
        VerticalDoors.OnPriorityCameraChaged += ChangeCameraPriority;
    }

    private void OnDisable()
    {
        VerticalDoors.OnPriorityCameraChaged -= ChangeCameraPriority;
    }

    public void ChangeCameraPriority(CinemachineVirtualCamera vCamera, int priorityValue)
    {
        vCamera.Priority = priorityValue;
    }
}
