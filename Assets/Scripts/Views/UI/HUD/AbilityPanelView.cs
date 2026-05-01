using UnityEngine;
using UnityEngine.UI;
using Pacman.ViewModels;



namespace Pacman.Views.UI.HUD
{
    public class AbilityPanelView : MonoBehaviour
    {
        [Header("Pellets")]
        [SerializeField] private Text m_PelletsText;

        [Header("Invincibility Button")]
        [SerializeField] private Button m_InvincibilityButton;
        [SerializeField] private Text m_InvincibilityCostText;


        [Header("Extra Life Button")]
        [SerializeField] private Button m_ExtraLifeButton;
        [SerializeField] private Text m_ExtraLifeCostText;

        private AbilityViewModel m_ViewModel;

        public void Construct(AbilityViewModel viewModel)
        {
            m_ViewModel = viewModel;

   
            m_ViewModel.OnPelletsChanged += UpdatePellets;
            m_ViewModel.OnInvincibilityCooldownChanged += UpdateInvincibilityCooldown;
            m_ViewModel.OnExtraLifeCooldownChanged += UpdateExtraLifeCooldown;
            m_ViewModel.OnExtraLifeAvailable += UpdateExtraLifeAvailable;

            m_InvincibilityButton?.onClick.AddListener(
                () => m_ViewModel.UseInvincibility());
            m_ExtraLifeButton?.onClick.AddListener(
                () => m_ViewModel.UseExtraLife());


            UpdatePellets(m_ViewModel.DisplayTotalPellets);
            UpdateExtraLifeAvailable(m_ViewModel.CanBuyExtraLife);

            m_InvincibilityCostText.text = "30 ";
            m_ExtraLifeCostText.text = "50 ";
        }

        private void UpdateExtraLifeAvailable(bool isAvailable)
        {
            if (m_ExtraLifeButton)
                m_ExtraLifeButton.interactable = isAvailable;


            if (m_ExtraLifeCostText)
                m_ExtraLifeCostText.text = isAvailable
                    ? "50"
                    : "Макс ❤️";
        }

        private void UpdatePellets(int pellets)
        {
            if (m_PelletsText)
                m_PelletsText.text = $" {pellets}";
        }

        private void UpdateInvincibilityCooldown(bool isOnCooldown)
        {
            if (m_InvincibilityButton)
                m_InvincibilityButton.interactable = !isOnCooldown;


        }

        private void UpdateExtraLifeCooldown(bool isOnCooldown)
        {
            if (m_ExtraLifeButton)
                m_ExtraLifeButton.interactable = !isOnCooldown;
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        private void OnDestroy()
        {
            if (m_ViewModel == null) return;
            m_ViewModel.OnPelletsChanged -= UpdatePellets;
            m_ViewModel.OnInvincibilityCooldownChanged -= UpdateInvincibilityCooldown;
            m_ViewModel.OnExtraLifeCooldownChanged -= UpdateExtraLifeCooldown;
            m_ViewModel.OnExtraLifeAvailable -= UpdateExtraLifeAvailable;

            m_InvincibilityButton?.onClick.RemoveAllListeners();
            m_ExtraLifeButton?.onClick.RemoveAllListeners();
        }
    }
} 