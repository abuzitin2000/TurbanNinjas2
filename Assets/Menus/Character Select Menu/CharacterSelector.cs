using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public GameObject characterSelectorMenu;
    public GameObject deviceSelectorMenu;
    public GameObject characterSelector;
    public GameObject deviceSelector;

    public int player1Selected;
    public int player2Selected;

    public RectTransform player1Selector;
    public RectTransform player2Selector;

    public int player1Phase;
    public int player2Phase;

    public ColorChanger player1Color;
    public ColorChanger player2Color;

    public Light player1Light;
    public Light player2Light;

    public float hueSpeed;
    private float hue;
    
    public List<RectTransform> upperPortraits;
    public List<RectTransform> lowerPortraits;

    public List<TMPro.TextMeshProUGUI> player1Info;
    public List<TMPro.TextMeshProUGUI> player2Info;

    public List<Image> player1InfoIcons;
    public List<Image> player2InfoIcons;

    public List<Sprite> inputIconList;

    [System.Serializable]
    public enum inputIcon
    {
        None,
        Down,
        Up,
        Left,
        Right,
        DownRight,
        DownLeft,
        UpRight,
        UpLeft,
        Punch,
        Kick,
        Plus
    }

    [System.Serializable]
    public class charInfo
    {
        public string name;
        public string type;
        public string move1Description;
        public List<inputIcon> move1InputIcons;
        public string move2Description;
        public List<inputIcon> move2InputIcons;
        public string move3Description;
        public List<inputIcon> move3InputIcons;
        public string jutsu;
    }

    public List<charInfo> charInfos;

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
            player1Info[0].text = charInfos[player1Selected].name;
            player1Info[1].text = "Type: " + charInfos[player1Selected].type;
            player1Info[2].text = charInfos[player1Selected].move1Description;
            player1InfoIcons[0].sprite = inputIconList[(int)charInfos[player1Selected].move1InputIcons[0]];
            player1InfoIcons[1].sprite = inputIconList[(int)charInfos[player1Selected].move1InputIcons[1]];
            player1InfoIcons[2].sprite = inputIconList[(int)charInfos[player1Selected].move1InputIcons[2]];
            player1InfoIcons[3].sprite = inputIconList[(int)charInfos[player1Selected].move1InputIcons[3]];
            player1InfoIcons[4].sprite = inputIconList[(int)charInfos[player1Selected].move1InputIcons[4]];
            player1Info[3].text = charInfos[player1Selected].move2Description;
            player1InfoIcons[5].sprite = inputIconList[(int)charInfos[player1Selected].move2InputIcons[0]];
            player1InfoIcons[6].sprite = inputIconList[(int)charInfos[player1Selected].move2InputIcons[1]];
            player1InfoIcons[7].sprite = inputIconList[(int)charInfos[player1Selected].move2InputIcons[2]];
            player1InfoIcons[8].sprite = inputIconList[(int)charInfos[player1Selected].move2InputIcons[3]];
            player1InfoIcons[9].sprite = inputIconList[(int)charInfos[player1Selected].move2InputIcons[4]];
            player1Info[4].text = charInfos[player1Selected].move3Description;
            player1InfoIcons[10].sprite = inputIconList[(int)charInfos[player1Selected].move3InputIcons[0]];
            player1InfoIcons[11].sprite = inputIconList[(int)charInfos[player1Selected].move3InputIcons[1]];
            player1InfoIcons[12].sprite = inputIconList[(int)charInfos[player1Selected].move3InputIcons[2]];
            player1InfoIcons[13].sprite = inputIconList[(int)charInfos[player1Selected].move3InputIcons[3]];
            player1InfoIcons[14].sprite = inputIconList[(int)charInfos[player1Selected].move3InputIcons[4]];
            player1Info[5].text = charInfos[player1Selected].jutsu;
        }
        else
        {
            player1Info[0].text = charInfos[player1Selected - 100 + lowerPortraits.Count].name;
            player1Info[1].text = "Type: " + charInfos[player1Selected - 100 + lowerPortraits.Count].type;
            player1Info[2].text = charInfos[player1Selected - 100 + lowerPortraits.Count].move1Description;
            player1InfoIcons[0].sprite = inputIconList[(int)charInfos[player1Selected - 100 + lowerPortraits.Count].move1InputIcons[0]];
            player1InfoIcons[1].sprite = inputIconList[(int)charInfos[player1Selected - 100 + lowerPortraits.Count].move1InputIcons[1]];
            player1InfoIcons[2].sprite = inputIconList[(int)charInfos[player1Selected - 100 + lowerPortraits.Count].move1InputIcons[2]];
            player1InfoIcons[3].sprite = inputIconList[(int)charInfos[player1Selected - 100 + lowerPortraits.Count].move1InputIcons[3]];
            player1InfoIcons[4].sprite = inputIconList[(int)charInfos[player1Selected - 100 + lowerPortraits.Count].move1InputIcons[4]];
            player1Info[3].text = charInfos[player1Selected - 100 + lowerPortraits.Count].move2Description;
            player1InfoIcons[5].sprite = inputIconList[(int)charInfos[player1Selected - 100 + lowerPortraits.Count].move2InputIcons[0]];
            player1InfoIcons[6].sprite = inputIconList[(int)charInfos[player1Selected - 100 + lowerPortraits.Count].move2InputIcons[1]];
            player1InfoIcons[7].sprite = inputIconList[(int)charInfos[player1Selected - 100 + lowerPortraits.Count].move2InputIcons[2]];
            player1InfoIcons[8].sprite = inputIconList[(int)charInfos[player1Selected - 100 + lowerPortraits.Count].move1InputIcons[3]];
            player1InfoIcons[9].sprite = inputIconList[(int)charInfos[player1Selected - 100 + lowerPortraits.Count].move1InputIcons[4]];
            player1Info[4].text = charInfos[player1Selected - 100 + lowerPortraits.Count].move3Description;
            player1InfoIcons[10].sprite = inputIconList[(int)charInfos[player1Selected - 100 + lowerPortraits.Count].move3InputIcons[0]];
            player1InfoIcons[11].sprite = inputIconList[(int)charInfos[player1Selected - 100 + lowerPortraits.Count].move3InputIcons[1]];
            player1InfoIcons[12].sprite = inputIconList[(int)charInfos[player1Selected - 100 + lowerPortraits.Count].move3InputIcons[2]];
            player1InfoIcons[13].sprite = inputIconList[(int)charInfos[player1Selected - 100 + lowerPortraits.Count].move1InputIcons[3]];
            player1InfoIcons[14].sprite = inputIconList[(int)charInfos[player1Selected - 100 + lowerPortraits.Count].move1InputIcons[4]];
            player1Info[5].text = charInfos[player1Selected - 100 + lowerPortraits.Count].jutsu;
        }

        if (player2Selected < 100)
        {
            player2Info[0].text = charInfos[player2Selected].name;
            player2Info[1].text = "Type: " + charInfos[player2Selected].type;
            player2Info[2].text = charInfos[player2Selected].move1Description;
            player2Info[3].text = charInfos[player2Selected].move2Description;
            player2Info[4].text = charInfos[player2Selected].move3Description;
            player2Info[5].text = charInfos[player2Selected].jutsu;
        }
        else
        {
            player2Info[0].text = charInfos[player2Selected - 100 + lowerPortraits.Count].name;
            player2Info[1].text = "Type: " + charInfos[player2Selected - 100 + lowerPortraits.Count].type;
            player2Info[2].text = charInfos[player2Selected - 100 + lowerPortraits.Count].move1Description;
            player2Info[3].text = charInfos[player2Selected - 100 + lowerPortraits.Count].move2Description;
            player2Info[4].text = charInfos[player2Selected - 100 + lowerPortraits.Count].move3Description;
            player2Info[5].text = charInfos[player2Selected - 100 + lowerPortraits.Count].jutsu;
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
            if (player1Phase == 0)
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
        }
        else
        {
            if (player2Phase == 0)
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
    }

    public void Right(bool player1, bool active, bool gamepad)
    {
        if (!active)
        {
            return;
        }

        if (player1)
        {
            if (player1Phase == 0)
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
        }
        else
        {
            if (player2Phase == 0)
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
    }

    public void Up(bool player1, bool active, bool gamepad)
    {
        if (!active)
        {
            return;
        }

        if (player1)
        {
            if (player1Phase == 0)
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
        }
        else
        {
            if (player2Phase == 0)
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

    public void Down(bool player1, bool active, bool gamepad)
    {
        if (!active)
        {
            return;
        }

        if (player1)
        {
            if (player1Phase == 0)
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
        }
        else
        {
            if (player2Phase == 0)
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

    public void Select(bool player1, bool active, bool gamepad)
    {
        if (!active)
        {
            return;
        }

        if (player1)
        {
            if (player1Phase == 0)
            {
                player1Phase += 1;

                SceneManager.LoadScene("StageSakura");
            }
        }
        else
        {
            if (player2Phase == 0)
            {
                player2Phase += 1;
            }
        }
    }

    public void Back(bool player1, bool active, bool gamepad)
    {
        if (!active)
        {
            return;
        }

        if ((player1 && player1Phase == 0) || (!player1 && player2Phase == 0))
        {
            // Reset Device Pairings
            GameObject.FindWithTag("InputManager").GetComponent<PlayerInputPairing>().ChangePairings(true, false, true, false);

            deviceSelector.GetComponent<DeviceSelection>().gamepad1Player = 0;
            deviceSelector.GetComponent<DeviceSelection>().gamepad2Player = 0;
            deviceSelector.GetComponent<DeviceSelection>().keyboard1Player = 0;
            deviceSelector.GetComponent<DeviceSelection>().keyboard2Player = 0;

            player1Phase = 0;
            player2Phase = 0;

            deviceSelectorMenu.SetActive(true);
            deviceSelector.SetActive(true);
            characterSelectorMenu.SetActive(false);
            characterSelector.SetActive(false);
        }

        if (player1)
        {
            if (player1Phase == 1)
            {
                player1Phase = 0;
            }
        }
        else
        {
            if (player2Phase == 1)
            {
                player2Phase = 0;
            }
        }
    }
}
