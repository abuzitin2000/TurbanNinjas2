using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraSetter : MonoBehaviour
{
    public CurrentSettings settings;

    private float checkTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkTimer > 0f)
        {
            checkTimer -= Time.deltaTime;
            return;
        }

        ChangeAntiAliasing(settings.currentAntialiasingMode);
        ChangeBrightness(settings.currentBrightness);

        checkTimer = 3f;
    }

    public void ChangeAntiAliasing(AntialiasingMode newAntialiasingMode)
    {
        foreach (Camera camera in Camera.allCameras)
        {
            UniversalAdditionalCameraData cameraData = camera.GetUniversalAdditionalCameraData();

            if (cameraData.antialiasing != newAntialiasingMode)
            {
                cameraData.antialiasing = newAntialiasingMode;
                cameraData.antialiasingQuality = AntialiasingQuality.High;
                Debug.Log("Changed AA");
            }
        }

        settings.currentAntialiasingMode = newAntialiasingMode;
    }

    public void ChangeBrightness(int newBrightness)
    {
        LiftGammaGain gammaObject = FindObjectOfType<LiftGammaGain>();

        if (gammaObject != null && gammaObject.gamma != new Vector4(0f, 0f, 0f, newBrightness))
        {
            gammaObject.gamma = new UnityEngine.Rendering.Vector4Parameter(new Vector4(0f, 0f, 0f, newBrightness / 100f));
            Debug.Log("Changed Gamma");
        }

        settings.currentBrightness = newBrightness;
    }
}
