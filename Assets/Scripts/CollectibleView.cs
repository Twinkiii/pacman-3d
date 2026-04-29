using UnityEngine;

namespace Pacman
{
    public class CollectibleView : MonoBehaviour, ICollectible
    {
        [SerializeField] private int m_ScoreValue = 10;
        private GameViewModel m_GameViewModel;
        private bool m_IsCollected;

        public int ScoreValue => m_ScoreValue;
        public bool IsCollected => m_IsCollected;

        public void Construct(GameViewModel gameViewModel)
        {
            m_GameViewModel = gameViewModel;
        }

        public void Collect()
        {
            if (m_IsCollected) return;
            m_IsCollected = true;

            m_GameViewModel?.NotifyCollected(m_ScoreValue);

            gameObject.SetActive(false);
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
