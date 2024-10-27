using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpecials : MonoBehaviour
{
    public void LoseAnimation(bool player1, BattleGameState gameState)
    {
        BattleGameState.CharacterState characterState;

        if (player1)
        {
            characterState = gameState.character1;
        }
		else
		{
            characterState = gameState.character2;
        }

        characterState.stun = 1000;
        characterState.pushback = 20;
        characterState.positionY += 150;

        if (!characterState.mirrored)
		{
            characterState.velocityX = -15;
        }
		else
		{
            characterState.velocityX = 15;
        }
    }
}
