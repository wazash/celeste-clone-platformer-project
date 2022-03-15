using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Data/Player/Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Moving data")]
        public float Speed = 5.0f;
        public float MinVelocityX = 0.01f;
        [Range(-14f, -5f)] // If less then -14 some issues with capturing exit falling velcity
        public float MinVelocityY = -14.0f;
        [Range(0f, 1f)]
        public float DecelerationFactor = 1.0f;
        [HideInInspector]
        public bool IsFacingRight = true;

        [Header("Jumping data")]
        public float JumpForce = 8.0f;
        public float CoyoteJumpForce = 8.0f;
        [Range(0f, 0.5f)]
        public float CoyoteTime = 0.1f;
        [Range(0f, 0.5f)]
        public float JumpBufferTime = 0.1f;
        [Range (0f, 2.5f)]
        public float WallJumpUpMiltiplier;
        [Range(-100, 0)]
        public float FallingSpeedRequiredToPlayLandingParticle;

        [Header("Walling data")]
        [Range(0f, 5f)]
        public float DefaultGravityScale = 2f;
        [Range(0f, 1f)]
        public float SlidingGravityFactor = 2f;
        [Range(0f, 5f)]
        public float ClimbingGravityFactor = 2f;
        public float Stamina = 5f;
        public Vector2 ChangePositionOffset;

        [Header("Input Axis names")]
        public AxisName HorizontalAxis = AxisName.Horizontal;
        public AxisName VerticalAxis = AxisName.Vertical;
        public AxisName JumpAxis = AxisName.Jump;
        public AxisName DashAxis = AxisName.Dash;
        public AxisName GrabWallAxis = AxisName.GrabWall;

        private void OnEnable()
        {
            IsFacingRight = true;
        }
    }

    public enum AxisName
    {
        Horizontal,
        Vertical,
        Jump,
        Dash,
        GrabWall
    }
}
