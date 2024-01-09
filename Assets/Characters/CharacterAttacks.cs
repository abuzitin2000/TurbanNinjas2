using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttacks : MonoBehaviour
{
    public BattleManager battleManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void AttackCharacters()
	{
        ProcessSpecials(battleManager.player1InputHistory, battleManager.player1Buttons, battleManager.gameState.player1, battleManager.player1Data);
        ProcessSpecials(battleManager.player2InputHistory, battleManager.player2Buttons, battleManager.gameState.player2, battleManager.player2Data);
        ProcessNormals(battleManager.player1Buttons, battleManager.gameState.player1, battleManager.player1Data);
        ProcessNormals(battleManager.player2Buttons, battleManager.gameState.player2, battleManager.player2Data);
    }

    private void ProcessNormals(PlayerButtons playerButtons, BattleGameState.CharacterState characterState, CharacterData characterData)
    {
        // Return if not able to attack
        if (characterState.stun > 0 || characterState.attacking)
        {
            return;
        }

        // Ground Normals
        if (characterState.jumping == 0)
        {
            // Standing
            if (!characterState.crouching)
            {
                // Light Punch
                if (playerButtons.GetLPunch())
                {
                    battleManager.characterAnimator.SetAnimation(characterState, characterData, "LPunch");
                    characterState.attacking = true;
                }
            }
        }
    }

    private void ProcessSpecials(Dictionary<int, PlayerButtons> inputHistory, PlayerButtons playerButtons, BattleGameState.CharacterState characterState, CharacterData characterData)
	{
        // Return if not able to do a special
        if (characterState.stun > 0 || (characterState.attacking && !characterState.cancellable))
        {
            return;
        }

        // Construct the special name
        string specialName = "";

        // Standing or Jumping Specials
        if (characterState.jumping == 0)
        {
            specialName += "Standing";
        }
		else
		{
            specialName += "Jumping";
        }

        // Light Punch
        if (playerButtons.GetLPunch())
        {
            specialName += "LPunch";
        }

        // Heeavy Punch
        if (playerButtons.GetHPunch())
        {
            specialName += "HPunch";
        }

        // Light Kick
        if (playerButtons.GetLKick())
        {
            specialName += "LKick";
        }

        // Heeavy Kick
        if (playerButtons.GetHKick())
        {
            specialName += "HKick";
        }

        // Check if a buttons was pressed
        if (specialName == "" || specialName == "Standing" || specialName == "Jumping")
		{
            return;
		}

        bool[] specialInputs = ProcessSpecialInputs(inputHistory, characterState);
        bool done = false;

        // Quarter Circle Forward
        if (specialInputs[0] && !done)
		{
            specialName += "QCF";
            done = battleManager.characterAnimator.SetAnimation(characterState, characterData, specialName);
        }

        // Quarter Circle Backward
        if (specialInputs[1] && !done)
        {
            specialName += "QCB";
            done = battleManager.characterAnimator.SetAnimation(characterState, characterData, specialName);
        }

        // DP Forward
        if (specialInputs[1] && !done)
        {
            specialName += "DPF";
            done = battleManager.characterAnimator.SetAnimation(characterState, characterData, specialName);
        }

        if (done)
		{
            characterState.attacking = true;
		}
    }

    private bool[] ProcessSpecialInputs(Dictionary<int, PlayerButtons> inputHistory, BattleGameState.CharacterState characterState)
    {
        // Special Bool Array
        // --- [QCF,QCB,DPF,DPB,HCF,HCB,BF,DU] ---
        bool[] specialInputs = new bool[8];

		// Phases
		int qcf = 0;
        int qcb = 0;
        int dpf = 0;
        int dpb = 0;
        int hcf = 0;
        int hcb = 0;
        int bf = 0;
        int du = 0;

        // Input History
        for (int i = 1; i < 60; i++)
        {
            PlayerButtons input = inputHistory[battleManager.gameState.frameTime - i];

            // If input too old
            if (input == null)
			{
                break;
			}

            // Forward
            if ((!characterState.mirrored && input.GetRight()) || (characterState.mirrored && input.GetLeft()))
            {
                // QCF
                if (qcf == 0 && i < battleManager.inputData.inputData.qcfForwardWindow)
				{
                    qcf = i;
				}
            }

            // Down
            if (input.GetDown())
            {
                // QCF
                if (qcf > 0 && i - qcf < battleManager.inputData.inputData.qcfForwardWindow)
                {
                    qcf = -1;
                    specialInputs[0] = true;
                }
            }
        }

        return specialInputs;
    }
}
