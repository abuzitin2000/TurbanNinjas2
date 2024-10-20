using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public BattleManager battleManager;

    public void CalculateCharactersMovement()
    {
        TurnCharacters(battleManager.gameState.player1, battleManager.gameState.player2);
        CalculateCharacterMovement(battleManager.player1Buttons, battleManager.gameState.player1, battleManager.player1Data, battleManager.player1InputHistory);
        CalculateCharacterMovement(battleManager.player2Buttons, battleManager.gameState.player2, battleManager.player2Data, battleManager.player2InputHistory);
    }

    private void CalculateCharacterMovement(PlayerButtons playerButtons, BattleGameState.CharacterState characterState, CharacterData characterData, Dictionary<int, PlayerButtons> inputHistory)
    {
        CharacterCrouch(playerButtons, characterState, characterData);
        CharacterDash(playerButtons, characterState, characterData, inputHistory);
        CharacterWalk(playerButtons, characterState, characterData);
        CharacterJump(playerButtons, characterState, characterData);
    }

    public void MoveCharacters()
    {
        MoveCharacter(battleManager.gameState.player1, battleManager.player1Data);
        MoveCharacter(battleManager.gameState.player2, battleManager.player2Data);
    }

    private void MoveCharacter(BattleGameState.CharacterState characterState, CharacterData characterData)
    {
        CharacterVerticalMovement(characterState, characterData);
        CharacterHorizontalMovement(characterState, characterData);
    }

    private void TurnCharacters(BattleGameState.CharacterState player1State, BattleGameState.CharacterState player2State)
    {
        if (player1State.positionX > player2State.positionX)
        {
            if (!player1State.attacking && player1State.stun == 0)
			{
                player1State.mirrored = true;
			}

            if (!player2State.attacking && player2State.stun == 0)
            {
                player2State.mirrored = false;
            }
        }

        if (player1State.positionX < player2State.positionX)
        {
            if (!player1State.attacking && player1State.stun == 0)
            {
                player1State.mirrored = false;
            }

            if (!player2State.attacking && player2State.stun == 0)
            {
                player2State.mirrored = true;
            }
        }
    }

    private void CharacterVerticalMovement(BattleGameState.CharacterState characterState, CharacterData characterData)
    {
        // Player Velocity
        characterState.positionY += characterState.velocityY;

        // Gravity
        if (characterState.positionY > battleManager.battleData.groundLevel)
        {
            characterState.velocityY -= characterData.stats.fallSpeed;
        }

        // If goes over limit
        if (characterState.positionY < battleManager.battleData.groundLevel)
        {
            characterState.positionY = battleManager.battleData.groundLevel;
        }

        // Set Grounded
        if (characterState.positionY == battleManager.battleData.groundLevel)
        {
            characterState.grounded = true;
            characterState.jumping = 0;
            characterState.velocityY = 0;
        }
		else
		{
            characterState.grounded = false;
        }
    }

    private void CharacterHorizontalMovement(BattleGameState.CharacterState characterState, CharacterData characterData)
    {
        // Player Velocity
        characterState.positionX += characterState.velocityX;

        // Deceleration on pushback
        if (characterState.stun > 0)
        {
            characterState.velocityX -= 0;
        }

        // If goes over limit
        if (characterState.positionX < battleManager.battleData.stageSize * -1)
        {
            characterState.positionX = battleManager.battleData.stageSize * -1;
        }
        if (characterState.positionX > battleManager.battleData.stageSize)
        {
            characterState.positionX = battleManager.battleData.stageSize;
        }

        // Handle character collisions
        battleManager.collisionManager.HandleCollisionBoxes();
    }

    private void CharacterWalk(PlayerButtons playerButtons, BattleGameState.CharacterState characterState, CharacterData characterData)
    {
        // Walk
        if (characterState.stun == 0 && !characterState.attacking && characterState.jumping == 0)
        {
            // Right
            if (playerButtons.GetRight(true))
            {
                if (!characterState.mirrored)
                {
                    if (!characterState.crouching)
					{
                        characterState.velocityX = characterData.stats.forwardMoveSpeed;
                        battleManager.characterAnimator.SetAnimation(characterState, characterData, "WalkForward");
					}

                    characterState.blocking = false;
                }
                else
                {
                    if (!characterState.crouching)
                    {
                        characterState.velocityX = characterData.stats.backwardMoveSpeed;
                        battleManager.characterAnimator.SetAnimation(characterState, characterData, "WalkBackward");
                    }

                    characterState.blocking = true;
                }
            }

            // Left
            else if (playerButtons.GetLeft(true))
            {
                if (!characterState.mirrored)
                {
                    if (!characterState.crouching)
                    {
                        characterState.velocityX = characterData.stats.backwardMoveSpeed * -1;
                        battleManager.characterAnimator.SetAnimation(characterState, characterData, "WalkBackward");
                    }

                    characterState.blocking = true;
                }
                else
                {
                    if (!characterState.crouching)
                    {
                        characterState.velocityX = characterData.stats.forwardMoveSpeed * -1;
                        battleManager.characterAnimator.SetAnimation(characterState, characterData, "WalkForward");
                    }

                    characterState.blocking = false;
                }
            }

            // Idle
            else
			{
                if (!characterState.crouching)
                {
                    characterState.velocityX = 0;
                    battleManager.characterAnimator.SetAnimation(characterState, characterData, "Idle");
                }

                characterState.blocking = false;
            }

            // Can't move while crouching
            if (characterState.crouching)
			{
                characterState.velocityX = 0;
            }
        }
    }

    private void CharacterDash(PlayerButtons playerButtons, BattleGameState.CharacterState characterState, CharacterData characterData, Dictionary<int, PlayerButtons> inputHistory)
	{
        // Dash
        if (characterState.stun == 0 && !characterState.attacking && characterState.jumping == 0 && !characterState.crouching)
        {
            // Right
            if (playerButtons.GetRight(false))
            {
                bool dashInput = false;

                // Input History
                for (int i = 1; i < battleManager.inputData.dashWindow; i++)
                {
                    int inputFrame = battleManager.gameState.frameTime - i;
                    PlayerButtons input = inputHistory[inputFrame];

                    // If input doesn't exist
                    if (input == null)
                    {
                        break;
                    }

                    if (input.GetRight(false))
					{
                        dashInput = true;
                        break;
					}

                    if (input.GetLeft(false) || input.GetDown(false))
                    {
                        break;
                    }
                }

                if (dashInput)
				{
                    if (!characterState.mirrored)
                    {
                        characterState.velocityX = characterData.stats.forwardDashSpeed;
                        battleManager.characterAnimator.SetAnimation(characterState, characterData, "DashForward");
                        characterState.attacking = true;
                    }
                    else
                    {
                        characterState.velocityX = characterData.stats.backwardDashSpeed;
                        battleManager.characterAnimator.SetAnimation(characterState, characterData, "DashBackward");
                        characterState.attacking = true;
                    }
                }
            }

            // Left
            if (playerButtons.GetLeft(false))
            {
                bool dashInput = false;

                // Input History
                for (int i = 1; i < battleManager.inputData.dashWindow; i++)
                {
                    int inputFrame = battleManager.gameState.frameTime - i;
                    PlayerButtons input = inputHistory[inputFrame];

                    // If input doesn't exist
                    if (input == null)
                    {
                        break;
                    }

                    if (input.GetLeft(false))
                    {
                        dashInput = true;
                        break;
                    }

                    if (input.GetRight(false) || input.GetDown(false))
                    {
                        break;
                    }
                }

                if (dashInput)
                {
                    if (!characterState.mirrored)
                    {
                        characterState.velocityX = characterData.stats.backwardDashSpeed * -1;
                        battleManager.characterAnimator.SetAnimation(characterState, characterData, "DashBackward");
                        characterState.attacking = true;
                    }
                    else
                    {
                        characterState.velocityX = characterData.stats.forwardDashSpeed * -1;
                        battleManager.characterAnimator.SetAnimation(characterState, characterData, "DashForward");
                        characterState.attacking = true;
                    }
                }
            }
        }
    }

    private void CharacterJump(PlayerButtons playerButtons, BattleGameState.CharacterState characterState, CharacterData characterData)
    {
        // Jump Start
        if (characterState.stun == 0 && !characterState.attacking && characterState.jumping == 0)
        {
            if (playerButtons.GetUp(true))
            {
                characterState.jumping = 2;
                characterState.jumpWindow = battleManager.inputData.jumpingWindow;
                characterState.velocityY = characterData.stats.jumpForce;
                battleManager.characterAnimator.SetAnimation(characterState, characterData, "JumpNeutral");
            }
        }

        // Jump Window
        if (characterState.stun == 0 && characterState.jumping != 0 && characterState.jumpWindow > 0)
        {
            if (playerButtons.GetLeft(true))
            {
                if (!characterState.mirrored)
                {
                    characterState.jumping = 1;
                    characterState.velocityX = characterData.stats.jumpHorizontalSpeed * -1;
                    battleManager.characterAnimator.SetAnimation(characterState, characterData, "JumpBackward");
                }
                else
                {
                    characterState.jumping = 3;
                    characterState.velocityX = characterData.stats.jumpHorizontalSpeed * -1;
                    battleManager.characterAnimator.SetAnimation(characterState, characterData, "JumpForward");
                }
            }

            if (playerButtons.GetRight(true))
            {
                if (!characterState.mirrored)
                {
                    characterState.jumping = 3;
                    characterState.velocityX = characterData.stats.jumpHorizontalSpeed;
                    battleManager.characterAnimator.SetAnimation(characterState, characterData, "JumpForward");
                }
                else
                {
                    characterState.jumping = 1;
                    characterState.velocityX = characterData.stats.jumpHorizontalSpeed;
                    battleManager.characterAnimator.SetAnimation(characterState, characterData, "JumpBackward");
                }
            }
        }

        // Reduce window
        if (characterState.jumpWindow > 0)
        {
            characterState.jumpWindow--;
        }
    }

    private void CharacterCrouch(PlayerButtons playerButtons, BattleGameState.CharacterState characterState, CharacterData characterData)
    {
        // Player 1 Crouch
        if (characterState.stun == 0 && !characterState.attacking && characterState.jumping == 0)
        {
            if (playerButtons.GetDown(true))
            {
                characterState.crouching = true;
                battleManager.characterAnimator.SetAnimation(characterState, characterData, "Crouch");
            }
            else
            {
                characterState.crouching = false;
            }
        }
    }
}
