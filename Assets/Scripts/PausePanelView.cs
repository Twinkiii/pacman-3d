using UnityEngine;
using UnityEngine.UI;
using System;

namespace Pacman
{
    public class PausePanelView : MonoBehaviour
    {
        [SerializeField] private Button m_ResumeButton;
        [SerializeField] private Button m_MenuButton;

        public event Action OnResumeClicked;
        public event Action OnMenuClicked;

        private void Awake()
        {
            m_ResumeButton?.onClick.AddListener(
                () => OnResumeClicked?.Invoke());
            m_MenuButton?.onClick.AddListener(
                () => OnMenuClicked?.Invoke());
            Hide();

        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        private void OnDestroy()
        {
            m_ResumeButton?.onClick.RemoveAllListeners();
            m_MenuButton?.onClick.RemoveAllListeners();
        }
    }
}