using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip startMusic, backMusic;

    private AudioSource track01, track02;

    private bool PlayingTrack1;

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        track01 = gameObject.AddComponent<AudioSource>();
        track02 = gameObject.AddComponent<AudioSource>();

        track01.playOnAwake = false;
        track02.playOnAwake = false;

        PlayingTrack1 = true;

        PlayTrack(startMusic);
    }

    public void PlayTrack(AudioClip newClip)
    {
        StopAllCoroutines();

        StartCoroutine(FadeTrack(newClip));

        PlayingTrack1 = !PlayingTrack1;
    }

    public void ReturnToMenuMusic()
    {
        PlayTrack(backMusic);
    }

    public void PauseTrack()
    {
        if (PlayingTrack1 == true)
        {
            track01.Pause();
        }

        if (PlayingTrack1 == false)
        {
            track02.Pause();
        }
    }

    public void UnPauseTrack()
    {
        if (PlayingTrack1 == true)
        {
            track01.Play();
        }

        if (PlayingTrack1 == false)
        {
            track02.Play();
        }
    }

    public void StopAndResetTrack()
    {
        if (PlayingTrack1 == true)
        {
            track01.Stop();
        }

        if (PlayingTrack1 == false)
        {
            track02.Stop();
        }
    }

    private IEnumerator FadeTrack(AudioClip newClip)
    {

        float timeToFade = 2f;
        float timeElapsed = 0;

        if (PlayingTrack1 == true)
        {
            track02.clip = newClip;
            track02.Play();

            while(timeElapsed < timeToFade)
            {
                track02.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                track01.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            track01.Pause();
        }
        else
        {
            track01.clip = newClip;
            track01.Play();

            while (timeElapsed < timeToFade)
            {
                track01.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                track02.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            track02.Pause();
        }
    }
}
