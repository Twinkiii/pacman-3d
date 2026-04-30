using UnityEngine;
using UnityEngine.UI;

namespace Pacman
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Text m_ScoreText;

        public void UpdateScore(int score)
        {
            if (m_ScoreText)
                m_ScoreText.text = $"Score: {score}";
        }
    }
}