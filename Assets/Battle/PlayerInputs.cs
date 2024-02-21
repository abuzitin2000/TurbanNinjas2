using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public BattleManager battleManager;

    public PlayerButtons rawPlayer1Buttons = new PlayerButtons();
    public PlayerButtons rawPlayer2Buttons = new PlayerButtons();

    public List<PlayerButtons> delayedPlayer1Queue = new List<PlayerButtons>();
    public List<PlayerButtons> delayedPlayer2Queue = new List<PlayerButtons>();

    private PlayerButtons polledPlayer1Buttons = new PlayerButtons();
    private PlayerButtons polledPlayer2Buttons = new PlayerButtons();

    private bool player1Reset;
    private bool player2Reset;

    public PlayerButtons GetPlayer1ProcessedButtons()
    {
        PlayerButtons tempButtons = new PlayerButtons();

        // Search for same frameTime
        for (int i = 0; i < delayedPlayer1Queue.Count; i++)
        {
            // If frameTime in the queue is the same as the given frame
            if (delayedPlayer1Queue[i].frameTime == battleManager.gameState.frameTime)
            {
                tempButtons = delayedPlayer1Queue[i].CreateCopy();
                delayedPlayer1Queue.RemoveAt(i);
            }
        }

        return tempButtons;
    }

    public PlayerButtons GetPlayer2ProcessedButtons()
    {
        PlayerButtons tempButtons = new PlayerButtons();

        // Search for same frameTime
        for (int i = 0; i < delayedPlayer2Queue.Count; i++)
        {
            // If frameTime in the queue is the same as the given frame
            if (delayedPlayer2Queue[i].frameTime == battleManager.gameState.frameTime)
            {
                tempButtons = delayedPlayer2Queue[i].CreateCopy();
                delayedPlayer2Queue.RemoveAt(i);
            }
        }

        return tempButtons;
    }

    public PlayerButtons DelayPlayer1PolledButtons()
    {
        PlayerButtons delayedButtons = GetPolledPlayer1Buttons();

        delayedButtons.frameTime += battleManager.inputData.inputDelay;
        delayedPlayer1Queue.Add(delayedButtons);

        // Remove oldest if above delay time
        if (delayedPlayer1Queue.Count > battleManager.inputData.inputDelay + 1)
        {
            delayedPlayer1Queue.RemoveAt(0);
        }

        return delayedButtons;
    }

    public PlayerButtons DelayPlayer2PolledButtons()
    {
        PlayerButtons delayedButtons = GetPolledPlayer2Buttons();

        delayedButtons.frameTime += battleManager.inputData.inputDelay;
        delayedPlayer2Queue.Add(delayedButtons);

        // Remove oldest if above delay time
        if (delayedPlayer2Queue.Count > battleManager.inputData.inputDelay + 1)
        {
            delayedPlayer2Queue.RemoveAt(0);
        }

        return delayedButtons;
    }

    private PlayerButtons GetPolledPlayer1Buttons()
	{
        PlayerButtons newButtons = polledPlayer1Buttons.CreateCopy();

        newButtons.frameTime = battleManager.gameState.frameTime;

        SOCD(newButtons);

        // Reset First Time Presses in case Fixedupdate runs more than once per Update
        polledPlayer1Buttons.ResetPresses();

        player1Reset = true;

        return newButtons;
    }

    private PlayerButtons GetPolledPlayer2Buttons()
    {
        PlayerButtons newButtons = polledPlayer2Buttons.CreateCopy();

        newButtons.frameTime = battleManager.gameState.frameTime;

        SOCD(newButtons);

        // Reset First Time Presses in case Fixedupdate runs more than once per Update
        polledPlayer2Buttons.ResetPresses();

        player2Reset = true;

        return newButtons;
    }

    public void SaveButtons()
    {
        battleManager.player1InputHistory[battleManager.gameState.frameTime] = battleManager.player1Buttons.CreateCopy();
        battleManager.player2InputHistory[battleManager.gameState.frameTime] = battleManager.player2Buttons.CreateCopy();
    }

    public void PollPlayer1Buttons()
    {
        if (player1Reset)
		{
            polledPlayer1Buttons.buttons = 0;
        }
        
        player1Reset = false;

        if (rawPlayer1Buttons.GetUp(false))
        {
            polledPlayer1Buttons.SetUpPress(true);
        }

        if (rawPlayer1Buttons.GetUp(true))
        {
            polledPlayer1Buttons.SetUpHold(true);
        }

        if (rawPlayer1Buttons.GetDown(false))
        {
            polledPlayer1Buttons.SetDownPress(true);
        }

        if (rawPlayer1Buttons.GetDown(true))
        {
            polledPlayer1Buttons.SetDownHold(true);
        }

        if (rawPlayer1Buttons.GetLeft(false))
        {
            polledPlayer1Buttons.SetLeftPress(true);
        }

        if (rawPlayer1Buttons.GetLeft(true))
        {
            polledPlayer1Buttons.SetLeftHold(true);
        }

        if (rawPlayer1Buttons.GetRight(false))
        {
            polledPlayer1Buttons.SetRightPress(true);
        }

        if (rawPlayer1Buttons.GetRight(true))
        {
            polledPlayer1Buttons.SetRightHold(true);
        }

        if (rawPlayer1Buttons.GetLP(false))
        {
            polledPlayer1Buttons.SetLPPress(true);
        }

        if (rawPlayer1Buttons.GetLP(true))
        {
            polledPlayer1Buttons.SetLPHold(true);
        }

        if (rawPlayer1Buttons.GetHP(false))
        {
            polledPlayer1Buttons.SetHPPress(true);
        }

        if (rawPlayer1Buttons.GetHP(true))
        {
            polledPlayer1Buttons.SetHPHold(true);
        }

        if (rawPlayer1Buttons.GetLK(false))
        {
            polledPlayer1Buttons.SetLKPress(true);
        }

        if (rawPlayer1Buttons.GetLK(true))
        {
            polledPlayer1Buttons.SetLKHold(true);
        }

        if (rawPlayer1Buttons.GetHK(false))
        {
            polledPlayer1Buttons.SetHKPress(true);
        }

        if (rawPlayer1Buttons.GetHK(true))
        {
            polledPlayer1Buttons.SetHKHold(true);
        }

        rawPlayer1Buttons.ResetPresses();
    }

    public void PollPlayer2Buttons()
    {
        if (player2Reset)
        {
            polledPlayer2Buttons.buttons = 0;
        }

        player2Reset = false;

        if (rawPlayer2Buttons.GetUp(false))
        {
            polledPlayer2Buttons.SetUpPress(true);
        }

        if (rawPlayer2Buttons.GetUp(true))
        {
            polledPlayer2Buttons.SetUpHold(true);
        }

        if (rawPlayer2Buttons.GetDown(false))
        {
            polledPlayer2Buttons.SetDownPress(true);
        }

        if (rawPlayer2Buttons.GetDown(true))
        {
            polledPlayer2Buttons.SetDownHold(true);
        }

        if (rawPlayer2Buttons.GetLeft(false))
        {
            polledPlayer2Buttons.SetLeftPress(true);
        }

        if (rawPlayer2Buttons.GetLeft(true))
        {
            polledPlayer2Buttons.SetLeftHold(true);
        }

        if (rawPlayer2Buttons.GetRight(false))
        {
            polledPlayer2Buttons.SetRightPress(true);
        }

        if (rawPlayer2Buttons.GetRight(true))
        {
            polledPlayer2Buttons.SetRightHold(true);
        }

        if (rawPlayer2Buttons.GetLP(false))
        {
            polledPlayer2Buttons.SetLPPress(true);
        }

        if (rawPlayer2Buttons.GetLP(true))
        {
            polledPlayer2Buttons.SetLPHold(true);
        }

        if (rawPlayer2Buttons.GetHP(false))
        {
            polledPlayer2Buttons.SetHPPress(true);
        }

        if (rawPlayer2Buttons.GetHP(true))
        {
            polledPlayer2Buttons.SetHPHold(true);
        }

        if (rawPlayer2Buttons.GetLK(false))
        {
            polledPlayer2Buttons.SetLKPress(true);
        }

        if (rawPlayer2Buttons.GetLK(true))
        {
            polledPlayer2Buttons.SetLKHold(true);
        }

        if (rawPlayer2Buttons.GetHK(false))
        {
            polledPlayer2Buttons.SetHKPress(true);
        }

        if (rawPlayer2Buttons.GetHK(true))
        {
            polledPlayer2Buttons.SetHKHold(true);
        }

        rawPlayer2Buttons.ResetPresses();
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

    // Raw Input Functions
    // ----------------------------------------------------
    // ----------------------------------------------------
    // ----------------------------------------------------

    public void InputLeft(bool player1, bool active)
    {
        if (player1)
        {
            rawPlayer1Buttons.SetLeftPress(active);
            rawPlayer1Buttons.SetLeftHold(active);
        }
        else
        {
            rawPlayer2Buttons.SetLeftPress(active);
            rawPlayer2Buttons.SetLeftHold(active);
        }
    }

    public void InputRight(bool player1, bool active)
    {
        if (player1)
        {
            rawPlayer1Buttons.SetRightPress(active);
            rawPlayer1Buttons.SetRightHold(active);
        }
        else
        {
            rawPlayer2Buttons.SetRightPress(active);
            rawPlayer2Buttons.SetRightHold(active);
        }
    }

    public void InputUp(bool player1, bool active)
    {
        if (player1)
        {
            rawPlayer1Buttons.SetUpPress(active);
            rawPlayer1Buttons.SetUpHold(active);
        }
        else
        {
            rawPlayer2Buttons.SetUpPress(active);
            rawPlayer2Buttons.SetUpHold(active);
        }
    }

    public void InputDown(bool player1, bool active)
    {
        if (player1)
        {
            rawPlayer1Buttons.SetDownPress(active);
            rawPlayer1Buttons.SetDownHold(active);
        }
        else
        {
            rawPlayer2Buttons.SetDownPress(active);
            rawPlayer2Buttons.SetDownHold(active);
        }
    }

    public void InputLP(bool player1, bool active)
    {
        if (player1)
        {
            rawPlayer1Buttons.SetLPPress(active);
            rawPlayer1Buttons.SetLPHold(active);
        }
        else
        {
            rawPlayer2Buttons.SetLPPress(active);
            rawPlayer2Buttons.SetLPHold(active);
        }
    }

    public void InputHP(bool player1, bool active)
    {
        if (player1)
        {
            rawPlayer1Buttons.SetHPPress(active);
            rawPlayer1Buttons.SetHPHold(active);
        }
        else
        {
            rawPlayer2Buttons.SetHPPress(active);
            rawPlayer2Buttons.SetHPHold(active);
        }
    }

    public void InputLK(bool player1, bool active)
    {
        if (player1)
        {
            rawPlayer1Buttons.SetLKPress(active);
            rawPlayer1Buttons.SetLKHold(active);
        }
        else
        {
            rawPlayer2Buttons.SetLKPress(active);
            rawPlayer2Buttons.SetLKHold(active);
        }
    }

    public void InputHK(bool player1, bool active)
    {
        if (player1)
        {
            rawPlayer1Buttons.SetHKPress(active);
            rawPlayer1Buttons.SetHKHold(active);
        }
        else
        {
            rawPlayer2Buttons.SetHKPress(active);
            rawPlayer2Buttons.SetHKHold(active);
        }
    }
}
