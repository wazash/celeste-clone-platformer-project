using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Data/Player/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float MovementVelocity = 5.0f;
    [Range(0f, 0.1f)]
    public float MinVelocityX = 0.01f;

    [Header("Jump State")]
    public float JumpVelocity = 8.0f;
    public int AmountOfJumps = 1;
    [Range(0f, 0.1f)]
    public float MinGroundedVelocityY = 0.01f;

    [Header("InAir State")]
    [Range(0f, 0.5f)]
    public float CoyoteTime = 0.1f;

    [Header("Check Variables")]
    [Range(0.0f, 2.0f)]
    public float GroundCheckRadius = 0.3f;
    public LayerMask WhatIsGround;
}

public enum AnimationName
{
    Idling,
    Running,
    InAir
}