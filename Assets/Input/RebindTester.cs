using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebindTester : MonoBehaviour
{
    public RebindsMenu rebindsMenu;

    public List<TMPro.TextMeshProUGUI> player1Buttons;
    public List<TMPro.TextMeshProUGUI> player2Buttons;

    public void Back(bool active)
    {
        if (!active)
        {
            return;
        }

        rebindsMenu.TestButtons();
    }

    public void Up(bool player1, bool active)
    {
        ChangeColor(player1, active, 0);
    }

    public void Down(bool player1, bool active)
    {
        ChangeColor(player1, active, 1);
    }

    public void Left(bool player1, bool active)
    {
        ChangeColor(player1, active, 2);
    }

    public void Right(bool player1, bool active)
    {
        ChangeColor(player1, active, 3);
    }

    public void LP(bool player1, bool active)
    {
        ChangeColor(player1, active, 4);
    }

    public void LK(bool player1, bool active)
    {
        ChangeColor(player1, active, 5);
    }

    public void HP(bool player1, bool active)
    {
        ChangeColor(player1, active, 6);
    }

    public void HK(bool player1, bool active)
    {
        ChangeColor(player1, active, 7);
    }

    private void ChangeColor(bool player1, bool active, int index)
    {
        if (player1)
        {
            if (active)
            {
                player1Buttons[index].color = Color.red;
            }
            else
            {
                player1Buttons[index].color = Color.white;
            }
        }
        else
        {
            if (active)
            {
                player2Buttons[index].color = Color.red;
            }
            else
            {
                player2Buttons[index].color = Color.white;
            }
        }
    }
}
