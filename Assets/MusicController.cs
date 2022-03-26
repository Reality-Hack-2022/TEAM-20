using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip[] sound;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        PlaySound();
    }

    public void PlaySound()
    {
        int rand = Random.Range(0, sound.Length - 1);
        audioSource.clip = sound[rand];
        audioSource.Play();
    }

    public void PlayCalmGameMusic()
    {

        // Just hardcode the index of this song
    }

    public void Play()
    {

        // Just hardcode the index of this song
        audioSource.clip = sound[1];
        audioSource.Play();
    }
}
