using System.Collections.Generic;
using UnityEngine;

public class ScannerEffect : MonoBehaviour
{
    public float maxScanRadius = 10f;
    public float scanDuration = 2f;
    private List<Crate> hits = new();
    private bool isScanning;
    private float timer = 0;
    private float currentRadius = 0;
    private int lastIndexAnalyzed = -1;

    private void Start() {
        isScanning = true;
        
        foreach (var collider in Physics.OverlapSphere(transform.position, maxScanRadius)) {
            if (collider.TryGetComponent(out Crate scannable)) {
                hits.Add(scannable);
            }
        }
        hits.Sort((a, b) => {
            return (int)((transform.position - a.transform.position).sqrMagnitude -
                         (transform.position - b.transform.position).sqrMagnitude);
        });
    }

    void Update()
    {
        if (isScanning) {
            timer += Time.deltaTime;
            currentRadius = Mathf.Lerp(0f, maxScanRadius, timer / scanDuration);
            
            while (true) {
                if (lastIndexAnalyzed >= hits.Count - 1) {
                    Destroy(gameObject);
                    break;
                }

                if ((transform.position - hits[lastIndexAnalyzed + 1].transform.position).magnitude <= currentRadius) {
                    lastIndexAnalyzed++;
                    hits[lastIndexAnalyzed].OnScanned();
                }
                else {
                    break;
                }
            }
        }
    }
    
    void OnDrawGizmos()
    {
        if (isScanning)
        {
            Gizmos.color = new Color(0f, 1f, 1f, 0.3f);
            Gizmos.DrawWireSphere(transform.position, currentRadius);
        }
    }
}
