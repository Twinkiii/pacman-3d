using UnityEngine;

namespace Pacman
{

    public class GameBootstrap : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private PlayerView m_PlayerView;
        [SerializeField] private VirtualJoystick m_Joystick;
        [SerializeField] private Transform m_PlayerSpawnPoint;


        [Header("Settings")]
        [SerializeField] private int m_StartLives = 3;
        [SerializeField] private float m_PlayerSpeed = 5f;

        private GameModel m_GameModel;
        private PlayerModel m_PlayerModel;
        private GameViewModel m_GameViewModel;
        private PlayerViewModel m_PlayerViewModel;

        private void Awake()
        {
            
            int totalPellets = FindObjectsByType<CollectibleView>().Length;

            
            m_GameModel = new GameModel(m_StartLives, totalPellets);
            m_PlayerModel = new PlayerModel(m_PlayerSpeed);

            
            m_GameViewModel = new GameViewModel(m_GameModel);
            m_PlayerViewModel = new PlayerViewModel(m_PlayerModel);

            
            m_PlayerViewModel.OnPlayerDied += m_GameViewModel.NotifyPlayerDied;

            
            m_PlayerView.Construct(m_PlayerViewModel, m_Joystick);
            

            
            foreach (var collectible in FindObjectsByType<CollectibleView>())
                collectible.Construct(m_GameViewModel);
        }

        private void OnDestroy()
        {
            if (m_PlayerViewModel != null)
                m_PlayerViewModel.OnPlayerDied -= m_GameViewModel.NotifyPlayerDied;
        }
    }
}
