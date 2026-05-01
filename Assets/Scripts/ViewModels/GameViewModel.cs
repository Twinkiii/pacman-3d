using Pacman.Common;
using Pacman.Models;
using System;



namespace Pacman.ViewModels
{
    public class GameViewModel
    {
        private readonly GameModel m_Model;

        public int Score => m_Model.Score;
        public int Lives => m_Model.Lives;
        public int CollectedPellets => m_Model.CollectedPellets;
        public int TotalPellets => m_Model.TotalPellets;
        public GameState CurrentState => m_Model.State;

        public event Action<int> OnScoreUpdated;
        public event Action<int> OnLivesUpdated;
        public event Action<int> OnPelletsUpdated;
        public event Action<GameState> OnStateChanged;


        public GameViewModel(GameModel model)
        {
            m_Model = model;
            m_Model.OnScoreChanged += score => OnScoreUpdated?.Invoke(score);
            m_Model.OnLivesChanged += lives => OnLivesUpdated?.Invoke(lives);
            m_Model.OnPelletsChanged += pellets => OnPelletsUpdated?.Invoke(pellets);
            m_Model.OnStateChanged += state => OnStateChanged?.Invoke(state);
            m_Model.OnAllPelletsCollected += () => m_Model.ChangeState(GameState.Win);
        }


        public void StartGame() => m_Model.ChangeState(GameState.Playing);
        public void PauseGame() => m_Model.ChangeState(GameState.Paused);
        public void ResumeGame() => m_Model.ChangeState(GameState.Playing);
        public void ConsumeAllSpendablePellets() => m_Model.ConsumeAllSpendablePellets();


        public void NotifyCollected(int scoreValue) => m_Model.AddScore(scoreValue);

        public void NotifyPlayerDied() => m_Model.LoseLife();

    }
}
