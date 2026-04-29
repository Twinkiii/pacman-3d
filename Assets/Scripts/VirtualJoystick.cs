using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Pacman
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPlayerInput
    {
        [SerializeField] private Image m_JoyBack;
        [SerializeField] private Image m_Joystick;

        public Vector2 MoveDirection => new Vector2(m_Value.x, m_Value.y);

        private Vector3 m_Value;
        private Vector2 m_JoyBackCenter;

        private void Start()
        {
            
            m_JoyBackCenter = m_JoyBack.rectTransform.position;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                m_JoyBack.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out position);

            float halfW = m_JoyBack.rectTransform.sizeDelta.x * 0.5f;
            float halfH = m_JoyBack.rectTransform.sizeDelta.y * 0.5f;

            // Нормализуем от -1 до 1
            m_Value.x = position.x / halfW;
            m_Value.y = position.y / halfH;

            if (m_Value.magnitude > 1f)
                m_Value = m_Value.normalized;

            
            float offsetX = halfW - m_Joystick.rectTransform.sizeDelta.x * 0.5f;
            float offsetY = halfH - m_Joystick.rectTransform.sizeDelta.y * 0.5f;

            m_Joystick.rectTransform.anchoredPosition = new Vector2(
                m_Value.x * offsetX,
                m_Value.y * offsetY);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_Value = Vector3.zero;
            m_Joystick.rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
