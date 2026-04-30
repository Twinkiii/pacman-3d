using System;

namespace Pacman
{
    public class GameModel
    {
        //Data____
        public int Score { get; private set; }
        public int Lives { get; private set; }
        public int TotalPellets { get; private set; }
        public int CollectedPellets { get; private set; }
        public GameState State { get; private set; }

        //Actions____
        public event Action<int> OnScoreChanged;
        public event Action<int> OnLivesChanged;
        public event Action<GameState> OnStateChanged;
        public event Action OnAllPelletsCollected;
        public event Action<int> OnPelletsChanged;


        public GameModel(int startLives, int totalPellets)
        {
            Lives = startLives;
            TotalPellets = totalPellets;
            CollectedPellets = 0;
            Score = 0;
            State = GameState.Start;
        }

        public void AddScore(int value)
        {
            Score += value;
            OnScoreChanged?.Invoke(Score);

            CollectedPellets++;
            OnPelletsChanged?.Invoke(CollectedPellets);

            if (CollectedPellets >= TotalPellets)
                OnAllPelletsCollected?.Invoke();

        }

        public void LoseLife()
        {
            Lives--;
            OnLivesChanged?.Invoke(Lives);

            if (Lives <= 0)
                ChangeState(GameState.Lose);
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
