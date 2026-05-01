using System;
using UnityEngine;
using Pacman.Core.Interfaces;
using Pacman.ViewModels;
using Pacman.Audio;

namespace Pacman.Views.Collectibles
{
    public class CollectibleView : MonoBehaviour, ICollectible
    {
        [SerializeField] private int m_ScoreValue = 10;
        private GameViewModel m_GameViewModel;
        private bool m_IsCollected;

        public int ScoreValue => m_ScoreValue;
        public bool IsCollected => m_IsCollected;

        public event Action OnCollected;

        public void Construct(GameViewModel gameViewModel)
        {
            m_GameViewModel = gameViewModel;
        }

        public void Collect()
        {
            if (m_IsCollected) return;
            m_IsCollected = true;

            Sound.PelletCollect.Play();

            m_GameViewModel?.NotifyCollected(m_ScoreValue);

            OnCollected?.Invoke();

            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                Collect();
            }
        }
    }
}
