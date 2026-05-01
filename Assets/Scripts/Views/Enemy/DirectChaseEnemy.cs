using Pacman.Common;

namespace Pacman.Views.Enemy
{
    public class DirectChaseEnemy : EnemyBase
    {
        override protected EnemyBehaviourType GetBehaviourType()
        {
            return EnemyBehaviourType.DirectChase;
        }

        override protected void UpdateBehaviour()
        {
            m_Agent.SetDestination(m_PlayerTransform.position);
            m_Model.SetChasing(true);
        }
    }
}
