using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static GraphicData;

public class CurrentSettings : MonoBehaviour
{
    public bool custom;
    public GraphicLevels currentPreset;

    public GraphicLevels currentTexture;
    public GraphicLevels currentMeshes;
    public GraphicLevels currentEffects;
    public GraphicLevels currentShadows;
    public AntialiasingMode currentAntialiasingMode;
    public MsaaQuality currentMSAA;
    public bool currentHDR;
    public float currentRenderScale;
    public int currentBrightness;
}
