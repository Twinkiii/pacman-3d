using UnityEngine;

namespace Pacman
{
    public class AmbushEnemy : EnemyBase
    {
        [SerializeField] private float m_AmbushRange = 4f;
        [SerializeField] private float m_ReturnDelay = 5f;

        private bool m_IsAmbushing = true;
        private Timer m_ReturnTimer;


        protected override void Awake()
        {
            base.Awake();
            m_ReturnTimer = new Timer(m_ReturnDelay);
        }

        protected override EnemyBehaviourType GetBehaviourType()
        {
            return EnemyBehaviourType.Ambush;
        }

        protected override void UpdateBehaviour()
        {
            m_ReturnTimer.RemoveTime(Time.deltaTime);
            float distToPlayer = Vector3.Distance(transform.position, m_PlayerTransform.position);

            if (m_IsAmbushing)
            {
                //Wait for player to get close enough, then start chasing
                m_Agent.ResetPath();
                if(distToPlayer < m_AmbushRange)
                {
                    m_IsAmbushing = false;
                    m_Model.SetChasing(true);
                    m_ReturnTimer.Restart();
                }
            }
            else
            {
                m_Agent.SetDestination(m_PlayerTransform.position);

                if(distToPlayer > m_ChaseRange && m_ReturnTimer.IsFinished)
                {
                    m_IsAmbushing = true;
                    m_Model.SetChasing(false);
                    m_Agent.SetDestination(m_SpawnPosition);
                }
            }
        }

        public override void ResetToSpawn()
        {
            base.ResetToSpawn();
            m_IsAmbushing = true;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, m_AmbushRange);
        }
#endif
    }
}
