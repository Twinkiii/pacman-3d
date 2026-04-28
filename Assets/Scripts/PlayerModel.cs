using System;
using UnityEngine;

namespace Pacman
{

    public class PlayerModel
    {
        public Vector3 Position { get; private set; }
        public Vector2 MoveDirection { get; private set; }
        public float Speed { get; private set; }
        public bool IsAlive { get; private set; }

        public event Action<Vector3> OnPositionChanged;
        public event Action OnDie;

        public PlayerModel(float speed)
        {
            Speed = speed;
            IsAlive = true;
        }

        public void UpdatePosition(Vector3 newPosition)
        {
            Position = newPosition;
            OnPositionChanged?.Invoke(Position);
        }

        public void Die()
        {
            IsAlive = false;
            OnDie?.Invoke();
        }

        public void SetDirection(Vector2 dir)
        {
            MoveDirection = dir;
        }

        public void Respawn(Vector3 spawnPoint)
        {
            IsAlive = true;
            UpdatePosition(spawnPoint);
        }
    }
}