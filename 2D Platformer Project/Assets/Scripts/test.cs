using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private CinemachineBrain brainCM;

    private void Start()
    {
        brainCM = GetComponent<CinemachineBrain>();
    }
    private void Update()
    {
        if (brainCM.IsBlending)
        {
            Time.timeScale = .1f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
