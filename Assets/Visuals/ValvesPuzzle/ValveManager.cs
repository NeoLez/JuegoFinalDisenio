using UnityEngine;

public class ValveManager : MonoBehaviour
{
    public static ValveManager Instance;

    public ValveController valve1;
    public ValveController valve2;
    public ValveController valve3;

    public Renderer waterRenderer;
    private Material waterMaterial;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (waterRenderer != null)
            waterMaterial = waterRenderer.material;
    }

    public void CheckValves()
    {
        if (valve1 == null || valve2 == null || valve3 == null || waterMaterial == null)
            return;

        Color c1 = valve1.CurrentColor; //azul
        Color c2 = valve2.CurrentColor; //rojo
        Color c3 = valve3.CurrentColor; //verde

        if (CompareColors(c1, Color.blue) && CompareColors(c2, Color.red) && CompareColors(c3, Color.green))
        {
          
            Color lightBlue = new Color(1f, 0.8f, 0.6f);

            waterMaterial.SetColor("_Water_Colour", lightBlue); 
          
        }
    }

    private bool CompareColors(Color a, Color b)
    {
        return Vector3.Distance(new Vector3(a.r, a.g, a.b), new Vector3(b.r, b.g, b.b)) < 0.1f;
    }
}

