using UnityEngine;
using Pacman.Core.Interfaces;
using Pacman.ViewModels;
using Pacman.Audio;
using Pacman.Views.Collectibles;

namespace Pacman.Views.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerView : MonoBehaviour
    {
        

        private PlayerViewModel m_ViewModel;
        private IPlayerInput m_Input;
        private CharacterController m_Controller;
        private Vector3 m_SpawnPostion;


        private bool m_IsInvincible;
        private float m_InvincibleDuration = 3.5f;
        private float m_InvincibleTimer;


        public void Construct(PlayerViewModel viewModel, IPlayerInput input)
        {
            m_ViewModel = viewModel;
            m_Input = input;

            m_ViewModel.OnPlayerDied += onDied;
        }
        private void Awake()
        {
            m_Controller = GetComponent<CharacterController>();
            m_SpawnPostion = transform.position;
        }

        private void Update()
        {
            if (m_ViewModel == null) return;

            if (m_IsInvincible)
            {
                m_InvincibleTimer -= Time.deltaTime;
                if (m_InvincibleTimer <= 0f)
                    m_IsInvincible = false;
            }


            var dir = m_Input.MoveDirection;
            m_ViewModel.SetMoveDirection(dir);

            var move = new Vector3(dir.x, 0f, dir.y) * m_ViewModel.Speed * Time.deltaTime;

            move.y -= 9.8f * Time.deltaTime;

            m_Controller.Move(move);

            m_ViewModel.ReportPosition(transform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == gameObject) return;

            if (other.TryGetComponent<CollectibleView>(out _)) return;

            Debug.Log($"Trigger with: {other.gameObject.name} layer: {other.gameObject.layer}");

            Debug.Log($"Trigger with: {other.gameObject.name}");
            if (other.TryGetComponent<IEnemy>(out _))
            {
                if (m_IsInvincible) return;
                m_IsInvincible = true;
                m_InvincibleTimer = m_InvincibleDuration;
                Debug.Log("Hit enemy!");
                m_ViewModel.NotifyHitEnemy();
            }
        }

        public void ActivateStartingInvincibility(float duration)
        {
            m_IsInvincible = true;
            m_InvincibleTimer = duration;
            Debug.Log($"Invincibility activated for {duration}s!");
        }

        private void onDied()
        {
            Sound.PlayerDie.Play();
            Debug.Log("PlayerView: OnDied called — respawning");
            m_Controller.enabled = false;
            transform.position = m_SpawnPostion;
            m_Controller.enabled = true;
            m_ViewModel.Respawn(m_SpawnPostion);
        }

        private void OnDestroy()
        {
            if (m_ViewModel != null)
            {
                m_ViewModel.OnPlayerDied -= onDied;
            }
        }
    }
}
