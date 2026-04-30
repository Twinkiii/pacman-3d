using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Pacman
{
    public class MainMenuView : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button m_PlayButton;
        [SerializeField] private Button m_QuitButton;

        [Header("Settings")]
        [SerializeField] private string m_GameSceneName = "GameScene";

        private void Awake()
        {
            m_PlayButton?.onClick.AddListener(OnPlayClicked);
            m_QuitButton?.onClick.AddListener(OnQuitClicked);
        }

        private void OnPlayClicked()
        {
            Sound.MenuClick.Play();
            SceneManager.LoadScene(m_GameSceneName);
        }

        private void OnQuitClicked()
        {
            Sound.MenuClick.Play();
            Application.Quit();
        }

        private void OnDestroy()
        {
            m_PlayButton?.onClick.RemoveAllListeners();
            m_QuitButton?.onClick.RemoveAllListeners();
        }
    }
}