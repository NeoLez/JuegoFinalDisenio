using UnityEngine;
using System.Collections.Generic;

public class MovingObstacle : MonoBehaviour
{
    public Vector3 puntoA;
    public Vector3 puntoB;
    private Vector3 startPosition;
    public float speed = 2f;

    private float originalSpeed;
    private float t = 0f;
    private bool goingTowardB = true;
    private Vector3 lastPosition;

    // Lista de rigidbodies encima de la plataforma
    private List<Rigidbody> objetosEncima = new List<Rigidbody>();

    void Start() {
        startPosition = transform.position;
        originalSpeed = speed;
        lastPosition = transform.position;
    }

    void Update()
    {
        // Mover la plataforma
        t += Time.deltaTime * speed * (goingTowardB ? 1 : -1);
        transform.position = Vector3.Lerp(startPosition + puntoA, startPosition + puntoB, t);

        if (t >= 1f)
        {
            t = 1f;
            goingTowardB = false;
        }
        else if (t <= 0f)
        {
            t = 0f;
            goingTowardB = true;
        }

        // Mover todos los objetos que están encima
        Vector3 delta = transform.position - lastPosition;
        foreach (Rigidbody rb in objetosEncima)
        {
            if (rb != null)
            {
                rb.MovePosition(rb.position + delta);
            }
        }

        lastPosition = transform.position;
    }

    public void Freeze(float factor, float duracion)
    {
        speed *= factor;
        Invoke(nameof(RestoreSpeed), duracion);
    }

    void RestoreSpeed()
    {
        speed = originalSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null && !objetosEncima.Contains(rb))
        {
            objetosEncima.Add(rb);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null && objetosEncima.Contains(rb))
        {
            objetosEncima.Remove(rb);
        }
    }
}
