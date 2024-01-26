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
        else if (playerButtons.GetHP(false))
        {
            specialName += "HP";
        }
        // Light Kick
        else if (playerButtons.GetLK(false))
        {
            specialName += "LK";
        }
        // Heeavy Kick
        else if (playerButtons.GetHK(false))
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

        // Down Up Charge
        if (specialInputs[7] && !done)
        {
            specialName += "DU";
            done = battleManager.characterAnimator.SetAnimation(characterState, characterData, specialName);
        }

        // Back Forward Charge
        if (specialInputs[6] && !done)
        {
            specialName += "BF";
            done = battleManager.characterAnimator.SetAnimation(characterState, characterData, specialName);
        }

        // DP Forward
        if (specialInputs[2] && !done)
        {
            specialName += "DPF";
            done = battleManager.characterAnimator.SetAnimation(characterState, characterData, specialName);
        }

        // DP Back
        if (specialInputs[3] && !done)
        {
            specialName += "DPB";
            done = battleManager.characterAnimator.SetAnimation(characterState, characterData, specialName);
        }

        // Half Circle Forward
        if (specialInputs[4] && !done)
        {
            specialName += "HCF";
            done = battleManager.characterAnimator.SetAnimation(characterState, characterData, specialName);
        }

        // Half Circle Back
        if (specialInputs[3] && !done)
        {
            specialName += "HCB";
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

            // If input doesn't exist
            if (input == null)
			{
                break;
			}

            // Quarter Circle Forward
            if (QCF == 2)
            {
                // Down (2)
                if (input.GetDown(false) && !(input.GetLeft(true) || input.GetRight(true)))
                {
                    if (timerQCF - inputFrame < battleManager.inputData.qcfDownWindow && timerQCF - inputFrame > battleManager.inputData.simultaneousWindow)
					{
                        QCF = 3;
                        specialInputs[0] = true;
					}
                }
            }
            else if (QCF == 1)
            {
                // Down Forward (3)
                if (((!characterState.mirrored && input.GetRight(false)) || (characterState.mirrored && input.GetLeft(false))) && input.GetDown(true))
                {
                    if (timerQCF - inputFrame < battleManager.inputData.qcfDownForwardWindow && timerQCF - inputFrame > battleManager.inputData.simultaneousWindow)
				    {
                        QCF = 2;
                        timerQCF = inputFrame;
					}
				}
            }
            else if (QCF == 0)
            {
                // Forward (6)
                if (((!characterState.mirrored && input.GetRight(true)) || (characterState.mirrored && input.GetLeft(true))) && !input.GetDown(true))
				{
                    if (battleManager.gameState.frameTime - inputFrame < battleManager.inputData.qcfForwardWindow)
				    {
                        QCF = 1;
                        timerQCF = inputFrame;
				    }
				}
            }

            // Quarter Circle Back
            if (QCB == 2)
            {
                // Down (2)
                if (input.GetDown(false) && !(input.GetLeft(true) || input.GetRight(true)))
                {
                    if (timerQCB - inputFrame < battleManager.inputData.qcbDownWindow && timerQCB - inputFrame > battleManager.inputData.simultaneousWindow)
                    {
                        QCB = 3;
                        specialInputs[1] = true;
                    }
                }
            }
            else if (QCB == 1)
            {
                // Down Back (1)
                if (((!characterState.mirrored && input.GetLeft(false)) || (characterState.mirrored && input.GetRight(false))) && input.GetDown(true))
                {
                    if (timerQCB - inputFrame < battleManager.inputData.qcbDownBackWindow && timerQCB - inputFrame > battleManager.inputData.simultaneousWindow)
                    {
                        QCB = 2;
                        timerQCB = inputFrame;
                    }
                }
            }
            else if (QCB == 0)
            {
                // Back (4)
                if (((!characterState.mirrored && input.GetLeft(true)) || (characterState.mirrored && input.GetRight(true))) && !input.GetDown(true))
                {
                    if (battleManager.gameState.frameTime - inputFrame < battleManager.inputData.qcbBackWindow)
					{
                        QCB = 1;
                        timerQCB = inputFrame;
                    }
                }
            }

            // Dragon Punch Forward
            if (DPF == 2)
            {
                // Forward (6)
                if (((!characterState.mirrored && input.GetRight(false)) || (characterState.mirrored && input.GetLeft(false))) && !input.GetDown(true))
                {
                    if (timerDPF - inputFrame < battleManager.inputData.dpfForwardWindow && timerDPF - inputFrame > battleManager.inputData.simultaneousWindow)
					{
                        DPF = 3;
                        specialInputs[2] = true;
					}
                }
            }
            else if (DPF == 1)
            {
                // Down (2)
                if (input.GetDown(false) && !(input.GetLeft(true) || input.GetRight(true)))
                {
                    if (timerDPF - inputFrame < battleManager.inputData.dpfDownWindow && timerDPF - inputFrame > battleManager.inputData.simultaneousWindow)
                    {
                        DPF = 2;
                        timerDPF = inputFrame;
                    }
                }
            }
            else if (DPF == 0)
            {
                // Down Forward (3)
                if (((!characterState.mirrored && input.GetRight(false)) || (characterState.mirrored && input.GetLeft(false))) && input.GetDown(true))
                {
                    if (battleManager.gameState.frameTime - inputFrame < battleManager.inputData.dpfDownForwardWindow)
                    {
                        DPF = 1;
                        timerDPF = inputFrame;
                    }
                }
            }

            // Dragon Punch Back
            if (DPB == 2)
            {
                // Back (4)
                if (((!characterState.mirrored && input.GetLeft(false)) || (characterState.mirrored && input.GetRight(false))) && !input.GetDown(true))
                {
                    if (timerDPB - inputFrame < battleManager.inputData.dpbBackWindow && timerDPB - inputFrame > battleManager.inputData.simultaneousWindow)
					{
                        DPB = 3;
                        specialInputs[3] = true;
					}
                }
            }
            else if (DPB == 1)
            {
                // Down (2)
                if (input.GetDown(false) && !(input.GetLeft(true) || input.GetRight(true)))
                {
                    if (timerDPB - inputFrame < battleManager.inputData.dpbDownWindow && timerDPB - inputFrame > battleManager.inputData.simultaneousWindow)
					{
                        DPB = 2;
                        timerDPB = inputFrame;
                    }
                }
            }
            else if (DPB == 0)
            {
                // Down Back (1)
                if (((!characterState.mirrored && input.GetLeft(false)) || (characterState.mirrored && input.GetRight(false))) && input.GetDown(true))
                {
                    if (battleManager.gameState.frameTime - inputFrame < battleManager.inputData.dpbDownBackWindow)
					{
                        DPB = 1;
                        timerDPB = inputFrame;
                    }
                }
            }

            // Half Circle Forward
            if (HCF == 2)
            {
                // Back (4)
                if (((!characterState.mirrored && input.GetLeft(false)) || (characterState.mirrored && input.GetRight(false))) && !input.GetDown(true))
                {
                    if (timerHCF - inputFrame < battleManager.inputData.hcfBackWindow && timerHCF - inputFrame > battleManager.inputData.simultaneousWindow)
                    {
                        HCF = 3;
                        specialInputs[4] = true;
                    }
                }
            }
            else if (HCF == 1)
            {
                // Down (2)
                if (input.GetDown(false) && !((!characterState.mirrored && input.GetRight(true)) || (characterState.mirrored && input.GetLeft(true))))
                {
                    if (timerHCF - inputFrame < battleManager.inputData.hcfDownWindow && timerHCF - inputFrame > battleManager.inputData.simultaneousWindow)
                    {
                        HCF = 2;
                        timerHCF = inputFrame;
                    }
                }
            }
            else if (HCF == 0)
            {
                // Forward (6)
                if (((!characterState.mirrored && input.GetRight(true)) || (characterState.mirrored && input.GetLeft(true))) && !input.GetDown(true))
                {
                    if (battleManager.gameState.frameTime - inputFrame < battleManager.inputData.hcfForwardWindow)
                    {
                        HCF = 1;
                        timerHCF = inputFrame;
                    }
                }
            }

            // Half Circle Back
            if (HCB == 2)
            {
                // Forward (6)
                if (((!characterState.mirrored && input.GetRight(false)) || (characterState.mirrored && input.GetLeft(false))) && !input.GetDown(true))
                {
                    if (timerHCB - inputFrame < battleManager.inputData.hcbForwardWindow && timerHCB - inputFrame > battleManager.inputData.simultaneousWindow)
                    {
                        HCB = 3;
                        specialInputs[5] = true;
                    }
                }
            }
            else if (HCB == 1)
            {
                // Down (2)
                if (input.GetDown(false) && !((!characterState.mirrored && input.GetLeft(true)) || (characterState.mirrored && input.GetRight(true))))
                {
                    if (timerHCB - inputFrame < battleManager.inputData.hcbDownWindow && timerHCB - inputFrame > battleManager.inputData.simultaneousWindow)
                    {
                        HCB = 2;
                        timerHCB = inputFrame;
                    }
                }
            }
            else if (HCB == 0)
            {
                // Back (4)
                if (((!characterState.mirrored && input.GetLeft(true)) || (characterState.mirrored && input.GetRight(true))) && !input.GetDown(true))
                {
                    if (battleManager.gameState.frameTime - inputFrame < battleManager.inputData.hcbBackWindow)
                    {
                        HCB = 1;
                        timerHCB = inputFrame;
                    }
                }
            }

            // Back Forward Charge
            if (BF > battleManager.inputData.bfBackChargeRequired)
			{
                specialInputs[6] = true;
            }
            else if (BF > 0)
            {
                // Back (4)
                if ((!characterState.mirrored && input.GetLeft(true)) || (characterState.mirrored && input.GetRight(true)))
                {
                    if (timerBF - inputFrame < battleManager.inputData.bfBackChargeWindow)
                    {
                        BF += 1;
                    }
                }
            }
            else if (BF == 0)
			{
                // Forward (6)
                if ((!characterState.mirrored && input.GetRight(false)) || (characterState.mirrored && input.GetLeft(false)))
                {
                    if (battleManager.gameState.frameTime - inputFrame < battleManager.inputData.bfForwardWindow)
                    {
                        BF = 1;
                        timerBF = inputFrame;
                    }
                }
            }

            // Down Up Charge
            if (DU > battleManager.inputData.duDownChargeRequired)
            {
                specialInputs[7] = true;
            }
            else if (DU > 0)
            {
                // Down (2)
                if (input.GetDown(true))
                {
                    if (timerDU - inputFrame < battleManager.inputData.duDownChargeWindow)
                    {
                        DU += 1;
                    }
                }
            }
            else if (DU == 0)
            {
                // Up (8)
                if (input.GetUp(false))
                {
                    if (battleManager.gameState.frameTime - inputFrame < battleManager.inputData.duUpWindow)
                    {
                        DU = 1;
                        timerDU = inputFrame;
                    }
                }
            }
        }

        return specialInputs;
    }
}
