using System;
using System.Collections;
using UnityEngine;

namespace Pacman
{
    public class AbilityViewModel
    {
        private readonly AbilityModel m_InvincibilityAbility;
        private readonly AbilityModel m_ExtraLifeAbility;
        private readonly PlayerProgressModel m_Progress;
        private readonly GameModel m_GameModel;
        private readonly MonoBehaviour m_Coroutiner;


        public event Action<int> OnPelletsChanged;
        public event Action<bool> OnInvincibilityCooldownChanged;
        public event Action<bool> OnExtraLifeCooldownChanged;
        public event Action<float> OnInvincibilityActivated; // duration
        public event Action OnExtraLifeActivated;
        public event Action<bool> OnExtraLifeAvailable;

        public int TotalPellets => m_Progress.TotalPellets;

        private const int MaxLives = 3;

        public AbilityViewModel(
            PlayerProgressModel progress,
            GameModel gameModel,
            MonoBehaviour coroutiner)
        {
            m_Progress = progress;
            m_GameModel = gameModel;
            m_Coroutiner = coroutiner;

            m_InvincibilityAbility = new AbilityModel(
                AbilityType.Invincibility, cost: 30, cooldown: 20f);

            m_ExtraLifeAbility = new AbilityModel(
                AbilityType.ExtraLife, cost: 50, cooldown: 0f);

            m_GameModel.OnPelletsChanged += _ => NotifyDisplayTotal();
            m_Progress.OnPelletsChanged += _ => NotifyDisplayTotal();

            m_GameModel.OnLivesChanged += lives =>
                OnExtraLifeAvailable?.Invoke(lives < MaxLives);
        }

        private void NotifyDisplayTotal()
        {
            int display = m_Progress.TotalPellets + m_GameModel.CollectedPellets;
            OnPelletsChanged?.Invoke(display);
        }

        public int DisplayTotalPellets =>
                   m_Progress.TotalPellets + m_GameModel.CollectedPellets;


        public void UseInvincibility()
        {
            if (m_InvincibilityAbility.IsOnCooldown) return;
            if (!TrySpendDisplay(m_InvincibilityAbility.Cost)) return;

            PlayerProgressService.Save(m_Progress);
            OnInvincibilityActivated?.Invoke(20f);
            NotifyDisplayTotal();

            m_Coroutiner.StartCoroutine(
                CooldownRoutine(m_InvincibilityAbility,
                    OnInvincibilityCooldownChanged));
        }

        private bool TrySpendDisplay(int cost)
        {
            
            int display = m_Progress.TotalPellets + m_GameModel.CollectedPellets;

            if (display < cost)
            {
                Debug.Log($"Not enough pellets! Have: {display} Need: {cost}");
                return false;
            }

            
            int fromSession = m_GameModel.CollectedPellets;
            if (fromSession >= cost)
            {
                
                m_GameModel.SpendCollectedPellets(cost);
            }
            else
            {

                int remainder = cost - fromSession;
                m_GameModel.SpendCollectedPellets(fromSession);
                m_Progress.TrySpendPellets(remainder);
            }

            return true;
        }


        public void UseExtraLife()
        {
            if (m_ExtraLifeAbility.IsOnCooldown) return;
            if (m_GameModel.Lives >= MaxLives)
            {
                Debug.Log("Max lives reached!");
                return;
            }
            if (!TrySpendDisplay(m_ExtraLifeAbility.Cost)) return;

            PlayerProgressService.Save(m_Progress);
            OnExtraLifeActivated?.Invoke();
            NotifyDisplayTotal();

            m_Coroutiner.StartCoroutine(
                CooldownRoutine(m_ExtraLifeAbility,
                    OnExtraLifeCooldownChanged));
        }

        public bool CanBuyExtraLife => m_GameModel.Lives < MaxLives;

        private IEnumerator CooldownRoutine(
            AbilityModel ability,
            Action<bool> onCooldownChanged)
        {
            ability.SetCooldown(true);
            onCooldownChanged?.Invoke(true);

            yield return new WaitForSeconds(ability.Cooldown);

            ability.SetCooldown(false);
            onCooldownChanged?.Invoke(false);
        }
    }
}