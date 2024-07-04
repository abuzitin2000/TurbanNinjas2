using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System;

public class VideoSettingsManager : MonoBehaviour
{
    public MainMenuManager mainMenuManager;
    public GameObject videoMenu;
    
    private GraphicSetter graphicSetter;

    public TMPro.TextMeshProUGUI windowTypeText;
    public TMPro.TextMeshProUGUI resolutionText;
    public TMPro.TextMeshProUGUI resolutionDropdownText;
    public TMPro.TextMeshProUGUI vsyncText;
    public TMPro.TextMeshProUGUI fpsText;
    public TMPro.TextMeshProUGUI qualityText;
    public TMPro.TextMeshProUGUI textureText;
    public TMPro.TextMeshProUGUI meshText;
    public TMPro.TextMeshProUGUI effectsText;
    public TMPro.TextMeshProUGUI shadowText;
    public TMPro.TextMeshProUGUI aaText;
    public TMPro.TextMeshProUGUI msaaText;
    public TMPro.TextMeshProUGUI hdrText;
    public TMPro.TextMeshProUGUI renderText;
    public TMPro.TextMeshProUGUI brightnessText;
    public TMPro.TextMeshProUGUI subtitlesText;

    public GameObject resolutionDropdown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (graphicSetter == null)
        {
            graphicSetter = FindAnyObjectByType<GraphicSetter>();
        }

        // Window Type
        switch (Screen.fullScreenMode)
        {
            case FullScreenMode.Windowed:
                windowTypeText.text = "Windowed";
                break;
            case FullScreenMode.FullScreenWindow:
                windowTypeText.text = "Borderless";
                break;
            case FullScreenMode.ExclusiveFullScreen:
                windowTypeText.text = "Fullscreen";
                break;
        }

        // Resolution
        resolutionText.text = Screen.width + "x" + Screen.height;
        resolutionDropdownText.text = resolutionText.text;

        // Vsync
        vsyncText.text = QualitySettings.vSyncCount == 0 ? "Off" : "On";

        // Fps Limit
        if (Application.targetFrameRate == -1)
        {
            fpsText.text = "Off";
        }
        else
        {
            fpsText.text = Application.targetFrameRate.ToString();
        }

        // Quality
        if (graphicSetter.currentSettings.custom)
        {
            qualityText.text = "Custom";
        }
        else
        {
            switch (graphicSetter.currentSettings.currentPreset)
            {
                case GraphicData.GraphicLevels.VeryLow:
                    qualityText.text = "Very Low";
                    break;
                case GraphicData.GraphicLevels.Low:
                    qualityText.text = "Low";
                    break;
                case GraphicData.GraphicLevels.Medium:
                    qualityText.text = "Medium";
                    break;
                case GraphicData.GraphicLevels.High:
                    qualityText.text = "High";
                    break;
                case GraphicData.GraphicLevels.VeryHigh:
                    qualityText.text = "Very High";
                    break;
                case GraphicData.GraphicLevels.Ultra:
                    qualityText.text = "Ultra";
                    break;
            }
        }

        textureText.text = graphicSetter.graphicData.textures[(int)graphicSetter.currentSettings.currentTexture].name;
        meshText.text = graphicSetter.graphicData.meshes[(int)graphicSetter.currentSettings.currentMeshes].name;
        effectsText.text = graphicSetter.graphicData.effects[(int)graphicSetter.currentSettings.currentEffects].name;
        shadowText.text = graphicSetter.graphicData.shadows[(int)graphicSetter.currentSettings.currentShadows].name;

        switch (graphicSetter.currentSettings.currentAntialiasingMode)
        {
            case AntialiasingMode.None:
                aaText.text = "OFF";
                break;
            case AntialiasingMode.FastApproximateAntialiasing:
                aaText.text = "FXAA";
                break;
            case AntialiasingMode.SubpixelMorphologicalAntiAliasing:
                aaText.text = "SMAA";
                break;
            case AntialiasingMode.TemporalAntiAliasing:
                aaText.text = "TAA";
                break;
            default:
                aaText.text = "OFF";
                break;
        }

        switch (graphicSetter.currentSettings.currentMSAA)
        {
            case MsaaQuality.Disabled:
                msaaText.text = "OFF";
                break;
            case MsaaQuality._2x:
                msaaText.text = "2X";
                break;
            case MsaaQuality._4x:
                msaaText.text = "4X";
                break;
            case MsaaQuality._8x:
                msaaText.text = "8X";
                break;
            default:
                msaaText.text = "OFF";
                break;
        }

        hdrText.text = graphicSetter.currentSettings.currentHDR ? "ON" : "OFF";
        renderText.text = (Mathf.Ceil(graphicSetter.currentSettings.currentRenderScale * 10f) * 10).ToString() + "%";

        brightnessText.text = (graphicSetter.currentSettings.currentBrightness / 2f / 10f + 5f).ToString();
    }

    public void MenuLeft(int index)
    {
        switch (index)
        {
            case 0:
                SwitchWindowType(false);
                break;
            case 2:
                SwitchVsync();
                break;
            case 3:
                SwitchFps(false);
                break;
            case 4:
                SwitchQualityPreset(false);
                break;
            case 5:
                SwitchTexture(false);
                break;
            case 6:
                SwitchMesh(false);
                break;
            case 7:
                SwitchEffect(false);
                break;
            case 8:
                SwitchShadow(false);
                break;
            case 9:
                SwitchAntiAliasing(false);
                break;
            case 10:
                SwitchMSAA(false);
                break;
            case 11:
                SwitchHDR(false);
                break;
            case 12:
                SwitchRenderScale(false);
                break;
            case 13:
                SwitchBrightness(false);
                break;
        }
    }

    public void MenuRight(int index)
    {
        switch (index)
        {
            case 0:
                SwitchWindowType(true);
                break;
            case 2:
                SwitchVsync();
                break;
            case 3:
                SwitchFps(true);
                break;
            case 4:
                SwitchQualityPreset(true);
                break;
            case 5:
                SwitchTexture(true);
                break;
            case 6:
                SwitchMesh(true);
                break;
            case 7:
                SwitchEffect(true);
                break;
            case 8:
                SwitchShadow(true);
                break;
            case 9:
                SwitchAntiAliasing(true);
                break;
            case 10:
                SwitchMSAA(true);
                break;
            case 11:
                SwitchHDR(true);
                break;
            case 12:
                SwitchRenderScale(true);
                break;
            case 13:
                SwitchBrightness(true);
                break;
        }
    }

    public void OpenDropdown()
    {
        foreach (UnityEngine.UI.Button button in videoMenu.GetComponentsInChildren<UnityEngine.UI.Button>(false))
        {
            button.enabled = false;
        }

        switch (mainMenuManager.selectedItem)
        {
            case 1:
                resolutionDropdown.SetActive(true);
                break;
        }

        mainMenuManager.ChangeMenuItems(6, 0);
    }

    public void CloseDropdown()
    {
        resolutionDropdown.SetActive(false);

        foreach (UnityEngine.UI.Button button in videoMenu.GetComponentsInChildren<UnityEngine.UI.Button>(false))
        {
            button.enabled = true;
        }

        mainMenuManager.ChangeMenuItems(6, 1);
    }

    public void SaveSettings()
    {
        switch (Screen.fullScreenMode)
        {
            case FullScreenMode.ExclusiveFullScreen:
                PlayerPrefs.SetInt("SettingsWindowType", 0);
                break;
            case FullScreenMode.FullScreenWindow:
                PlayerPrefs.SetInt("SettingsWindowType", 1);
                break;
            case FullScreenMode.Windowed:
                PlayerPrefs.SetInt("SettingsWindowType", 2);
                break;
        }
        
        PlayerPrefs.SetInt("SettingsResolutionWidth", Screen.width);
        PlayerPrefs.SetInt("SettingsResolutionHeight", Screen.height);
        PlayerPrefs.SetInt("SettingsVsync", QualitySettings.vSyncCount == 0 ? 0 : 1);
        PlayerPrefs.Save();
    }

    private void SwitchWindowType(bool right)
    {
        int currentIndex = graphicSetter.graphicData.screenModes.IndexOf(Screen.fullScreenMode);
        currentIndex += right ? 1 : -1;

        if (currentIndex > graphicSetter.graphicData.screenModes.Count - 1)
        {
            currentIndex = 0;
        }
        if (currentIndex < 0)
        {
            currentIndex = graphicSetter.graphicData.screenModes.Count - 1;
        }

        Screen.SetResolution(Screen.width, Screen.height, graphicSetter.graphicData.screenModes[currentIndex]);
    }

    public void SwitchResolution()
    {
        int width = int.Parse(mainMenuManager.buttons[mainMenuManager.selectedItem].GetComponent<TMPro.TextMeshProUGUI>().text.Split("x")[0]);
        int heigth = int.Parse(mainMenuManager.buttons[mainMenuManager.selectedItem].GetComponent<TMPro.TextMeshProUGUI>().text.Split("x")[1]);
        Screen.SetResolution(width, heigth, Screen.fullScreenMode);

        CloseDropdown();
    }

    private void SwitchVsync()
    {
        if (QualitySettings.vSyncCount == 0)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    private void SwitchFps(bool right)
    {
        int currentIndex = graphicSetter.graphicData.fpsLimits.IndexOf(Application.targetFrameRate);
        currentIndex += right ? 1 : -1;

        if (currentIndex > graphicSetter.graphicData.fpsLimits.Count - 1)
        {
            currentIndex = 0;
        }
        if (currentIndex < 0)
        {
            currentIndex = graphicSetter.graphicData.fpsLimits.Count - 1;
        }

        Application.targetFrameRate = graphicSetter.graphicData.fpsLimits[currentIndex];
    }

    private void SwitchQualityPreset(bool right)
    {
        int currentIndex = (int)graphicSetter.currentSettings.currentPreset;
        currentIndex += right ? 1 : -1;
        currentIndex = currentIndex > graphicSetter.graphicData.presets.Count - 1 ? 0 : currentIndex;
        currentIndex = currentIndex < 0 ? graphicSetter.graphicData.presets.Count - 1 : currentIndex;

        graphicSetter.SwitchPreset((GraphicData.GraphicLevels)currentIndex);
    }

    private void SwitchTexture(bool right)
    {
        int currentIndex = (int)graphicSetter.currentSettings.currentTexture;
        currentIndex += right ? 1 : -1;
        currentIndex = currentIndex > graphicSetter.graphicData.textures.Count - 1 ? 0 : currentIndex;
        currentIndex = currentIndex < 0 ? graphicSetter.graphicData.textures.Count - 1 : currentIndex;

        graphicSetter.SwitchTextureQuality((GraphicData.GraphicLevels)currentIndex);
        graphicSetter.SetCustom(false);
    }

    private void SwitchMesh(bool right)
    {
        int currentIndex = (int)graphicSetter.currentSettings.currentMeshes;
        currentIndex += right ? 1 : -1;
        currentIndex = currentIndex > graphicSetter.graphicData.meshes.Count - 1 ? 0 : currentIndex;
        currentIndex = currentIndex < 0 ? graphicSetter.graphicData.meshes.Count - 1 : currentIndex;

        graphicSetter.SwitchMeshQuality((GraphicData.GraphicLevels)currentIndex);
        graphicSetter.SetCustom(false);
    }

    private void SwitchEffect(bool right)
    {
        int currentIndex = (int)graphicSetter.currentSettings.currentEffects;
        currentIndex += right ? 1 : -1;
        currentIndex = currentIndex > graphicSetter.graphicData.effects.Count - 1 ? 0 : currentIndex;
        currentIndex = currentIndex < 0 ? graphicSetter.graphicData.effects.Count - 1 : currentIndex;

        graphicSetter.SwitchEffectsQuality((GraphicData.GraphicLevels)currentIndex);
        graphicSetter.SetCustom(false);
    }

    private void SwitchShadow(bool right)
    {
        int currentIndex = (int)graphicSetter.currentSettings.currentShadows;
        currentIndex += right ? 1 : -1;
        currentIndex = currentIndex > graphicSetter.graphicData.shadows.Count - 1 ? 0 : currentIndex;
        currentIndex = currentIndex < 0 ? graphicSetter.graphicData.shadows.Count - 1 : currentIndex;

        graphicSetter.SwitchShadowQuality((GraphicData.GraphicLevels)currentIndex);
        graphicSetter.SetCustom(true);
    }

    private void SwitchAntiAliasing(bool right)
    {
        int currentIndex = (int)graphicSetter.currentSettings.currentAntialiasingMode;
        currentIndex += right ? 1 : -1;

        if (currentIndex > Enum.GetNames(typeof(AntialiasingMode)).Length - 1)
        {
            currentIndex = 0;
        }
        if (currentIndex < 0)
        {
            currentIndex = Enum.GetNames(typeof(AntialiasingMode)).Length - 1;
        }

        graphicSetter.SwitchAntiAliasing((AntialiasingMode)currentIndex);
        graphicSetter.SetCustom(false);
    }

    private void SwitchMSAA(bool right)
    {
        List<MsaaQuality> qualities = new List<MsaaQuality> { MsaaQuality.Disabled, MsaaQuality._2x, MsaaQuality._4x, MsaaQuality._8x };

        int currentIndex = qualities.IndexOf(graphicSetter.currentSettings.currentMSAA);
        currentIndex += right ? 1 : -1;

        if (currentIndex > qualities.Count - 1)
        {
            currentIndex = 0;
        }
        if (currentIndex < 0)
        {
            currentIndex = qualities.Count - 1;
        }

        graphicSetter.SwitchMSAA(qualities[currentIndex]);
        graphicSetter.SetCustom(false);
    }

    private void SwitchHDR(bool right)
    {
        graphicSetter.SwitchHDR(!graphicSetter.currentSettings.currentHDR);
        graphicSetter.SetCustom(false);
    }

    private void SwitchRenderScale(bool right)
    {
        float currentValue = graphicSetter.currentSettings.currentRenderScale;
        currentValue += right ? 0.1f : -0.1f;

        if (currentValue > 2f)
        {
            currentValue = 0.1f;
        }
        if (currentValue < 0.1f)
        {
            currentValue = 2f;
        }

        graphicSetter.SwitchRenderScale(currentValue);
        graphicSetter.SetCustom(false);
    }

    private void SwitchBrightness(bool right)
    {
        int currentValue = graphicSetter.currentSettings.currentBrightness;
        currentValue += right ? 10 : -10;

        if (currentValue > 100)
        {
            currentValue = -100;
        }
        if (currentValue < -100)
        {
            currentValue = 100;
        }

        graphicSetter.SwitchBrightness(currentValue);
    }
}
