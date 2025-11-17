using DefaultNamespace;
using UnityEngine;

public class VenomTrigger : MonoBehaviour
{
    public Material mat;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<VenomHandler>().SetPoisoned(true);
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) 
            other.gameObject.GetComponent<VenomHandler>().SetPoisoned(false);
    }
}
