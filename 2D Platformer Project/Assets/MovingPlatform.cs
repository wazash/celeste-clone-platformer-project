using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float duration;

    [SerializeField] private Transform startPosition, endPosition;

    private new Rigidbody2D rigidbody;
    private Sequence sequence;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(endPosition.position, duration).SetEase(Ease.InCubic));
        sequence.AppendInterval(1);
        sequence.Append(transform.DOMove(startPosition.position, duration * 2).SetEase(Ease.Linear));
        sequence.AppendInterval(.5f);
        sequence.SetLoops(-1, LoopType.Restart);
        sequence.OnStepComplete(() => sequence.Pause());
        sequence.Pause();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!sequence.IsPlaying())
            {
                sequence.Play();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //sequence.Pause();

            collision.transform.parent = null;
        }
    }
}
