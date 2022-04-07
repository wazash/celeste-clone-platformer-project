using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if(player != null && player.StateMachine.CurrentState == player.DashState)
            {
                Destroy(gameObject, .2f);
                player.StateMachine.ChangeState(player.InAirState);
                player.SetVelocityZero();
            }
        }
    }
}
