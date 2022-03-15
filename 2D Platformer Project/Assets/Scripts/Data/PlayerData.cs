using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Data/Player/Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Moving data")]
        public float Speed = 5.0f;
        public float MinVelocityX = 0.01f;
        [Range(0f, 1f)]
        public float DecelerationFactor = 1.0f;
        [HideInInspector]
        public bool IsFacingRight = true;

        [Header("Jumping data")]
        public float JumpForce = 7.0f;
        [Range (0f, .5f)]
        public float WallJumpUpMiltiplier;

        [Header("Walling data")]
        [Range(0f, 5f)]
        public float DefaultGravityScale = 2f;
        [Range (0f, 1f)]
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
