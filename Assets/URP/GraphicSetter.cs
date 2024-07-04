using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static GraphicData;

public class GraphicSetter : MonoBehaviour
{
    public GraphicData graphicData;
    public CurrentSettings currentSettings;

    public List<UniversalRenderPipelineAsset> urpAssets;
    private UniversalRenderPipelineAsset data;

    // Start is called before the first frame update
    void Start()
    {
        data = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCustom(bool shadow)
    {
        // Have to reapply other setting since shadow quality change overrides other settings
        if (shadow)
        {
            SwitchTextureQuality(currentSettings.currentTexture);
            SwitchMeshQuality(currentSettings.currentMeshes);
            SwitchEffectsQuality(currentSettings.currentEffects);
            SwitchAntiAliasing(currentSettings.currentAntialiasingMode);
            SwitchMSAA(currentSettings.currentMSAA);
            SwitchHDR(currentSettings.currentHDR);
            SwitchRenderScale(currentSettings.currentRenderScale);
        }

        currentSettings.custom = true;
    }

    public void SwitchPreset(GraphicLevels graphicLevel)
    {
        // Shadow should be changed first because every other setting writes over the shadow asset
        SwitchShadowQuality(graphicData.presets[((int)graphicLevel)].shadowQuality);

        SwitchTextureQuality(graphicData.presets[((int)graphicLevel)].textureQuality);
        SwitchMeshQuality(graphicData.presets[((int)graphicLevel)].meshQuality);
        SwitchEffectsQuality(graphicData.presets[((int)graphicLevel)].effectsQuality);
        SwitchAntiAliasing(graphicData.presets[((int)graphicLevel)].antiAliasing);
        SwitchMSAA(graphicData.presets[((int)graphicLevel)].msaa);
        SwitchHDR(graphicData.presets[((int)graphicLevel)].hdr);
        SwitchRenderScale(graphicData.presets[((int)graphicLevel)].renderScale);

        currentSettings.currentPreset = graphicLevel;

        currentSettings.custom = false;
    }

    public void SwitchTextureQuality(GraphicLevels textureLevel)
    {
        QualitySettings.globalTextureMipmapLimit = graphicData.textures[(int)textureLevel].mipmapLimit;
        QualitySettings.anisotropicFiltering = graphicData.textures[(int)textureLevel].anisotropicFiltering;
        data.supportsCameraDepthTexture = graphicData.textures[(int)textureLevel].depthTextures;
        data.supportsCameraOpaqueTexture = graphicData.textures[(int)textureLevel].opaqueTextures;

        currentSettings.currentTexture = textureLevel;
    }

    public void SwitchMeshQuality(GraphicLevels meshLevel)
    {
        QualitySettings.lodBias = graphicData.meshes[(int)meshLevel].lodBias;
        QualitySettings.maximumLODLevel = graphicData.meshes[(int)meshLevel].maxLod;
        QualitySettings.skinWeights = graphicData.meshes[(int)meshLevel].skinWeights;

        currentSettings.currentMeshes = meshLevel;
    }

    public void SwitchEffectsQuality(GraphicLevels effectsLevel)
    {
        QualitySettings.realtimeReflectionProbes = graphicData.effects[(int)effectsLevel].reflectionProbes;
        QualitySettings.particleRaycastBudget = graphicData.effects[(int)effectsLevel].particleRaycastBudget;
        data.colorGradingMode = graphicData.effects[(int)effectsLevel].colorGrading;
        data.colorGradingLutSize = graphicData.effects[(int)effectsLevel].colorGradingLutSize;

        currentSettings.currentEffects = effectsLevel;
    }

    public void SwitchShadowQuality(GraphicLevels shadowLevel)
    {
        // Have to first change the quality asset because other settings rely on it
        QualitySettings.SetQualityLevel((int)shadowLevel);
        data = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
        Debug.Log(data.name);

        QualitySettings.realtimeGICPUUsage = (int)graphicData.shadows[(int)shadowLevel].realtimeGI;
        QualitySettings.shadowmaskMode = graphicData.shadows[(int)shadowLevel].shadowmaskMode;

        currentSettings.currentShadows = shadowLevel;
    }

    public void SwitchAntiAliasing(AntialiasingMode antiAliasingType)
    {
        FindAnyObjectByType<CameraSetter>().ChangeAntiAliasing(antiAliasingType);

        currentSettings.currentAntialiasingMode = antiAliasingType;
    }

    public void SwitchMSAA(MsaaQuality msaa)
    {
        data.msaaSampleCount = (int)msaa;

        currentSettings.currentMSAA = msaa;
    }

    public void SwitchHDR(bool hdr)
    {
        data.supportsHDR = hdr;

        currentSettings.currentHDR = hdr;
    }

    public void SwitchRenderScale(float renderScale)
    {
        data.renderScale = renderScale;

        currentSettings.currentRenderScale = renderScale;
    }

    public void SwitchBrightness(int brightness)
    {
        FindAnyObjectByType<CameraSetter>().ChangeBrightness(brightness);

        currentSettings.currentBrightness = brightness;
    }
}
