using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private Image fadeImage; 
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 1f;
            fadeImage.color = c;
            
            StartCoroutine(FadeOut());
        }
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeIn(sceneName));
    }

    private IEnumerator FadeIn(string sceneName)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            Color c = fadeImage.color;
            c.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeOut()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            Color c = fadeImage.color;
            c.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }
    }
}