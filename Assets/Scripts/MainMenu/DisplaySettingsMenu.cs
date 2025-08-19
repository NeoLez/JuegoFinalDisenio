using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DisplaySettingsMenu : MonoBehaviour
{
    //TP2 Enzo Francisco Melidoni
    public Dropdown resolutionDropdown;
    public Dropdown screenModeDropdown;

    private List<Resolution> customResolutions = new List<Resolution>
    {
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 1360, height = 768 },
        new Resolution { width = 1280, height = 768},
        new Resolution { width = 1280, height = 720},
        new Resolution { width = 1176, height = 664},
        new Resolution { width = 1024, height = 768},
        new Resolution { width = 800, height = 600}
    };

    void Start()
    {
        List<string> resOptions = new List<string>();
        int currentResIndex = 0;

        for (int i = 0; i < customResolutions.Count; i++)
        {
            Resolution res = customResolutions[i];
            string option = $"{res.width} x {res.height}";

            if (res.width == Screen.width && res.height == Screen.height)
                option += " (Recomendada)";

            resOptions.Add(option);

            if (res.width == Screen.width && res.height == Screen.height)
                currentResIndex = i;
        }

        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResIndex);
        int savedScreenModeIndex = PlayerPrefs.GetInt("ScreenModeIndex", 0);
        
        if (savedResolutionIndex < 0 || savedResolutionIndex >= customResolutions.Count)
            savedResolutionIndex = currentResIndex;

        if (savedScreenModeIndex < 0 || savedScreenModeIndex > 2)
            savedScreenModeIndex = 0;

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resOptions);
        resolutionDropdown.value = savedResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        List<string> screenModes = new List<string>
        {
            "Pantalla completa",
            "Ventana"
        };
        screenModeDropdown.ClearOptions();
        screenModeDropdown.AddOptions(screenModes);
        screenModeDropdown.value = savedScreenModeIndex;
        screenModeDropdown.RefreshShownValue();

        ApplySettings(savedResolutionIndex, savedScreenModeIndex);

        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
        screenModeDropdown.onValueChanged.AddListener(OnScreenModeChanged);
    }

    void OnResolutionChanged(int index)
    {
        PlayerPrefs.SetInt("ResolutionIndex", index);
        PlayerPrefs.Save();
        ApplySettings(index, screenModeDropdown.value);
        Debug.Log($"Resolución cambiada a: {customResolutions[index].width}x{customResolutions[index].height}");
    }

    void OnScreenModeChanged(int index)
    {
        PlayerPrefs.SetInt("ScreenModeIndex", index);
        PlayerPrefs.Save();
        ApplySettings(resolutionDropdown.value, index);
        Debug.Log("Modo de pantalla cambiado a: " + GetSpanishScreenModeName(index));
    }

    void ApplySettings(int resolutionIndex, int modeIndex)
    {
        if (resolutionIndex < 0 || resolutionIndex >= customResolutions.Count) return;

        Resolution res = customResolutions[resolutionIndex];
        FullScreenMode mode;

        switch (modeIndex)
        {
            case 0: mode = FullScreenMode.FullScreenWindow; break;
            case 1: mode = FullScreenMode.Windowed; break;
            default: mode = FullScreenMode.FullScreenWindow; break;
        }
        
        Screen.SetResolution(res.width, res.height, mode, 0);
    }

    string GetSpanishScreenModeName(int index)
    {
        switch (index)
        {
            case 0: return "Pantalla completa";
            case 1: return "Ventana";
            default: return "Por defecto";
        }
    }
}
