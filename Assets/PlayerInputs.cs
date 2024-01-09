using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public BattleManager battleManager;

    public PlayerButtons PollPlayer1Buttons()
    {
        PlayerButtons polledButtons = new PlayerButtons();

        polledButtons.frameTime = battleManager.gameState.frameTime;

        if (Input.GetKeyDown("w"))
        {
            polledButtons.SetUp(true);
        }

        if (Input.GetKey("w"))
        {
            polledButtons.SetUp(true);
        }

        if (Input.GetKey("s"))
        {
            polledButtons.SetDown(true);
        }

        if (Input.GetKey("a"))
        {
            polledButtons.SetLeft(true);
        }

        if (Input.GetKey("d"))
        {
            polledButtons.SetRight(true);
        }

        if (Input.GetKey("o"))
        {
            polledButtons.SetLPunch(true);
        }

        SOCD(polledButtons);

        return polledButtons;
    }

    public PlayerButtons PollPlayer2Buttons()
    {
        PlayerButtons polledButtons = new PlayerButtons();

        polledButtons.frameTime = battleManager.gameState.frameTime;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            polledButtons.SetUp(true);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            polledButtons.SetDown(true);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            polledButtons.SetLeft(true);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            polledButtons.SetRight(true);
        }

        SOCD(polledButtons);

        return polledButtons;
    }

    private void SOCD(PlayerButtons socdButtons)
    {
        if (socdButtons.GetUp() && socdButtons.GetDown())
        {
            socdButtons.SetUp(false);
            socdButtons.SetDown(false);
        }

        if (socdButtons.GetLeft() && socdButtons.GetRight())
        {
            socdButtons.SetLeft(false);
            socdButtons.SetRight(false);
        }
    }

}
