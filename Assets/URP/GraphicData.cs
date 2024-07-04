using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "GameData/Graphics")]
[System.Serializable]
public class GraphicData : ScriptableObject
{
    public enum GraphicLevels
    {
        VeryLow,
        Low,
        Medium,
        High,
        VeryHigh,
        Ultra
    }

    [System.Serializable]
    public class GraphicPresets
    {
        public string name;
        public GraphicLevels textureQuality;
        public GraphicLevels meshQuality;
        public GraphicLevels effectsQuality;
        public GraphicLevels shadowQuality;
        public AntialiasingMode antiAliasing;
        public MsaaQuality msaa;
        public bool hdr;
        public float renderScale;
    }

    [System.Serializable]
    public class TextureQuality
    {
        public string name;
        public int mipmapLimit;
        public AnisotropicFiltering anisotropicFiltering;
        public bool depthTextures;
        public bool opaqueTextures;
    }

    [System.Serializable]
    public class MeshQuality
    {
        public string name;
        public float lodBias;
        public int maxLod;
        public SkinWeights skinWeights;
    }

    [System.Serializable]
    public class EffectsQuality
    {
        public string name;
        public bool reflectionProbes;
        public int particleRaycastBudget;
        public ColorGradingMode colorGrading;
        public int colorGradingLutSize;
    }

    [System.Serializable]
    public class ShadowQuality
    {
        public string name;
        public RealtimeGICPUUsage realtimeGI;
        public ShadowmaskMode shadowmaskMode;
        public GraphicLevels shadowObject;
    }

    public List<FullScreenMode> screenModes;
    public List<int> fpsLimits;
    public List<GraphicPresets> presets;
    public List<TextureQuality> textures;
    public List<MeshQuality> meshes;
    public List<EffectsQuality> effects;
    public List<ShadowQuality> shadows;
}
