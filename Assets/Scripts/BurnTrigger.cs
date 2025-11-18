using DefaultNamespace;
using UnityEngine;

public class BurnTrigger : MonoBehaviour
{

    private void OnCollisionEnter(Collision other) {
        Debug.Log("Culo");
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<BurnHandler>().SetBurning(true);
    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log("Culo2");
        if (other.gameObject.CompareTag("Player")) 
            other.gameObject.GetComponent<BurnHandler>().SetBurning(false);
    }
}
