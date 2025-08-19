using System.Collections;
using UnityEngine;

public class InvertControls : MonoBehaviour
{
    public AudioClip enterSound; // Sonido al entrar
    public AudioClip exitSound;  // Sonido al salir
    public AudioSource audioSource; // Fuente de audio para reproducir los sonidos

    private Coroutine invertCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource != null && enterSound != null)
                audioSource.PlayOneShot(enterSound);

            invertCoroutine = StartCoroutine(InvertControlsAfterDelay(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource != null && exitSound != null)
                audioSource.PlayOneShot(exitSound);

            if (invertCoroutine != null)
            {
                StopCoroutine(invertCoroutine);
                invertCoroutine = null;
            }

            MovementControllerTest movement = other.GetComponent<MovementControllerTest>();
            if (movement != null)
            {
                movement.InvertControls(false);
            }
        }
    }

    private IEnumerator InvertControlsAfterDelay(Collider player)
    {
        yield return new WaitForSeconds(1f);

        MovementControllerTest movement = player.GetComponent<MovementControllerTest>();
        if (movement != null)
        {
            movement.InvertControls(true);
        }
    }
}
