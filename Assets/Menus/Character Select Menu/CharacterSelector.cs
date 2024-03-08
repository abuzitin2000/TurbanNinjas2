using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public int player1Selected;
    public int player2Selected;

    public List<RectTransform> upperPortraits;
    public List<RectTransform> lowerPortraits;

    public RectTransform player1Selector;
    public RectTransform player2Selector;

    public ColorChanger player1Color;
    public ColorChanger player2Color;

    public Light player1Light;
    public Light player2Light;

    public float hueSpeed;
    private float hue;

    public List<TMPro.TextMeshProUGUI> player1Info;
    public List<TMPro.TextMeshProUGUI> player2Info;

    public List<string> ninjaNames;
    public List<string> ninjaTypes;
    public List<string> ninjaMove1s;
    public List<string> ninjaMove2s;
    public List<string> ninjaMove3s;
    public List<string> ninjaJutsus;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Selector Positions
        if (player1Selected < 100)
        {
            player1Selector.position = lowerPortraits[player1Selected].position;
        }
        else
        {
            player1Selector.position = upperPortraits[player1Selected - 100].position;
        }

        if (player2Selected < 100)
        {
            player2Selector.position = lowerPortraits[player2Selected].position;
        }
        else
        {
            player2Selector.position = upperPortraits[player2Selected - 100].position;
        }

        // Model Colors and Light Colors
        switch (player1Selected)
        {
            case 0:
                player1Color.currentCharacterIndex = NinjaColors.Pink;
                player1Light.color = player1Color.colorData.pink;
                break;
            case 1:
                player1Color.currentCharacterIndex = NinjaColors.Red;
                player1Light.color = player1Color.colorData.red;
                break;
            case 2:
                player1Color.currentCharacterIndex = NinjaColors.Orange;
                player1Light.color = player1Color.colorData.orange;
                break;
            case 3:
                player1Color.currentCharacterIndex = NinjaColors.Yellow;
                player1Light.color = player1Color.colorData.yellow;
                break;
            case 4:
                player1Color.currentCharacterIndex = -1;
                player1Color.ChangeColor(Color.HSVToRGB(hue, 0.84f, 0.67f));
                player1Light.color = Color.HSVToRGB(hue, 0.84f, 0.67f);
                hue += Time.deltaTime * hueSpeed;
                if (hue > 1f)
                    hue = 0f;
                break;
            case 5:
                player1Color.currentCharacterIndex = NinjaColors.Green;
                player1Light.color = player1Color.colorData.green;
                break;
            case 6:
                player1Color.currentCharacterIndex = NinjaColors.Turquoise;
                player1Light.color = player1Color.colorData.turquoise;
                break;
            case 7:
                player1Color.currentCharacterIndex = NinjaColors.Blue;
                player1Light.color = player1Color.colorData.blue;
                break;
            case 8:
                player1Color.currentCharacterIndex = NinjaColors.Purple;
                player1Light.color = player1Color.colorData.purple;
                break;
            case 100:
                player1Color.currentCharacterIndex = NinjaColors.White;
                player1Light.color = player1Color.colorData.white;
                break;
            case 101:
                player1Color.currentCharacterIndex = NinjaColors.Gray;
                player1Light.color = player1Color.colorData.gray;
                break;
            case 102:
                player1Color.currentCharacterIndex = NinjaColors.Black;
                player1Light.color = player1Color.colorData.black;
                break;
            default:
                player1Color.currentCharacterIndex = -1;
                player1Light.color = Color.white;
                break;
        }

        switch (player2Selected)
        {
            case 0:
                player2Color.currentCharacterIndex = NinjaColors.Pink;
                player2Light.color = player2Color.colorData.pink;
                break;
            case 1:
                player2Color.currentCharacterIndex = NinjaColors.Red;
                player2Light.color = player2Color.colorData.red;
                break;
            case 2:
                player2Color.currentCharacterIndex = NinjaColors.Orange;
                player2Light.color = player2Color.colorData.orange;
                break;
            case 3:
                player2Color.currentCharacterIndex = NinjaColors.Yellow;
                player2Light.color = player2Color.colorData.yellow;
                break;
            case 4:
                player2Color.currentCharacterIndex = -1;
                player2Color.ChangeColor(Color.HSVToRGB(hue, 0.84f, 0.67f));
                player2Light.color = Color.HSVToRGB(hue, 0.84f, 0.67f);
                hue += Time.deltaTime * hueSpeed;
                if (hue > 1f)
                    hue = 0f;
                break;
            case 5:
                player2Color.currentCharacterIndex = NinjaColors.Green;
                player2Light.color = player2Color.colorData.green;
                break;
            case 6:
                player2Color.currentCharacterIndex = NinjaColors.Turquoise;
                player2Light.color = player2Color.colorData.turquoise;
                break;
            case 7:
                player2Color.currentCharacterIndex = NinjaColors.Blue;
                player2Light.color = player2Color.colorData.blue;
                break;
            case 8:
                player2Color.currentCharacterIndex = NinjaColors.Purple;
                player2Light.color = player2Color.colorData.purple;
                break;
            case 100:
                player2Color.currentCharacterIndex = NinjaColors.White;
                player2Light.color = player2Color.colorData.white;
                break;
            case 101:
                player2Color.currentCharacterIndex = NinjaColors.Gray;
                player2Light.color = player2Color.colorData.gray;
                break;
            case 102:
                player2Color.currentCharacterIndex = NinjaColors.Black;
                player2Light.color = player2Color.colorData.black;
                break;
            default:
                player2Color.currentCharacterIndex = -1;
                player2Light.color = Color.white;
                break;
        }

        // Texts
        if (player1Selected < 100)
        {
            player1Info[0].text = ninjaNames[player1Selected];
            player1Info[1].text = ninjaTypes[player1Selected];
            player1Info[2].text = ninjaMove1s[player1Selected];
            player1Info[3].text = ninjaMove2s[player1Selected];
            player1Info[4].text = ninjaMove3s[player1Selected];
            player1Info[5].text = ninjaJutsus[player1Selected];
        }
        else
        {
            player1Info[0].text = ninjaNames[player1Selected - 100 + lowerPortraits.Count];
            player1Info[1].text = ninjaTypes[player1Selected - 100 + lowerPortraits.Count];
            player1Info[2].text = ninjaMove1s[player1Selected - 100 + lowerPortraits.Count];
            player1Info[3].text = ninjaMove2s[player1Selected - 100 + lowerPortraits.Count];
            player1Info[4].text = ninjaMove3s[player1Selected - 100 + lowerPortraits.Count];
            player1Info[5].text = ninjaJutsus[player1Selected - 100 + lowerPortraits.Count];
        }

        if (player2Selected < 100)
        {
            player2Info[0].text = ninjaNames[player2Selected];
            player2Info[1].text = ninjaTypes[player2Selected];
            player2Info[2].text = ninjaMove1s[player2Selected];
            player2Info[3].text = ninjaMove2s[player2Selected];
            player2Info[4].text = ninjaMove3s[player2Selected];
            player2Info[5].text = ninjaJutsus[player2Selected];
        }
        else
        {
            player2Info[0].text = ninjaNames[player2Selected - 100 + lowerPortraits.Count];
            player2Info[1].text = ninjaTypes[player2Selected - 100 + lowerPortraits.Count];
            player2Info[2].text = ninjaMove1s[player2Selected - 100 + lowerPortraits.Count];
            player2Info[3].text = ninjaMove2s[player2Selected - 100 + lowerPortraits.Count];
            player2Info[4].text = ninjaMove3s[player2Selected - 100 + lowerPortraits.Count];
            player2Info[5].text = ninjaJutsus[player2Selected - 100 + lowerPortraits.Count];
        }
    }

    public void Left(bool player1, bool active, bool gamepad)
    {
        if (!active)
        {
            return;
        }

        if (player1)
        {
            player1Selected -= 1;

            if (player1Selected == -1)
            {
                player1Selected = lowerPortraits.Count - 1;
            }

            if (player1Selected == 99)
            {
                player1Selected = 100 + upperPortraits.Count - 1;
            }
        }
        else
        {
            player2Selected -= 1;

            if (player2Selected == -1)
            {
                player2Selected = lowerPortraits.Count - 1;
            }

            if (player2Selected == 99)
            {
                player2Selected = 100 + upperPortraits.Count - 1;
            }
        }
    }

    public void Right(bool player1, bool active, bool gamepad)
    {
        if (!active)
        {
            return;
        }

        if (player1)
        {
            player1Selected += 1;

            if (player1Selected == lowerPortraits.Count)
            {
                player1Selected = 0;
            }

            if (player1Selected == 100 + upperPortraits.Count)
            {
                player1Selected = 100;
            }
        }
        else
        {
            player2Selected += 1;

            if (player2Selected == lowerPortraits.Count)
            {
                player2Selected = 0;
            }

            if (player2Selected == 100 + upperPortraits.Count)
            {
                player2Selected = 100;
            }
        }
    }

    public void Up(bool player1, bool active, bool gamepad)
    {
        if (!active)
        {
            return;
        }

        if (player1)
        {
            if (player1Selected < 4)
            {
                player1Selected = 100;
            }
            else if (player1Selected == 4)
            {
                player1Selected = 101;
            }
            else if (player1Selected > 4 && player1Selected < 100)
            {
                player1Selected = 102;
            }
            else if (player1Selected == 100)
            {
                player1Selected = 3;
            }
            else if (player1Selected == 101)
            {
                player1Selected = 4;
            }
            else if (player1Selected == 102)
            {
                player1Selected = 5;
            }
        }
        else
        {
            if (player2Selected < 4)
            {
                player2Selected = 100;
            }
            else if (player2Selected == 4)
            {
                player2Selected = 101;
            }
            else if (player2Selected > 4 && player2Selected < 100)
            {
                player2Selected = 102;
            }
            else if (player2Selected == 100)
            {
                player2Selected = 3;
            }
            else if (player2Selected == 101)
            {
                player2Selected = 4;
            }
            else if (player2Selected == 102)
            {
                player2Selected = 5;
            }
        }
    }

    public void Down(bool player1, bool active, bool gamepad)
    {
        if (!active)
        {
            return;
        }

        if (player1)
        {
            if (player1Selected < 4)
            {
                player1Selected = 100;
            }
            else if (player1Selected == 4)
            {
                player1Selected = 101;
            }
            else if (player1Selected > 4 && player1Selected < 100)
            {
                player1Selected = 102;
            }
            else if (player1Selected == 100)
            {
                player1Selected = 3;
            }
            else if (player1Selected == 101)
            {
                player1Selected = 4;
            }
            else if (player1Selected == 102)
            {
                player1Selected = 5;
            }
        }
        else
        {
            if (player2Selected < 4)
            {
                player2Selected = 100;
            }
            else if (player2Selected == 4)
            {
                player2Selected = 101;
            }
            else if (player2Selected > 4 && player2Selected < 100)
            {
                player2Selected = 102;
            }
            else if (player2Selected == 100)
            {
                player2Selected = 3;
            }
            else if (player2Selected == 101)
            {
                player2Selected = 4;
            }
            else if (player2Selected == 102)
            {
                player2Selected = 5;
            }
        }
    }
}
