using System;
using UnityEngine;

namespace Pacman.Common
{
    public enum EnemyBehaviourType
    {
        DirectChase,
        PatrolAndChase,
        Ambush
    }

    public class EnemyModel
    {
        public EnemyBehaviourType BehaviourType { get; }
        public Vector3 SpawnPosition { get; }
        public Vector3 Position { get; private set; }
        public bool IsChasing { get; private set; }
        public float Speed { get; private set; }

        public event Action<bool> OnChaseStateChanged;

        public EnemyModel(EnemyBehaviourType behaviourType, Vector3 spawnPosition, float speed)
        {
            BehaviourType = behaviourType;
            SpawnPosition = spawnPosition;
            Position = spawnPosition;
            Speed = speed;
        }

        public void UpdatePosition(Vector3 newPosition) => Position = newPosition;

        public void SetChasing(bool isChasing)
        {
            if (IsChasing == isChasing) return;
            IsChasing = isChasing;
            OnChaseStateChanged?.Invoke(IsChasing);
        }

        public void ResetToSpawn()
        {
            UpdatePosition(SpawnPosition);
            SetChasing(false);
        }

    }
}
