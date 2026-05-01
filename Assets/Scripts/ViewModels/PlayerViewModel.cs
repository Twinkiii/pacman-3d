using System;
using UnityEngine;
using Pacman.Models;


namespace Pacman.ViewModels
{
    public class PlayerViewModel
    {
        private readonly PlayerModel m_Model;

        public float Speed => m_Model.Speed;
        public bool IsAlive => m_Model.IsAlive;

        public event Action OnPlayerDied;

        public PlayerViewModel(PlayerModel model)
        {
            m_Model = model;
            m_Model.OnDie += () => OnPlayerDied?.Invoke();
        }
        
        public void ReportPosition(Vector3 newPosition) => m_Model.UpdatePosition(newPosition);
        public void SetMoveDirection(Vector2 dir) => m_Model.SetDirection(dir);
        public void NotifyHitEnemy() 
        {
            Debug.Log("PlayerViewModel: NotifyHitEnemy called");
            m_Model.Die();
        }
        public void Respawn(Vector3 spawnPoint) => m_Model.Respawn(spawnPoint);
    }
}
