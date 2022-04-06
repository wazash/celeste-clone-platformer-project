using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float launchPower;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.SetVelocityY(launchPower);
            player.DashState.ResetCanDash();
        }
    }
}
