using UnityEngine;

namespace Pacman
{
    public class MinimapCameraFollow : MonoBehaviour
    {
        private Transform m_Target;
        private float m_Height;

        private void Start()
        {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                m_Target = playerObj.transform;

            
            m_Height = transform.position.y;
        }

        private void LateUpdate()
        {
            if (m_Target == null) return;

            transform.position = new Vector3(
                m_Target.position.x,
                m_Height,
                m_Target.position.z);
        }
    }
}