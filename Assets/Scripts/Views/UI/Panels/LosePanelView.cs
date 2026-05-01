using UnityEngine;
using UnityEngine.UI;
using System;

namespace Pacman.Views.UI.Panels
{
    public class LosePanelView : MonoBehaviour
    {
        [SerializeField] private Text m_TitleText;
        [SerializeField] private Text m_ScoreText;
        [SerializeField] private Text m_PelletsText;
        [SerializeField] private Button m_RestartButton;
        [SerializeField] private Button m_MenuButton;

        public event Action OnRestartClicked;
        public event Action OnMenuClicked;

        private void Awake()
        {
            m_RestartButton?.onClick.AddListener(
                () => OnRestartClicked?.Invoke());
            m_MenuButton?.onClick.AddListener(
                () => OnMenuClicked?.Invoke());

            Hide();
        }

        public void Show(int score, int collected, int total)
        {
            gameObject.SetActive(true);

            if (m_TitleText)
                m_TitleText.text = "Игра окончена";
            if (m_ScoreText)
                m_ScoreText.text = $"Счёт: {score}";
            if (m_PelletsText)
                m_PelletsText.text = $"Собрано: {collected}/{total}";
        }

        public void Hide() => gameObject.SetActive(false);

        private void OnDestroy()
        {
            m_RestartButton?.onClick.RemoveAllListeners();
            m_MenuButton?.onClick.RemoveAllListeners();
        }
    }
}