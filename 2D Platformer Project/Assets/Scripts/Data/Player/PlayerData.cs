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

    [Header("Wall Jump State")]
    public float WallJumpVelocity = 15f;
    public float WallJumpTime = 0.3f;
    public Vector2 WallJumpAngle = new Vector2(1, 2);

    [Header("InAir State")]
    [Range(0f, 0.5f)]
    public float CoyoteTime = 0.1f;
    public float MaxFaliingSpeed = 10;
    [Range(0f, 1.0f)]
    public float JumpVelocityReductionFactor = 0.1f;

    [Header("Wall Slide State")]
    [Range(0f, 10f)]
    public float WallSlideVelocity = 2.5f;

    [Header("Wall Climb State")]
    public float WallClimbVelocity = 3f;

    [Header("Ledge Climb State")]
    public Vector2 StartOffSet;
    public Vector2 StopOffSet;

    [Header("Check Variables")]
    [Range(0.0f, 1.0f)]
    public float WallCheckDistace = 0.2f;
    [Range(0.0f, 2.0f)]
    public float GroundCheckRadius = 0.3f;
    public LayerMask WhatIsGround;
}

public enum AnimationName
{
    Idling,
    Running,
    InAir,
    WallGrab,
    WallSlide,
    WallClimb,
    Ledge
}