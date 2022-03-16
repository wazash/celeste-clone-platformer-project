using Cinemachine;
using UnityEngine;

public class ControllTimeBetweenRoomChange : MonoBehaviour
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
