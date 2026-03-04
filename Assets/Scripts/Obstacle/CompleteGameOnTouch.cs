using Facts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Obstacle {
    public class CompleteGameOnTouch : MonoBehaviour {
        public string sceneToLoad;

        private bool playerInRange = false;

        private void Update() {
            if (playerInRange && Input.GetKeyDown(KeyCode.F)) {
                Events.ON_PLAYER_COMPLETED_GAME.Raise(Unit.Default);
                SceneManager.LoadScene(sceneToLoad);
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player"))
                playerInRange = true;
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("Player"))
                playerInRange = false;
        }
    }
}