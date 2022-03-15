using Cinemachine;
using UnityEngine;

public class RoomsCameraTransition : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineConfiner confiner;

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            virtualCamera.gameObject.SetActive(true);
            virtualCamera.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision )
    {
      if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            virtualCamera.gameObject.SetActive(false);
            virtualCamera.enabled = false;
        }
    }
}