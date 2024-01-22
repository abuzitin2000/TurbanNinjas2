using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttacks : MonoBehaviour
{
    public BattleManager battleManager;

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
                if (playerButtons.GetLP(false))
                {
                    battleManager.characterAnimator.SetAnimation(characterState, characterData, "StLP");
                    characterState.attacking = true;
                }
            }

            // Crouching
            if (characterState.crouching)
            {
                // Light Punch
                if (playerButtons.GetLP(false))
                {
                    battleManager.characterAnimator.SetAnimation(characterState, characterData, "CrLP");
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
            specialName += "St";
        }
		else
		{
            specialName += "Jmp";
        }

        // Light Punch
        if (playerButtons.GetLP(false))
        {
            specialName += "LP";
        }

        // Heeavy Punch
        if (playerButtons.GetHP(false))
        {
            specialName += "HP";
        }

        // Light Kick
        if (playerButtons.GetLK(false))
        {
            specialName += "LK";
        }

        // Heeavy Kick
        if (playerButtons.GetHK(false))
        {
            specialName += "HK";
        }

        // Check if a buttons was pressed
        if (specialName == "" || specialName == "St" || specialName == "Jmp")
		{
            return;
		}

        bool[] specialInputs = ProcessSpecialInputs(inputHistory, characterState);
        bool done = false;

        // DP Forward
        if (specialInputs[2] && !done)
        {
            specialName += "DPF";
            done = battleManager.characterAnimator.SetAnimation(characterState, characterData, specialName);
        }

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

        Debug.Log(specialInputs[0] + " " + specialInputs[1] + " " + specialInputs[2]);

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
		int QCF = 0;
        int QCB = 0;
        int DPF = 0;
        int DPB = 0;
        int HCF = 0;
        int HCB = 0;
        int BF = 0;
        int DU = 0;

        // Timers
        int timerQCF = 0;
        int timerQCB = 0;
        int timerDPF = 0;
        int timerDPB = 0;
        int timerHCF = 0;
        int timerHCB = 0;
        int timerBF = 0;
        int timerDU = 0;

        // Input History
        for (int i = 1; i < 60; i++)
        {
            int inputFrame = battleManager.gameState.frameTime - i;
            PlayerButtons input = inputHistory[inputFrame];

            // If input too old
            if (input == null)
			{
                break;
			}

            // Forward
            if ((!characterState.mirrored && input.GetRight(false)) || (characterState.mirrored && input.GetLeft(false)))
            {
                // QCF
                if (QCF == 0 && battleManager.gameState.frameTime - inputFrame < battleManager.inputData.qcfForwardWindow)
				{
                    QCF = 1;
                    timerQCF = inputFrame;
				}

                // DPF
                if (DPF == 0 && battleManager.gameState.frameTime - inputFrame < battleManager.inputData.dpfForwardSecondWindow)
                {
                    DPF = 1;
                    timerDPF = inputFrame;
                }
                if (DPF == 2 && timerDPF - inputFrame < battleManager.inputData.dpfForwardFirstWindow && timerDPF - inputFrame > battleManager.inputData.simultaneousWindow)
                {
                    DPF = 3;
                    specialInputs[2] = true;
                }
            }

            // Down
            if (input.GetDown(false))
            {
                // QCF
                if (QCF == 1 && timerQCF - inputFrame < battleManager.inputData.qcfDownWindow && timerQCF - inputFrame > battleManager.inputData.simultaneousWindow)
                {
                    QCF = 2;
                    specialInputs[0] = true;
                }

                // QCB
                if (QCB == 1 && timerQCB - inputFrame < battleManager.inputData.qcbDownWindow && timerQCB - inputFrame > battleManager.inputData.simultaneousWindow)
                {
                    QCB = 2;
                    specialInputs[1] = true;
                }

                // DPF
                if (DPF == 1 && timerDPF - inputFrame < battleManager.inputData.dpfDownWindow && timerDPF - inputFrame > battleManager.inputData.simultaneousWindow)
                {
                    DPF = 2;
                    timerDPF = inputFrame;
                }
            }

            // Back
            if ((!characterState.mirrored && input.GetLeft(false)) || (characterState.mirrored && input.GetRight(false)))
            {
                // QCB
                if (QCB == 0 && battleManager.gameState.frameTime - inputFrame < battleManager.inputData.qcbBackWindow)
                {
                    QCB = 1;
                    timerQCB = inputFrame;
                }
            }
        }

        return specialInputs;
    }
}
