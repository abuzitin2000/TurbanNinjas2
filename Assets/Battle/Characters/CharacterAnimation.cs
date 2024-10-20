using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public BattleManager battleManager;
    public CollisionManager collisionManager;

    public Animator player1Animator;
    public Animator player2Animator;

    private string player1CurrentAnimation;
    private string player2CurrentAnimation;

    public void AnimateCharacters()
    {
        if (player1CurrentAnimation != battleManager.player1Data.characterAnimations[battleManager.gameState.player1.animation].animationFileName)
        {
            player1Animator.CrossFadeInFixedTime(battleManager.player1Data.characterAnimations[battleManager.gameState.player1.animation].animationFileName, 0.1f, 0);
            player1CurrentAnimation = battleManager.player1Data.characterAnimations[battleManager.gameState.player1.animation].animationFileName;
        }
        else if (!player1Animator.IsInTransition(0))
        {
            player1Animator.Play(battleManager.player1Data.characterAnimations[battleManager.gameState.player1.animation].animationFileName, 0, GetAnimationPercentage(battleManager.player1Data, battleManager.gameState.player1.animation, battleManager.gameState.player1.frame));
        }

        if (player2CurrentAnimation != battleManager.player2Data.characterAnimations[battleManager.gameState.player2.animation].animationFileName)
        {
            player2Animator.CrossFadeInFixedTime(battleManager.player2Data.characterAnimations[battleManager.gameState.player2.animation].animationFileName, 0.1f, 0);
            player2CurrentAnimation = battleManager.player2Data.characterAnimations[battleManager.gameState.player2.animation].animationFileName;
        }
        else if (!player2Animator.IsInTransition(0))
        {
            player2Animator.Play(battleManager.player2Data.characterAnimations[battleManager.gameState.player2.animation].animationFileName, 0, GetAnimationPercentage(battleManager.player2Data, battleManager.gameState.player2.animation, battleManager.gameState.player2.frame));
        }

        battleManager.character1.transform.localScale = new Vector3(battleManager.gameState.player1.mirrored ? -1f : 1f, 1f, 1f);
        battleManager.character2.transform.localScale = new Vector3(battleManager.gameState.player2.mirrored ? -1f : 1f, 1f, 1f);
        battleManager.character1.transform.eulerAngles = new Vector3(battleManager.character1.transform.eulerAngles.x, battleManager.gameState.player1.mirrored ? 270f : -270f, battleManager.character1.transform.eulerAngles.z);
        battleManager.character2.transform.eulerAngles = new Vector3(battleManager.character2.transform.eulerAngles.x, battleManager.gameState.player2.mirrored ? 270f : -270f, battleManager.character2.transform.eulerAngles.z);
    }

    public bool SetAnimation(BattleGameState.CharacterState characterState, CharacterData characterData, string name)
    {
        int anim = characterData.characterAnimations.FindIndex(x => x.name == name);

        // Check if anim exists
        if (anim == -1)
		{
            Debug.Log("Not Found");
            return false;
		}

        // Start from begining if animation is not set as looping or a new animation is starting
        if (anim != characterState.animation || (anim == characterState.animation && !characterData.characterAnimations[anim].loop))
        {
            SetFrame(characterState, characterData, anim, 0);
            characterState.attacking = false;
            characterState.hit = false;
        }

        characterState.animation = anim;

        return true;
    }

    public void SetFrame(BattleGameState.CharacterState characterState, CharacterData characterData, int anim, int frame)
    {
        int framePhase = FindPhase(characterData, anim, frame);

        // Check if frame exists
        if (framePhase > characterData.characterAnimations[anim].phases.Count - 1)
		{
            Debug.LogError("Animation not found!!!");
            return;
		}

        // If animation is over
        if (framePhase == -1)
        {
            // If already Idle
            if (anim == 0)
			{
                SetFrame(characterState, characterData, anim, 0);
			}

            SetAnimation(characterState, characterData, "Idle");
            return;
        }

        characterState.frame = frame;

        // Animation settings
        characterState.cancellable = characterData.characterAnimations[anim].phases[framePhase].specialCancellable;

        // Change velocity
        if (characterData.characterAnimations[anim].phases[framePhase].changeVelocity)
		{
            if (!characterState.mirrored)
			{
                characterState.velocityX = characterData.characterAnimations[anim].phases[framePhase].velocityX;
			}
			else
			{
                characterState.velocityX = characterData.characterAnimations[anim].phases[framePhase].velocityX * -1;
            }

            characterState.velocityY = characterData.characterAnimations[anim].phases[framePhase].velocityY;
        }
    }

    public void AdvanceFrames()
    {
        // Player 1
        SetFrame(battleManager.gameState.player1, battleManager.player1Data, battleManager.gameState.player1.animation, battleManager.gameState.player1.frame + 1);

        // Player 2
        SetFrame(battleManager.gameState.player2, battleManager.player2Data, battleManager.gameState.player2.animation, battleManager.gameState.player2.frame + 1);
    }

    public int FindPhase(CharacterData characterData, int anim, int frame)
	{
        // End animation if animation has no frames
        if (characterData.characterAnimations[anim].phases.Count == 0 || characterData.characterAnimations[anim].phases[0].duration == 0)
        {
            return -1;
        }

        // Search Phase
        int phase = 0;

        while (frame + 1 > characterData.characterAnimations[anim].phases[phase].duration)
        {
            frame -= characterData.characterAnimations[anim].phases[phase].duration;
            phase++;

            // If the frame is higher than enire animation length therefore finished
            if (phase > characterData.characterAnimations[anim].phases.Count - 1)
            {
                return -1;
            }
        }

        return phase;
    }

    private float GetAnimationPercentage(CharacterData characterData, int anim, int frame)
    {
        int phase = FindPhase(characterData, anim, frame);

        // If animation is not found
        if (phase == -1)
        {
            return 0f;
        }

        // Find Phase Percentage
        int upperCount = characterData.characterAnimations[anim].phases[0].duration - 1;
        int lowerCount = 0;

        int i = 1;
        while (i <= phase)
        {
            lowerCount = upperCount + 1;
            upperCount += characterData.characterAnimations[anim].phases[i].duration;
            i++;
        }

        float percentage = (float)(frame - lowerCount) / (float)(upperCount - lowerCount);

        // Find Animation Time
        float animationTime = Mathf.Lerp(characterData.characterAnimations[anim].phases[phase].animStartTime, characterData.characterAnimations[anim].phases[phase].animEndTime, percentage);

        if (animationTime < 0f)
        {
            animationTime = 0f;
        }

        if (animationTime > 1f)
        {
            animationTime = 1f;
        }

        return animationTime;
    }

    private int GetTotalFrameCount(CharacterData characterData, int anim)
    {
        int totalFrameCount = 0;

        foreach (CharacterData.characteranimationphase phase in characterData.characterAnimations[anim].phases)
        {
            totalFrameCount += phase.duration;
        }

        return totalFrameCount;
    }

    public void CallSpecialFunctions()
	{
        // Player 1 Special Function
        int player1FramePhase = FindPhase(battleManager.player1Data, battleManager.gameState.player1.animation, battleManager.gameState.player1.frame);
        battleManager.player1Data.characterAnimations[battleManager.gameState.player1.animation].phases[player1FramePhase].specialFunctions.Invoke(true, battleManager.gameState);

        // Player 2 Special Function
        int player2FramePhase = FindPhase(battleManager.player2Data, battleManager.gameState.player2.animation, battleManager.gameState.player2.frame);
        battleManager.player2Data.characterAnimations[battleManager.gameState.player2.animation].phases[player2FramePhase].specialFunctions.Invoke(false, battleManager.gameState);
    }

    private void OnDrawGizmos()
    {
        // Player 1 Hurtbox
        foreach (CharacterData.hurtboxdata hurtbox in battleManager.player1Data.characterAnimations[battleManager.gameState.player1.animation].phases[FindPhase(battleManager.player1Data, battleManager.gameState.player1.animation, battleManager.gameState.player1.frame)].hurtBoxes)
        {
            int mirrorhurtboxPositionoffsetX = battleManager.gameState.player1.positionX > battleManager.gameState.player2.positionX ? hurtbox.positionoffsetX * -1 : hurtbox.positionoffsetX;
            int mirrorhurtboxsizeX = battleManager.gameState.player1.positionX > battleManager.gameState.player2.positionX ? hurtbox.sizeX * -1 : hurtbox.sizeX;

            Gizmos.color = new Color(0, 1, 1, 0.5f);
            Gizmos.DrawCube(new Vector3((battleManager.gameState.player1.positionX + mirrorhurtboxPositionoffsetX + (mirrorhurtboxsizeX / 2f)) / BattleManager.renderRatio, (battleManager.gameState.player1.positionY + hurtbox.positionoffsetY - (hurtbox.sizeY / 2f)) / BattleManager.renderRatio, 0), new Vector2(mirrorhurtboxsizeX / BattleManager.renderRatio, hurtbox.sizeY / BattleManager.renderRatio));
        }

        // Player 1 Hitbox
        foreach (CharacterData.hitboxdata hitbox in battleManager.player1Data.characterAnimations[battleManager.gameState.player1.animation].phases[FindPhase(battleManager.player1Data, battleManager.gameState.player1.animation, battleManager.gameState.player1.frame)].hitBoxes)
        {
            int mirrorhitboxPositionoffsetX = battleManager.gameState.player1.positionX > battleManager.gameState.player2.positionX ? hitbox.positionoffsetX * -1 : hitbox.positionoffsetX;
            int mirrorhitboxsizeX = battleManager.gameState.player1.positionX > battleManager.gameState.player2.positionX ? hitbox.sizeX * -1 : hitbox.sizeX;

            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(new Vector3((battleManager.gameState.player1.positionX + mirrorhitboxPositionoffsetX + (mirrorhitboxsizeX / 2f)) / BattleManager.renderRatio, (battleManager.gameState.player1.positionY + hitbox.positionoffsetY - (hitbox.sizeY / 2f)) / BattleManager.renderRatio, 0), new Vector2(mirrorhitboxsizeX / BattleManager.renderRatio, hitbox.sizeY / BattleManager.renderRatio));
        }

        // Player 1 Collisionbox
        foreach (CharacterData.collisionboxdata collisionbox in battleManager.player1Data.characterAnimations[battleManager.gameState.player1.animation].phases[FindPhase(battleManager.player1Data, battleManager.gameState.player1.animation, battleManager.gameState.player1.frame)].collisionBoxes)
        {
            int mirrorcollisionboxPositionoffsetX = battleManager.gameState.player1.positionX > battleManager.gameState.player2.positionX ? collisionbox.positionoffsetX * -1 : collisionbox.positionoffsetX;
            int mirrorcollisionboxsizeX = battleManager.gameState.player1.positionX > battleManager.gameState.player2.positionX ? collisionbox.sizeX * -1 : collisionbox.sizeX;

            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(new Vector3((battleManager.gameState.player1.positionX + mirrorcollisionboxPositionoffsetX + (mirrorcollisionboxsizeX / 2f)) / BattleManager.renderRatio, (battleManager.gameState.player1.positionY + collisionbox.positionoffsetY - (collisionbox.sizeY / 2f)) / BattleManager.renderRatio, 0), new Vector2(mirrorcollisionboxsizeX / BattleManager.renderRatio, collisionbox.sizeY / BattleManager.renderRatio));
        }

        // Player 2 Hurtbox
        foreach (CharacterData.hurtboxdata hurtbox in battleManager.player2Data.characterAnimations[battleManager.gameState.player2.animation].phases[FindPhase(battleManager.player2Data, battleManager.gameState.player2.animation, battleManager.gameState.player2.frame)].hurtBoxes)
        {
            int mirrorhurtboxPositionoffsetX = battleManager.gameState.player1.positionX < battleManager.gameState.player2.positionX ? hurtbox.positionoffsetX * -1 : hurtbox.positionoffsetX;
            int mirrorhurtboxsizeX = battleManager.gameState.player1.positionX < battleManager.gameState.player2.positionX ? hurtbox.sizeX * -1 : hurtbox.sizeX;

            Gizmos.color = new Color(0, 1, 1, 0.5f);
            Gizmos.DrawCube(new Vector3((battleManager.gameState.player2.positionX + mirrorhurtboxPositionoffsetX + (mirrorhurtboxsizeX / 2f)) / BattleManager.renderRatio, (battleManager.gameState.player2.positionY + hurtbox.positionoffsetY - (hurtbox.sizeY / 2f)) / BattleManager.renderRatio, 0), new Vector2(mirrorhurtboxsizeX / BattleManager.renderRatio, hurtbox.sizeY / BattleManager.renderRatio));
        }

        // Player 2 Hitbox
        foreach (CharacterData.hitboxdata hitbox in battleManager.player2Data.characterAnimations[battleManager.gameState.player2.animation].phases[FindPhase(battleManager.player2Data, battleManager.gameState.player2.animation, battleManager.gameState.player2.frame)].hitBoxes)
        {
            int mirrorhitboxPositionoffsetX = battleManager.gameState.player1.positionX < battleManager.gameState.player2.positionX ? hitbox.positionoffsetX * -1 : hitbox.positionoffsetX;
            int mirrorhitboxsizeX = battleManager.gameState.player1.positionX < battleManager.gameState.player2.positionX ? hitbox.sizeX * -1 : hitbox.sizeX;

            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(new Vector3((battleManager.gameState.player2.positionX + mirrorhitboxPositionoffsetX + (mirrorhitboxsizeX / 2f)) / BattleManager.renderRatio, (battleManager.gameState.player2.positionY + hitbox.positionoffsetY - (hitbox.sizeY / 2f)) / BattleManager.renderRatio, 0), new Vector2(mirrorhitboxsizeX / BattleManager.renderRatio, hitbox.sizeY / BattleManager.renderRatio));
        }

        // Player 2 Collisionbox
        foreach (CharacterData.collisionboxdata collisionbox in battleManager.player2Data.characterAnimations[battleManager.gameState.player2.animation].phases[FindPhase(battleManager.player2Data, battleManager.gameState.player2.animation, battleManager.gameState.player2.frame)].collisionBoxes)
        {
            int mirrorcollisionboxPositionoffsetX = battleManager.gameState.player2.positionX > battleManager.gameState.player1.positionX ? collisionbox.positionoffsetX * -1 : collisionbox.positionoffsetX;
            int mirrorcollisionboxsizeX = battleManager.gameState.player2.positionX > battleManager.gameState.player1.positionX ? collisionbox.sizeX * -1 : collisionbox.sizeX;

            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(new Vector3((battleManager.gameState.player2.positionX + mirrorcollisionboxPositionoffsetX + (mirrorcollisionboxsizeX / 2f)) / BattleManager.renderRatio, (battleManager.gameState.player2.positionY + collisionbox.positionoffsetY - (collisionbox.sizeY / 2f)) / BattleManager.renderRatio, 0), new Vector2(mirrorcollisionboxsizeX / BattleManager.renderRatio, collisionbox.sizeY / BattleManager.renderRatio));
        }
    }
}
