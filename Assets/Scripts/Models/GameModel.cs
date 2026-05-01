using Pacman.Common;
using System;

namespace Pacman.Models
{
    public class GameModel
    {
        //Data____
        public int Score { get; private set; }
        public int Lives { get; private set; }
        public int TotalPellets { get; private set; }
        public int CollectedPellets { get; private set; }

        public int SpendablePellets { get; private set; }

        public GameState State { get; private set; }

        //Actions____
        public event Action<int> OnScoreChanged;
        public event Action<int> OnLivesChanged;
        public event Action<GameState> OnStateChanged;
        public event Action OnAllPelletsCollected;
        public event Action<int> OnPelletsChanged;
        public event Action<int> OnSpendablePelletsChanged;


        public GameModel(int startLives, int totalPellets)
        {
            Lives = startLives;
            TotalPellets = totalPellets;
            CollectedPellets = 0;
            SpendablePellets = 0;
            Score = 0;
            State = GameState.Start;
        }

        public void AddScore(int value)
        {
            Score += value;
            OnScoreChanged?.Invoke(Score);

            CollectedPellets++;
            SpendablePellets++;
            OnPelletsChanged?.Invoke(CollectedPellets);
            OnSpendablePelletsChanged?.Invoke(SpendablePellets);

            if (CollectedPellets >= TotalPellets)
                OnAllPelletsCollected?.Invoke();

        }

        public void AddLife()
        {
            Lives++;
            OnLivesChanged?.Invoke(Lives);
        }


        public void LoseLife()
        {
            Lives--;
            OnLivesChanged?.Invoke(Lives);

            if (Lives <= 0)
                ChangeState(GameState.Lose);
        }

        public void SpendCollectedPellets(int amount)
        {
            SpendablePellets -= amount;
            if (SpendablePellets < 0) SpendablePellets = 0;
            OnSpendablePelletsChanged?.Invoke(SpendablePellets);
        }

        public void ChangeState(GameState newState)
        {
            if (State == newState)
                return;

            State = newState;
            OnStateChanged?.Invoke(State);
        }
    }
}
