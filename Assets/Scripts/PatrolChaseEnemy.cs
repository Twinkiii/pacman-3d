using Pacman;
using UnityEngine;

namespace Pacman
{

    public class PatrolChaseEnemy : EnemyBase
    {
        [SerializeField] private Transform[] m_PatrolPoints;
        [SerializeField] private float m_DistanceThreshold = 0.5f;

        private int m_CurrentPatrolIndex;
        private bool m_IsChasing;


        private Timer m_ChaseTimer;
        [SerializeField] private float m_ChaseCooldown = 3f;

        protected override void Awake()
        {
            base.Awake();
            m_ChaseTimer = new Timer(m_ChaseCooldown);
        }

        protected override EnemyBehaviourType GetBehaviourType()
        {
            return EnemyBehaviourType.PatrolAndChase;
        }  

        protected override void UpdateBehaviour()
        {
            m_ChaseTimer.RemoveTime(Time.deltaTime);

            if (IsPlayerInRange())
            {
                m_IsChasing = true;
                m_ChaseTimer.Restart();
            }
            else if (m_ChaseTimer.IsFinished)
            {
                m_IsChasing = false;
            }

            m_Model.SetChasing(m_IsChasing);

            if (m_IsChasing)
            {
                m_Agent.SetDestination(m_PlayerTransform.position);
            }
            else
            {
                Patrol();
            }
        }

        private void Patrol()
        {
            if (m_PatrolPoints == null || m_PatrolPoints.Length == 0) return;

            var target = m_PatrolPoints[m_CurrentPatrolIndex].position;
            m_Agent.SetDestination(target);

            if (Vector3.Distance(transform.position, target) < m_DistanceThreshold)
                m_CurrentPatrolIndex = (m_CurrentPatrolIndex + 1) % m_PatrolPoints.Length;
        }

        public void SetPatrolPoints(Transform[] points)
        {
            m_PatrolPoints = points;
        }
    }
}