using UnityEngine;



namespace Pacman
{
    public class LivesView : MonoBehaviour
    {
        [SerializeField] private GameObject m_Heart1;
        [SerializeField] private GameObject m_Heart2;
        [SerializeField] private GameObject m_Heart3;

        public void UpdateLives(int lives)
        {
            m_Heart1?.SetActive(lives >= 1);
            m_Heart2?.SetActive(lives >= 2);
            m_Heart3?.SetActive(lives >= 3);
        }
    }
}

