using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEfffect : MonoBehaviour
{
    //esto va en el componente de texto :p
    
    public float floatSpeed = 2f;
    public float floatHeight = 0.1f;

    private TextMeshPro text;
    private Vector3 startPos;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
        startPos = transform.position;
    }

    void Update()
    {
       
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        
    }
}
