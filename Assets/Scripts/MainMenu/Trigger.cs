using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "NombreDeLaEscena";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<SceneFader>().FadeToScene(nextSceneName);
        }
    }
}