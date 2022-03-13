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

        private void OnEnable()
        {
            IsFacingRight = true;
        }
    }
}
