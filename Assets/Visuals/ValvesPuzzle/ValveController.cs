using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveController : MonoBehaviour
{
    [SerializeField] float angle = 0.2f;
    [SerializeField] AudioClip soundClip;
    private AudioSource audioSource;


    private bool playerInside = false;
    private bool isRotating = false;

    private Renderer rend;
    private Material material;

    private Color currentColor;

    private Color[] colorOptions = new Color[]
    {
        Color.green,
        Color.blue,
        Color.yellow,
        new Color(1f, 0.4f, 0.7f), // Rosa
        new Color(1f, 0f, 0f),     // Rojo
    };

    public Color CurrentColor => currentColor; 

    private void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            material = rend.material;
        }

        audioSource = gameObject.AddComponent<AudioSource>(); 
        audioSource.clip = soundClip;
    }

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E) && !isRotating)
        {
            StartCoroutine(RotateForSeconds(1.5f));

            audioSource.Play(); 
            StartCoroutine(RotateForSeconds(1.5f));
        }
    }

    private IEnumerator RotateForSeconds(float duration)
    {
        isRotating = true;
        float timer = 0f;

        while (timer < duration)
        {
            Rotar();
            timer += Time.deltaTime;
            yield return null;
        }

        CambiarColor();
        isRotating = false;
    }

    void Rotar()
    {
        transform.Rotate(0, 0, angle);
    }

    void CambiarColor()
    {
        if (material != null)
        {
            currentColor = colorOptions[Random.Range(0, colorOptions.Length)];

            material.color = currentColor;
            material.SetColor("_BaseColor", currentColor);
            material.SetColor("TopColor", currentColor);
            material.SetColor("BottomColor", currentColor);

           
            ValveManager.Instance?.CheckValves();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
