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

        if (Input.GetKey("w"))
        {
            polledButtons.up = true;
        }

        if (Input.GetKey("s"))
        {
            polledButtons.down = true;
        }

        if (Input.GetKey("a"))
        {
            polledButtons.left = true;
        }

        if (Input.GetKey("d"))
        {
            polledButtons.right = true;
        }

        return polledButtons;
    }

    public PlayerButtons PollPlayer2Buttons()
    {
        PlayerButtons polledButtons = new PlayerButtons();

        polledButtons.frameTime = battleManager.gameState.frameTime;

        if (Input.GetKey("up"))
        {
            polledButtons.up = true;
        }

        if (Input.GetKey("down"))
        {
            polledButtons.down = true;
        }

        if (Input.GetKey("left"))
        {
            polledButtons.left = true;
        }

        if (Input.GetKey("right"))
        {
            polledButtons.right = true;
        }

        return polledButtons;
    }
}
