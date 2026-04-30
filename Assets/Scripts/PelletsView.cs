using UnityEngine;
using UnityEngine.UI;

namespace Pacman
{
    public class PelletsView : MonoBehaviour
    {
        [SerializeField] private Text m_PelletsText;

        private int m_Total;

        public void Initialise(int total)
        {
            m_Total = total;
            UpdatePellets(0);
        }

        public void UpdatePellets(int collected)
        {
            if (m_PelletsText)
                m_PelletsText.text = $"{collected}/{m_Total}";
        }
    }
}
