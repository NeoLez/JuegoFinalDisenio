using DefaultNamespace;
using UnityEngine;

public class BurnTrigger : MonoBehaviour
{

    private void OnCollisionEnter(Collision other) {
        Debug.Log("aaaa me quemo");
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<BurnHandler>().SetBurning(true);
    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log("no me quemo B)");
        if (other.gameObject.CompareTag("Player")) 
            other.gameObject.GetComponent<BurnHandler>().SetBurning(false);
    }
}
