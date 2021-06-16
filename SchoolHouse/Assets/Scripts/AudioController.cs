using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Collections;

public class AudioController : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioController instance;

    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlayMusic(string name)
    {
        StopAllCoroutines();

        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            return;
        }
        s.source.Play();

        StartCoroutine(FadeTrackIn(name));
    }

    public void StopMusic(string name)
    {
        StopAllCoroutines();

        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            return;
        }

        s.source.Pause();
    }

    public void StopMusicAndReset(string name)
    {
        StopAllCoroutines();

        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            return;
        }

        s.source.Stop();
    }

    public IEnumerator FadeTrackIn(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.Play();

        float timeToFade = 5f;
        float timeElapsed = 0f;

        while(timeElapsed < timeToFade)
        {
            s.source.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator FadeTrackOut(string name)
    {
        float timeToFade = 5f;
        float timeElapsed = 0f;

        Sound s = Array.Find(sounds, sound => sound.name == name);

        while (timeElapsed < timeToFade)
        {
            s.source.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        s.source.Pause();
    }

    private IEnumerator FadeTrackOutAndReset(string name)
    {
        float timeToFade = 5f;
        float timeElapsed = 0f;

        Sound s = Array.Find(sounds, sound => sound.name == name);

        while (timeElapsed < timeToFade)
        {
            s.source.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        s.source.Stop();
    }
}
