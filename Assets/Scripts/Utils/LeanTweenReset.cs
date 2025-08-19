using UnityEngine;

public static class LeanTweenReset {
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void ResetLeanTween() {
        LeanTween.reset();
    }
}