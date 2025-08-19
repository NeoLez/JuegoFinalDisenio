using Achievements;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu {
    public class AchievementMenu : MonoBehaviour {
        [SerializeField] public Image hielo;
        [SerializeField] public Sprite hieloSprite;
        [SerializeField] public Image fuego;
        [SerializeField] public Sprite fuegoSprite;
        [SerializeField] public Image dash;
        [SerializeField] public Sprite dashSprite;
        [SerializeField] public Image muerte;
        [SerializeField] public Sprite muerteSprite;
        [SerializeField] public Image completado;
        [SerializeField] public Sprite completadoSprite;
        private void Start() {
            AchievementManager.USE_FREEZE_SPELL.OnCompleted += () => hielo.sprite = hieloSprite;
            AchievementManager.USE_FIRE_SPELL.OnCompleted += () => fuego.sprite = fuegoSprite;
            AchievementManager.USE_DASH_SPELL.OnCompleted += () => dash.sprite = dashSprite;
            AchievementManager.DIE_ONCE.OnCompleted += () => muerte.sprite = muerteSprite;
            AchievementManager.COMPLETE_GAME.OnCompleted += () => completado.sprite = completadoSprite;
        }
    }
}