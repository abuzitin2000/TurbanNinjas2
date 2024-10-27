using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public BattleManager battleManager;

    public void CalculateCharactersMovement()
    {
        TurnCharacters();
        CalculateCharacterMovement(true, battleManager.player1Buttons, battleManager.player1InputHistory);
        CalculateCharacterMovement(false, battleManager.player2Buttons, battleManager.player2InputHistory);
    }

    private void CalculateCharacterMovement(bool isCharacter1, PlayerButtons playerButtons, Dictionary<int, PlayerButtons> inputHistory)
    {
        CharacterCrouch(isCharacter1, playerButtons);
        CharacterDash(isCharacter1, playerButtons, inputHistory);
        CharacterWalk(isCharacter1, playerButtons);
        CharacterJump(isCharacter1, playerButtons);
    }

    public void MoveCharacters()
    {
        MoveCharacter(true);
        MoveCharacter(false);
    }

    private void MoveCharacter(bool isCharacter1)
    {
        CharacterVerticalMovement(isCharacter1);
        CharacterHorizontalMovement(isCharacter1);
    }

    private void TurnCharacters()
    {
        // Character States
        BattleGameState.CharacterState character1State = battleManager.gameState.character1;
        BattleGameState.CharacterState character2State = battleManager.gameState.character2;

        // When Character 1 is on Right Side
        if (character1State.positionX > character2State.positionX)
        {
            if (!character1State.attacking && character1State.stun == 0)
			{
                character1State.mirrored = true;
			}

            if (!character2State.attacking && character2State.stun == 0)
            {
                character2State.mirrored = false;
            }
        }

        // When Character 2 is on Right Side
        if (character1State.positionX < character2State.positionX)
        {
            if (!character1State.attacking && character1State.stun == 0)
            {
                character1State.mirrored = false;
            }

            if (!character2State.attacking && character2State.stun == 0)
            {
                character2State.mirrored = true;
            }
        }
    }

    private void CharacterVerticalMovement(bool isCharacter1)
    {
        // Character Data
        BattleGameState.CharacterState characterState = isCharacter1 ? battleManager.gameState.character1 : battleManager.gameState.character2;
        StatsData characterStats = isCharacter1 ? battleManager.ninjaStats[battleManager.player1Ninja] : battleManager.ninjaStats[battleManager.player2Ninja];

        // Character Velocity
        characterState.positionY += characterState.velocityY;

        // Gravity
        if (characterState.positionY > battleManager.battleData.groundLevel)
        {
            characterState.velocityY -= characterStats.fallSpeed;
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

    private void CharacterHorizontalMovement(bool isCharacter1)
    {
        // Character Data
        BattleGameState.CharacterState characterState = isCharacter1 ? battleManager.gameState.character1 : battleManager.gameState.character2;

        // Character Velocity
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

    private void CharacterWalk(bool isCharacter1, PlayerButtons playerButtons)
    {
        // Character Data
        BattleGameState.CharacterState characterState = isCharacter1 ? battleManager.gameState.character1 : battleManager.gameState.character2;
        StatsData characterStats = isCharacter1 ? battleManager.ninjaStats[battleManager.player1Ninja] : battleManager.ninjaStats[battleManager.player2Ninja];

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
                        characterState.velocityX = characterStats.forwardMoveSpeed;
                        battleManager.characterAnimator.SetAnimation(isCharacter1, "WalkForward");
					}

                    characterState.blocking = false;
                }
                else
                {
                    if (!characterState.crouching)
                    {
                        characterState.velocityX = characterStats.backwardMoveSpeed;
                        battleManager.characterAnimator.SetAnimation(isCharacter1, "WalkBackward");
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
                        characterState.velocityX = characterStats.backwardMoveSpeed * -1;
                        battleManager.characterAnimator.SetAnimation(isCharacter1, "WalkBackward");
                    }

                    characterState.blocking = true;
                }
                else
                {
                    if (!characterState.crouching)
                    {
                        characterState.velocityX = characterStats.forwardMoveSpeed * -1;
                        battleManager.characterAnimator.SetAnimation(isCharacter1, "WalkForward");
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
                    battleManager.characterAnimator.SetAnimation(isCharacter1, "Idle");
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

    private void CharacterDash(bool isCharacter1, PlayerButtons playerButtons, Dictionary<int, PlayerButtons> inputHistory)
	{
        // Character Data
        BattleGameState.CharacterState characterState = isCharacter1 ? battleManager.gameState.character1 : battleManager.gameState.character2;
        StatsData characterStats = isCharacter1 ? battleManager.ninjaStats[battleManager.player1Ninja] : battleManager.ninjaStats[battleManager.player2Ninja];

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
                        characterState.velocityX = characterStats.forwardDashSpeed;
                        battleManager.characterAnimator.SetAnimation(isCharacter1, "DashForward");
                        characterState.attacking = true;
                    }
                    else
                    {
                        characterState.velocityX = characterStats.backwardDashSpeed;
                        battleManager.characterAnimator.SetAnimation(isCharacter1, "DashBackward");
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
                        characterState.velocityX = characterStats.backwardDashSpeed * -1;
                        battleManager.characterAnimator.SetAnimation(isCharacter1, "DashBackward");
                        characterState.attacking = true;
                    }
                    else
                    {
                        characterState.velocityX = characterStats.forwardDashSpeed * -1;
                        battleManager.characterAnimator.SetAnimation(isCharacter1, "DashForward");
                        characterState.attacking = true;
                    }
                }
            }
        }
    }

    private void CharacterJump(bool isCharacter1, PlayerButtons playerButtons)
    {
        // Character Data
        BattleGameState.CharacterState characterState = isCharacter1 ? battleManager.gameState.character1 : battleManager.gameState.character2;
        StatsData characterStats = isCharacter1 ? battleManager.ninjaStats[battleManager.player1Ninja] : battleManager.ninjaStats[battleManager.player2Ninja];

        // Jump Start
        if (characterState.stun == 0 && !characterState.attacking && characterState.jumping == 0)
        {
            if (playerButtons.GetUp(true))
            {
                characterState.jumping = 2;
                characterState.jumpWindow = battleManager.inputData.jumpingWindow;
                characterState.velocityY = characterStats.jumpForce;
                battleManager.characterAnimator.SetAnimation(isCharacter1, "JumpNeutral");
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
                    characterState.velocityX = characterStats.jumpHorizontalSpeed * -1;
                    battleManager.characterAnimator.SetAnimation(isCharacter1, "JumpBackward");
                }
                else
                {
                    characterState.jumping = 3;
                    characterState.velocityX = characterStats.jumpHorizontalSpeed * -1;
                    battleManager.characterAnimator.SetAnimation(isCharacter1, "JumpForward");
                }
            }

            if (playerButtons.GetRight(true))
            {
                if (!characterState.mirrored)
                {
                    characterState.jumping = 3;
                    characterState.velocityX = characterStats.jumpHorizontalSpeed;
                    battleManager.characterAnimator.SetAnimation(isCharacter1, "JumpForward");
                }
                else
                {
                    characterState.jumping = 1;
                    characterState.velocityX = characterStats.jumpHorizontalSpeed;
                    battleManager.characterAnimator.SetAnimation(isCharacter1, "JumpBackward");
                }
            }
        }

        // Reduce window
        if (characterState.jumpWindow > 0)
        {
            characterState.jumpWindow--;
        }
    }

    private void CharacterCrouch(bool isCharacter1, PlayerButtons playerButtons)
    {
        // Character Data
        BattleGameState.CharacterState characterState = isCharacter1 ? battleManager.gameState.character1 : battleManager.gameState.character2;

        // Crouch
        if (characterState.stun == 0 && !characterState.attacking && characterState.jumping == 0)
        {
            if (playerButtons.GetDown(true))
            {
                characterState.crouching = true;
                battleManager.characterAnimator.SetAnimation(isCharacter1, "Crouch");
            }
            else
            {
                characterState.crouching = false;
            }
        }
    }
}
