using UnityEngine;

namespace Pacman
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform m_Target;
        [SerializeField] private Vector3 m_Offset = new Vector3(0, 12, 0);
        [SerializeField] private float m_SmoothSpeed = 5f;

        private void LateUpdate()
        {
            if (m_Target == null) return;

            Vector3 desiredPos = m_Target.position + m_Offset;
            transform.position = Vector3.Lerp(
                transform.position,
                desiredPos,
                m_SmoothSpeed * Time.deltaTime);
        }
    }
}
