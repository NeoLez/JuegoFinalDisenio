using UnityEngine;

public class MusicFadeOut : MonoBehaviour
{
    [Header("Fade Settings")]
    public AudioSource musicSource;         
    public float fadeDuration = 3f;         

    private float initialVolume;

    void Start()
    {
        if (musicSource != null)
        {
            initialVolume = musicSource.volume;
            StartCoroutine(FadeOutMusic());
        }
    }

    private System.Collections.IEnumerator FadeOutMusic()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(initialVolume, 0f, elapsed / fadeDuration);
            yield return null;
        }

        musicSource.volume = 0f;
        musicSource.Stop();
    }
}