using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip track1; // First background track
    public AudioClip track2; // Second background track

    private AudioSource audioSource1;
    private AudioSource audioSource2;

    private bool isFading1 = false;
    private bool isFading2 = false;

    void Awake()
    {
        // Prevent duplicate instances when switching scenes
        if (FindObjectsOfType<BackgroundMusic>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject); // Keep music playing across scenes

        // Create and configure AudioSources
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource2 = gameObject.AddComponent<AudioSource>();

        SetupAudioSource(audioSource1, track1);
        SetupAudioSource(audioSource2, track2);

        // Start with both tracks playing
        PlayTrack(audioSource1, fadeIn: true);
        PlayTrack(audioSource2, fadeIn: true);
    }

    private void SetupAudioSource(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.loop = true;
        source.playOnAwake = false;
        source.volume = 0f; // Start silent for fade-in
    }

    public void PlayTrack(AudioSource source, bool fadeIn = false)
    {
        if (source.clip == null) return;

        source.Play();
        if (fadeIn)
        {
            StartCoroutine(FadeIn(source));
        }
    }

    public void AdjustTrack1Volume(float volume)
    {
        StartCoroutine(FadeToVolume(audioSource1, volume));
    }

    public void AdjustTrack2Volume(float volume)
    {
        StartCoroutine(FadeToVolume(audioSource2, volume));
    }

    private IEnumerator FadeIn(AudioSource source, float duration = 1.5f)
    {
        float startVolume = 0f;
        float targetVolume = 0.5f; // Adjust as needed

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(startVolume, targetVolume, t / duration);
            yield return null;
        }

        source.volume = targetVolume;
    }

    private IEnumerator FadeToVolume(AudioSource source, float targetVolume, float duration = 1.5f)
    {
        if ((source == audioSource1 && isFading1) || (source == audioSource2 && isFading2))
            yield break; // Prevent multiple fades

        if (source == audioSource1) isFading1 = true;
        if (source == audioSource2) isFading2 = true;

        float startVolume = source.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(startVolume, targetVolume, t / duration);
            yield return null;
        }

        source.volume = targetVolume;

        if (source == audioSource1) isFading1 = false;
        if (source == audioSource2) isFading2 = false;
    }
}