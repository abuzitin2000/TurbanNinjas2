using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorChanger : MonoBehaviour
{
    public SkinnedMeshRenderer meshRenderer;
    public ColorData colorData;

    public int currentCharacterIndex;

    private int colorID;

    // Start is called before the first frame update
    void Start()
    {
        colorID = Shader.PropertyToID("_NinjaColor");
    }

    // Update is called once per frame
    void Update()
    {
        Color currentColor;

        switch (currentCharacterIndex)
        {
            case NinjaColors.Pink:
                currentColor = colorData.pink;
                break;
            case NinjaColors.Red:
                currentColor = colorData.red;
                break;
            case NinjaColors.Orange:
                currentColor = colorData.orange;
                break;
            case NinjaColors.Yellow:
                currentColor = colorData.yellow;
                break;
            case NinjaColors.Green:
                currentColor = colorData.green;
                break;
            case NinjaColors.Turquoise:
                currentColor = colorData.turquoise;
                break;
            case NinjaColors.Blue:
                currentColor = colorData.blue;
                break;
            case NinjaColors.Purple:
                currentColor = colorData.purple;
                break;
            case NinjaColors.White:
                currentColor = colorData.white;
                break;
            case NinjaColors.Gray:
                currentColor = colorData.gray;
                break;
            case NinjaColors.Black:
                currentColor = colorData.black;
                break;
            default:
                currentColor = Color.white;
                break;
        }

        if (currentCharacterIndex != -1)
            ChangeColor(currentColor);
    }

    public void ChangeColor(Color color)
    {
        meshRenderer.sharedMaterials[0].SetColor(colorID, color);
    }
}
