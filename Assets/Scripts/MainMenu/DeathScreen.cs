namespace MainMenu {
    public class DeathScreen {
        public void Respawn() {
            GameManager.Player.GetComponent<PlayerHealth>().Respawn();
        }
    }
}