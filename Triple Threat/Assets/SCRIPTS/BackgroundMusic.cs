using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour
{
    [Header("Audio Sources (Assign in Inspector)")]
    public AudioSource audioSource1; // First background track
    public AudioSource audioSource2; // Second background track

    [Header("Audio Clips (Assign in Inspector)")]
    public AudioClip track1; 
    public AudioClip track2;

    private bool isFading1 = false;
    private bool isFading2 = false;

    void Start()
    {
        SetupAudioSource(audioSource1, track1);
        SetupAudioSource(audioSource2, track2);

        // Start with both tracks playing
        PlayTrack(audioSource1, fadeIn: true);
        PlayTrack(audioSource2, fadeIn: true);
    }

    private void SetupAudioSource(AudioSource source, AudioClip clip)
    {
        if (source != null && clip != null)
        {
            source.clip = clip;
            source.loop = true;
            source.playOnAwake = false;
            source.volume = 0f; // Start silent for fade-in
        }
        else
        {
            Debug.LogWarning("Missing AudioSource or AudioClip assignment.");
        }
    }

    public void PlayTrack(AudioSource source, bool fadeIn = false)
    {
        if (source != null && source.clip != null)
        {
            source.Play();
            if (fadeIn)
            {
                StartCoroutine(FadeIn(source));
            }
        }
    }

    public void AdjustTrack1Volume(float volume)
    {
        if (audioSource1 != null)
            StartCoroutine(FadeToVolume(audioSource1, volume));
    }

    public void AdjustTrack2Volume(float volume)
    {
        if (audioSource2 != null)
            StartCoroutine(FadeToVolume(audioSource2, volume));
    }

    private IEnumerator FadeIn(AudioSource source, float duration = 1.5f)
    {
        if (source == null) yield break;

        float startVolume = 0f;
        float targetVolume = source.volume > 0 ? source.volume : 0.5f;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(startVolume, targetVolume, t / duration);
            yield return null;
        }

        source.volume = targetVolume;
    }

    private IEnumerator FadeToVolume(AudioSource source, float targetVolume, float duration = 1.5f)
    {
        if (source == null) yield break;

        bool isFading = source == audioSource1 ? isFading1 : isFading2;
        if (isFading) yield break; // Prevent overlapping fades

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
