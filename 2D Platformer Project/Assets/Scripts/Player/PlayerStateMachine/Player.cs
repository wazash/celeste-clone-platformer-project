using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
     

    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    #endregion

    #region Check variables
    [SerializeField]
    protected Transform groundCheck; 
    #endregion

    #region Other variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; } // 0 - left, 1 - right 


    private Vector2 workspace;
    #endregion


    #region Unity callback functions
    private void Awake()
    {
        // Create State Machine
        StateMachine = new PlayerStateMachine();

        //Create States
        IdleState = new PlayerIdleState(this, StateMachine, playerData, AnimationName.Idling.ToString());
        MoveState = new PlayerMoveState(this, StateMachine, playerData, AnimationName.Running.ToString());
        JumpState = new PlayerJumpState(this, StateMachine, playerData, AnimationName.InAir.ToString());
        InAirState = new PlayerInAirState(this, StateMachine, playerData, AnimationName.InAir.ToString());
    }

    private void Start()
    {
        // Get components
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rigidbody = GetComponent<Rigidbody2D>();

        // Initialize starting state
        StateMachine.Initialize(IdleState);

        // Set facing direction to right
        FacingDirection = 1;
    }

    private void Update()
    {
        // Calculate Logic
        CurrentVelocity = Rigidbody.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        // Calculate Physics
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion


    #region Set methods

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        Rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        Rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    } 
    #endregion

    #region Check methods
    public bool CheckIsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.GroundCheckRadius, playerData.WhatIsGround);
    }
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
            Flip();
    }
    #endregion

    #region Other methods
    private void OnDrawGizmos()
    {
        Gizmos.color = CheckIsGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, playerData.GroundCheckRadius);
    }
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    } 
    #endregion
}
