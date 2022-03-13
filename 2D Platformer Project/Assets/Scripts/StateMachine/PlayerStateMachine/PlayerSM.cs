using Assets.Scripts.Data;
using Assets.Scripts.StateMachine.PlayerStateMachine.States;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerSM : StateMachineBase
    {
        [field:Header("Components")]
        [field:SerializeField]
        public Rigidbody2D Rigidbody { get; private set; }
        [field:SerializeField]
        public PlayerData PlayerData { get; private set; }
        [field:SerializeField]
        public Animator Animator { get; private set; }

        [Header("Ground Checker")]
        [SerializeField]
        private Transform groundChecker;
        [SerializeField]
        private Vector2 groundCheckerSize;
        [SerializeField]
        private LayerMask groundLayer;

        public Idling IdlingState { get; private set; }
        public Runing RuningState { get; private set; }
        public Raising RaisingState { get; private set; }
        public Falling FallingState { get; private set; }

        private void Awake()
        {
            IdlingState = new Idling(this);
            RuningState = new Runing(this);
            RaisingState = new Raising(this); 
            FallingState = new Falling(this);
        }

        protected override StateBase GetInitialState()
        {
            return IdlingState;
        }

        public bool CheckGrounded()
        {
            return Physics2D.OverlapBox(groundChecker.position, groundCheckerSize, 0.0f, groundLayer);
        }

        public void FlipDirection(Vector2 input)
        {
            if (input.x < 0 && PlayerData.IsFacingRight)
            {
                PlayerData.IsFacingRight = false;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (input.x > 0 && !PlayerData.IsFacingRight)
            {
                PlayerData.IsFacingRight = true;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(groundChecker.position, groundCheckerSize);
        }
    }
}
