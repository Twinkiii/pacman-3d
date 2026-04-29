using UnityEngine;

namespace Pacman
{
    public class AmbushEnemy : EnemyBase
    {
        [SerializeField] private float m_AmbushRange = 4f;
        [SerializeField] private float m_ReturnDelay = 5f;

        private enum State { Ambushing, Chasing, Returning }
        private State m_State = State.Ambushing;
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

            switch (m_State)
            {
                case State.Ambushing:
                   
                    m_Agent.ResetPath();
                    m_Model.SetChasing(false);

                    if (distToPlayer < m_AmbushRange)
                    {
                        
                        m_State = State.Chasing;
                        m_Model.SetChasing(true);
                        m_ReturnTimer.Restart();
                    }
                    break;

                case State.Chasing:
                   
                    m_Agent.SetDestination(m_PlayerTransform.position);

                    if (distToPlayer > m_ChaseRange && m_ReturnTimer.IsFinished)
                    {
                        
                        m_State = State.Returning;
                        m_Model.SetChasing(false);
                    }
                    break;

                case State.Returning:
                    
                    m_Agent.SetDestination(m_SpawnPosition);

                    float distToSpawn = Vector3.Distance(
                        transform.position,
                        m_SpawnPosition);

                    if (distToSpawn < 0.5f)
                    {
                        
                        m_State = State.Ambushing;
                    }

                    
                    if (distToPlayer < m_AmbushRange)
                    {
                        m_State = State.Chasing;
                        m_Model.SetChasing(true);
                        m_ReturnTimer.Restart();
                    }
                    break;
            }
        }

        public override void ResetToSpawn()
        {
            base.ResetToSpawn();
            m_State = State.Ambushing;
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
