using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/NinjaStats")]
public class StatsData : ScriptableObject
{
    public int health;
    public int forwardMoveSpeed;
    public int backwardMoveSpeed;
    public int jumpForce;
    public int jumpHorizontalSpeed;
    public int fallSpeed;
    public int forwardDashSpeed;
    public int backwardDashSpeed;
}