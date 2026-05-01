using UnityEngine;
using UnityEngine.UI;
using System;

namespace Pacman.Views.UI.HUD
{
    public class PauseButtonView : MonoBehaviour
    {
        [SerializeField] private Button m_PauseButton;


        public event Action OnPauseClicked;

        private void Awake()
        {
            m_PauseButton?.onClick.AddListener(
                () => OnPauseClicked?.Invoke());
        }

        public void Show()
        {
            gameObject.SetActive(true);

        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            m_PauseButton?.onClick.RemoveAllListeners();
        }
    }
}