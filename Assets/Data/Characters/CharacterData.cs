using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "GameData/Character")]
public class CharacterData : ScriptableObject
{
    [System.Serializable]
    public class characterdata
    {
        public int health;
        public int forwardMoveSpeed;
        public int backwardMoveSpeed;
        public int jumpForce;
        public int jumpHorizontalSpeed;
        public int fallSpeed;
    }

    [System.Serializable]
    public class characteranimation
    {
        public string name;
        public bool loop;
        public List<characteranimationframe> frames;
    }

    [System.Serializable]
    public class characteranimationframe
    {
        public int duration;
        public Sprite sprite;
        public bool specialCancellable;
        public bool changeVelocity;
        public int velocityX;
        public int velocityY;
        public List<hurtboxdata> hurtBoxes;
        public List<hitboxdata> hitBoxes;
        public List<collisionboxdata> collisionBoxes;
        public UnityEvent specialFunctions;
    }

    [System.Serializable]
    public class hurtboxdata
    {
        public int positionoffsetX;
        public int positionoffsetY;
        public int sizeX;
        public int sizeY;
    }

    [System.Serializable]
    public class hitboxdata
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
    public characterdata stats;

    [SerializeField]
    public List<characteranimation> characterAnimations;
}