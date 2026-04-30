using UnityEngine;
using UnityEngine.SceneManagement;


namespace Pacman
{
    public class GameHUDView : MonoBehaviour, IGameStateListener
    {
        [Header("HUD")]
        [SerializeField] private ScoreView m_ScoreView;
        [SerializeField] private LivesView m_LivesView;
        [SerializeField] private PelletsView m_PelletsView;
        [SerializeField] private PauseButtonView m_PauseButtonView;

        [Header("Joystick")]
        [SerializeField] private GameObject m_JoystickObject;

        [Header("Panels")]
        [SerializeField] private PausePanelView m_PausePanel;
        [SerializeField] private WinPanelView m_WinPanel;
        [SerializeField] private LosePanelView m_LosePanel;

        private GameViewModel m_ViewModel;

        public void Construct(GameViewModel viewModel, int totalPellets)
        {
            m_ViewModel = viewModel;

            m_PelletsView?.Initialise(totalPellets);

            m_ViewModel.OnScoreUpdated += m_ScoreView.UpdateScore;
            m_ViewModel.OnLivesUpdated += m_LivesView.UpdateLives;
            m_ViewModel.OnPelletsUpdated += m_PelletsView.UpdatePellets;
            m_ViewModel.OnStateChanged += OnGameStateChanged;

            m_PauseButtonView.OnPauseClicked += m_ViewModel.PauseGame;
            m_PausePanel.OnResumeClicked += m_ViewModel.ResumeGame;
            m_PausePanel.OnMenuClicked += GoToMenu;
            m_WinPanel.OnRestartClicked += RestartScene;
            m_WinPanel.OnMenuClicked += GoToMenu;
            m_LosePanel.OnRestartClicked += RestartScene;
            m_LosePanel.OnMenuClicked += GoToMenu;

            m_ScoreView?.UpdateScore(0);
            m_LivesView?.UpdateLives(m_ViewModel.Lives);

            m_ViewModel.StartGame();
        }

        public void OnGameStateChanged(GameState state)
        {
            m_PausePanel?.Hide();
            m_WinPanel?.Hide();
            m_LosePanel?.Hide();
            m_PauseButtonView?.Hide();

            switch (state)
            {
                case GameState.Playing:
                    m_PauseButtonView?.Show();
                    ShowJoystick();
                    Time.timeScale = 1f;
                    break;

                case GameState.Paused:
                    m_PausePanel?.Show();
                    HideJoystick();
                    Time.timeScale = 0f;
                    break;

                case GameState.Win:
                    m_WinPanel?.Show(
                        m_ViewModel.Score,
                        m_ViewModel.CollectedPellets,
                        m_ViewModel.TotalPellets);
                    HideJoystick();
                    Time.timeScale = 0f;
                    break;

                case GameState.Lose:
                    m_LosePanel?.Show(
                        m_ViewModel.Score,
                        m_ViewModel.CollectedPellets,
                        m_ViewModel.TotalPellets);
                    HideJoystick();
                    Time.timeScale = 0f;
                    break;
            }
        }

        private void ShowJoystick()
        {
            m_JoystickObject?.SetActive(true);
        }

        private void HideJoystick()
        {
            m_JoystickObject?.SetActive(false);
        }

        private void RestartScene()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().name);
        }

        private void GoToMenu()
        {
            Time.timeScale = 1f;
            // SceneManager.LoadScene("MainMenu");
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().name);
        }

        private void OnDestroy()
        {
            if (m_ViewModel == null) return;

            m_ViewModel.OnScoreUpdated -= m_ScoreView.UpdateScore;
            m_ViewModel.OnLivesUpdated -= m_LivesView.UpdateLives;
            m_ViewModel.OnPelletsUpdated -= m_PelletsView.UpdatePellets;
            m_ViewModel.OnStateChanged -= OnGameStateChanged;
        }
    }
}