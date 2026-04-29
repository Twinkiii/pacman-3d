using UnityEngine;

namespace Pacman
{
    public class GameBootstrap : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private VirtualJoystick m_Joystick;

        [Header("Settings")]
        [SerializeField] private int m_StartLives = 3;
        [SerializeField] private float m_PlayerSpeed = 5f;

        private GameModel m_GameModel;
        private PlayerModel m_PlayerModel;
        private GameViewModel m_GameViewModel;
        private PlayerViewModel m_PlayerViewModel;

        private void Start()
        {

            var playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj == null)
            {
                Debug.LogError("GameBootstrap: Player not found!");
                return;
            }

            var playerView = playerObj.GetComponent<PlayerView>();
            if (playerView == null)
            {
                Debug.LogError("GameBootstrap: PlayerView not found on Player object!");
                return;
            }

            int totalPellets = FindObjectsByType<CollectibleView>().Length;


            m_GameModel = new GameModel(m_StartLives, totalPellets);
            m_PlayerModel = new PlayerModel(m_PlayerSpeed);


            m_GameViewModel = new GameViewModel(m_GameModel);
            m_PlayerViewModel = new PlayerViewModel(m_PlayerModel);


            m_PlayerViewModel.OnPlayerDied += m_GameViewModel.NotifyPlayerDied;


            playerView.Construct(m_PlayerViewModel, m_Joystick);



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