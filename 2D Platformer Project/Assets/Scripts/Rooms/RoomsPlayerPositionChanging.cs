using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsPlayerPositionChanging : MonoBehaviour
{
    private float enterVel = 30;
    [SerializeField] private float exitVelocity = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();

            var relativePosition = transform.InverseTransformPoint(collision.transform.position);

            if(relativePosition.y <= 0)
            {
                player.InputHandler.playerInput.currentActionMap.Disable();
                player.StateMachine.ChangeState(player.InAirState);
                player.SetVelocityY(enterVel);
            }

            EventsManager.OnYellowAppleRestart.Invoke(); // Restart yellow apple
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();

            var relativePosition = transform.InverseTransformPoint(collision.transform.position);

            if (relativePosition.y >= 1)
            {
                player.InputHandler.playerInput.currentActionMap.Enable();
                player.StateMachine.ChangeState(player.InAirState);
                player.SetVelocityY(exitVelocity);
            }
        }
    }
}
