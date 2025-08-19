using UnityEngine;
using UnityEngine.UIElements;

public class PuzzleZone : MonoBehaviour
{
    public Transform snapPoint;
    public int puzzleIndex;
    public AudioClip placeBoxSound;

    public PuzzleManager puzzleManager; // 👈 Nueva referencia // 👈 ChatGPT estuvo aqui

    private bool isOccupied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isOccupied && other.GetComponent<Box>() != null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            GameManager.Player.GetComponent<Drag>().DisengageObject(other.gameObject);
            other.transform.position = snapPoint.position;
            other.transform.rotation = snapPoint.rotation;

            isOccupied = true;

            if (placeBoxSound != null)
                GameManager.AudioSystem.PlaySoundPositional(placeBoxSound, transform.position, GameManager.AudioSystem.VFX);

            // 🔁 Usamos la referencia pública // 👈 y aqui tambien
            if (puzzleManager != null)
                puzzleManager.PlaceBoxInPuzzle(puzzleIndex);
        }
    }
}