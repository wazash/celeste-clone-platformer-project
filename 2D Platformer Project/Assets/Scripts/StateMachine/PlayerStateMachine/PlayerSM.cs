using Assets.Scripts.Data.Player;
using Assets.Scripts.StateMachine.PlayerStateMachine.States;
using DG.Tweening;
using System;
using System.Collections;
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
        private SpriteRenderer spriteRenderer;
        #endregion

        public bool CanPlayerControll { get; private set; } = true;

        public float CoyoteJumpTimer { get; set; }

        public float JumpBufferTimer { get; set; }

        public float ExitVelocityY { get; set; }

        [SerializeField]
        private FloatVariable backgroundAnimationTime;
        public float backgroundAnimationTimeOffset = 0.2f;

        #region Particles

        [field: Header("Particles")]
        [field: SerializeField]
        public ParticleSystem FootDustPS { get; private set; }
        [field: SerializeField]
        public ParticleSystem JumpedDust { get; private set; }
        [field: SerializeField]
        public ParticleSystem LandingDust { get; private set; }
        [field: SerializeField]
        public ParticleSystem DeathParticle { get; private set; }
        [field: SerializeField]
        public ParticleSystem SpawnParticle { get; private set; }

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

        public static Action OnPlayerDeath;

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
            spriteRenderer = GetComponent<SpriteRenderer>();

            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            // Global jump buffer counter
            JumpBufferTimer -= Time.deltaTime;
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

        /// <summary>
        /// Disable player controlls and stop his velocity
        /// </summary>
        public void StopPlayer()
        {
            CanPlayerControll = false;
            Rigidbody.velocity = Vector2.zero;
        }

        /// <summary>
        ///  Enable player controlls
        /// </summary>
        public void StartPlayer()
        {
            CanPlayerControll = true;
        }

        /// <summary>
        /// Set player position at current spawn point position and play spawning particles
        /// </summary>
        private void RespawnPlayer()
        {
            transform.position = PlayerData.CurrentSpawnPoint.position;
            SpawnParticle.Play();
        }

        public void DeathSequence()
        {
            StopPlayer();
            Rigidbody.gravityScale = 0;
            Vector3 currentScale = transform.localScale;

            StartCoroutine(Death(PlayerData.TimeAfterDeath, PlayerData.TimeBeforeSpawn, currentScale));

        }

        /// <summary>
        /// Start player death sequence includin respawning
        /// </summary>
        /// <param name="timeAfterDeath"></param>
        /// <param name="timeBeforeSpawn"></param>
        /// <param name="currentScale"></param>
        /// <returns></returns>
        private IEnumerator Death(float timeAfterDeath, float timeBeforeSpawn, Vector3 currentScale)
        {
            transform.DOScale(0, 0.25f).OnComplete(() => DeathParticle.Play());

            yield return new WaitForSeconds(PlayerData.DyingAnimationTime);

            OnPlayerDeath?.Invoke();
            yield return new WaitForSeconds(backgroundAnimationTime.value + backgroundAnimationTimeOffset);

            RespawnPlayer();

            yield return new WaitForSeconds(timeBeforeSpawn);

            transform.DOScale(currentScale, 0.20f).OnComplete(() =>
            {
                Rigidbody.gravityScale = PlayerData.DefaultGravityScale;
                StartPlayer();
            });

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
