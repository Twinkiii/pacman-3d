using System;
using UnityEngine;

namespace Pacman
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerView : MonoBehaviour
    {
        

        private PlayerViewModel m_ViewModel;
        private IPlayerInput m_Input;
        private CharacterController m_Controller;
        private Vector3 m_SpawnPostion;

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

            var dir = m_Input.MoveDirection;
            m_ViewModel.SetMoveDirection(dir);

            var move = new Vector3(dir.x, 0f, dir.y) * m_ViewModel.Speed * Time.deltaTime;

            move.y -= 9.8f * Time.deltaTime;

            m_Controller.Move(move);

            m_ViewModel.ReportPosition(transform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IEnemy>(out _))
                m_ViewModel.NotifyHitEnemy();
        }



        private void onDied()
        {
            transform.position = m_SpawnPostion;
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
