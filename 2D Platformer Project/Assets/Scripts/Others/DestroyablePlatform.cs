using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlatform : MonoBehaviour
{
    private new Collider2D collider2D;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float timeToDestroy;

    private void Start()
    {
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DestroyPlatform(timeToDestroy));
        }
    }

    private IEnumerator DestroyPlatform(float time)
    {
        // Play animation
        yield return new WaitForSecondsRealtime(time);

        SetPlatform(false);

        yield return new WaitForSecondsRealtime(time);

        SetPlatform(true);
    }

    public void SetPlatform(bool value)
    {
        collider2D.enabled = value;
        spriteRenderer.enabled = value;
    }
}
