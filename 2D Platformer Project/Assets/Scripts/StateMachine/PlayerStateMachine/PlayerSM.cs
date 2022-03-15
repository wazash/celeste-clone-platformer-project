using Assets.Scripts.Data;
using Assets.Scripts.StateMachine.PlayerStateMachine.States;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerSM : StateMachineBase
    {
        #region Components
        [field: Header("Components")]
        [field: SerializeField]
        public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField]
        public PlayerData PlayerData { get; private set; }
        [field: SerializeField]
        public Animator Animator { get; private set; }
        #endregion

        #region Particles
        [field: Header("Particles")]
        [field: SerializeField]
        public ParticleSystem FootDustPS { get; private set; }
        [field: SerializeField]
        public ParticleSystem JumpedDust { get; private set; }
        #endregion

        #region Checkers
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
        #endregion

        #region States
        public Idling IdlingState { get; private set; }
        public Runing RuningState { get; private set; }
        public Raising RaisingState { get; private set; }
        public Falling FallingState { get; private set; }
        public WallGrabIdling WallGrabIdlingState { get; private set; }
        public WallGrabSliding WallGrabSlidingState { get; private set; }
        public WallGrabClimbing WallGrabClimbingState { get; private set; }
        public WallGrabCornerClimbing WallGrabCornerClimbingState { get; private set; } 
        #endregion

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
        // Initialization start state
        protected override StateBase GetInitialState()
        {
            return IdlingState;
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }


        #endregion


        #region Own Methods
        /// <summary>
        /// Check if player is grounded. Use GroundedChecker game object to specify position
        /// </summary>
        /// <returns></returns>
        public bool CheckGrounded()
        {
            return Physics2D.OverlapBox(groundChecker.position, groundCheckerSize, 0.0f, groundLayer);
        }

        /// <summary>
        /// Check if player is in position that allows grab wall. Use WallChecker to specify position
        /// </summary>
        /// <returns></returns>
        public bool CheckCanGrabWall()
        {
            return Physics2D.OverlapBox(wallChecker.position, wallCheckerSize, 0, groundLayer);
        }
        /// <summary>
        /// Check if player can climb on a ledge. If top box is not colliding wall and bot is colliding wall, then player can climb.
        /// </summary>
        /// <returns></returns>
        public bool CheckCanCornerGrab()
        {
            bool isTopCollide = Physics2D.OverlapBox(topCornerGrabChecker.position, topCornerGrabCheckerSize, 0.0f, groundLayer);
            bool isBotCollide = Physics2D.OverlapBox(botCornerGrabChecker.position, botCornerGrabCheckerSize, 0.0f, groundLayer);

            return !isTopCollide && isBotCollide;
        }

        /// <summary>
        /// Fliping player
        /// </summary>
        /// <param name="input"></param>
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

            // Corner Grab Checker
            // Top box
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(topCornerGrabChecker.position, topCornerGrabCheckerSize);
            // Bottom box
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(botCornerGrabChecker.position, botCornerGrabCheckerSize);
        }
        #endregion
    }
}
