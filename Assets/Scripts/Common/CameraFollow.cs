using UnityEngine;

namespace Pacman.Common
{
    public class CameraFollow : MonoBehaviour
    {
        
        [SerializeField] private Vector3 m_Offset = new Vector3(0, 12, 0);
        [SerializeField] private float m_SmoothSpeed = 5f;

        private Transform m_Target;

        private void Start()
        {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                m_Target = playerObj.transform;
            else
                Debug.LogError("CameraFollow: Player not found!");
        }

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
