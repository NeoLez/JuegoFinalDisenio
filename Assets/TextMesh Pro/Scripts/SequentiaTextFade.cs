using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SequentiaTextFade : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> texts;

    [SerializeField] private float fadeDuration;

    [SerializeField] private KeyCode skipKey = KeyCode.Space;

    [SerializeField] private string nextSceneName = "Level1";

    private bool skipCurrent;

    private void Awake()
    {
        InitializeTexts();
    }

    private void Start()
    {
        StartCoroutine(PlaySequence());
    }

    private void Update()
    {
        if (Input.GetKeyDown(skipKey))
        {
            skipCurrent = true;
        }
    }

    private void InitializeTexts()
    {
        foreach (var text in texts)
        {
            if (text == null) continue;

            text.alpha = 0f;
            text.interactable = false;
            text.blocksRaycasts = false;
        }
    }

    private IEnumerator PlaySequence()
    {
        foreach (var text in texts)
        {
            skipCurrent = false;

            yield return FadeWithSkip(text, 0f, 1f);

            yield return FadeWithSkip(text, 1f, 0f);

            text.alpha = 0f;
        }

        LoadNextScene();
    }

    private IEnumerator FadeWithSkip(CanvasGroup text, float from, float to)
    {
        float elapsed = 0f;
        text.alpha = from;

        while (elapsed < fadeDuration)
        {
            if (skipCurrent)
            {
                text.alpha = 0f;
                yield break;
            }

            elapsed += Time.deltaTime;
            text.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            yield return null;
        }

        text.alpha = to;
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}

