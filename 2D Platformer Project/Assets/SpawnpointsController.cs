using Assets.Scripts.Data.Player;
using Assets.Scripts.StateMachine.PlayerStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointsController : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerData != null)
        {
            playerData.CurrentSpawnPoint = this.transform;
        }
    }
}
