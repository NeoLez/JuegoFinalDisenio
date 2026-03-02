using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Achievements {
    public class AchievementNotification : MonoBehaviour {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI achievementNameText;
        [SerializeField] private Image iconImage;

        [Header("Timing")]
        [SerializeField] private float slideTime   = 0.4f;
        [SerializeField] private float displayTime = 3.0f;

        [Header("Easing")]
        [SerializeField] private AnimationCurve slideCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        private RectTransform _rt;
        private float _panelWidth;

        private void Awake() {
            _rt = GetComponent<RectTransform>();
            _panelWidth = _rt.rect.width;
            MoveOffScreen();
        }

        public void Show(string name, Sprite icon = null) {
            if (achievementNameText != null) achievementNameText.text = name;
            if (iconImage != null && icon != null) iconImage.sprite = icon;

            StopAllCoroutines();
            StartCoroutine(AnimationSequence());
        }

        private IEnumerator AnimationSequence() {
            _panelWidth = _rt.rect.width;

            yield return StartCoroutine(Slide(offScreen: false));
            yield return new WaitForSeconds(displayTime);
            yield return StartCoroutine(Slide(offScreen: true));

            gameObject.SetActive(false);
        }

        private IEnumerator Slide(bool offScreen) {
            float startX = _rt.anchoredPosition.x;
            float endX   = offScreen ? _panelWidth : 0f;

            float elapsed = 0f;
            while (elapsed < slideTime) {
                elapsed += Time.deltaTime;
                float t = slideCurve.Evaluate(Mathf.Clamp01(elapsed / slideTime));
                _rt.anchoredPosition = new Vector2(Mathf.LerpUnclamped(startX, endX, t),
                                                   _rt.anchoredPosition.y);
                yield return null;
            }
            _rt.anchoredPosition = new Vector2(endX, _rt.anchoredPosition.y);
        }

        private void MoveOffScreen() {
            Canvas.ForceUpdateCanvases();
            _panelWidth = _rt.rect.width > 0 ? _rt.rect.width : 300f;
            _rt.anchoredPosition = new Vector2(_panelWidth, _rt.anchoredPosition.y);
        }
    }
}