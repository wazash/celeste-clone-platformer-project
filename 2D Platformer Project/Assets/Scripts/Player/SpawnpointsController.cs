using Assets.Scripts.Data.Player;
using UnityEngine;

public class SpawnpointsController : MonoBehaviour
{
    [SerializeField]
    private OldPlayerData playerData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerData != null)
        {
            playerData.CurrentSpawnPoint = this.transform;
        }
    }
}
