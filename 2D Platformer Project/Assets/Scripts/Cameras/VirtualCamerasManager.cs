using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamerasManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventsManager.OnCameraPriorityChanged.AddListener(ChangeCameraPriority);
    }

    private void OnDisable()
    {
        EventsManager.OnCameraPriorityChanged.RemoveListener(ChangeCameraPriority);
    }

    private void ChangeCameraPriority(CinemachineVirtualCamera virtualCamera, int priorityValue)
    {
        virtualCamera.Priority = priorityValue;
    }

    
}
