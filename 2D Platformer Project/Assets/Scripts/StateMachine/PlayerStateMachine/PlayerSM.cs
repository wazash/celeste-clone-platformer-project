using Assets.Scripts.Data;
using Assets.Scripts.StateMachine.PlayerStateMachine.States;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerSM : StateMachineBase
    {
        [field: Header("Components")]
        [field: SerializeField]
        public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField]
        public PlayerData PlayerData { get; private set; }
        [field: SerializeField]
        public Animator Animator { get; private set; }

        [Header("Ground Checker")]
        [SerializeField]
        private Transform groundChecker;
        [SerializeField]
        private Vector2 groundCheckerSize;
        [SerializeField]
        private LayerMask groundLayer;

        [Header("Wall Checker")]
        [SerializeField]
        private Transform wallChecker;
        [SerializeField]
        private Vector2 wallCheckerSize;

        [Header("Corner Grab Checker")]
        [SerializeField]
        private Transform topCornerGrabChecker;
        [SerializeField]
        private Vector2 topCornerGrabCheckerSize;
        [Space]
        [SerializeField]
        private Transform botCornerGrabChecker;
        [SerializeField]
        private Vector2 botCornerGrabCheckerSize;

        public Idling IdlingState { get; private set; }
        public Runing RuningState { get; private set; }
        public Raising RaisingState { get; private set; }
        public Falling FallingState { get; private set; }
        public WallGrabIdling WallGrabIdlingState { get; private set; }
        public WallGrabSliding WallGrabSlidingState { get; private set; }
        public WallGrabClimbing WallGrabClimbingState { get; private set; }
        public WallGrabCornerClimbing WallGrabCornerClimbingState { get; private set; }

        private void Awake()
        {
            #region Set States
            // Grounded States
            IdlingState = new Idling(this);
            RuningState = new Runing(this);
            // InAir States
            RaisingState = new Raising(this);
            FallingState = new Falling(this);
            // WallGrabbed States
            WallGrabIdlingState = new WallGrabIdling(this);
            WallGrabSlidingState = new WallGrabSliding(this);
            WallGrabClimbingState = new WallGrabClimbing(this); 
            WallGrabCornerClimbingState = new WallGrabCornerClimbing(this);
            #endregion
        }

        #region Overrided Methods
        protected override StateBase GetInitialState()
        {
            return IdlingState;
        }

        #endregion


        #region Own Methods
        public bool CheckGrounded()
        {
            return Physics2D.OverlapBox(groundChecker.position, groundCheckerSize, 0.0f, groundLayer);
        }

        public bool CheckCanGrabWall()
        {
            return Physics2D.OverlapBox(wallChecker.position, wallCheckerSize, 0, groundLayer);
        }

        public bool CheckCanCornerGrab()
        {
            bool isTopCollide = Physics2D.OverlapBox(topCornerGrabChecker.position, topCornerGrabCheckerSize, 0.0f, groundLayer);
            bool isBotCollide = Physics2D.OverlapBox(botCornerGrabChecker.position, botCornerGrabCheckerSize, 0.0f, groundLayer);

            return !isTopCollide && isBotCollide;
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
            // Ground Checker
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(groundChecker.position, groundCheckerSize);

            // Wall Checker
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(wallChecker.position, wallCheckerSize);

            //Corner Grab Checker
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(topCornerGrabChecker.position, topCornerGrabCheckerSize);

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(botCornerGrabChecker.position, botCornerGrabCheckerSize);
        } 
        #endregion
    }
}
