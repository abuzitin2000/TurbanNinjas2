using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public BattleManager battleManager;
    public CollisionManager collisionManager;

    public SpriteRenderer player1SpriteRenderer;
    public SpriteRenderer player2SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AnimateCharacters()
    {
        player1SpriteRenderer.sprite = battleManager.player1Data.characterAnimations[battleManager.gameState.player1.animation].frames[FindSprite(battleManager.player1Data, battleManager.gameState.player1.animation, battleManager.gameState.player1.frame)].sprite;
        player2SpriteRenderer.sprite = battleManager.player2Data.characterAnimations[battleManager.gameState.player2.animation].frames[FindSprite(battleManager.player2Data, battleManager.gameState.player2.animation, battleManager.gameState.player2.frame)].sprite;
        
        player1SpriteRenderer.flipX = battleManager.gameState.player1.mirrored;
        player2SpriteRenderer.flipX = battleManager.gameState.player2.mirrored;
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
        int framePhase = FindSprite(characterData, anim, frame);

        // Check if frame exists
        if (framePhase > characterData.characterAnimations[anim].frames.Count - 1)
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
        characterState.cancellable = characterData.characterAnimations[anim].frames[framePhase].specialCancellable;

        // Change velocity
        if (characterData.characterAnimations[anim].frames[framePhase].changeVelocity)
		{
            if (!characterState.mirrored)
			{
                characterState.velocityX = characterData.characterAnimations[anim].frames[framePhase].velocityX;
			}
			else
			{
                characterState.velocityX = characterData.characterAnimations[anim].frames[framePhase].velocityX * -1;
            }

            characterState.velocityY = characterData.characterAnimations[anim].frames[framePhase].velocityY;
        }
    }

    public void AdvanceFrames()
    {
        // Player 1
        SetFrame(battleManager.gameState.player1, battleManager.player1Data, battleManager.gameState.player1.animation, battleManager.gameState.player1.frame + 1);

        // Player 2
        SetFrame(battleManager.gameState.player2, battleManager.player2Data, battleManager.gameState.player2.animation, battleManager.gameState.player2.frame + 1);
    }

    public int FindSprite(CharacterData characterData, int anim, int frame)
	{
        int spriteSearch = 0;

        while (frame + 1 > characterData.characterAnimations[anim].frames[spriteSearch].duration)
        {
            frame -= characterData.characterAnimations[anim].frames[spriteSearch].duration;
            spriteSearch++;

            // If the frame is higher than enire animation length
            if (spriteSearch > characterData.characterAnimations[anim].frames.Count - 1)
            {
                return -1;
            }
        }

        return spriteSearch;
    }

    private void OnDrawGizmos()
    {
        // Player 1 Hurtbox
        foreach (CharacterData.hurtboxdata hurtbox in battleManager.player1Data.characterAnimations[battleManager.gameState.player1.animation].frames[FindSprite(battleManager.player1Data, battleManager.gameState.player1.animation, battleManager.gameState.player1.frame)].hurtBoxes)
        {
            int mirrorhurtboxPositionoffsetX = battleManager.gameState.player1.positionX > battleManager.gameState.player2.positionX ? hurtbox.positionoffsetX * -1 : hurtbox.positionoffsetX;
            int mirrorhurtboxsizeX = battleManager.gameState.player1.positionX > battleManager.gameState.player2.positionX ? hurtbox.sizeX * -1 : hurtbox.sizeX;

            Gizmos.color = new Color(0, 1, 1, 0.5f);
            Gizmos.DrawCube(new Vector3((battleManager.gameState.player1.positionX + mirrorhurtboxPositionoffsetX + (mirrorhurtboxsizeX / 2f)) / BattleManager.renderRatio, (battleManager.gameState.player1.positionY + hurtbox.positionoffsetY - (hurtbox.sizeY / 2f)) / BattleManager.renderRatio, 0), new Vector2(mirrorhurtboxsizeX / BattleManager.renderRatio, hurtbox.sizeY / BattleManager.renderRatio));
        }

        // Player 1 Hitbox
        foreach (CharacterData.hitboxdata hitbox in battleManager.player1Data.characterAnimations[battleManager.gameState.player1.animation].frames[FindSprite(battleManager.player1Data, battleManager.gameState.player1.animation, battleManager.gameState.player1.frame)].hitBoxes)
        {
            int mirrorhitboxPositionoffsetX = battleManager.gameState.player1.positionX > battleManager.gameState.player2.positionX ? hitbox.positionoffsetX * -1 : hitbox.positionoffsetX;
            int mirrorhitboxsizeX = battleManager.gameState.player1.positionX > battleManager.gameState.player2.positionX ? hitbox.sizeX * -1 : hitbox.sizeX;

            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(new Vector3((battleManager.gameState.player1.positionX + mirrorhitboxPositionoffsetX + (mirrorhitboxsizeX / 2f)) / BattleManager.renderRatio, (battleManager.gameState.player1.positionY + hitbox.positionoffsetY - (hitbox.sizeY / 2f)) / BattleManager.renderRatio, 0), new Vector2(mirrorhitboxsizeX / BattleManager.renderRatio, hitbox.sizeY / BattleManager.renderRatio));
        }

        // Player 1 Collisionbox
        foreach (CharacterData.collisionboxdata collisionbox in battleManager.player1Data.characterAnimations[battleManager.gameState.player1.animation].frames[FindSprite(battleManager.player1Data, battleManager.gameState.player1.animation, battleManager.gameState.player1.frame)].collisionBoxes)
        {
            int mirrorcollisionboxPositionoffsetX = battleManager.gameState.player1.positionX > battleManager.gameState.player2.positionX ? collisionbox.positionoffsetX * -1 : collisionbox.positionoffsetX;
            int mirrorcollisionboxsizeX = battleManager.gameState.player1.positionX > battleManager.gameState.player2.positionX ? collisionbox.sizeX * -1 : collisionbox.sizeX;

            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(new Vector3((battleManager.gameState.player1.positionX + mirrorcollisionboxPositionoffsetX + (mirrorcollisionboxsizeX / 2f)) / BattleManager.renderRatio, (battleManager.gameState.player1.positionY + collisionbox.positionoffsetY - (collisionbox.sizeY / 2f)) / BattleManager.renderRatio, 0), new Vector2(mirrorcollisionboxsizeX / BattleManager.renderRatio, collisionbox.sizeY / BattleManager.renderRatio));
        }

        // Player 2 Hurtbox
        foreach (CharacterData.hurtboxdata hurtbox in battleManager.player2Data.characterAnimations[battleManager.gameState.player2.animation].frames[FindSprite(battleManager.player2Data, battleManager.gameState.player2.animation, battleManager.gameState.player2.frame)].hurtBoxes)
        {
            int mirrorhurtboxPositionoffsetX = battleManager.gameState.player1.positionX < battleManager.gameState.player2.positionX ? hurtbox.positionoffsetX * -1 : hurtbox.positionoffsetX;
            int mirrorhurtboxsizeX = battleManager.gameState.player1.positionX < battleManager.gameState.player2.positionX ? hurtbox.sizeX * -1 : hurtbox.sizeX;

            Gizmos.color = new Color(0, 1, 1, 0.5f);
            Gizmos.DrawCube(new Vector3((battleManager.gameState.player2.positionX + mirrorhurtboxPositionoffsetX + (mirrorhurtboxsizeX / 2f)) / BattleManager.renderRatio, (battleManager.gameState.player2.positionY + hurtbox.positionoffsetY - (hurtbox.sizeY / 2f)) / BattleManager.renderRatio, 0), new Vector2(mirrorhurtboxsizeX / BattleManager.renderRatio, hurtbox.sizeY / BattleManager.renderRatio));
        }

        // Player 2 Hitbox
        foreach (CharacterData.hitboxdata hitbox in battleManager.player2Data.characterAnimations[battleManager.gameState.player2.animation].frames[FindSprite(battleManager.player2Data, battleManager.gameState.player2.animation, battleManager.gameState.player2.frame)].hitBoxes)
        {
            int mirrorhitboxPositionoffsetX = battleManager.gameState.player1.positionX < battleManager.gameState.player2.positionX ? hitbox.positionoffsetX * -1 : hitbox.positionoffsetX;
            int mirrorhitboxsizeX = battleManager.gameState.player1.positionX < battleManager.gameState.player2.positionX ? hitbox.sizeX * -1 : hitbox.sizeX;

            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(new Vector3((battleManager.gameState.player2.positionX + mirrorhitboxPositionoffsetX + (mirrorhitboxsizeX / 2f)) / BattleManager.renderRatio, (battleManager.gameState.player2.positionY + hitbox.positionoffsetY - (hitbox.sizeY / 2f)) / BattleManager.renderRatio, 0), new Vector2(mirrorhitboxsizeX / BattleManager.renderRatio, hitbox.sizeY / BattleManager.renderRatio));
        }

        // Player 2 Collisionbox
        foreach (CharacterData.collisionboxdata collisionbox in battleManager.player2Data.characterAnimations[battleManager.gameState.player2.animation].frames[FindSprite(battleManager.player2Data, battleManager.gameState.player2.animation, battleManager.gameState.player2.frame)].collisionBoxes)
        {
            int mirrorcollisionboxPositionoffsetX = battleManager.gameState.player2.positionX > battleManager.gameState.player1.positionX ? collisionbox.positionoffsetX * -1 : collisionbox.positionoffsetX;
            int mirrorcollisionboxsizeX = battleManager.gameState.player2.positionX > battleManager.gameState.player1.positionX ? collisionbox.sizeX * -1 : collisionbox.sizeX;

            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(new Vector3((battleManager.gameState.player2.positionX + mirrorcollisionboxPositionoffsetX + (mirrorcollisionboxsizeX / 2f)) / BattleManager.renderRatio, (battleManager.gameState.player2.positionY + collisionbox.positionoffsetY - (collisionbox.sizeY / 2f)) / BattleManager.renderRatio, 0), new Vector2(mirrorcollisionboxsizeX / BattleManager.renderRatio, collisionbox.sizeY / BattleManager.renderRatio));
        }
    }
}
