using Assets.Scripts.StateMachine.PlayerStateMachine;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class PillarGateBehaviour : MonoBehaviour
{
    [SerializeField]
    private float duration;
    [SerializeField]
    private float position;
    [SerializeField]
    private Transform gate;

    private CinemachineVirtualCamera cam;

    private void Start()
    {
        cam = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    public void OpenInAxisY()
    {
        cam.Priority = 11;
        PlayerSM player = FindObjectOfType<PlayerSM>();
        player.StopPlayer();
        gate.DOShakePosition(1, strength: .05f, vibrato: 20).OnComplete(() => {
            gate.DOMoveY(transform.position.y + position, duration).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() => {
                cam.Priority = 9;
                player.StartPlayer();
            });
        });
      
        
    }
}
