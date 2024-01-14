using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public BattleManager battleManager;

    private PlayerButtons polledPlayer1Buttons = new PlayerButtons();
    private PlayerButtons polledPlayer2Buttons = new PlayerButtons();

    private bool player1Reset;
    private bool player2Reset;

    public PlayerButtons GetPlayer1Buttons()
	{
        PlayerButtons newButtons = polledPlayer1Buttons.CreateCopy();

        newButtons.frameTime = battleManager.gameState.frameTime;

        SOCD(newButtons);

        // Reset First Time Presses in case Fixedupdate runs more than once per Update
        polledPlayer1Buttons.SetUpPress(false);
        polledPlayer1Buttons.SetDownPress(false);
        polledPlayer1Buttons.SetLeftPress(false);
        polledPlayer1Buttons.SetRightPress(false);
        polledPlayer1Buttons.SetLPPress(false);
        polledPlayer1Buttons.SetHPPress(false);
        polledPlayer1Buttons.SetLKPress(false);
        polledPlayer1Buttons.SetHKPress(false);

        player1Reset = true;

        return newButtons;
    }

    public PlayerButtons GetPlayer2Buttons()
    {
        PlayerButtons newButtons = polledPlayer2Buttons.CreateCopy();

        newButtons.frameTime = battleManager.gameState.frameTime;

        SOCD(newButtons);

        // Reset First Time Presses in case Fixedupdate runs more than once per Update
        polledPlayer2Buttons.SetUpPress(false);
        polledPlayer2Buttons.SetDownPress(false);
        polledPlayer2Buttons.SetLeftPress(false);
        polledPlayer2Buttons.SetRightPress(false);
        polledPlayer2Buttons.SetLPPress(false);
        polledPlayer2Buttons.SetHPPress(false);
        polledPlayer2Buttons.SetLKPress(false);
        polledPlayer2Buttons.SetHKPress(false);

        player2Reset = true;

        return newButtons;
    }

    public void PollPlayer1Buttons()
    {
        if (player1Reset)
		{
            polledPlayer1Buttons.buttons = 0;
        }
        
        player1Reset = false;

        if (Input.GetKeyDown("w"))
        {
            polledPlayer1Buttons.SetUpPress(true);
        }

        if (Input.GetKey("w"))
        {
            polledPlayer1Buttons.SetUpHold(true);
        }

        if (Input.GetKeyDown("s"))
        {
            polledPlayer1Buttons.SetDownPress(true);
        }

        if (Input.GetKey("s"))
        {
            polledPlayer1Buttons.SetDownHold(true);
        }

        if (Input.GetKeyDown("a"))
        {
            polledPlayer1Buttons.SetLeftPress(true);
        }

        if (Input.GetKey("a"))
        {
            polledPlayer1Buttons.SetLeftHold(true);
        }

        if (Input.GetKeyDown("d"))
        {
            polledPlayer1Buttons.SetRightPress(true);
        }

        if (Input.GetKey("d"))
        {
            polledPlayer1Buttons.SetRightHold(true);
        }

        if (Input.GetKeyDown("o"))
        {
            polledPlayer1Buttons.SetLPPress(true);
        }

        if (Input.GetKey("o"))
        {
            polledPlayer1Buttons.SetLPHold(true);
        }
    }

    public void PollPlayer2Buttons()
    {
        if (player2Reset)
        {
            polledPlayer2Buttons.buttons = 0;
        }

        player2Reset = false;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            polledPlayer2Buttons.SetUpPress(true);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            polledPlayer2Buttons.SetUpHold(true);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            polledPlayer2Buttons.SetDownPress(true);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            polledPlayer2Buttons.SetDownHold(true);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            polledPlayer2Buttons.SetLeftPress(true);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            polledPlayer2Buttons.SetLeftHold(true);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            polledPlayer2Buttons.SetRightPress(true);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            polledPlayer2Buttons.SetRightHold(true);
        }

        if (Input.GetKeyDown("b"))
        {
            polledPlayer2Buttons.SetLPPress(true);
        }

        if (Input.GetKey("b"))
        {
            polledPlayer2Buttons.SetLPHold(true);
        }
    }

    private void SOCD(PlayerButtons socdButtons)
    {
        if (socdButtons.GetUp(true) && socdButtons.GetDown(true))
        {
            socdButtons.SetUpPress(false);
            socdButtons.SetUpHold(false);
            socdButtons.SetDownPress(false);
            socdButtons.SetDownHold(false);
        }

        if (socdButtons.GetLeft(true) && socdButtons.GetRight(true))
        {
            socdButtons.SetLeftPress(false);
            socdButtons.SetLeftHold(false);
            socdButtons.SetRightPress(false);
            socdButtons.SetRightHold(false);

        }
    }

}
