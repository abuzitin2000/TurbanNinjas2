using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunManager : MonoBehaviour
{
    public BattleManager battleManager;

    public void ReduceStuns()
	{
        ReduceStun(true);
        ReduceStun(false);
        ReducePushBacks(true);
        ReducePushBacks(false);
    }

    private void ReduceStun(bool isCharacter1)
	{
        // Character State
        BattleGameState.CharacterState characterState = isCharacter1 ? battleManager.gameState.character1 : battleManager.gameState.character2;

        if (characterState.stun > 0)
		{
            characterState.stun -= 1;

            // End Stun
            if (characterState.stun == 0)
			{
                battleManager.characterAnimator.SetAnimation(isCharacter1, "Idle");
			}
        }
    }

    private void ReducePushBacks(bool isCharacter1)
    {
        // Character State
        BattleGameState.CharacterState characterState = isCharacter1 ? battleManager.gameState.character1 : battleManager.gameState.character2;

        if (characterState.pushback > 0)
        {
            characterState.pushback -= 1;

            // End Stun
            if (characterState.pushback == 0)
            {
                characterState.velocityX = 0;
            }
        }
    }
}
