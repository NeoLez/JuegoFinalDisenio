using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Achievements {
    public class AchievementNotificationQueue : MonoBehaviour {
        public static AchievementNotificationQueue Instance { get; private set; }

        [Header("Prefab")]
        [SerializeField] private AchievementNotification notificationPrefab;

        [Header("Icons")]
        [SerializeField] private Sprite hieloSprite;
        [SerializeField] private Sprite fuegoSprite;
        [SerializeField] private Sprite dashSprite;
        [SerializeField] private Sprite muerteSprite;
        [SerializeField] private Sprite completadoSprite;
        [SerializeField] private Sprite walkedJumpedSprite;

        private readonly Queue<(string name, Sprite icon)> _queue = new();
        private bool _isShowing = false;
        private AchievementNotification _instance;

        private void Awake() {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;

            _instance = Instantiate(notificationPrefab, transform);
            _instance.gameObject.SetActive(false);
        }

        private void Start() {
            AchievementManager.USE_FREEZE_SPELL.OnCompleted   += () => Enqueue("Use Freeze Spell",   hieloSprite);
            AchievementManager.USE_FIRE_SPELL.OnCompleted     += () => Enqueue("Use Fire Spell",     fuegoSprite);
            AchievementManager.USE_DASH_SPELL.OnCompleted     += () => Enqueue("Use Dash Spell",     dashSprite);
            AchievementManager.DIE_ONCE.OnCompleted           += () => Enqueue("Die Once",           muerteSprite);
            AchievementManager.COMPLETE_GAME.OnCompleted      += () => Enqueue("Complete Game",      completadoSprite);
        }

        public void Enqueue(string achievementName, Sprite icon = null) {
            if (this == null) return;
            _queue.Enqueue((achievementName, icon));
            if (!_isShowing) StartCoroutine(ProcessQueue());
        }

        private void OnDestroy() {
            AchievementManager.USE_FREEZE_SPELL.OnCompleted   -= () => Enqueue("Use Freeze Spell",   hieloSprite);
            AchievementManager.USE_FIRE_SPELL.OnCompleted     -= () => Enqueue("Use Fire Spell",     fuegoSprite);
            AchievementManager.USE_DASH_SPELL.OnCompleted     -= () => Enqueue("Use Dash Spell",     dashSprite);
            AchievementManager.DIE_ONCE.OnCompleted           -= () => Enqueue("Die Once",           muerteSprite);
            AchievementManager.COMPLETE_GAME.OnCompleted      -= () => Enqueue("Complete Game",      completadoSprite);
        }

        private IEnumerator ProcessQueue() {
            _isShowing = true;
            while (_queue.Count > 0) {
                var (name, icon) = _queue.Dequeue();
                _instance.gameObject.SetActive(true);
                _instance.Show(name, icon);

                yield return new WaitUntil(() => !_instance.gameObject.activeSelf);
                yield return new WaitForSeconds(0.2f);
            }
            _isShowing = false;
        }
    }
}