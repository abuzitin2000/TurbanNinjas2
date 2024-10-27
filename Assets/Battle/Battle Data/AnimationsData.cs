using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "GameData/Animations")]
public class AnimationsData : ScriptableObject
{
    [System.Serializable]
    public class CharacterAnimation
    {
        public string name;
        public string animationFileName;
        public bool loop;
        public List<CharacterAnimationPhase> phases;
    }

    [System.Serializable]
    public class CharacterAnimationPhase
    {
        public int duration;
        public float animStartTime;
        public float animEndTime;
        public bool specialCancellable;
        public bool changeVelocity;
        public int velocityX;
        public int velocityY;
        public List<HurtboxData> hurtBoxes;
        public List<HitboxData> hitBoxes;
        public List<collisionboxdata> collisionBoxes;
        public SpecialEvent specialFunctions;
    }

    [System.Serializable]
    public class HurtboxData
    {
        public int positionoffsetX;
        public int positionoffsetY;
        public int sizeX;
        public int sizeY;
    }

    [System.Serializable]
    public class HitboxData
    {
        public int positionoffsetX;
        public int positionoffsetY;
        public int sizeX;
        public int sizeY;
        public int damage;
        public int chipDamage;
        public int hitStun;
        public int blockStun;
        public int type;
        public int pushbackStrength;
        public int pushbackDuration;
        public int hitStop;
        public int juggleStrength;
    }

    [System.Serializable]
    public class collisionboxdata
    {
        public int positionoffsetX;
        public int positionoffsetY;
        public int sizeX;
        public int sizeY;
    }

    [SerializeField]
    public List<CharacterAnimation> characterAnimations;
}