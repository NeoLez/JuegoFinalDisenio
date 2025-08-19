using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public float updateInterval = 0.5f;

    public GameObject fpsUIGroup;         
    public Image checkmarkImage;          
    private bool isEnabled = false;

    private float timeSinceLastUpdate = 0f;
    private int frameCount = 0;

    void Start()
    {
        SetState(false); 
    }

    void Update()
    {
        if (!isEnabled) return;

        frameCount++;
        timeSinceLastUpdate += Time.unscaledDeltaTime;

        if (timeSinceLastUpdate >= updateInterval)
        {
            float fps = frameCount / timeSinceLastUpdate;
            fpsText.text = $"FPS: {fps:F1}";

            if (fps < 30)
                fpsText.color = Color.red;
            else if (fps < 60)
                fpsText.color = Color.yellow;
            else
                fpsText.color = Color.green;

            frameCount = 0;
            timeSinceLastUpdate = 0f;
        }
    }

    public void ToggleFPS()
    {
        SetState(!isEnabled);
    }

    private void SetState(bool state)
    {
        isEnabled = state;

        if (fpsUIGroup != null)
            fpsUIGroup.SetActive(state);

        if (checkmarkImage != null)
            checkmarkImage.enabled = state;
    }
}