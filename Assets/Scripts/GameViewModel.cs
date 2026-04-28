using System;
using UnityEngine;

namespace Pacman
{
    public class GameViewModel
    {
        private readonly GameModel m_Model;

        public int Score => m_Model.Score;
        public int Lives => m_Model.Lives;
        public GameState CurrentState => m_Model.State;

        public event Action<int> OnScoreUpdated;
        public event Action<int> OnLivesUpdated;
        public event Action<GameState> OnStateChanged;


        public GameViewModel(GameModel model)
        {
            m_Model = model;
            m_Model.OnScoreChanged += score => OnScoreUpdated?.Invoke(score);
            m_Model.OnLivesChanged += lives => OnLivesUpdated?.Invoke(lives);
            m_Model.OnStateChanged += state => OnStateChanged?.Invoke(state);
            m_Model.OnAllPelletsCollected += () => m_Model.ChangeState(GameState.Win);
        }


        public void StartGame() => m_Model.ChangeState(GameState.Playing);
        public void PauseGame() => m_Model.ChangeState(GameState.Paused);
        public void ResumeGame() => m_Model.ChangeState(GameState.Playing);


        public void NotifyCollected(int scoreValue) => m_Model.AddScore(scoreValue);

        public void NotifyPlayerDied() => m_Model.LoseLife();

    }
}
