using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Data/Player/Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Moving data")]
        public float Speed = 5.0f;
        [Range(1.0f, 3.0f)]
        public float DecelerationFactor = 1.0f;

    }
}
