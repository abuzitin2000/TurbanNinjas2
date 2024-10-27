using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public BattleManager battleManager;
    public CollisionManager collisionManager;

    public Animator character1Animator;
    public Animator character2Animator;

    private string character1CurrentAnimation;
    private string character2CurrentAnimation;

    public void AnimateCharacters()
    {
        // Character States
        BattleGameState.CharacterState character1State = battleManager.gameState.character1;
        BattleGameState.CharacterState character2State = battleManager.gameState.character2;

        // Animations to Play
        string character1AnimationName = GetAnimation(battleManager.character1Datas, character1State.animation).animationFileName;
        string character2AnimationName = GetAnimation(battleManager.character2Datas, character2State.animation).animationFileName;

        if (character1CurrentAnimation != character1AnimationName)
        {
            character1Animator.CrossFadeInFixedTime(character1AnimationName, 0.1f, 0);
            character1CurrentAnimation = character1AnimationName;
        }
        else if (!character1Animator.IsInTransition(0))
        {
            character1Animator.Play(character1AnimationName, 0, GetAnimationPercentage(battleManager.character1Datas, character1State.animation, character1State.frame));
        }

        if (character2CurrentAnimation != character2AnimationName)
        {
            character2Animator.CrossFadeInFixedTime(character2AnimationName, 0.1f, 0);
            character2CurrentAnimation = character2AnimationName;
        }
        else if (!character2Animator.IsInTransition(0))
        {
            character2Animator.Play(character2AnimationName, 0, GetAnimationPercentage(battleManager.character2Datas, character2State.animation, character2State.frame));
        }

        battleManager.character1.transform.localScale = new Vector3(battleManager.gameState.character1.mirrored ? -1f : 1f, 1f, 1f);
        battleManager.character2.transform.localScale = new Vector3(battleManager.gameState.character2.mirrored ? -1f : 1f, 1f, 1f);
        battleManager.character1.transform.eulerAngles = new Vector3(battleManager.character1.transform.eulerAngles.x, battleManager.gameState.character1.mirrored ? 270f : -270f, battleManager.character1.transform.eulerAngles.z);
        battleManager.character2.transform.eulerAngles = new Vector3(battleManager.character2.transform.eulerAngles.x, battleManager.gameState.character2.mirrored ? 270f : -270f, battleManager.character2.transform.eulerAngles.z);
    }

    public void AdvanceFrames()
    {
        // Player 1
        SetFrame(true, GetAnimation(battleManager.character1Datas, battleManager.gameState.character1.animation), battleManager.gameState.character1.frame + 1);

        // Player 2
        SetFrame(false, GetAnimation(battleManager.character2Datas, battleManager.gameState.character2.animation), battleManager.gameState.character2.frame + 1);
    }

    public bool SetAnimation(bool isCharacter1, string animationName)
    {
        // Get Animations Data List
        List<AnimationsData> animationsData = isCharacter1 ? battleManager.character1Datas : battleManager.character2Datas;

        // Find Animation Index
        int animationIndex = FindAnimationIndexbyName(animationsData, animationName);

        // Check if animation exists
        if (animationIndex == -1)
        {
            Debug.Log("Animation To Set Not Found");
            return false;
        }

        // Get Animation Data
        AnimationsData.CharacterAnimation animation = GetAnimation(animationsData, animationIndex);

        // Current Character State
        BattleGameState.CharacterState characterState = isCharacter1 ? battleManager.gameState.character1 : battleManager.gameState.character2;

        // Start Animation from start if actually changing animation or resetting it
        if (animationIndex != characterState.animation || (animationIndex == characterState.animation && !animation.loop))
        {
            SetFrame(isCharacter1, animation, 0);
            characterState.attacking = false;
            characterState.hit = false;
        }

        // Set Animation
        characterState.animation = animationIndex;

        return true;
    }

    private void SetFrame(bool isCharacter1, AnimationsData.CharacterAnimation animation, int frame)
    {
        int framePhase = FindPhase(animation, frame);

        // Check if frame exists
        if (framePhase > animation.phases.Count - 1)
		{
            Debug.LogError("Frame Doesn't Exists!");
            return;
		}

        // If animation is over
        if (framePhase == -1)
        {
            // If already Idle
            if (animation.name == "Idle")
			{
                SetFrame(isCharacter1, animation, 0);
			}

            SetAnimation(isCharacter1, "Idle");
            return;
        }

        // Current Character State
        BattleGameState.CharacterState characterState = isCharacter1 ? battleManager.gameState.character1 : battleManager.gameState.character2;

        // Set Frame
        characterState.frame = frame;

        // Animation settings
        characterState.cancellable = animation.phases[framePhase].specialCancellable;

        // Change velocity
        if (animation.phases[framePhase].changeVelocity)
		{
            if (!characterState.mirrored)
			{
                characterState.velocityX = animation.phases[framePhase].velocityX;
			}
			else
			{
                characterState.velocityX = animation.phases[framePhase].velocityX * -1;
            }

            characterState.velocityY = animation.phases[framePhase].velocityY;
        }
    }

    public int FindPhase(AnimationsData.CharacterAnimation animation, int frame)
	{
        // End animation if animation has no frames
        if (animation.phases.Count == 0 || animation.phases[0].duration == 0)
        {
            return -1;
        }

        // Search Phase
        int phase = 0;

        while (frame + 1 > animation.phases[phase].duration)
        {
            frame -= animation.phases[phase].duration;
            phase++;

            // If the frame is higher than enire animation length therefore finished
            if (phase > animation.phases.Count - 1)
            {
                return -1;
            }
        }

        return phase;
    }

    private int FindAnimationIndexbyName(List<AnimationsData> animationsData, string animationName)
    {
        // Find animation index
        int animationIndex = -1;
        int fileIndex = 0;

        foreach (AnimationsData animations in animationsData)
        {
            animationIndex = animations.characterAnimations.FindIndex(x => x.name == animationName);

            // Found
            if (animationIndex != -1)
            {
                break;
            }

            fileIndex++;
        }

        return animationIndex + fileIndex * 100;
    }

    private AnimationsData.CharacterAnimation GetAnimation(List<AnimationsData> animationsData, int animationIndex)
    {
        // Animation Data
        AnimationsData.CharacterAnimation animation;

        if (animationIndex < 100)
        {
            animation = animationsData[0].characterAnimations[animationIndex];
        }
        else if (animationIndex < 200)
        {
            animation = animationsData[1].characterAnimations[animationIndex - 100];
        }
        else
        {
            animation = animationsData[2].characterAnimations[animationIndex - 200];
        }

        return animation;
    }

    private float GetAnimationPercentage(List<AnimationsData> animationsData, int animationIndex, int frame)
    {
        // Animation Data
        AnimationsData.CharacterAnimation animation = GetAnimation(animationsData, animationIndex);
        int phase = FindPhase(animation, frame);

        // If animation is not found
        if (phase == -1)
        {
            Debug.Log("Animation Not Found While Trying To Animate Character!");
            return 0f;
        }

        // Find Phase Percentage
        int upperCount = animation.phases[0].duration - 1;
        int lowerCount = 0;

        int i = 1;
        while (i <= phase)
        {
            lowerCount = upperCount + 1;
            upperCount += animation.phases[i].duration;
            i++;
        }

        float percentage = (float)(frame - lowerCount) / (float)(upperCount - lowerCount);

        // Find Animation Time
        float animationTime = Mathf.Lerp(animation.phases[phase].animStartTime, animation.phases[phase].animEndTime, percentage);

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

    public AnimationsData.CharacterAnimationPhase GetAnimationPhase(bool isCharacter1)
    {
        // Get Animations Data List
        List<AnimationsData> animationsData = isCharacter1 ? battleManager.character1Datas : battleManager.character2Datas;

        // Current Character State
        BattleGameState.CharacterState characterState = isCharacter1 ? battleManager.gameState.character1 : battleManager.gameState.character2;

        int animationPhase = FindPhase(GetAnimation(animationsData, characterState.animation), characterState.frame);

        return GetAnimation(animationsData, characterState.animation).phases[animationPhase];
    }

    public void CallSpecialFunctions()
	{
        // Character 1 Animation Data
        AnimationsData.CharacterAnimation character1Animation = GetAnimation(battleManager.character1Datas, battleManager.gameState.character1.animation);
        int character1AnimationPhase = FindPhase(character1Animation, battleManager.gameState.character1.frame);

        // Character 2 Animation Data
        AnimationsData.CharacterAnimation character2Animation = GetAnimation(battleManager.character2Datas, battleManager.gameState.character2.animation);
        int character2AnimationPhase = FindPhase(character2Animation, battleManager.gameState.character2.frame);

        // Character 1 Special Function
        if (character1Animation.phases[character1AnimationPhase].specialFunctions != null)
        {
            character1Animation.phases[character1AnimationPhase].specialFunctions.Invoke(true, battleManager.gameState);
        }

        // Character 2 Special Function
        if (character2Animation.phases[character2AnimationPhase].specialFunctions != null)
        {
            character2Animation.phases[character2AnimationPhase].specialFunctions.Invoke(false, battleManager.gameState);
        }
    }
}
