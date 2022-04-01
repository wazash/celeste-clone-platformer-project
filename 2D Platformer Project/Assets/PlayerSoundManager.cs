using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    private AudioSource playerAudioSource;

    private void OnEnable()
    {
        EventsManager.OnPlayeySfxPlay.AddListener(PlayPlayerSFX);
    }
    private void OnDisable()
    {
        EventsManager.OnPlayeySfxPlay.RemoveListener(PlayPlayerSFX);
    }

    private void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
    }

    public void PlayPlayerSFX(AudioClip clip)
    {
        playerAudioSource.PlayOneShot(clip);
    }
}
