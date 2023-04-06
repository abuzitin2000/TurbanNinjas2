using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Character")]
public class CharacterData : ScriptableObject
{
    [System.Serializable]
    public class characterdata
    {
        public int moveSpeed;
        public int jumpForce;
        public int jumpHorizontalSpeed;
        public int fallSpeed;
    }

    [SerializeField]
    public characterdata characterData;
}