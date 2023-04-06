using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public BattleManager battleManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MoveCharacters()
    {
        CharacterJump();
        CharacterCrouch();
        CharacterGravity();
        CharacterWalk();
    }

    private void CharacterGravity()
    {
        // Player Velocity
        battleManager.gameState.player1PositionY += battleManager.gameState.player1VerticalVelocity;
        battleManager.gameState.player2PositionY += battleManager.gameState.player2VerticalVelocity;

        // Player 1 Gravity
        if (battleManager.gameState.player1PositionY > battleManager.battleData.battleData.groundLevel)
        {
            battleManager.gameState.player1VerticalVelocity -= battleManager.player1Data.characterData.fallSpeed;
        }

        // If goes over limit
        if (battleManager.gameState.player1PositionY < battleManager.battleData.battleData.groundLevel)
        {
            battleManager.gameState.player1PositionY = battleManager.battleData.battleData.groundLevel;
        }

        // Set Grounded
        if (battleManager.gameState.player1PositionY == battleManager.battleData.battleData.groundLevel)
        {
            battleManager.gameState.player1Grounded = true;
            battleManager.gameState.player1Jumping = 0;
            battleManager.gameState.player1VerticalVelocity = 0;
        }

        // Player 2 Gravity
        if (battleManager.gameState.player2PositionY > battleManager.battleData.battleData.groundLevel)
        {
            battleManager.gameState.player2VerticalVelocity -= battleManager.player2Data.characterData.fallSpeed;
        }

        // If goes over limit
        if (battleManager.gameState.player2PositionY < battleManager.battleData.battleData.groundLevel)
        {
            battleManager.gameState.player2PositionY = battleManager.battleData.battleData.groundLevel;
        }

        // Set Grounded
        if (battleManager.gameState.player2PositionY == battleManager.battleData.battleData.groundLevel)
        {
            battleManager.gameState.player2Grounded = true;
            battleManager.gameState.player2Jumping = 0;
            battleManager.gameState.player2VerticalVelocity = 0;
        }
    }

    private void CharacterWalk()
    {
        // Move Player 1
        if (battleManager.gameState.player1Stun == 0 && battleManager.gameState.player1Jumping == 0 && !battleManager.gameState.player1Crouching)
        {
            if (battleManager.player1Buttons.GetRight())
            {
                battleManager.gameState.player1PositionX += battleManager.player1Data.characterData.moveSpeed;
            }

            if (battleManager.player1Buttons.GetLeft())
            {
                battleManager.gameState.player1PositionX -= battleManager.player1Data.characterData.moveSpeed;
            }
        }

        // Move Player 2
        if (battleManager.gameState.player2Stun == 0 && battleManager.gameState.player2Jumping == 0 && !battleManager.gameState.player2Crouching)
        {
            if (battleManager.player2Buttons.GetRight())
            {
                battleManager.gameState.player2PositionX += battleManager.player2Data.characterData.moveSpeed;
            }

            if (battleManager.player2Buttons.GetLeft())
            {
                battleManager.gameState.player2PositionX -= battleManager.player2Data.characterData.moveSpeed;
            }
        }
    }

    private void CharacterJump()
    {
        // Player 1 Jump Start
        if (battleManager.gameState.player1Stun == 0 && battleManager.gameState.player1Jumping == 0)
        {
            if (battleManager.player1Buttons.GetUp())
            {
                battleManager.gameState.player1Jumping = 2;
                battleManager.gameState.player1JumpingWindow = battleManager.inputData.inputData.jumpingWindow;
                battleManager.gameState.player1VerticalVelocity = battleManager.player1Data.characterData.jumpForce;
            }
        }

        // Player 2 Jump Start
        if (battleManager.gameState.player2Stun == 0 && battleManager.gameState.player2Jumping == 0)
        {
            if (battleManager.player2Buttons.GetUp())
            {
                battleManager.gameState.player2Jumping = 2;
                battleManager.gameState.player2JumpingWindow = battleManager.inputData.inputData.jumpingWindow;
                battleManager.gameState.player2VerticalVelocity = battleManager.player2Data.characterData.jumpForce;
            }
        }

        // Player 1 Jump Window
        if (battleManager.gameState.player1Stun == 0 && battleManager.gameState.player1Jumping != 0 && battleManager.gameState.player1JumpingWindow > 0)
        {
            if (battleManager.player1Buttons.GetLeft())
            {
                if (!battleManager.gameState.player1Mirror)
                {
                    battleManager.gameState.player1Jumping = 1;
                }
                else
                {
                    battleManager.gameState.player1Jumping = 3;
                }
            }

            if (battleManager.player1Buttons.GetRight())
            {
                if (!battleManager.gameState.player1Mirror)
                {
                    battleManager.gameState.player1Jumping = 3;
                }
                else
                {
                    battleManager.gameState.player1Jumping = 1;
                }
            }
        }

        // Reduce window
        if (battleManager.gameState.player1JumpingWindow > 0)
        {
            battleManager.gameState.player1JumpingWindow--;
        }

        // Player 2 Jump Window
        if (battleManager.gameState.player2Stun == 0 && battleManager.gameState.player2Jumping != 0 && battleManager.gameState.player2JumpingWindow > 0)
        {
            if (battleManager.player2Buttons.GetLeft())
            {
                if (!battleManager.gameState.player2Mirror)
                {
                    battleManager.gameState.player2Jumping = 1;
                }
                else
                {
                    battleManager.gameState.player2Jumping = 3;
                }
            }

            if (battleManager.player2Buttons.GetRight())
            {
                if (!battleManager.gameState.player2Mirror)
                {
                    battleManager.gameState.player2Jumping = 3;
                }
                else
                {
                    battleManager.gameState.player2Jumping = 1;
                }
            }
        }

        // Reduce window
        if (battleManager.gameState.player2JumpingWindow > 0)
        {
            battleManager.gameState.player2JumpingWindow--;
        }

        // Player 1 Air Movement
        if (battleManager.gameState.player1Stun == 0 && battleManager.gameState.player1Jumping != 0)
        {
            if (battleManager.gameState.player1Jumping == 1)
            {
                if (!battleManager.gameState.player1Mirror)
                {
                    battleManager.gameState.player1PositionX -= battleManager.player1Data.characterData.jumpHorizontalSpeed;
                }
                else
                {
                    battleManager.gameState.player1PositionX += battleManager.player1Data.characterData.jumpHorizontalSpeed;
                }
            }

            if (battleManager.gameState.player1Jumping == 3)
            {
                if (!battleManager.gameState.player1Mirror)
                {
                    battleManager.gameState.player1PositionX += battleManager.player1Data.characterData.jumpHorizontalSpeed;
                }
                else
                {
                    battleManager.gameState.player1PositionX -= battleManager.player1Data.characterData.jumpHorizontalSpeed;
                }
            }
        }

        // Player 2 Air Movement
        if (battleManager.gameState.player2Stun == 0 && battleManager.gameState.player2Jumping != 0)
        {
            if (battleManager.gameState.player2Jumping == 1)
            {
                if (!battleManager.gameState.player2Mirror)
                {
                    battleManager.gameState.player2PositionX -= battleManager.player2Data.characterData.jumpHorizontalSpeed;
                }
                else
                {
                    battleManager.gameState.player2PositionX += battleManager.player2Data.characterData.jumpHorizontalSpeed;
                }
            }

            if (battleManager.gameState.player2Jumping == 3)
            {
                if (!battleManager.gameState.player2Mirror)
                {
                    battleManager.gameState.player2PositionX += battleManager.player2Data.characterData.jumpHorizontalSpeed;
                }
                else
                {
                    battleManager.gameState.player2PositionX -= battleManager.player2Data.characterData.jumpHorizontalSpeed;
                }
            }
        }
    }

    private void CharacterCrouch()
    {
        // Player 1 Crouch
        if (battleManager.gameState.player1Stun == 0 && battleManager.gameState.player1Jumping == 0)
        {
            if (battleManager.player1Buttons.GetDown())
            {
                battleManager.gameState.player1Crouching = true;
            }
            else
            {
                battleManager.gameState.player1Crouching = false;
            }
        }

        // Player 2 Crouch
        if (battleManager.gameState.player2Stun == 0 && battleManager.gameState.player2Jumping == 0)
        {
            if (battleManager.player2Buttons.GetDown())
            {
                battleManager.gameState.player2Crouching = true;
            }
            else
            {
                battleManager.gameState.player2Crouching = false;
            }
        }
    }
}
