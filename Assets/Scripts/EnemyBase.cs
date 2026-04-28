using UnityEngine;
using UnityEngine.AI;

namespace Pacman
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class EnemyBase : MonoBehaviour, IEnemy
    {
        [SerializeField] protected float m_Speed = 3f;
        [SerializeField] protected float m_ChaseRange = 8f;

        protected NavMeshAgent m_Agent;
        protected Transform m_PlayerTransform;
        protected EnemyModel m_Model;
        protected Vector3 m_SpawnPosition;

        protected virtual void Awake()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            m_SpawnPosition = transform.position;
            m_Model = new EnemyModel(GetBehaviourType(), m_SpawnPosition, m_Speed);
            m_Agent.speed = m_Speed;
        }

        protected virtual void Start()
        {
            // Ищем игрока по тегу — View знает о сцене
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                m_PlayerTransform = playerObj.transform;
        }

        protected virtual void Update()
        {
            if (m_PlayerTransform == null) return;
            UpdateBehaviour();
            m_Model.UpdatePosition(transform.position);
        }

        protected abstract void UpdateBehaviour();
        protected abstract EnemyBehaviourType GetBehaviourType();

        protected bool IsPlayerInRange()
        {
            return Vector3.Distance(transform.position, m_PlayerTransform.position) < m_ChaseRange;
        }

        // --- IEnemy ---
        public virtual void StartChase()
        {
            m_Model.SetChasing(true);
        }

        public virtual void StopChase()
        {
            m_Model.SetChasing(false);
        }

        public void ResetToSpawn()
        {
            m_Agent.Warp(m_SpawnPosition);
            m_Model.ResetToSpawn();
        }
    }
}

