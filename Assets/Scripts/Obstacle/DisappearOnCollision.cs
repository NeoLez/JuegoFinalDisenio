using System.Collections;
using UnityEngine;

public class DisappearOnCollision : MonoBehaviour
{
    private bool hasDisappeared = false;
    private Renderer rend;
    private Collider col;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasDisappeared && collision.gameObject.CompareTag("Player"))
        {
            if (Random.value < 0.25f)
            {
                hasDisappeared = true;
                StartCoroutine(DisappearAndReappear());
            }
        }
    }

    private IEnumerator DisappearAndReappear()
    {
        rend.enabled = false;
        col.enabled = false;
        yield return new WaitForSeconds(3f);
        rend.enabled = true;
        col.enabled = true;
        hasDisappeared = false;
    }
}

