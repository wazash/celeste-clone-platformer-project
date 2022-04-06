using Cinemachine;
using UnityEngine;

/// <summary>
/// Changing active Virtual Camera while crossing room.
/// </summary>
public class RoomsVirtualCameraChanging : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            //virtualCamera.gameObject.SetActive(true);
            //virtualCamera.enabled = true;
            EventsManager.OnCameraPriorityChanged.Invoke(virtualCamera, 11);
        }
    }

    private void OnTriggerExit2D(Collider2D collision )
    {
      if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            //virtualCamera.gameObject.SetActive(false);
            //virtualCamera.enabled = false;

            EventsManager.OnCameraPriorityChanged.Invoke(virtualCamera, 9);
        }
    }
}