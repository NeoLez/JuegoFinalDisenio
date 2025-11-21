using UnityEngine;

public class SensorToxicoSimple : MonoBehaviour
{
    public Material materialToxico;
    public string nombrePropiedad = "_MasterIntensity";

    private void OnTriggerEnter(Collider other)
    {
        // Mensaje de prueba 1: ¿Detecta CUALQUIER colisión?
        Debug.Log("¡Choqué con algo! Es un objeto llamado: " + other.name + " y tiene el Tag: " + other.tag);

        if (other.CompareTag("ToxicGround"))
        {
            // Mensaje de prueba 2: ¿Detecta que es el tag correcto?
            Debug.Log("--> ¡Es el PISO TÓXICO! Intentando prender el efecto...");

            if (materialToxico != null)
            {
                materialToxico.SetFloat(nombrePropiedad, 1f);
            }
            else
            {
                Debug.LogError("¡ERROR! No has asignado el Material Tóxico en el inspector.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ToxicGround"))
        {
            Debug.Log("--> Salí del piso tóxico. Apagando efecto.");
            if (materialToxico != null)
            {
                materialToxico.SetFloat(nombrePropiedad, 0f);
            }
        }
    }
}