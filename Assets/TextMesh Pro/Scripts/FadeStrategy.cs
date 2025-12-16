using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FadeStrategy
{
    public static IEnumerator Fade(
         CanvasGroup canvasGroup,
         float from,
         float to,
         float duration)
    {
        float elapsed = 0f;
        canvasGroup.alpha = from;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = to;
    }
}

